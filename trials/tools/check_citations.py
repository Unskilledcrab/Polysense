#!/usr/bin/env python3
"""Manual-trial checker: sentence accounting, passage IDs, verbatim quotes, numbers, loaded words.

Usage:
    python3 trials/tools/check_citations.py trials/001-*/workspace.md trials/001-*/passages.md

Conventions (see docs/product/content-style-guide.md, section 6):
  * Every sentence ends with a marker placed right after it:
      [P-012] or [P-012, P-013]    factual sentence cited to passage(s)
      {analysis} / {analysis: how}   our synthesis of cited facts (optionally say how)
      {editorial} / {editorial: why} framing that cannot be cited (needs verifier sign-off)
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
from decimal import ROUND_HALF_UP, Decimal, InvalidOperation
from pathlib import Path

MARKER = re.compile(r"\[(P-\d{3}(?:\s*,\s*P-\d{3})*)\]|\{(analysis|editorial)(?::[^}]*)?\}|\{computed:[^}]*\}")
PASSAGE_HEAD = re.compile(r"^###\s+(P-\d{3})\b")
QUOTE = re.compile(r"[\"“]([^\"”]{3,})[\"”]")
NUMBER = re.compile(r"(?<![\w-])\$?\d[\d,]*(?:\.\d+)?(?:-\d+)*%?")
LABELED = re.compile(r"\b(?:layer|option|question|card|step)\s+$", re.I)   # our own labels, not facts
# "No party disputes ..." claims something about every source, including ones we couldn't read. Style guide:
# absence claims need the whole text, so scope them ("no party whose testimony we read"). Trial 002 had 10.
ABSENCE = re.compile(r"\bno (?:party|parties|one|other party|source|filing|record|passage)\b[^.;]{0,40}?\b(?:disputes?|disagrees?|"
                     r"asks?|argues?|says?|said|mentions?|objects?|opposes?|challenges?|proposes?|claims?|denies|contests?)\b", re.I)
SCOPED = re.compile(r"\b(?:we (?:could )?(?:read|found|archived|checked|reviewed)|our (?:archived )?sources|archived|we could find)\b", re.I)
SCALE = {"thousand": 3, "million": 6, "billion": 9}
# Words that signal loaded framing when used outside a verbatim quote (style guide §5).
LOADED = [
    "radical", "extreme", "common-sense", "common sense", "giveaway", "attack on", "slash",
    "gut ", "guts ", "crack down", "crackdown", "job-killing", "disastrous", "draconian",
    "reckless", "landmark", "historic", "sweeping",
]
# Party and left-right labels: a warning outside quotes, so the drafter checks the fact really needs one
# (style guide §7: keep political parties out of it). Whole words only, so "nonpartisan" doesn't match.
PARTY = re.compile(r"\b(republicans?|democrats?|democratic party|gop|liberals?|conservatives?|left-wing|right-wing|"
                   r"far-left|far-right|the left|the right|partisan|bipartisan)\b", re.I)


NUMBER_WORDS = {
    "zero": "0", "one": "1", "two": "2", "three": "3", "four": "4", "five": "5", "six": "6",
    "seven": "7", "eight": "8", "nine": "9", "ten": "10", "eleven": "11", "twelve": "12",
}


def with_numerals(text: str) -> str:
    """Add digit forms of number words so 'grade twelve' also matches '12'."""
    return text + " " + " ".join(NUMBER_WORDS[w] for w in re.findall(r"[a-z]+", text) if w in NUMBER_WORDS)


def number_found(bare: str, scale: str, flat: str, nums: list[Decimal]) -> bool:
    """Is a number from the sentence in the cited passages?

    Whole numbers only, so "4" does not pass because "2024" is in the passage (Trial 003 found the old
    substring match let that through). "05" in 05/05/2026 equals "5". A rounded figure with 2 or more
    significant digits passes ("$1.48" for 1.48058; "$220 million" for $220,454,272); a 1-digit figure
    must match exactly. An identifier or range such as 26-0729 must appear whole, or each part must.
    """
    if "-" in bare:
        return bare in flat or all(number_found(p, "", flat, nums) for p in bare.split("-") if p)
    claim = Decimal(bare)
    if claim in nums:
        return True
    if len(claim.as_tuple().digits) < 2:
        return False
    places = Decimal(1).scaleb(claim.as_tuple().exponent)
    for n in nums:
        for shift in {0, SCALE.get(scale, 0)}:
            try:
                if n.scaleb(-shift).quantize(places, ROUND_HALF_UP) == claim:
                    return True
            except InvalidOperation:
                continue
    return False


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
            no_abbrev = re.sub(r"\b(?:Sen|Rep|Gov|Dr|Mr|Mrs|Ms|St|No|vs|Jan|Feb|Mar|Apr|Aug|Sept|Oct|Nov|Dec|Jr|Inc|Co)\.|\b[ap]\.m\.|\bU\.S\.|\b[A-Z]\.(?=\s+[A-Z])", "", chunk)
            if len(re.findall(r"[.!?](?:\s|$)", no_abbrev)) > 1:
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
                    # American style may add one closing period/comma inside the quote marks;
                    # every word must still match the passage exactly.
                    if norm(q).rstrip(".,") not in cited:
                        errors.append(f"L{no}: quote not verbatim in {ids}: \"{q[:60]}\"")
                flat = with_numerals(cited).replace(",", "")
                nums = [Decimal(t) for t in re.findall(r"\d+(?:\.\d+)?", flat)]
                for n in NUMBER.finditer(chunk):
                    if LABELED.search(chunk[:n.start()]):
                        continue                                  # the page's own label: "Option 1", "Layer 2"
                    scale = re.match(r"\s*(thousand|million|billion)\b", chunk[n.end():], re.I)
                    bare = n.group(0).replace("$", "").replace(",", "").rstrip("%")
                    if not number_found(bare, scale.group(1).lower() if scale else "", flat, nums):
                        warnings.append(f"L{no}: number {n.group(0)} not found in {ids} (tag {{computed}} if derived)")
            else:
                kind = m.group(2) or "computed"
                stats[kind] += 1
            unquoted = norm(QUOTE.sub("", chunk))
            for word in LOADED:
                if word in f" {unquoted} ":
                    warnings.append(f"L{no}: loaded word '{word.strip()}' outside a quote")
            if ABSENCE.search(unquoted) and not SCOPED.search(unquoted):
                warnings.append(f"L{no}: absence claim '{ABSENCE.search(unquoted).group(0)[:40]}' covers sources we "
                                "didn't read; scope it (\"no party whose testimony we read …\")")
            for m_party in PARTY.finditer(unquoted):
                warnings.append(f"L{no}: party or left-right label '{m_party.group(0)}' outside a quote; "
                                "keep it only if the fact needs it")
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
