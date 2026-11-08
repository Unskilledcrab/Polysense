#!/usr/bin/env python3
"""Dump an .xlsx workbook's cell values (stdlib only), one row per line: "Sheet!ROW: A=.. | B=..".

Usage: python3 -I xlsx2txt.py IN.xlsx OUT.txt
Cached formula results are used (the value Excel last saved). Untrusted input: no DTDs allowed.
"""
from __future__ import annotations

import re
import sys
import zipfile
import xml.etree.ElementTree as ET

NS = "{http://schemas.openxmlformats.org/spreadsheetml/2006/main}"
RNS = "{http://schemas.openxmlformats.org/officeDocument/2006/relationships}"


def safe(data: bytes) -> ET.Element:
    if b"<!DOCTYPE" in data[:4096].upper() or b"<!ENTITY" in data.upper():
        raise SystemExit("refusing XML with DTD/entity declarations")
    return ET.fromstring(data)


def main(src: str, dst: str) -> int:
    out = []
    with zipfile.ZipFile(src) as z:
        names = set(z.namelist())
        shared = []
        if "xl/sharedStrings.xml" in names:
            for si in safe(z.read("xl/sharedStrings.xml")).findall(NS + "si"):
                shared.append("".join(t.text or "" for t in si.iter(NS + "t")))
        wb = safe(z.read("xl/workbook.xml"))
        rels = safe(z.read("xl/_rels/workbook.xml.rels"))
        target = {r.get("Id"): r.get("Target") for r in rels}
        for sh in wb.iter(NS + "sheet"):
            name = sh.get("name")
            path = target[sh.get(RNS + "id")].lstrip("/")
            path = path if path.startswith("xl/") else "xl/" + path
            out.append(f"=== sheet: {name}")
            root = safe(z.read(path))
            for row in root.iter(NS + "row"):
                cells = []
                for c in row.findall(NS + "c"):
                    ref, t = c.get("r"), c.get("t")
                    v = c.find(NS + "v")
                    if t == "s" and v is not None:
                        val = shared[int(v.text)]
                    elif t == "inlineStr":
                        val = "".join(x.text or "" for x in c.iter(NS + "t"))
                    else:
                        val = v.text if v is not None else ""
                    if val not in ("", None):
                        cells.append(f"{re.sub(r'[0-9]+', '', ref)}={val}")
                if cells:
                    out.append(f"{name}!{row.get('r')}: " + " | ".join(cells))
    with open(dst, "w", encoding="utf-8") as fh:
        fh.write("\n".join(out) + "\n")
    print(f"wrote {dst}: {len(out)} lines")
    return 0


if __name__ == "__main__":
    if len(sys.argv) != 3:
        sys.exit(__doc__)
    sys.exit(main(sys.argv[1], sys.argv[2]))
