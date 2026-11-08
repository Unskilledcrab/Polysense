#!/usr/bin/env python3
"""Extract visible text from a .docx (stdlib only): body paragraphs, tables (one row per line,
cells joined with " | "), then footnotes. Tracked deletions and field codes are skipped.

Usage: python3 -I docx2txt.py IN.docx OUT.txt
Treats the input as untrusted: reads members in memory, refuses DTD/entity declarations.
"""
from __future__ import annotations

import sys
import zipfile
import xml.etree.ElementTree as ET

W = "{http://schemas.openxmlformats.org/wordprocessingml/2006/main}"


def safe_xml(data: bytes) -> ET.Element:
    head = data[:4096].upper()
    if b"<!DOCTYPE" in head or b"<!ENTITY" in data.upper():
        raise SystemExit("refusing XML with DTD/entity declarations")
    return ET.fromstring(data)


def run_text(el: ET.Element) -> str:
    out = []
    for node in el.iter():
        tag = node.tag
        if tag == W + "t":
            out.append(node.text or "")
        elif tag in (W + "tab", W + "ptab"):
            out.append(" ")
        elif tag in (W + "br", W + "cr"):
            out.append("\n")
        elif tag == W + "noBreakHyphen":
            out.append("-")
        elif tag == W + "footnoteReference":
            out.append(f"[fn{node.get(W + 'id')}]")
    return "".join(out)


def para_text(p: ET.Element) -> str:
    # Skip tracked deletions and field instructions by walking runs ourselves.
    parts = []
    for child in p.iter():
        if child.tag == W + "r":
            # A run inside w:del is a deletion; ElementTree has no parent pointers, so we
            # pre-mark deleted runs below.
            if child.get("__deleted"):
                continue
            parts.append(run_text(child))
    return "".join(parts)


MC = "{http://schemas.openxmlformats.org/markup-compatibility/2006}"


def mark_deleted(root: ET.Element) -> None:
    for d in root.iter(W + "del"):
        for r in d.iter(W + "r"):
            r.set("__deleted", "1")
    for fb in root.iter(MC + "Fallback"):   # duplicate copy of text boxes for old Word versions
        for r in fb.iter(W + "r"):
            r.set("__deleted", "1")
    for r in root.iter(W + "r"):
        if any(c.tag == W + "instrText" for c in r):
            r.set("__deleted", "1")


def body_lines(body: ET.Element) -> list[str]:
    lines: list[str] = []
    for el in body:
        if el.tag == W + "p":
            lines.append(para_text(el))
        elif el.tag == W + "tbl":
            for tr in el.iter(W + "tr"):
                cells = []
                for tc in tr.findall(W + "tc"):
                    cells.append(" ".join(para_text(p) for p in tc.iter(W + "p")).strip())
                lines.append(" | ".join(cells))
        elif el.tag == W + "sdt":
            content = el.find(W + "sdtContent")
            if content is not None:
                lines.extend(body_lines(content))
    return lines


def main(src: str, dst: str) -> int:
    with zipfile.ZipFile(src) as z:
        names = set(z.namelist())
        doc = safe_xml(z.read("word/document.xml"))
        mark_deleted(doc)
        body = doc.find(W + "body")
        lines = body_lines(body)
        if "word/footnotes.xml" in names:
            fn = safe_xml(z.read("word/footnotes.xml"))
            mark_deleted(fn)
            notes = []
            for f in fn.findall(W + "footnote"):
                fid = f.get(W + "id")
                if fid in ("-1", "0"):
                    continue
                txt = " ".join(para_text(p) for p in f.iter(W + "p")).strip()
                if txt:
                    notes.append(f"[footnote {fid}] {txt}")
            if notes:
                lines.append("")
                lines.append("=== FOOTNOTES ===")
                lines.extend(notes)
    text = "\n".join(ln.rstrip() for ln in lines)
    with open(dst, "w", encoding="utf-8") as fh:
        fh.write(text + "\n")
    print(f"wrote {dst}: {len(text)} chars, {len(lines)} lines")
    return 0


if __name__ == "__main__":
    if len(sys.argv) != 3:
        sys.exit(__doc__)
    sys.exit(main(sys.argv[1], sys.argv[2]))
