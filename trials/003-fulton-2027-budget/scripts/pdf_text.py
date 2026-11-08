#!/usr/bin/env python3
"""Plain text from a PDF with pdfplumber (MIT; listed in docs/architecture/dependencies.md).

Usage: uv run --with pdfplumber python3 -I pdf_text.py FILE.pdf [--prefix] > FILE.txt
Default: '[page N]' on its own line before each page's text (pdfplumber extract_text).
--prefix: for line-numbered legal documents, write every line as 'pN: <line>' (the format
pdf_markup.py writes), so trials/tools/build_passages.py strips the page tag and line number.
"""
import sys

import pdfplumber

prefix = "--prefix" in sys.argv[2:]
with pdfplumber.open(sys.argv[1]) as pdf:
    for n, page in enumerate(pdf.pages, 1):
        text = page.extract_text() or ""
        if prefix:
            for line in text.splitlines():
                print(f"p{n}: {line}")
        else:
            print(f"[page {n}]")
            print(text)
