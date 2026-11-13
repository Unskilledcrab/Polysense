#!/usr/bin/env python3
"""Balance lints and the balance surface: recurring fairness findings, turned into code.

Each rule below is something an AI reviewer found, at a cost of a full review, in the reader-first cycle. The lints
print CHECK lines (prompts for a person or one reviewer, not failures) because wording rules have exceptions.

    lint_balance.py lint    TRIAL_DIR [--strict]    print CHECK lines (exit 1 with --strict if any)
    lint_balance.py surface TRIAL_DIR               print the lines that carry the page's balance, for ONE fairness
                                                    reviewer to read (about 1,500-2,500 words, not the whole page)

Rules:
  verbs      attribution verbs that characterize ("warns", "argues", "admits") instead of "says"
  values     the values guide: under two lines; over 35 words; a recommendation word in our voice; a label word that
             another side's line also uses
  unknowns   a "Not known yet" item without an "as of <date>"
  caveats    "In short" lines whose closing caveats differ only by a trailing "yet"
  grades     a grade word in a card label that the page's printed rule ("How to read these cards") never defines
  terms      a term from page.yaml `terms:` used on a tab before the phrase that defines it

Needs PyYAML only for the terms rule and the surface's title (uv run --with pyyaml ...); the rest is stdlib.
"""
from __future__ import annotations

import re
import sys
from pathlib import Path

sys.path.insert(0, str(Path(__file__).parent))
from check_citations import MARKER, QUOTE  # noqa: E402

VERBS = re.compile(r"\b(warns?|warned|argues?|argued|admits?|admitted|concedes?|conceded|insists?|insisted|"
                   r"complains?|complained|claimed|claims that)\b(?!\s+(?:for|against)\b)", re.I)
RECOMMEND = re.compile(r"\b(fits|best|choose|better|worse|should)\b", re.I)
GRADES = [("supported, with limits", "with limits"), ("supported", "supported"), ("mixed", "mixed"),
          ("uncertain", "uncertain"), ("contested", "contested"), ("values question", "values"),
          ("not mainly a factual question", "not mainly factual")]
STOP = set("about their there which would could should other these those being where while after before under "
           "between through every people public".split())


def plain(line: str) -> str:
    return re.sub(r"\s+", " ", MARKER.sub("", re.sub(r"<!--.*?-->", "", line))).strip()


def unquoted(text: str) -> str:
    return QUOTE.sub('""', text)


def sections(path: Path):
    """Yield (line_no, layer, sub, raw_line) for every non-empty line."""
    layer = sub = ""
    for no, raw in enumerate(path.read_text(encoding="utf-8").splitlines(), 1):
        if raw.startswith("## "):
            layer, sub = raw[3:].strip(), ""
        elif raw.startswith("### "):
            sub = raw[4:].strip()
        elif raw.strip():
            yield no, layer, sub, raw


