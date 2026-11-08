#!/usr/bin/env python3
"""Snapshot a source: save exact bytes + response headers, print sha256 and metadata, extract visible text.

Usage:
    uv run --with httpx --with beautifulsoup4 python3 trials/tools/snapshot.py TRIAL_DIR SOURCE_ID URL

Writes into TRIAL_DIR/_raw/ (git-ignored):
    <id>.<ext>          exact response bytes (ext from content-type: html, pdf, json, txt)
    <id>.headers.txt    status line + response headers
    <id>.txt            visible text (HTML only; scripts/styles/nav removed) for passage extraction
Prints a YAML snippet to paste into sources.yaml (retrieved_at, http_status, content_type, sha256, local_file).

Identifies itself honestly in the User-Agent. Respect each site's terms; one request per source.
"""
from __future__ import annotations

import datetime as dt
import hashlib
import sys
from pathlib import Path

try:
    import httpx
    from bs4 import BeautifulSoup
except ImportError:  # pragma: no cover
    sys.exit("needs httpx + beautifulsoup4: uv run --with httpx --with beautifulsoup4 python3 trials/tools/snapshot.py ...")

UA = "Mozilla/5.0 (compatible; civic-research-trial/0.1; +https://github.com/Unskilledcrab/Polysense)"
EXT = {"text/html": "html", "application/pdf": "pdf", "application/json": "json", "text/plain": "txt"}


def visible_text(html: bytes) -> str:
    soup = BeautifulSoup(html, "html.parser")
    for tag in soup(["script", "style", "noscript", "nav", "footer", "header", "form", "svg", "iframe"]):
        tag.decompose()
    lines = [ln.strip() for ln in soup.get_text("\n").splitlines()]
    return "\n".join(ln for ln in lines if ln)


def main(trial: Path, sid: str, url: str) -> int:
    raw = trial / "_raw"
    raw.mkdir(parents=True, exist_ok=True)
    with httpx.Client(follow_redirects=True, timeout=60, headers={"User-Agent": UA}) as client:
        r = client.get(url)
    when = dt.datetime.now(dt.timezone.utc).strftime("%Y-%m-%dT%H:%M:%SZ")
    ctype = r.headers.get("content-type", "").split(";")[0].strip()
    ext = EXT.get(ctype, "bin")
    body = r.content
    (raw / f"{sid}.{ext}").write_bytes(body)
    hdr = [f"HTTP {r.status_code} {r.reason_phrase}", f"final-url: {r.url}"] + [f"{k}: {v}" for k, v in r.headers.items()]
    (raw / f"{sid}.headers.txt").write_text("\n".join(hdr) + "\n", encoding="utf-8")
    if ext == "html":
        (raw / f"{sid}.txt").write_text(visible_text(body), encoding="utf-8")
    digest = hashlib.sha256(body).hexdigest()
    print(f"    retrieved_at: \"{when}\"")
    print(f"    http_status: {r.status_code}")
    print(f"    final_url: \"{r.url}\"")
    print(f"    content_type: \"{ctype}\"")
    print(f"    sha256: {digest}")
    print(f"    local_file: {sid}.{ext}")
    print(f"    bytes: {len(body)}")
    return 0 if r.status_code == 200 else 1


if __name__ == "__main__":
    if len(sys.argv) != 4:
        sys.exit(__doc__)
    sys.exit(main(Path(sys.argv[1]), sys.argv[2], sys.argv[3]))
