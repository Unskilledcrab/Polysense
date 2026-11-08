#!/usr/bin/env python3
"""Manual-trial checker: sentence accounting, passage IDs, verbatim quotes, numbers, loaded words.

Usage:
    python3 trials/tools/check_citations.py trials/001-*/workspace.md trials/001-*/passages.md

Conventions (see docs/product/content-style-guide.md, section 6):
  * Every sentence ends with a marker placed right after it:
      [P-012] or [P-012, P-013]    factual sentence cited to passage(s)
      {analysis}                   our synthesis of cited facts
      {editorial}                  framing that cannot be cited (needs reviewer sign-off)
      {computed: <arithmetic>}     arithmetic from cited numbers
  * Passages in passages.md look like:
      ### P-001 · S01 · p.1, lines 1-4
      > passage text (may span several "> " lines)
  * Headings, blank lines, blockquotes, HTML comments, code fences and short label lines
    (no sentence punctuation, fewer than 6 words) are not checked.
    Add <!-- nocheck --> to the end of a line to skip it explicitly.

Standard library only. Exit code 1 if any ERROR is found.
"""
from __future__ import annotations

import re
import sys
import unicodedata
from pathlib import Path

MARKER = re.compile(r"\[(P-\d{3}(?:\s*,\s*P-\d{3})*)\]|\{(analysis|editorial)\}|\{computed:[^}]*\}")
PASSAGE_HEAD = re.compile(r"^###\s+(P-\d{3})\b")
QUOTE = re.compile(r"[\"“]([^\"”]{3,})[\"”]")
NUMBER = re.compile(r"(?<![\w-])\$?\d[\d,]*(?:\.\d+)?%?")
# Words that signal loaded framing when used outside a verbatim quote (style guide §5).
LOADED = [
    "radical", "extreme", "common-sense", "common sense", "giveaway", "attack on", "slash",
    "gut ", "guts ", "crack down", "crackdown", "job-killing", "disastrous", "draconian",
    "reckless", "landmark", "historic", "sweeping",
]


def norm(text: str) -> str:
    text = unicodedata.normalize("NFKC", text)
    text = text.replace("“", '"').replace("”", '"').replace("’", "'").replace("‘", "'")
    return re.sub(r"\s+", " ", text).strip().lower()


def load_passages(path: Path) -> dict[str, str]:
    passages: dict[str, list[str]] = {}
    current = None
    for line in path.read_text(encoding="utf-8").splitlines():
        m = PASSAGE_HEAD.match(line)
        if m:
            current = m.group(1)
            passages[current] = []
        elif current and line.startswith(">"):
            passages[current].append(line[1:].strip())
        elif current and line.startswith("#"):
            current = None
    return {pid: norm(" ".join(lines)) for pid, lines in passages.items()}


def checkable_lines(path: Path):
    in_fence = False
    for no, raw in enumerate(path.read_text(encoding="utf-8").splitlines(), 1):
        line = raw.strip()
        if line.startswith("```"):
            in_fence = not in_fence
            continue
        if in_fence or not line or line.startswith(("#", ">", "<!--", "|---", "| ---")):
            continue
        if line.endswith("<!-- nocheck -->"):
            continue
        line = re.sub(r"^\s*(?:[-*]|\d+\.)\s+", "", line)      # list bullets
        line = line.replace("|", " ").strip()                     # table cells
        line = re.sub(r"<!--.*?-->", "", line)
        if not line:
            continue
        words = re.findall(r"[A-Za-z]+", MARKER.sub("", line))
        if not MARKER.search(line) and not re.search(r"[.!?]$", line) and len(words) < 6:
            continue                                              # short label line
        yield no, line


def main(workspace: Path, passages_path: Path) -> int:
    passages = load_passages(passages_path)
    errors, warnings = [], []
    stats = {"cited": 0, "analysis": 0, "editorial": 0, "computed": 0}

    for no, line in checkable_lines(workspace):
        pos = 0
        for m in MARKER.finditer(line):
            chunk = line[pos:m.start()].strip()
            pos = m.end()
            if not re.search(r"[A-Za-z]", chunk):
                errors.append(f"L{no}: marker {m.group(0)} has no sentence before it")
                continue
            if len(re.findall(r"[.!?](?:\s|$)", chunk)) > 1:
                warnings.append(f"L{no}: several sentences share one marker: {chunk[:70]}…")
            if m.group(1):
                stats["cited"] += 1
                ids = [i.strip() for i in m.group(1).split(",")]
                missing = [i for i in ids if i not in passages]
                if missing:
                    errors.append(f"L{no}: unknown passage id(s) {missing}")
                    continue
                cited = " ".join(passages[i] for i in ids)
                for q in QUOTE.findall(chunk):
                    if norm(q) not in cited:
                        errors.append(f"L{no}: quote not verbatim in {ids}: \"{q[:60]}\"")
                for n in NUMBER.findall(chunk):
                    bare = n.replace("$", "").replace(",", "").rstrip("%")
                    if bare not in cited.replace(",", ""):
                        warnings.append(f"L{no}: number {n} not found in {ids} (tag {{computed}} if derived)")
            else:
                kind = m.group(2) or "computed"
                stats[kind] += 1
            unquoted = norm(QUOTE.sub("", chunk))
            for word in LOADED:
                if word in f" {unquoted} ":
                    warnings.append(f"L{no}: loaded word '{word.strip()}' outside a quote")
        tail = line[pos:].strip()
        if re.search(r"[A-Za-z]{2,}", tail):
            errors.append(f"L{no}: sentence without citation or tag: {tail[:80]}")

    total = sum(stats.values())
    print(f"Checked {workspace}")
    print(f"  sentences/units: {total}  " + "  ".join(f"{k}: {v}" for k, v in stats.items()))
    print(f"  passages available: {len(passages)}")
    for e in errors:
        print("ERROR  ", e)
    for w in warnings:
        print("WARN   ", w)
    print(f"Result: {'FAIL' if errors else 'PASS'} ({len(errors)} errors, {len(warnings)} warnings)")
    return 1 if errors else 0


if __name__ == "__main__":
    if len(sys.argv) != 3:
        sys.exit(__doc__)
    sys.exit(main(Path(sys.argv[1]), Path(sys.argv[2])))
