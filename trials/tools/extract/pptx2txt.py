#!/usr/bin/env python3
"""Extract visible slide text from a .pptx using only the standard library (zipfile + xml).

Usage: python3 -I pptx2txt.py DECK.pptx > DECK.txt
Prints '=== slide N ===' then one line per paragraph (a:p), joining its text runs (a:t),
in slide order (ppt/slides/slideN.xml). Tables are read cell by cell, one paragraph per line.
Speaker notes are not included. Screening aid; spot-check against the deck.
"""
import re
import sys
import zipfile
import xml.etree.ElementTree as ET

A = "{http://schemas.openxmlformats.org/drawingml/2006/main}"


def main(path: str) -> None:
    with zipfile.ZipFile(path) as z:
        names = [n for n in z.namelist() if re.fullmatch(r"ppt/slides/slide\d+\.xml", n)]
        names.sort(key=lambda n: int(re.search(r"(\d+)\.xml$", n).group(1)))
        for n in names:
            root = ET.fromstring(z.read(n))
            print(f"=== slide {re.search(r'(\d+)', n.split('/')[-1]).group(1)} ===")
            for p in root.iter(A + "p"):
                text = "".join(t.text or "" for t in p.iter(A + "t")).strip()
                if text:
                    print(text)


if __name__ == "__main__":
    main(sys.argv[1])
