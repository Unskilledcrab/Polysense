#!/usr/bin/env python3
"""Render the UX prototype page from a trial's verified files.

Usage:
    uv run --with pyyaml python3 trials/tools/render_prototype.py TRIAL_DIR TEMPLATE.html OUT.html

Reads TRIAL_DIR/{workspace.md, home.md, passages.md, sources.yaml, verification/summary.json}
and TRIAL_DIR/_raw/hb1009-247117.markup.txt (from pdf_markup.py), embeds them as JSON in the
template, and writes a single self-contained HTML page. Nothing is typed by hand into the page:
every sentence, chip and source comes from the checked trial files.
"""
from __future__ import annotations

import datetime as dt
import hashlib
import json
import re
import sys
from pathlib import Path

try:
    import yaml
except ImportError:  # pragma: no cover
    sys.exit("needs PyYAML: uv run --with pyyaml python3 trials/tools/render_prototype.py ...")

MARKER = re.compile(r"\[(P-\d{3}(?:\s*,\s*P-\d{3})*)\]|\{(analysis|editorial)(?::\s*([^}]*))?\}|\{computed:\s*([^}]*)\}")
SECTION_IDS = {"30-second": "summary", "layer 1": "overview", "layer 2": "decision", "layer 3": "options",
               "layer 4": "arguments", "layer 5": "evidence"}
# Chip labels: one or two words, so a chip fits at the end of a line on a phone. The drawer shows full details.
SHORT = {"S01": "Law", "S02": "Bill record", "S03": "Amendment", "S09": "Governor",
         "S10c": "Fiscal notes", "S12": "Survey", "S13a": "Florida study", "S13b": "England study",
         "S16": "News, Mar 12", "S16b": "News, Mar 17", "S19": "Cairo High"}
TYPE_LABEL = {"primary-government": "Primary · government", "secondary-research": "Research", "news": "News",
              "local-government": "Local government", "advocacy": "Advocacy"}
BILL_LINES = [("p2", 22), ("p2", 23), ("p2", 28), ("p3", 42), ("p3", 43), ("p3", 44), ("p3", 45), ("p3", 46), ("p3", 47), ("p3", 48)]


def split_units(text: str) -> list[dict]:
    out, pos = [], 0
    for m in MARKER.finditer(text):
        chunk = text[pos:m.start()].strip()
        pos = m.end()
        if m.group(1):
            out.append({"t": chunk, "cite": [p.strip() for p in m.group(1).split(",")]})
        elif m.group(2):
            out.append({"t": chunk, "tag": m.group(2), "note": (m.group(3) or "").strip()})
        else:
            out.append({"t": chunk, "tag": "computed", "note": (m.group(4) or "").strip()})
    tail = text[pos:].strip()
    if tail:
        out.append({"t": tail})
    return [u for u in out if u["t"] or u.get("cite") or u.get("tag")]


def parse_block(line: str) -> dict:
    kind = "para"
    m = re.match(r"^\s*(?:(-)|(\d+)\.)\s+(.*)$", line)
    if m:
        kind = "bullet" if m.group(1) else "num"
        line = m.group(3)
    label = None
    lm = re.match(r"^\*\*(.+?):\*\*\s*(.*)$", line)
    if lm:
        label, line = lm.group(1), lm.group(2)
    return {"kind": kind, "label": label, "units": split_units(line)}


def parse_card_meta(line: str) -> dict:
    parts = [p.strip() for p in line.replace("<!-- nocheck -->", "").split("·")]
    meta = {"kind": "Anticipated" if parts and parts[0].startswith("Anticipated") else "Documented"}
    for p in parts:
        if p.lower().startswith("side:"):
            meta["side"] = p.split(":", 1)[1].strip()
        if p.lower().startswith("claim type:"):
            meta["claimType"] = p.split(":", 1)[1].strip()
        if p.lower().startswith("evidence:"):
            meta["evidence"] = p.split(":", 1)[1].strip()
    return meta


def parse_workspace(path: Path) -> tuple[dict, dict]:
    sections, cur, sub, why = [], None, None, {}
    for raw in path.read_text(encoding="utf-8").splitlines():
        line = raw.rstrip()
        if not line or line.startswith("# "):
            continue
        if line.startswith("## "):
            title = line[3:].strip()
            sid = next((v for k, v in SECTION_IDS.items() if title.lower().startswith(k)), title.lower())
            cur, sub = {"id": sid, "title": title, "blocks": [], "subs": []}, None
            sections.append(cur)
            continue
        if cur is None:
            continue
        if line.startswith("### "):
            sub = {"title": line[4:].strip(), "blocks": [], "cardMeta": None}
            cur["subs"].append(sub)
            continue
        if line.startswith("|"):
            cells = [c.strip() for c in line.strip("|").split("|")]
            ids = re.findall(r"S\d+[a-z]?", cells[0]) if cells else []
            if ids and len(cells) >= 5:
                for sid in ids:
                    why[sid] = cells[4].replace("<!-- nocheck -->", "").strip(" |")
            continue
        if line.endswith("<!-- nocheck -->"):
            if sub is not None and cur["id"] == "arguments":
                sub["cardMeta"] = parse_card_meta(line)
            continue
        (sub["blocks"] if sub is not None else cur["blocks"]).append(parse_block(line))
    return {"sections": sections}, why


