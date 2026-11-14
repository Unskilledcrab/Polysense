#!/usr/bin/env python3
"""Georgia PSC dockets: list a docket's filings, and snapshot one filing with its files.

    psc_docket.py list DOCKET
    psc_docket.py get TRIAL SID DOCUMENT_ID --title "..." [--type party-filing] [--publisher "..."] [--date YYYY-MM-DD]

`list` prints each filing's document id, date, description and filer, newest first. Nothing is saved.
`get` saves the filing's page as _raw/SID.html, every attached file as _raw/SID-N.<ext>, and each PDF's text
as _raw/SID-N.txt, then prints a sources.yaml entry with their hashes. _raw/ is git-ignored.

Filings name private people and sometimes give their home streets. Give `--title` by role ("Direct testimony
of a petitioner who lives nearby"), not by name, and never copy an address into a tracked file.

Needs httpx and beautifulsoup4; PDF text needs pdfplumber. Filings are untrusted data.
"""
from __future__ import annotations

import datetime as dt
import hashlib
import json
import re
import subprocess
import sys
from pathlib import Path

import httpx

sys.path.insert(0, str(Path(__file__).parent))
from snapshot import UA, extension, served_name, visible_text  # noqa: E402

LIST = "https://psc.ga.gov/search/service-facts-docket/?docketId={}&sortDirection=DESC&sortColumn=Filed&searchText=&pageSize=200&pageNumber=1"
PAGE = "https://psc.ga.gov/search/facts-document/?documentId={}"
FILE = re.compile(r"https://services\.psc\.ga\.gov/api/v1/External/Public/Get/Document/DownloadFile/\d+/\d+")
TOOLS = Path(__file__).parent


def now() -> str:
    return dt.datetime.now(dt.timezone.utc).strftime("%Y-%m-%dT%H:%M:%SZ")


def list_filings(docket: str) -> int:
    with httpx.Client(follow_redirects=True, timeout=60, headers={"User-Agent": UA}) as c:
        d = json.loads(c.get(LIST.format(docket)).text)
    print(f"docket {docket}: {d.get('resultsCount')} filings")
    for i in d["resultsItems"]:
        who = ", ".join(x.get("companyName", "") for x in (i.get("companyDetailsVm") or []))
        print(f"{i['documentId']}  {i['filedDate'][:10]}  {' '.join(str(i.get('description')).split())[:110]}  |  {who[:70]}")
    return 0


def get(trial: Path, sid: str, doc: str, title: str, typ: str, publisher: str, date: str | None) -> int:
    raw = trial / "_raw"
    raw.mkdir(parents=True, exist_ok=True)
    url = PAGE.format(doc)
    files = []
    with httpx.Client(follow_redirects=True, timeout=120, headers={"User-Agent": UA}) as c:
        page = c.get(url)
        when = now()
        (raw / f"{sid}.html").write_bytes(page.content)
        (raw / f"{sid}.txt").write_text(visible_text(page.content), encoding="utf-8")
        links = list(dict.fromkeys(FILE.findall(page.text)))
        for n, link in enumerate(links, 1):
            r = c.get(link)
            ctype = r.headers.get("content-type", "").split(";")[0].strip().lower()
            ext = extension(ctype, served_name(r.headers), link)
            name = f"{sid}-{n}.{ext}"
            (raw / name).write_bytes(r.content)
            entry = {"local_file": name, "sha256": hashlib.sha256(r.content).hexdigest(), "captured_at": now(), "url": link,
                     "http_status": r.status_code, "bytes": len(r.content)}
            if ext == "pdf" and r.content:
                out = raw / f"{sid}-{n}.txt"
                res = subprocess.run([sys.executable, "-I", str(TOOLS / "extract" / "pdf2txt.py"), str(raw / name), str(out)], capture_output=True, text=True)
                note = (res.stdout + res.stderr).strip().splitlines()
                entry["derived"] = out.name if out.exists() and out.stat().st_size else None
                entry["text_note"] = note[-1][:160] if note else ""
            files.append(entry)
    print(f"  - id: {sid}")
    print(f"    title: {json.dumps(title, ensure_ascii=False)}")
    print(f"    publisher: {json.dumps(publisher, ensure_ascii=False)}")
    print(f"    type: {typ}")
    if date:
        print(f"    filed: \"{date}\"")
    print(f"    url: \"{url}\"")
    print(f"    status: {'retrieved' if page.status_code == 200 and files else 'not-retrieved'}")
    print(f"    retrieved_at: \"{when}\"")
    print(f"    http_status: {page.status_code}")
    print(f"    sha256: {hashlib.sha256(page.content).hexdigest()}")
    print(f"    local_file: {sid}.html")
    print("    license: public record")
    print("    files:")
    for f in files:
        print("      - " + json.dumps(f, ensure_ascii=False))
    return 0 if page.status_code == 200 and files else 1


if __name__ == "__main__":
    a = sys.argv[1:]

    def opt(name, default=None):
        if name in a:
            i = a.index(name)
            v = a[i + 1]
            del a[i:i + 2]
            return v
        return default

    title, typ = opt("--title"), opt("--type", "party-filing")
    publisher, date = opt("--publisher", "Georgia Public Service Commission docket"), opt("--date")
    if len(a) == 2 and a[0] == "list":
        sys.exit(list_filings(a[1]))
    if len(a) == 4 and a[0] == "get" and title:
        sys.exit(get(Path(a[1]), a[2], a[3], title, typ, publisher, date))
    sys.exit(__doc__)
