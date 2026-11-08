#!/usr/bin/env python3
"""Verify that every retrieved source in a trial matches the sha256 recorded in sources.yaml.

Usage:
    python3 trials/tools/verify_hashes.py trials/001-*/sources.yaml          # verify
    python3 trials/tools/verify_hashes.py trials/001-*/sources.yaml --print  # show hashes of _raw files

Each source with `status: retrieved` must have `local_file` (relative to the trial's `_raw/` folder)
and `sha256`. Requires PyYAML (MIT): `uv run --with pyyaml python3 trials/tools/verify_hashes.py ...`
"""
from __future__ import annotations

import hashlib
import sys
from pathlib import Path

try:
    import yaml
except ImportError:  # pragma: no cover
    sys.exit("PyYAML is required: uv run --with pyyaml python3 trials/tools/verify_hashes.py ...")


def sha256(path: Path) -> str:
    h = hashlib.sha256()
    with path.open("rb") as fh:
        for block in iter(lambda: fh.read(1 << 16), b""):
            h.update(block)
    return h.hexdigest()


def main(manifest: Path, show: bool) -> int:
    raw = manifest.parent / "_raw"
    data = yaml.safe_load(manifest.read_text(encoding="utf-8"))
    if show:
        for f in sorted(raw.glob("*")) if raw.exists() else []:
            print(f"{sha256(f)}  {f.name}")
        return 0
    bad = 0
    counts: dict[str, int] = {}
    for src in data.get("sources", []):
        status = src.get("status", "unknown")
        counts[status] = counts.get(status, 0) + 1
        if status != "retrieved":
            continue
        entries = src.get("files") or [{"local_file": src.get("local_file"), "sha256": src.get("sha256")}]
        for entry in entries:
            local = raw / str(entry.get("local_file") or "")
            if not entry.get("local_file") or not local.is_file():
                print(f"MISSING  {src['id']}: {local}")
                bad += 1
            elif sha256(local) != entry.get("sha256"):
                print(f"MISMATCH {src['id']}: recorded {entry.get('sha256')} != actual {sha256(local)}")
                bad += 1
            else:
                print(f"OK       {src['id']}: {entry['sha256'][:12]}…  {entry['local_file']}")
    print("status counts:", counts)
    print(f"Result: {'FAIL' if bad else 'PASS'} ({bad} problems)")
    return 1 if bad else 0


if __name__ == "__main__":
    if len(sys.argv) < 2:
        sys.exit(__doc__)
    sys.exit(main(Path(sys.argv[1]), "--print" in sys.argv[2:]))
