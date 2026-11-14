#!/usr/bin/env python3
"""Privacy gate: no street address, and no private person's name, in a page's tracked files.

    check_privacy.py TRIAL_DIR

Checks workspace.md, home.md, passages.md, page.yaml, sources.yaml, candidates.yaml and the JSON records.
  1. Street addresses ("123 Oak Road"). Public landmark addresses are allowed when page.yaml lists them under
     `public_addresses:` (the PSC's hearing room, a county building).
  2. Names listed one per line in TRIAL_DIR/_raw/private_names.txt, if that file exists. The list itself is in
     the git-ignored _raw/ folder, so the names it guards are never committed.

Invariant 7: never store, log or commit a person's address. Filings often give them. Exit 1 on any finding.
Stdlib only (PyYAML for `public_addresses`, optional).
"""
from __future__ import annotations

import re
import sys
from pathlib import Path

FILES = ("workspace.md", "home.md", "passages.md", "page.yaml", "sources.yaml", "candidates.yaml", "revisions.jsonl", "status.json", "claims.jsonl", "watch.json")
STREET = re.compile(r"\b\d{1,6}\s+(?:[A-Z][A-Za-z.'-]+\s+){1,4}(?:Road|Rd|Street|St|Lane|Ln|Drive|Dr|Way|Court|Ct|Highway|Hwy|Avenue|Ave|Boulevard|Blvd|Circle|Cir|Trail|Trl|Place|Pl|Parkway|Pkwy)\b\.?")
NOT_A_STREET = re.compile(r"(Superior|State|Juvenile|Magistrate|Probate|Supreme|District|Municipal|Federal|Appeals|Recorder's|Drug|Family) Court$")


def main(trial: Path) -> int:
    allowed: list[str] = []
    try:
        import yaml
        allowed = [str(x).lower() for x in (yaml.safe_load((trial / "page.yaml").read_text(encoding="utf-8")) or {}).get("public_addresses") or []]
    except (ImportError, FileNotFoundError):
        pass
    names_file = trial / "_raw" / "private_names.txt"
    names = [n.strip() for n in names_file.read_text(encoding="utf-8").splitlines() if n.strip()] if names_file.exists() else []
    bad = 0
    for name in FILES:
        f = trial / name
        if not f.exists():
            continue
        for no, line in enumerate(f.read_text(encoding="utf-8", errors="replace").splitlines(), 1):
            for m in STREET.finditer(line):
                if NOT_A_STREET.search(m.group(0)):   # "$1,989 State Court" in a budget table is a court, not a street
                    continue
                found = m.group(0).lower().rstrip(".")
                if not any(a and (found in a or a in line.lower()) for a in allowed):
                    print(f"ADDRESS  {name}:{no}  a street address (not in page.yaml public_addresses)")
                    bad += 1
            for n in names:
                if re.search(r"\b" + re.escape(n) + r"\b", line, re.I):
                    print(f"NAME     {name}:{no}  a private person's name from _raw/private_names.txt")
                    bad += 1
    print(f"{trial.name}: {'FAIL' if bad else 'PASS'} ({bad} findings; {len(names)} guarded names; {len(allowed)} public addresses allowed)")
    return 1 if bad else 0


if __name__ == "__main__":
    if len(sys.argv) != 2:
        sys.exit(__doc__)
    sys.exit(main(Path(sys.argv[1])))
