#!/usr/bin/env python3
"""Render the UX prototype page from one or more trials' checked files.

Usage:
    uv run --with pyyaml python3 trials/tools/render_prototype.py TEMPLATE.html OUT.html TRIAL_DIR [TRIAL_DIR ...]

For each TRIAL_DIR, reads page.yaml (display settings: slug, title, header chips, short source labels,
optional bill-markup view), workspace.md, home.md, passages.md, sources.yaml and, if present,
verification/summary.json. Embeds them as JSON in the template and writes one self-contained HTML page.
Nothing is typed by hand into the page: every sentence, chip and source comes from the trial files.
"""
from __future__ import annotations

import datetime as dt
import hashlib
import json
import re
import sys
from pathlib import Path

try:
    import yaml
except ImportError:  # pragma: no cover
    sys.exit("needs PyYAML: uv run --with pyyaml python3 trials/tools/render_prototype.py ...")

MARKER = re.compile(r"\[(P-\d{3}(?:\s*,\s*P-\d{3})*)\]|\{(analysis|editorial)(?::\s*([^}]*))?\}|\{computed:\s*([^}]*)\}")
SECTION_IDS = {"30-second": "summary", "layer 1": "overview", "layer 2": "decision", "layer 3": "options",
               "layer 4": "arguments", "layer 5": "evidence"}
TYPE_LABEL = {"primary-government": "Primary · government", "secondary-research": "Research", "news": "News",
              "local-government": "Local government", "advocacy": "Advocacy", "party-filing": "Party filing"}


def split_units(text: str) -> list[dict]:
    out, pos = [], 0
    for m in MARKER.finditer(text):
        chunk = text[pos:m.start()].strip()
        pos = m.end()
        if m.group(1):
            out.append({"t": chunk, "cite": [p.strip() for p in m.group(1).split(",")]})
        elif m.group(2):
            out.append({"t": chunk, "tag": m.group(2), "note": (m.group(3) or "").strip()})
        else:
            out.append({"t": chunk, "tag": "computed", "note": (m.group(4) or "").strip()})
    tail = text[pos:].strip()
    if tail:
        out.append({"t": tail})
    return [u for u in out if u["t"] or u.get("cite") or u.get("tag")]


def parse_block(line: str) -> dict:
    kind = "para"
    m = re.match(r"^\s*(?:(-)|(\d+)\.)\s+(.*)$", line)
    if m:
        kind = "bullet" if m.group(1) else "num"
        line = m.group(3)
    label = None
    lm = re.match(r"^\*\*(.+?):\*\*\s*(.*)$", line)
    if lm:
        label, line = lm.group(1), lm.group(2)
    return {"kind": kind, "label": label, "units": split_units(line)}


def parse_card_meta(line: str) -> dict:
    # "Documented · Side: … · Claim type: … · Evidence: …". Evidence is the last field and is kept
    # whole, even if it contains "·" (Trial 003 found labels silently cut at the first "·").
    text = line.replace("<!-- nocheck -->", "").strip()
    head, *evidence = re.split(r"(?i)\bEvidence:", text, maxsplit=1)
    parts = [p.strip() for p in head.split("·") if p.strip()]
    meta = {"kind": "Anticipated" if parts and parts[0].startswith("Anticipated") else "Documented"}
    for p in parts:
        key, _, value = p.partition(":")
        if key.strip().lower() == "side":
            meta["side"] = value.strip()
        elif key.strip().lower() == "claim type":
            meta["claimType"] = value.strip()
        elif p not in ("Documented", "Anticipated"):
            sys.exit(f"card meta line has an unknown field {p!r}: {line}")
    if evidence:
        meta["evidence"] = evidence[0].strip()
    return meta


