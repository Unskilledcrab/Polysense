#!/usr/bin/env python3
"""Claims ledger: one verdict per sentence, kept until the sentence or its cited passages change.

A full source check used to re-read the whole page and every passage (200-340k tokens) on every round, even when
three sentences had changed. The ledger keys each verdict to a hash of the sentence, its marker and the text of the
passages it cites. A sentence needs checking again only when that hash changes.

    ledger.py sync  TRIAL_DIR                      rebuild claims.jsonl for the current page; keep verdicts whose key matches
    ledger.py seed  TRIAL_DIR REPORT=PAGE [...]    import verdicts from past source-check reports. PAGE is the page
                                                   version the report checked: a file path, or a git ref such as abc123:path
    ledger.py pending TRIAL_DIR                    list units with no current verdict (exit 1 if any)
    ledger.py status  TRIAL_DIR                    counts

Files covered: workspace.md and home.md. Stdlib only.
"""
from __future__ import annotations

import hashlib
import json
import re
import subprocess
import sys
from pathlib import Path

sys.path.insert(0, str(Path(__file__).parent))
from check_citations import MARKER, checkable_lines, load_passages, norm  # noqa: E402

OK = {"supports", "valid"}
FLAG = {"partial", "does_not_support", "wrong_span", "questionable", "invalid"}
FILES = ("workspace.md", "home.md")


def units(path: Path):
    """Yield one dict per sentence-and-marker unit, the same split check_citations.py uses."""
    for no, line in checkable_lines(path):
        pos = 0
        for m in MARKER.finditer(line):
            chunk = line[pos:m.start()].strip()
            pos = m.end()
            if not re.search(r"[A-Za-z]", chunk):
                continue
            marker = m.group(0)
            kind = "cited" if m.group(1) else (m.group(2) or "computed")
            yield {"line": no, "sentence": chunk, "marker": marker, "kind": kind,
                   "passages": sorted(set(re.findall(r"P-\d{3}", marker)))}


def visual_units(trial: Path):
    """The cited or tagged notes under page.yaml `visuals:`, each prefixed with the figure it captions, so the pictures
    on the page are checked like sentences. Needs PyYAML; without it the notes are skipped (and stay pending)."""
    try:
        import yaml
    except ImportError:
        return
    v = (yaml.safe_load((trial / "page.yaml").read_text(encoding="utf-8")) or {}).get("visuals") or {}
    notes = [(f"{n['value']}: ", n["note"]) for n in v.get("numbers", [])]
    if v.get("split"):
        notes.append((v["split"]["title"] + ": ", v["split"]["note"]))
    for part in (v.get("bill") or {}).get("parts", []):
        notes.append((f"{v['bill']['title']}. {part['name']} ({part['dir']}): ", part["note"]))
    if (v.get("bill") or {}).get("note"):
        notes.append((v["bill"]["title"] + ": ", v["bill"]["note"]))
    if v.get("seats"):
        notes.append((f"{v['seats']['total']} seats, {v['seats'].get('vacant', 0)} vacant: ", v["seats"]["note"]))
    for i, (lead, note) in enumerate(notes, 1):
        pos = 0
        for m in MARKER.finditer(note):
            chunk = note[pos:m.start()].strip()
            pos = m.end()
            kind = "cited" if m.group(1) else (m.group(2) or "computed")
            yield {"line": i, "sentence": lead + chunk, "marker": m.group(0), "kind": kind,
                   "passages": sorted(set(re.findall(r"P-\d{3}", m.group(0))))}


def key_of(u: dict, passages: dict[str, str]) -> str:
    body = "\n".join([norm(u["sentence"]), norm(u["marker"])] + [passages.get(p, "?") for p in u["passages"]])
    return hashlib.sha256(body.encode("utf-8")).hexdigest()[:16]


def load_ledger(trial: Path) -> dict[str, dict]:
    f = trial / "claims.jsonl"
    if not f.exists():
        return {}
    rows = [json.loads(x) for x in f.read_text(encoding="utf-8").splitlines() if x.strip()]
    return {r["key"]: r for r in rows}


def current(trial: Path) -> list[dict]:
    passages = load_passages(trial / "passages.md")
    out = []
    for name in FILES:
        if (trial / name).exists():
            for u in units(trial / name):
                u["file"] = name
                u["key"] = key_of(u, passages)
                out.append(u)
    for u in visual_units(trial):
        u["file"] = "page.yaml"
        u["key"] = key_of(u, passages)
        out.append(u)
    return out