def lint(trial: Path) -> list[str]:
    ws = trial / "workspace.md"
    out, values, unknown_lines, inshort, labels, rule_text = [], [], [], [], [], ""
    for no, layer, sub, raw in sections(ws):
        text = plain(raw)
        if raw.rstrip().endswith("<!-- nocheck -->"):
            if "Evidence:" in raw:
                labels.append((no, raw.split("Evidence:", 1)[1].replace("<!-- nocheck -->", "")))
            continue
        if layer.lower().startswith("layer 5"):
            continue
        for m in VERBS.finditer(unquoted(text)):
            out.append(f"CHECK L{no} verbs: \"{m.group(0)}\" characterizes a speaker; \"says\" is neutral: …{text[max(0, m.start() - 40):m.end() + 30]}…")
        if sub.lower().startswith("if you care most about"):
            values.append((no, text))
        if sub.lower().startswith("not known") and raw.lstrip().startswith("-"):
            unknown_lines.append((no, text))
        if re.match(r"\s*-\s+\*\*In short:\*\*", raw):
            inshort.append((no, text))
        if sub.lower().startswith("how to read"):
            rule_text += " " + text.lower()

    lines = [(no, t) for no, t in values if re.match(r"-?\s*\*\*If you care most about", t)]
    if values and len(lines) < 2:
        out.append(f"CHECK L{values[0][0]} values: the guide has {len(lines)} line(s); a guide that quotes one side reads as a recommendation")
    for no, t in values:
        body = unquoted(t)
        for m in RECOMMEND.finditer(body):
            if not re.search(r"(doesn't|does not) recommend", body):
                out.append(f"CHECK L{no} values: \"{m.group(0)}\" in our voice in the values guide")
            elif m.group(0).lower() != "recommend":
                out.append(f"CHECK L{no} values: \"{m.group(0)}\" in our voice in the values guide")
    for no, t in lines:
        words = len(re.sub(r"[*]", "", t).lstrip("- ").split())
        if words > 35:
            out.append(f"CHECK L{no} values: line is {words} words (budget 35)")
        label = re.match(r"-?\s*\*\*If you care most about (.+?):\*\*", t)
        keys = {w for w in re.findall(r"[a-z]{6,}", label.group(1).lower())} - STOP if label else set()
        for no2, t2 in lines:
            if no2 == no:
                continue
            body2 = re.sub(r"^-?\s*\*\*.+?:\*\*", "", t2).lower()
            shared = sorted(k for k in keys if k[:6] in body2)
            if shared:
                out.append(f"CHECK L{no} values: label word(s) {shared} also appear in the line at L{no2}; is this value claimed by both sides?")

    for no, t in unknown_lines:
        if not re.search(r"as of [A-Z][a-z]{2} \d{1,2}, \d{4}", t):
            out.append(f"CHECK L{no} unknowns: no \"as of <date>\": {t[:80]}")

    closers = [(no, re.split(r"(?<=[.!?])\s+", t.strip())[-1]) for no, t in inshort]
    with_yet = {re.sub(r"\s+yet\.$", ".", c): no for no, c in closers if c.endswith("yet.")}
    for no, c in closers:
        if not c.endswith("yet.") and c[:20] in {k[:20] for k in with_yet}:
            out.append(f"CHECK L{no} caveats: closing caveat matches another card's but drops \"yet\": {c[:80]}")

    for no, label in labels:
        low = re.sub(r"\([^()]*\)", "", label.lower())
        for grade, token in GRADES:
            if grade in low and token not in rule_text and not (grade == "supported" and "supported, with limits" in low and "supported" in rule_text):
                out.append(f"CHECK L{no} grades: label uses \"{grade}\" but the page's printed rule doesn't define it")
                break

    cfg = trial / "page.yaml"
    try:
        import yaml
        terms = (yaml.safe_load(cfg.read_text(encoding="utf-8")) or {}).get("terms") or {}
    except ImportError:
        terms = {}
    first: dict[tuple[str, str], tuple[int, str]] = {}
    for no, layer, sub, raw in sections(ws):
        if raw.rstrip().endswith("<!-- nocheck -->") or layer.lower().startswith("layer 5"):
            continue
        for term in terms:
            if re.search(rf"(?<![A-Za-z]){re.escape(term)}(?![A-Za-z])", raw) and (layer, term) not in first:
                first[(layer, term)] = (no, raw)
    for (layer, term), (no, raw) in first.items():
        if terms[term].lower() not in raw.lower():
            out.append(f"CHECK L{no} terms: \"{term}\" appears on \"{layer}\" before \"{terms[term]}\"")
    return out


def surface(trial: Path) -> str:
    out = []
    try:
        import yaml
        cfg = yaml.safe_load((trial / "page.yaml").read_text(encoding="utf-8")) or {}
        out.append(f"TITLE: {cfg.get('title', '')}")
        for k, v in (cfg.get("option_chips") or {}).items():
            out.append(f"OPTION CHIP {k}: {v.get('text') if isinstance(v, dict) else v}")
    except ImportError:
        pass
    keep = re.compile(r"\*\*(In short|Who benefits|Who pays|What it does|What's being decided|Who it affects|What's next):\*\*")
    for no, layer, sub, raw in sections(trial / "workspace.md"):
        low, s = layer.lower(), sub.lower()
        take = (low.startswith("30-second") or (low.startswith("what it means") and not sub)
                or s.startswith("if you care most about") or keep.search(raw)
                or (raw.rstrip().endswith("<!-- nocheck -->") and "Side:" in raw))
        if take:
            head = f"[{sub or layer}] " if (keep.search(raw) or "Side:" in raw) and sub else ""
            out.append(f"L{no} {head}{plain(raw)}")
    return "\n".join(out)


if __name__ == "__main__":
    if len(sys.argv) < 3 or sys.argv[1] not in ("lint", "surface"):
        sys.exit(__doc__)
    t = Path(sys.argv[2])
    if sys.argv[1] == "surface":
        print(surface(t))
    else:
        found = lint(t)
        print("\n".join(found) if found else "no balance prompts")
        print(f"{t.name}: {len(found)} CHECK line(s)")
        sys.exit(1 if found and "--strict" in sys.argv else 0)