def parse_workspace(path: Path) -> tuple[dict, dict]:
    sections, cur, sub, why = [], None, None, {}
    for raw in path.read_text(encoding="utf-8").splitlines():
        line = raw.rstrip()
        if not line or line.startswith("# "):
            continue
        if line.startswith("## "):
            title = line[3:].strip()
            sid = next((v for k, v in SECTION_IDS.items() if title.lower().startswith(k)), title.lower())
            cur, sub = {"id": sid, "title": title, "blocks": [], "subs": []}, None
            sections.append(cur)
            continue
        if cur is None:
            continue
        if line.startswith("### "):
            sub = {"title": line[4:].strip(), "blocks": [], "cardMeta": None}
            cur["subs"].append(sub)
            continue
        if line.startswith("|"):
            cells = [c.strip() for c in line.strip("|").split("|")]
            ids = re.findall(r"S\d+[a-z]?", cells[0]) if cells else []
            if ids and len(cells) >= 5:
                for sid in ids:
                    why[sid] = cells[4].replace("<!-- nocheck -->", "").strip(" |")
            continue
        if line.endswith("<!-- nocheck -->"):
            if sub is not None and cur["id"] == "arguments":
                sub["cardMeta"] = parse_card_meta(line)
            continue
        (sub["blocks"] if sub is not None else cur["blocks"]).append(parse_block(line))
    return {"sections": sections}, why


def parse_home(path: Path) -> dict:
    secs, cur = [], None
    for line in path.read_text(encoding="utf-8").splitlines():
        if line.startswith("## "):
            title = line[3:].strip()
            cur = {"id": re.sub(r"[^a-z]+", "-", title.lower()).strip("-"), "title": title, "items": []}
            secs.append(cur)
        elif cur is not None and line.startswith("- "):
            b = parse_block(line)
            label = b.get("label") or ""
            power, when = None, label
            if "·" in label:
                first, rest = [x.strip() for x in label.split("·", 1)]
                if first in ("Decide", "Input", "Observe"):
                    power, when = first, rest
            cur["items"].append({"power": power, "when": when, "units": b["units"]})
    return {"sections": secs}


def parse_passages(path: Path) -> dict:
    out, cur = {}, None
    for line in path.read_text(encoding="utf-8").splitlines():
        m = re.match(r"^###\s+(P-\d{3})\s+·\s+(\S+)\s+·\s+(.*)$", line)
        if m:
            cur = m.group(1)
            out[cur] = {"source": m.group(2), "locator": m.group(3).strip(), "text": "", "note": None}
        elif cur and line.startswith("> "):
            out[cur]["text"] += line[2:].strip()
        elif cur and line.startswith("*Note: "):
            out[cur]["note"] = line[len("*Note: "):].rstrip("*").strip()
    return out


def bill_changes(markup_path: Path, wanted: list[tuple[str, int]]) -> list[dict]:
    lines = {}
    for line in markup_path.read_text(encoding="utf-8").splitlines():
        m = re.match(r"^(p\d+):\s+(\d+)\s+(.*)$", line)
        if m:
            lines[(m.group(1), int(m.group(2)))] = m.group(3)
    out = []
    for key in wanted:
        text = lines.get(key, "")
        segs = []
        for tok in text.split(" "):
            k, t = "txt", tok
            if tok.startswith("[-") and tok.endswith("-]"):
                k, t = "del", tok[2:-2]
            elif tok.startswith("{+") and tok.endswith("+}"):
                k, t = "ins", tok[2:-2]
            if segs and segs[-1]["k"] == k:
                segs[-1]["t"] += " " + t
            else:
                if segs:
                    segs[-1]["t"] += " " if segs[-1]["k"] == "txt" else ""
                    if k == "txt" or segs[-1]["k"] != "txt":
                        t = (" " if segs[-1]["k"] != "txt" else "") + t
                segs.append({"k": k, "t": t})
        # Display fix (same as passage P-008): the comma after "January 1" is struck with ", 2026,".
        for i, s in enumerate(segs):
            if s["k"] == "txt" and s["t"].endswith("January 1, ") and i + 1 < len(segs) and segs[i + 1]["k"] == "del":
                s["t"] = s["t"][:-2]
                segs[i + 1]["t"] = ", " + segs[i + 1]["t"] + " "
        out.append({"n": key[1], "segs": segs})
    return out