def parse_home(path: Path) -> dict:
    secs, cur = [], None
    for line in path.read_text(encoding="utf-8").splitlines():
        if line.startswith("## "):
            title = line[3:].strip()
            cur = {"id": re.sub(r"[^a-z]+", "-", title.lower()).strip("-"), "title": title, "items": []}
            secs.append(cur)
        elif cur is not None and line.startswith("- "):
            b = parse_block(line)
            label = b.get("label") or ""
            power, when = None, label
            if "·" in label:
                first, rest = [x.strip() for x in label.split("·", 1)]
                if first in ("Decide", "Input", "Observe"):
                    power, when = first, rest
            cur["items"].append({"power": power, "when": when, "units": b["units"]})
    return {"sections": secs}


def parse_passages(path: Path) -> dict:
    out, cur = {}, None
    for line in path.read_text(encoding="utf-8").splitlines():
        m = re.match(r"^###\s+(P-\d{3})\s+·\s+(\S+)\s+·\s+(.*)$", line)
        if m:
            cur = m.group(1)
            out[cur] = {"source": m.group(2), "locator": m.group(3).strip(), "text": "", "note": None}
        elif cur and line.startswith("> "):
            out[cur]["text"] += line[2:].strip()
        elif cur and line.startswith("*Note: "):
            out[cur]["note"] = line[len("*Note: "):].rstrip("*").strip()
    return out


def bill_changes(markup_path: Path) -> list[dict]:
    lines = {}
    for line in markup_path.read_text(encoding="utf-8").splitlines():
        m = re.match(r"^(p\d+):\s+(\d+)\s+(.*)$", line)
        if m:
            lines[(m.group(1), int(m.group(2)))] = m.group(3)
    out = []
    for key in BILL_LINES:
        text = lines.get(key, "")
        segs = []
        for tok in text.split(" "):
            k, t = "txt", tok
            if tok.startswith("[-") and tok.endswith("-]"):
                k, t = "del", tok[2:-2]
            elif tok.startswith("{+") and tok.endswith("+}"):
                k, t = "ins", tok[2:-2]
            if segs and segs[-1]["k"] == k:
                segs[-1]["t"] += " " + t
            else:
                if segs:
                    segs[-1]["t"] += " " if segs[-1]["k"] == "txt" else ""
                    if k == "txt" or segs[-1]["k"] != "txt":
                        t = (" " if segs[-1]["k"] != "txt" else "") + t
                segs.append({"k": k, "t": t})
        # Display fix (same as passage P-008): the comma after "January 1" is struck with ", 2026,".
        for i, s in enumerate(segs):
            if s["k"] == "txt" and s["t"].endswith("January 1, ") and i + 1 < len(segs) and segs[i + 1]["k"] == "del":
                s["t"] = s["t"][:-2]
                segs[i + 1]["t"] = ", " + segs[i + 1]["t"] + " "
        out.append({"n": key[1], "segs": segs})
    return out


def main(trial: Path, template: Path, out_path: Path) -> int:
    workspace, why = parse_workspace(trial / "workspace.md")
    passages = parse_passages(trial / "passages.md")
    srcs = yaml.safe_load((trial / "sources.yaml").read_text(encoding="utf-8"))["sources"]
    used = sorted({p["source"] for p in passages.values()}, key=lambda s: [x["id"] for x in srcs].index(s))
    sources = {}
    for s in srcs:
        if s["id"] not in used:
            continue
        sha = s.get("sha256") or ((s.get("files") or [{}])[0].get("sha256"))
        sources[s["id"]] = {"title": s.get("title"), "publisher": s.get("publisher"), "type": s.get("type"),
                            "typeLabel": TYPE_LABEL.get(s.get("type"), s.get("type")), "url": s.get("url"),
                            "retrieved_at": s.get("retrieved_at"), "sha256": sha, "method": s.get("method"),
                            "short": SHORT.get(s["id"], s["id"]), "why": why.get(s["id"])}
    summary_path = trial / "verification" / "summary.json"
    verification = json.loads(summary_path.read_text(encoding="utf-8")) if summary_path.exists() else {
        "intro": "Verification has not run yet.", "stats": [], "sections": [], "verified": False}
    rev = hashlib.sha256((trial / "workspace.md").read_bytes()).hexdigest()
    rev_no = re.search(r"revision (\d+)", (trial / "workspace.md").read_text(encoding="utf-8"))
    rev_label = f"revision {rev_no.group(1)}" if rev_no else "revision ?"
    data = {
        "meta": {
            "verified": bool(verification.get("verified")),
            "statusText": verification.get("statusText", "Verification pending"),
            "disclosure": ("Drafted with AI help from the archived sources below. Every sentence links to its source or is "
                           "labeled as our analysis. Checked by automated tests and independent AI reviewers. In this trial, "
                           "no person reviewed it."),
            "footer": (f"Prototype · Trial 001 · {rev_label} (sha256 {rev[:12]}) · built {dt.date.today().isoformat()} · "
                       "Independent project, not a government website."),
            "revision": rev,
        },
        "home": parse_home(trial / "home.md"),
        "workspace": workspace,
        "passages": passages,
        "sources": sources,
        "sourceOrder": used,
        "billChanges": bill_changes(trial / "_raw" / "hb1009-247117.markup.txt"),
        "verification": verification,
    }
    blob = json.dumps(data, ensure_ascii=False).replace("</", "<\\/")
    html = template.read_text(encoding="utf-8").replace("/*__DATA__*/", blob)
    out_path.parent.mkdir(parents=True, exist_ok=True)
    out_path.write_text(html, encoding="utf-8")
    print(f"wrote {out_path} ({len(html) // 1024} KB): {len(passages)} passages, {len(sources)} sources, revision {rev[:12]}")
    return 0


if __name__ == "__main__":
    if len(sys.argv) != 4:
        sys.exit(__doc__)
    sys.exit(main(Path(sys.argv[1]), Path(sys.argv[2]), Path(sys.argv[3])))