def write(trial: Path, rows: list[dict], store: dict[str, dict]) -> None:
    """Current units first (in page order), then stored verdicts for text that is no longer on the page (kept so a
    reverted sentence doesn't need a new check)."""
    seen, lines = set(), []
    for u in rows:
        old = store.get(u["key"], {})
        for f in ("verdict", "span", "by", "source", "problem"):
            if old.get(f):
                u[f] = old[f]
        seen.add(u["key"])
        lines.append(json.dumps(u, ensure_ascii=False))
    for k, r in store.items():
        if k not in seen and r.get("verdict"):
            r = dict(r, file=r.get("file", "workspace.md"), line=0)   # line 0: not on the current page
            lines.append(json.dumps(r, ensure_ascii=False))
    (trial / "claims.jsonl").write_text("\n".join(lines) + "\n", encoding="utf-8")


def read_ref(ref: str) -> str:
    if ":" in ref and not Path(ref).exists():
        return subprocess.run(["git", "show", ref], capture_output=True, text=True, check=True).stdout
    return Path(ref).read_text(encoding="utf-8")


def seed(trial: Path, pairs: list[str]) -> None:
    passages, store = load_passages(trial / "passages.md"), load_ledger(trial)
    tmp = trial / ".ledger-seed.md"
    added = missed = 0
    for pair in pairs:
        report, ref = pair.split("=", 1)
        rep = json.loads(Path(report).read_text(encoding="utf-8"))
        tmp.write_text(read_ref(ref), encoding="utf-8")
        by_line: dict[int, list[dict]] = {}
        for u in units(tmp):
            by_line.setdefault(u["line"], []).append(u)
        for r in rep.get("units", []):
            if r.get("file") in ("home.md", "page.yaml"):
                continue
            verdict = (r.get("verdict") or "").strip().lower()
            cands = by_line.get(int(r.get("line") or 0), [])
            want = norm(r.get("marker", ""))
            same = [u for u in cands if norm(u["marker"])[:40] == want[:40]] or cands
            head = re.sub(r"[*…]|\.\.\.", "", norm(r.get("sentence", "")))[:28]
            hit = [u for u in same if head and head in norm(u["sentence"]).replace("*", "")] or (same if len(same) == 1 else [])
            if not hit or verdict not in OK | FLAG:
                missed += 1
                continue
            u = hit[0]
            u.update(key=key_of(u, passages), file="workspace.md", verdict=verdict, span=r.get("evidence_span", ""),
                     by="top-tier source check", source=Path(report).parent.name, problem=r.get("problem", "")[:300])
            store[u["key"]] = u
            added += 1
    tmp.unlink(missing_ok=True)
    write(trial, current(trial), store)
    print(f"seeded {added} verdicts ({missed} report units not matched)")


def report(trial: Path, list_pending: bool) -> int:
    rows = [json.loads(x) for x in (trial / "claims.jsonl").read_text(encoding="utf-8").splitlines() if x.strip()]
    rows = [r for r in rows if r.get("line")]
    pend = [r for r in rows if not r.get("verdict")]
    flag = [r for r in rows if r.get("verdict") in FLAG]
    print(f"{trial.name}: {len(rows)} units; {len(rows) - len(pend) - len(flag)} checked OK; "
          f"{len(flag)} flagged; {len(pend)} pending")
    if list_pending:
        for r in pend:
            print(f"  PENDING {r['file']} L{r['line']} {r['marker'][:28]}: {r['sentence'][:90]}")
    for r in flag:
        print(f"  FLAG    {r['file']} L{r['line']} [{r['verdict']}] {r.get('problem', '')[:110]}")
    return 1 if pend else 0


if __name__ == "__main__":
    if len(sys.argv) < 3 or sys.argv[1] not in ("sync", "seed", "pending", "status"):
        sys.exit(__doc__)
    cmd, trial = sys.argv[1], Path(sys.argv[2])
    if cmd == "seed":
        seed(trial, sys.argv[3:])
    else:
        write(trial, current(trial), load_ledger(trial))
    sys.exit(report(trial, cmd == "pending") if cmd in ("pending", "status", "sync") else 0)
