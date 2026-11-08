#!/usr/bin/env python3
"""Extract a PDF's text layer as "pN: LINE text" lines, the prefix format build_passages.py strips.

Usage: uv run --with pdfplumber python3 -I pdf2txt.py IN.pdf OUT.txt [--margin X]

LINE is the document's own margin line number when the line starts with one (a number whose
left edge is left of --margin, default 62 pt), else 0. Pages with no text layer are reported.
"""
from __future__ import annotations

import re
import sys

import pdfplumber


def main(src: str, dst: str, margin: float) -> int:
    out, empty = [], []
    with pdfplumber.open(src) as pdf:
        for n, page in enumerate(pdf.pages, 1):
            lines = page.extract_text_lines(return_chars=False)
            if not lines:
                empty.append(n)
                continue
            for ln in lines:
                text = ln["text"].strip()
                if not text or (re.fullmatch(r"\d{1,2}", text) and ln["x0"] < margin):
                    continue  # blank numbered line
                m = re.match(r"^(\d{1,2})\s+(.*)$", text)
                if m and ln["x0"] < margin:
                    out.append(f"p{n}: {m.group(1)} {m.group(2)}")
                else:
                    out.append(f"p{n}: 0 {text}")
    with open(dst, "w", encoding="utf-8") as fh:
        fh.write("\n".join(out) + "\n")
    print(f"wrote {dst}: {len(out)} lines; pages without text: {empty or 'none'}")
    return 0


if __name__ == "__main__":
    args = sys.argv[1:]
    margin = 62.0
    if "--margin" in args:
        i = args.index("--margin")
        margin = float(args[i + 1])
        del args[i : i + 2]
    if len(args) != 2:
        sys.exit(__doc__)
    sys.exit(main(args[0], args[1], margin))
