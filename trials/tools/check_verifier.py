#!/usr/bin/env python3
"""Check an independent verifier's report, and (optionally) score it against planted errors.

Usage:
    python3 trials/tools/check_verifier.py REPORT.json PASSAGES.md [MUTATION_KEY.json]

1. Grounding check (always): every "supports"/"partial" verdict must quote an evidence_span that
   appears verbatim (after whitespace/quote normalization) in one of the cited passages. A verifier
   that invents support is caught here; its report fails.
2. Verdict counts, plus every non-"supports" finding listed for triage.
3. Calibration (with a mutation key): a planted error counts as CAUGHT only if the verifier flagged a
   unit or framing issue on that line (±1) AND the flag's text mentions one of the error's keywords.
   This stops a vague flag on a busy line from getting credit for an error it didn't name.

Standard library only. Exit code 1 if grounding fails or (with a key) catch rate < 0.9.
"""
from __future__ import annotations

import json
import re
import sys
import unicodedata
from pathlib import Path

FLAG_VERDICTS = {"partial", "does_not_support", "wrong_span", "questionable", "invalid"}


def norm(text: str) -> str:
    text = unicodedata.normalize("NFKC", text or "")
    text = text.replace("“", '"').replace("”", '"').replace("’", "'").replace("‘", "'")
    return re.sub(r"\s+", " ", text).strip().lower()


def load_passages(path: Path) -> dict[str, str]:
    out, cur = {}, None
    for line in path.read_text(encoding="utf-8").splitlines():
        m = re.match(r"^###\s+(P-\d{3})\b", line)
        if m:
            cur = m.group(1)
            out[cur] = ""
        elif cur and line.startswith(">"):
            out[cur] += " " + line[1:].strip()
    return {k: norm(v) for k, v in out.items()}


def main(report_path: Path, passages_path: Path, key_path: Path | None) -> int:
    rep = json.loads(report_path.read_text(encoding="utf-8"))
    passages = load_passages(passages_path)
    units = rep.get("units", [])
    counts: dict[str, int] = {}
    ungrounded, flags = [], []
    for u in units:
        v = (u.get("verdict") or "").strip().lower()
        counts[v] = counts.get(v, 0) + 1
        ids = re.findall(r"P-\d{3}", u.get("marker", ""))
        if v in {"supports", "partial"} and ids:
            span = norm(u.get("evidence_span", "")).strip(" .,;:")
            if not span or not any(span in passages.get(i, "") for i in ids):
                ungrounded.append(u)
        if v in FLAG_VERDICTS:
            flags.append(u)
    framing = rep.get("framing_issues", [])

    print(f"Report: {report_path.name}  units: {len(units)}  verdicts: {counts}")
    print(f"Grounding: {len(units) - len(ungrounded)}/{len(units)} units OK; {len(ungrounded)} ungrounded evidence spans")
    for u in ungrounded:
        print(f"  UNGROUNDED L{u.get('line')}: {u.get('marker')} span={u.get('evidence_span', '')[:70]!r}")
    print(f"Findings to triage: {len(flags)} unit flags, {len(framing)} framing issues")
    for u in flags:
        print(f"  L{u.get('line')} [{u.get('verdict')}] {u.get('problem', '')[:150]}")
    for f in framing:
        print(f"  L{f.get('line')} [framing/{f.get('severity')}] {f.get('issue', '')[:150]}")

    rc = 1 if ungrounded else 0
    if key_path:
        key = json.loads(key_path.read_text(encoding="utf-8"))
        texts_by_line: dict[int, list[str]] = {}
        for u in flags:
            texts_by_line.setdefault(int(u.get("line") or 0), []).append(
                " ".join(str(u.get(k, "")) for k in ("sentence", "problem", "suggested_fix", "correct_passage", "verdict")))
        for f in framing:
            texts_by_line.setdefault(int(f.get("line") or 0), []).append(" ".join(str(f.get(k, "")) for k in ("text", "issue")))
        caught = 0
        print("Calibration against planted errors:")
        for m in key:
            near = [t for ln in (m["line"] - 1, m["line"], m["line"] + 1) for t in texts_by_line.get(ln, [])]
            hit = any(any(kw.lower() in norm(t) for kw in m["keywords"]) for t in near)
            caught += hit
            print(f"  {'CAUGHT' if hit else 'MISSED'}  {m['id']} {m['type']:<20} L{m['line']}: {m['mutated'][:70]}")
        rate = caught / len(key)
        print(f"Catch rate: {caught}/{len(key)} = {rate:.0%}")
        if rate < 0.9:
            rc = 1
    print("Result:", "FAIL" if rc else "PASS")
    return rc


if __name__ == "__main__":
    if len(sys.argv) not in (3, 4):
        sys.exit(__doc__)
    sys.exit(main(Path(sys.argv[1]), Path(sys.argv[2]), Path(sys.argv[3]) if len(sys.argv) == 4 else None))
