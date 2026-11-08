#!/usr/bin/env python3
"""Recover strike-through (deleted) and underlined (inserted) words in Georgia bill PDFs.

Georgia bills show deleted text with a strike-through line and new text with an underline.
Plain text extraction merges both ("restricted prohibited"), which silently changes meaning.
This tool finds thin horizontal lines/rects and classifies each word they cross:
  * line through the middle of the word's height  -> [-deleted-]
  * line at/just below the word's baseline         -> {+inserted+}

Usage:
    uv run --with pdfplumber python3 trials/tools/pdf_markup.py BILL.pdf [--only-marked | --clean | --old]

Output: one line per PDF text line, prefixed by page number:
    (default)  markup shown as [-deleted-] and {+inserted+}
    --clean    the law as enacted: deleted words dropped, inserted words kept (no markers)
    --old      the law as it was: inserted words dropped, deleted words kept (no markers)
Requires pdfplumber (MIT). Screening aid: spot-check the output against the PDF visually.
"""
from __future__ import annotations

import sys

try:
    import pdfplumber
except ImportError:  # pragma: no cover
    sys.exit("pdfplumber is required: uv run --with pdfplumber python3 trials/tools/pdf_markup.py ...")


def horizontal_segments(page):
    segs = []
    for ln in page.lines:
        if abs(ln["top"] - ln["bottom"]) < 1.5:
            segs.append((min(ln["x0"], ln["x1"]), max(ln["x0"], ln["x1"]), (ln["top"] + ln["bottom"]) / 2))
    for r in page.rects:
        if r["height"] < 2.0 and r["width"] > 2.0:
            segs.append((r["x0"], r["x1"], (r["top"] + r["bottom"]) / 2))
    return segs


def classify(word, segs):
    h = word["bottom"] - word["top"]
    for x0, x1, y in segs:
        overlap = min(x1, word["x1"]) - max(x0, word["x0"])
        if overlap < 0.6 * (word["x1"] - word["x0"]):
            continue
        rel = (y - word["top"]) / h if h else 0
        if 0.3 <= rel <= 0.75:
            return "strike"
        if 0.8 <= rel <= 1.35:
            return "under"
    return None


def render(text: str, kind: str | None, mode: str) -> str | None:
    if mode == "clean":
        return None if kind == "strike" else text
    if mode == "old":
        return None if kind == "under" else text
    return f"[-{text}-]" if kind == "strike" else f"{{+{text}+}}" if kind == "under" else text


def main(path: str, mode: str) -> None:
    with pdfplumber.open(path) as pdf:
        for pno, page in enumerate(pdf.pages, 1):
            segs = horizontal_segments(page)
            words = page.extract_words(keep_blank_chars=False, use_text_flow=True)
            lines: dict[int, list] = {}
            for w in words:
                lines.setdefault(round(w["top"]), []).append(w)
            for top in sorted(lines):
                out, marked = [], False
                for w in sorted(lines[top], key=lambda w: w["x0"]):
                    kind = classify(w, segs)
                    marked |= kind is not None
                    piece = render(w["text"], kind, mode)
                    if piece is not None:
                        out.append(piece)
                if marked or mode != "only-marked":
                    print(f"p{pno}: " + " ".join(out))


if __name__ == "__main__":
    if len(sys.argv) < 2:
        sys.exit(__doc__)
    flags = sys.argv[2:]
    chosen = "only-marked" if "--only-marked" in flags else "clean" if "--clean" in flags else "old" if "--old" in flags else "markup"
    main(sys.argv[1], chosen)
