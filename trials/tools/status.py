#!/usr/bin/env python3
"""Write each trial's status from the ledger and the gate, so no agent has to write (or re-read) status prose.

    status.py TRIAL_DIR [TRIAL_DIR ...]

For each trial it runs the gate and the ledger, then writes:
  status.json                 revision, gate results, ledger counts, pending and flagged sentences
  verification/summary.json   only the fields the page shows as status: verified, statusText and the sentence count.
                              A page is "verified" only when the gate passes and no sentence is pending or flagged.

Stdlib only (the hash check and the lints need PyYAML and are skipped if it is missing).
"""
from __future__ import annotations

import datetime
import json
import re
import subprocess
import sys
from pathlib import Path

sys.path.insert(0, str(Path(__file__).parent))
from ledger import FLAG, current, load_ledger, write  # noqa: E402

TOOLS = Path(__file__).parent


def run(*args: str) -> tuple[bool, str]:
    r = subprocess.run([sys.executable, *args], capture_output=True, text=True)
    last = (r.stdout.strip().splitlines() or [""])[-1]
    return r.returncode == 0, last


def status(trial: Path) -> dict:
    write(trial, current(trial), load_ledger(trial))
    rows = [json.loads(x) for x in (trial / "claims.jsonl").read_text(encoding="utf-8").splitlines() if x.strip()]
    rows = [r for r in rows if r.get("line")]
    pending = [r for r in rows if not r.get("verdict")]
    flagged = [r for r in rows if r.get("verdict") in FLAG]
    gate = {}
    for name in ("workspace.md", "home.md"):
        ok, last = run(str(TOOLS / "check_citations.py"), str(trial / name), str(trial / "passages.md"))
        gate[f"citations:{name}"] = {"pass": ok, "result": last}
    ok, _ = run(str(TOOLS / "readability.py"), str(trial / "workspace.md"))
    out = subprocess.run([sys.executable, str(TOOLS / "readability.py"), str(trial / "workspace.md")],
                         capture_output=True, text=True).stdout
    over = re.search(r"Sections over target: (\d+)", out)
    gate["readability"] = {"pass": bool(over) and over.group(1) == "0" and "WARN" not in out}
    try:
        import yaml  # noqa: F401
        ok, last = run(str(TOOLS / "verify_hashes.py"), str(trial / "sources.yaml"))
        gate["hashes"] = {"pass": ok, "result": last}
        _, last = run(str(TOOLS / "lint_balance.py"), "lint", str(trial))
        gate["balance_prompts"] = int(re.search(r"(\d+) CHECK", last).group(1)) if re.search(r"(\d+) CHECK", last) else None
    except ImportError:
        pass
    head = (trial / "workspace.md").read_text(encoding="utf-8").splitlines()[2]
    rev = re.search(r"revision (\d+(?:\.\d+)?)", head)
    gate_ok = all(v["pass"] for v in gate.values() if isinstance(v, dict))
    brief = lambda r: {"file": r["file"], "line": r["line"], "sentence": r["sentence"][:140],   # noqa: E731
                       "verdict": r.get("verdict"), "problem": r.get("problem", "")}
    st = {"trial": trial.name, "revision": rev.group(1) if rev else None, "generated": datetime.date.today().isoformat(),
          "gate": gate, "units": len(rows), "checked_ok": len(rows) - len(pending) - len(flagged),
          "flagged": [brief(r) for r in flagged], "pending": [brief(r) for r in pending],
          "verified": gate_ok and not pending and not flagged}
    (trial / "status.json").write_text(json.dumps(st, indent=1, ensure_ascii=False) + "\n", encoding="utf-8")

    sp = trial / "verification" / "summary.json"
    if sp.exists():
        d = json.loads(sp.read_text(encoding="utf-8"))
        d["verified"] = st["verified"]
        waiting = len(pending) + len(flagged)
        d["statusText"] = "Passed our checks" if st["verified"] else (
            f"{waiting} sentence{'s' if waiting != 1 else ''} waiting for a check" if gate_ok else "Automatic checks failing")
        if d.get("stats"):
            d["stats"][0]["value"] = str(len(rows))
        sp.write_text(json.dumps(d, indent=1, ensure_ascii=False) + "\n", encoding="utf-8")
    return st


if __name__ == "__main__":
    if len(sys.argv) < 2:
        sys.exit(__doc__)
    for t in sys.argv[1:]:
        s = status(Path(t))
        print(f"{s['trial']}: revision {s['revision']}; {s['checked_ok']}/{s['units']} sentences checked; "
              f"{len(s['pending'])} pending; {len(s['flagged'])} flagged; gate {'PASS' if all(v['pass'] for v in s['gate'].values() if isinstance(v, dict)) else 'FAIL'}; "
              f"verified: {s['verified']}")
