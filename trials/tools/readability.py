#!/usr/bin/env python3
"""Reading-grade screen per workspace section (target: grade 6–8 for the 30-second version and Layers 1–3).

Usage:
    python3 trials/tools/readability.py trials/001-*/workspace.md

Computes the Flesch–Kincaid grade with a simple heuristic syllable counter, using only the standard
library. (We tried textstat, but current releases fetch NLTK data from the network at runtime, which
breaks our dependency policy. See docs/architecture/dependencies.md.)

Citation markers, tags and Markdown syntax are stripped before scoring. Formulas are a screening
aid only. Comprehension testing with people decides (docs/product/usability-testing.md).
"""
from __future__ import annotations

import re
import sys
from pathlib import Path

MARKER = re.compile(r"\[(?:P-\d{3}(?:\s*,\s*P-\d{3})*)\]|\{[^}]*\}")
TARGET_SECTIONS = ("30-second", "what it means for you", "layer 1", "layer 2", "layer 3")
TARGET_MAX = 8.5
# Length budgets from the style guide (v0.6), checked as warnings. Words exclude citations and tags.
BUDGETS = {"30-second version": 90, "If you … (first sentence)": 25, "If you … (whole line)": 45,
           "In short": 40, "Values line": 35, "Layer 3 intro": 60}
# "both", "neither" or "either" standing alone as a pronoun ("Count both, or neither?") instead of naming
# the thing (style guide v0.6, section 9). A word followed by a noun ("both the contracts") passes.
BARE = re.compile(r"\b(both|neither|either)\b(?=\s*[,.;:?!)]|\s+(?:or|nor|is|are|was|were|would|will|could|"
                  r"raises?|lowers?|counts?|gets?)\b)", re.I)


def syllables(word: str) -> int:
    word = word.lower().strip("'")
    if len(word) <= 3:
        return 1
    word = re.sub(r"(?:[^laeiouy]es|ed|[^laeiouy]e)$", "", word)  # silent endings
    word = re.sub(r"^y", "", word)
    return max(1, len(re.findall(r"[aeiouy]{1,2}", word)))


def fk_grade(text: str) -> float:
    # A sentence may end inside closing quotes or brackets: 'said "no." Then' (Trial 002).
    sentences = max(1, len(re.findall(r"[.!?]+[\"'”’)\]]*(?:\s|$)", text)))
    words = re.findall(r"[A-Za-z][A-Za-z'-]*", text)
    if not words:
        return 0.0
    syl = sum(syllables(w) for w in words)
    return 0.39 * (len(words) / sentences) + 11.8 * (syl / len(words)) - 15.59


def sections(text: str):
    title, buf = "(preamble)", []
    for line in text.splitlines():
        if line.startswith("## "):
            yield title, "\n".join(buf)
            title, buf = line[3:].strip(), []
        else:
            buf.append(line)
    yield title, "\n".join(buf)


def clean(md: str) -> str:
    # Lines marked <!-- nocheck --> are metadata such as a card's evidence label: short fragments that
    # inflate the grade if scored as prose (Trial 003). Their length is reported separately in main().
    md = "\n".join(line for line in md.splitlines() if not line.rstrip().endswith("<!-- nocheck -->"))
    md = MARKER.sub("", md)
    md = re.sub(r"<!--.*?-->", "", md, flags=re.S)
    lines = []
    for line in md.splitlines():
        line = re.sub(r"^\s*(?:[-*>]|\d+\.)\s+", "", line).strip()
        if line and not re.search(r"[.!?][\"'”’)\]]*$", line):
            line += "."                      # bullets and table cells count as sentences
        lines.append(line)
    md = re.sub(r"[*_`#|]", " ", " ".join(lines))
    return re.sub(r"\s+", " ", md).strip()


