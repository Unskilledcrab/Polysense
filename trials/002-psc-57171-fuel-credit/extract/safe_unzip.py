#!/usr/bin/env python3
"""List or safely extract an untrusted zip: flat file names only, no paths, size-capped.

Usage: python3 -I safe_unzip.py ZIP [OUTDIR]
Without OUTDIR, only lists members. With OUTDIR (must not exist), extracts each regular file
under its base name. Refuses absolute paths, '..', symlinks, duplicates and members over 200 MB.
"""
from __future__ import annotations

import os
import stat
import sys
import zipfile

MAX = 200 * 1024 * 1024


def main(zpath: str, outdir: str | None) -> int:
    with zipfile.ZipFile(zpath) as z:
        infos = z.infolist()
        for i in infos:
            mode = (i.external_attr >> 16) & 0xFFFF
            print(f"{i.file_size:>10}  {i.filename}{'  [symlink]' if stat.S_ISLNK(mode) else ''}")
        if not outdir:
            return 0
        os.makedirs(outdir)  # fails if it exists: always a new, empty directory
        seen = set()
        for i in infos:
            if i.is_dir():
                continue
            mode = (i.external_attr >> 16) & 0xFFFF
            name = i.filename.replace("\\", "/")
            if stat.S_ISLNK(mode) or name.startswith("/") or ".." in name.split("/"):
                print(f"SKIP unsafe member: {i.filename}")
                continue
            base = os.path.basename(name)
            if not base or base in seen or i.file_size > MAX:
                print(f"SKIP member: {i.filename}")
                continue
            seen.add(base)
            with z.open(i) as src, open(os.path.join(outdir, base), "wb") as dst:
                dst.write(src.read(MAX + 1)[:MAX])
        print(f"extracted {len(seen)} files to {outdir}")
    return 0


if __name__ == "__main__":
    if len(sys.argv) not in (2, 3):
        sys.exit(__doc__)
    sys.exit(main(sys.argv[1], sys.argv[2] if len(sys.argv) == 3 else None))
