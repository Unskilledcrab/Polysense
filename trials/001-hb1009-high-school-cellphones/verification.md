# Verification: Trial 001 (HB 1009)

**Status: not run.** There is no workspace to check yet (blocked at step 2). The checklist is from
[runbook step 5](../../docs/process/runbooks/bill-workspace.md#step-5-verify-deterministic-checklist--verificationmd).

| Check | Tool | Result |
|---|---|---|
| Every sentence cited or tagged | `trials/tools/check_citations.py` | pending |
| Quotes verbatim in the cited passage | `check_citations.py` | pending |
| Numbers and dates appear in cited passages (or `{computed}`) | `check_citations.py` (warnings) | pending |
| All cited passage IDs exist | `check_citations.py` | pending |
| Source files match the recorded SHA-256 | `trials/tools/verify_hashes.py` | pending |
| Reading grade ≤ 8.5 for the 30-second version and Layers 1–3 | `trials/tools/readability.py` | pending |
| Jargon defined or removed | by hand | pending |
| No loaded words outside quotes | `check_citations.py` + by hand | pending |
| Structural symmetry (same fields, strongest case for each option) | by hand | pending |
| Length budgets | by hand | pending |
| D1–D7 discrepancies settled from primary sources | by hand | pending |

## Where I'm least sure (so far, from discovery only)

1. **The effective date (D1).** If a district page says 2026–27 while the law says 2027–28, the
   workspace must explain the difference: a district may choose to start early. It must not just pick
   one.
2. **Research findings (S13).** The candidate studies came from memory and haven't been checked. Their
   findings may differ from what I recall, so nothing will be written about them until each paper is
   read.
3. **Attributing testimony.** Quotes reported by news outlets must be confirmed against the hearing
   video or the official record before they appear as documented arguments. Until a second reviewer
   exists, named-speaker content won't be published (it may still appear in trial output for review).
