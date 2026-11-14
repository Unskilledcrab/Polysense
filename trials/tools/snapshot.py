#!/usr/bin/env python3
"""Snapshot a source: save exact bytes + response headers, print sha256 and metadata, extract visible text.

Usage:
    uv run --with httpx --with beautifulsoup4 python3 trials/tools/snapshot.py TRIAL_DIR SOURCE_ID URL

Writes into TRIAL_DIR/_raw/ (git-ignored):
    <id>.<ext>          exact response bytes (ext from the content type, else from the served file name
                        or URL: html, pdf, json, txt, csv, xml, docx, xlsx, pptx, zip, doc, xls)
    <id>.headers.txt    status line + response headers
    <id>.txt            visible text (HTML only; scripts/styles/nav removed) for passage extraction
Prints a YAML snippet to paste into sources.yaml (retrieved_at, http_status, content_type, sha256, local_file).

Identifies itself honestly in the User-Agent. Respect each site's terms; one request per source.
Exits 1 on a non-200 status or an empty body (both are still saved as evidence). For Word, Excel,
PowerPoint, zip and PDF files it prints which trials/tools/extract/ tool makes the text.
"""
from __future__ import annotations

import datetime as dt
import hashlib
import re
import sys
from pathlib import Path, PurePosixPath
from urllib.parse import urlparse

NEEDS = "needs httpx + beautifulsoup4: uv run --with httpx --with beautifulsoup4 python3 trials/tools/snapshot.py ..."
try:
    from bs4 import BeautifulSoup
except ImportError:  # pragma: no cover
    sys.exit(NEEDS)

UA = "Mozilla/5.0 (compatible; civic-research-trial/0.1; +https://github.com/Unskilledcrab/Polysense)"
EXT = {"text/html": "html", "application/pdf": "pdf", "application/json": "json", "text/plain": "txt",
       "text/csv": "csv", "application/xml": "xml", "text/xml": "xml", "application/zip": "zip",
       "application/x-zip-compressed": "zip", "application/msword": "doc", "application/vnd.ms-excel": "xls",
       "application/vnd.openxmlformats-officedocument.wordprocessingml.document": "docx",
       "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet": "xlsx",
       "application/vnd.openxmlformats-officedocument.presentationml.presentation": "pptx"}
EXTRACT = {"pdf": "extract/pdf2txt.py", "docx": "extract/docx2txt.py", "xlsx": "extract/xlsx2txt.py",
           "pptx": "extract/pptx2txt.py", "zip": "extract/safe_unzip.py"}


def served_name(headers) -> str:
    """The file name in Content-Disposition, if the server sent one."""
    m = re.search(r'filename\*?=(?:UTF-8\'\')?"?([^";]+)"?', headers.get("content-disposition", ""), re.I)
    return m.group(1).strip() if m else ""


def extension(ctype: str, name: str, url: str) -> str:
    """From the content type; for generic types (octet-stream), from the served name or the URL path."""
    if ctype in EXT:
        return EXT[ctype]
    for candidate in (name, PurePosixPath(urlparse(url).path).name):
        suffix = PurePosixPath(candidate.lower()).suffix.lstrip(".")
        if suffix in EXT.values():
            return suffix
    return "bin"


def visible_text(html: bytes) -> str:
    soup = BeautifulSoup(html, "html.parser")
    for tag in soup(["script", "style", "noscript", "nav", "footer", "header", "form", "svg", "iframe"]):
        tag.decompose()
    lines = [ln.strip() for ln in soup.get_text("\n").splitlines()]
    return "\n".join(ln for ln in lines if ln)


def main(trial: Path, sid: str, url: str) -> int:
    try:   # imported here so desk.py can use visible_text() without a network library
        import httpx
    except ImportError:  # pragma: no cover
        sys.exit(NEEDS)
    raw = trial / "_raw"
    raw.mkdir(parents=True, exist_ok=True)
    with httpx.Client(follow_redirects=True, timeout=60, headers={"User-Agent": UA}) as client:
        r = client.get(url)
    when = dt.datetime.now(dt.timezone.utc).strftime("%Y-%m-%dT%H:%M:%SZ")
    ctype = r.headers.get("content-type", "").split(";")[0].strip().lower()
    name = served_name(r.headers)
    ext = extension(ctype, name, str(r.url))
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
    if name:
        print(f"    # served file name: {name}")
    if ext in EXTRACT and body:
        print(f"    # text: python3 -I trials/tools/{EXTRACT[ext]} (see trials/tools/README.md)")
    if not body:
        print(f"EMPTY BODY: {sid} returned no bytes; record it as not retrieved", file=sys.stderr)
        return 1
    return 0 if r.status_code == 200 else 1


if __name__ == "__main__":
    if len(sys.argv) != 4:
        sys.exit(__doc__)
    sys.exit(main(Path(sys.argv[1]), sys.argv[2], sys.argv[3]))