def main(path: Path) -> int:
    over = 0
    print(f"{'section':45} {'words':>6} {'FK grade':>9}  target ≤ {TARGET_MAX}")
    for title, body in sections(path.read_text(encoding="utf-8")):
        text = clean(body)
        words = len(text.split())
        if words < 20:
            continue
        grade = fk_grade(text)
        flag = ""
        if title.lower().startswith(TARGET_SECTIONS):
            flag = "OK" if grade <= TARGET_MAX else "OVER"
            over += flag == "OVER"
        print(f"{title[:45]:45} {words:>6} {grade:>9.1f}  {flag}")
    print(f"Sections over target: {over}")
    for warning in budget_warnings(path.read_text(encoding="utf-8")):
        # Budgets are warnings; CHECK lines (possible vague references) are prompts for the writer, since
        # "no data on either" is fine when the two things are named in the same sentence.
        print(warning if warning.startswith("CHECK") else "WARN " + warning)
    labels = [(no, len(MARKER.sub("", re.sub(r"<!--.*?-->", "", line)).split()))
              for no, line in enumerate(path.read_text(encoding="utf-8").splitlines(), 1)
              if line.rstrip().endswith("<!-- nocheck -->") and "Evidence:" in line]
    if labels:
        no, longest = max(labels, key=lambda x: x[1])
        print(f"Card labels (not scored): {len(labels)}; longest {longest} words (L{no})")
    return 0


def words(text: str) -> int:
    return len(re.findall(r"[A-Za-z0-9$%][\w$%,.'’-]*", MARKER.sub("", re.sub(r"<!--.*?-->", "", text))))


def budget_warnings(md: str) -> list[str]:
    """Style-guide length budgets and bare both/neither/either, as warnings (exit code stays 0)."""
    out, section, sub = [], "", ""
    for no, line in enumerate(md.splitlines(), 1):
        if line.startswith("## "):
            section, sub = line[3:].strip().lower(), ""
            if section.startswith("30-second"):
                body = []
                for nxt in md.splitlines()[no:]:
                    if nxt.startswith("## "):
                        break
                    body.append(nxt)
                n = words("\n".join(b for b in body if not b.rstrip().endswith("<!-- nocheck -->")))
                if n > BUDGETS["30-second version"]:
                    out.append(f"L{no}: 30-second version is {n} words (budget {BUDGETS['30-second version']})")
            continue
        if line.startswith("### "):
            sub = line[4:].strip().lower()
            continue
        if line.rstrip().endswith("<!-- nocheck -->") or line.startswith("|"):
            continue
        text = re.sub(r"^\s*(?:[-*]|\d+\.)\s+", "", line)
        label = re.match(r"^\*\*(.+?):\*\*\s*(.*)$", text)
        if section.startswith("what it means for you") and not sub and label and label.group(1).lower().startswith("if you"):
            first = re.split(r"(?<=[\]}])\s+", label.group(2), maxsplit=1)[0]
            if words(first) > BUDGETS["If you … (first sentence)"]:
                out.append(f"L{no}: first sentence of '{label.group(1)}' is {words(first)} words (budget 25)")
            if words(label.group(2)) > BUDGETS["If you … (whole line)"]:
                out.append(f"L{no}: '{label.group(1)}' line is {words(label.group(2))} words (budget 45)")
        if label and label.group(1).lower() == "in short" and words(label.group(2)) > BUDGETS["In short"]:
            out.append(f"L{no}: In short is {words(label.group(2))} words (budget 40)")
        if label and sub.startswith("if you care most about") and words(label.group(2)) + words(label.group(1)) > BUDGETS["Values line"]:
            out.append(f"L{no}: values line is {words(label.group(2)) + words(label.group(1))} words (budget 35)")
        if section.startswith("layer 3") and not sub and text.strip() and words(text) > BUDGETS["Layer 3 intro"]:
            out.append(f"L{no}: Layer 3 intro paragraph is {words(text)} words (budget 60)")
        unquoted = re.sub(r"[\"“][^\"”]*[\"”]", "", MARKER.sub("", text))
        for m in BARE.finditer(unquoted):
            out.append(f"CHECK L{no}: bare '{m.group(1)}': can a reader tell what it refers to? If not, name it")
    return out


if __name__ == "__main__":
    if len(sys.argv) != 2:
        sys.exit(__doc__)
    sys.exit(main(Path(sys.argv[1])))