def build_workspace(trial: Path) -> dict:
    cfg = yaml.safe_load((trial / "page.yaml").read_text(encoding="utf-8"))
    short = cfg.get("short_labels") or {}
    workspace, why = parse_workspace(trial / "workspace.md")
    passages = parse_passages(trial / "passages.md")
    srcs = yaml.safe_load((trial / "sources.yaml").read_text(encoding="utf-8"))["sources"]
    order = [x["id"] for x in srcs]
    used = sorted({p["source"] for p in passages.values()}, key=order.index)
    sources = {}
    for s in srcs:
        if s["id"] not in used:
            continue
        sha = s.get("sha256") or ((s.get("files") or [{}])[0].get("sha256"))
        sources[s["id"]] = {"title": s.get("title"), "publisher": s.get("publisher"), "type": s.get("type"),
                            "typeLabel": TYPE_LABEL.get(s.get("type"), s.get("type")), "url": s.get("url"),
                            "retrieved_at": s.get("retrieved_at"), "sha256": sha, "method": s.get("method"),
                            "short": short.get(s["id"], s["id"]), "why": why.get(s["id"])}
    summary_path = trial / "verification" / "summary.json"
    verification = json.loads(summary_path.read_text(encoding="utf-8")) if summary_path.exists() else {
        "intro": "Checks have not run yet.", "stats": [], "sections": [], "verified": False}
    text = (trial / "workspace.md").read_text(encoding="utf-8")
    rev = hashlib.sha256(text.encode("utf-8")).hexdigest()
    rev_no = re.search(r"revision (\d+)", text)
    markup = cfg.get("markup") or {}
    changes = bill_changes(trial / markup["file"], [tuple(x) for x in markup["lines"]]) if markup else []
    return {
        "slug": cfg["slug"], "code": cfg["code"], "title": cfg["title"], "linkText": cfg["link_text"],
        "trial": trial.name.split("-")[0],
        "status": cfg.get("status") or [], "optionChips": cfg.get("option_chips") or {},
        "verified": bool(verification.get("verified")),
        "statusText": verification.get("statusText", "Checks pending"),
        "disclosure": ("Drafted with AI help from the archived sources below. Every sentence links to its source or is "
                       "labeled as our analysis. Checked by automated tests and independent AI reviewers. In this trial, "
                       "no person reviewed it."),
        "revisionLabel": (f"revision {rev_no.group(1)}" if rev_no else "revision ?") + f" (sha256 {rev[:12]})",
        "workspace": workspace, "passages": passages, "sources": sources, "sourceOrder": used,
        "billChanges": changes, "markupNote": markup.get("note"), "verification": verification,
    }


def merge_home(trials: list[Path], workspaces: list[dict]) -> dict:
    """One home page from every trial's home.md, in section order; each item links to its workspace."""
    merged: dict[str, dict] = {}
    for trial, ws in zip(trials, workspaces):
        for sec in parse_home(trial / "home.md")["sections"]:
            tgt = merged.setdefault(sec["id"], {"id": sec["id"], "title": sec["title"], "items": []})
            for it in sec["items"]:
                tgt["items"].append(dict(it, slug=ws["slug"], linkText=ws["linkText"]))
    return {"sections": list(merged.values())}


def main(template: Path, out_path: Path, trials: list[Path]) -> int:
    workspaces = [build_workspace(t) for t in trials]
    slugs = [w["slug"] for w in workspaces]
    if len(set(slugs)) != len(slugs):
        sys.exit(f"duplicate slugs: {slugs}")
    data = {
        "meta": {"footer": f"Prototype · built {dt.date.today().isoformat()} · Independent project, not a government website."},
        "workspaces": workspaces,
        "home": merge_home(trials, workspaces),
    }
    blob = json.dumps(data, ensure_ascii=False).replace("</", "<\\/")
    html = template.read_text(encoding="utf-8").replace("/*__DATA__*/", blob)
    out_path.parent.mkdir(parents=True, exist_ok=True)
    out_path.write_text(html, encoding="utf-8")
    for w in workspaces:
        print(f"  {w['slug']}: {w['revisionLabel']}, {len(w['passages'])} passages, {len(w['sources'])} sources")
    print(f"wrote {out_path} ({len(html) // 1024} KB): {len(workspaces)} workspace(s)")
    return 0


if __name__ == "__main__":
    if len(sys.argv) < 4:
        sys.exit(__doc__)
    sys.exit(main(Path(sys.argv[1]), Path(sys.argv[2]), [Path(a) for a in sys.argv[3:]]))
