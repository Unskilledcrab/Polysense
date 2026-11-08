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
TARGET_SECTIONS = ("30-second", "layer 1", "layer 2", "layer 3")
TARGET_MAX = 8.5


def syllables(word: str) -> int:
    word = word.lower().strip("'")
    if len(word) <= 3:
        return 1
    word = re.sub(r"(?:[^laeiouy]es|ed|[^laeiouy]e)$", "", word)  # silent endings
    word = re.sub(r"^y", "", word)
    return max(1, len(re.findall(r"[aeiouy]{1,2}", word)))


def fk_grade(text: str) -> float:
    sentences = max(1, len(re.findall(r"[.!?]+(?:\s|$)", text)))
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
        if line and not re.search(r"[.!?]$", line):
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
    labels = [(no, len(MARKER.sub("", re.sub(r"<!--.*?-->", "", line)).split()))
              for no, line in enumerate(path.read_text(encoding="utf-8").splitlines(), 1)
              if line.rstrip().endswith("<!-- nocheck -->") and "Evidence:" in line]
    if labels:
        no, longest = max(labels, key=lambda x: x[1])
        print(f"Card labels (not scored): {len(labels)}; longest {longest} words (L{no})")
    return 0


if __name__ == "__main__":
    if len(sys.argv) != 2:
        sys.exit(__doc__)
    sys.exit(main(Path(sys.argv[1])))
