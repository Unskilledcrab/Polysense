# Log: Trial 001 (HB 1009)

Times are UTC on 2026-11-08. They measure **agent** wall-clock time, which isn't comparable to the time
a person would need. Human review time is recorded separately in `review.md`.

| Step | Start → end | What happened | Friction / failures |
|---|---|---|---|
| Setup | before 03:41 (exact time not recorded) | Asked for a repository-specific network environment so Georgia sources could be fetched | **The environments service rejected the request.** Every Georgia host returns proxy 403 (CONNECT denied); re-checked at about 03:42, still blocked. Decision: do every step that doesn't need fetching; never substitute content |
| 0 + 1. Choose, find sources | 03:42 → 03:44 | 8 web searches. Scored HB 1009, HB 1000, HB 1199 and SB 33 and chose HB 1009 (14/15). Recorded 20 sources (11 primary government, 2 research, 7 news or local) with types and purposes. Found **7 discrepancies** (D1–D7) | No reachable authoritative list of signed bills. Search summaries mix dates and outlets. The AJC is paywalled. Every primary record was unreachable, so most primary URLs are still `to-locate` |
| Tools | 03:44 → 03:46 | Built and tested `check_citations.py`, `verify_hashes.py` and `readability.py` on a throwaway fixture; all caught their planted errors | **textstat dropped:** its current release downloads NLTK data at runtime, which failed behind the proxy and is a hidden network dependency. Replaced with a stdlib formula; [dependencies.md](../../docs/architecture/dependencies.md) updated |
| Trial files | 03:46 → 03:48 | Wrote README (choice, rubric, D-list, how to resume), sources.yaml, the verification checklist, the review form and this log; updated the runbook to v0.1 | — |
| 2. Snapshot | — | **Blocked** | Network policy |
| 3–8 | — | Not started | Depends on step 2 |

## Rules learned (fed back into the runbook)

1. **Search summaries are for discovery only, never evidence.** Search results contradicted each other on
   at least three facts (D1, D2, D3). → Runbook step 1 now says so explicitly.
2. **Record discrepancies before retrieving anything.** A D-list turns retrieval into a targeted
   verification checklist. → Added to runbook step 1.
3. **Each bill needs its own host list.** A bill's evidence spans the legislature, the governor's office,
   the Department of Education, a university, news sites and journals, far beyond the first allowlist
   guess. → Runbook step 1 now produces a "hosts needed" list.
4. **Check a library's runtime network behavior before adopting it** (textstat/NLTK). → Noted in
   dependencies.md.
5. **When reports conflict about a local fact** (a district's start date versus the state law's), the
   likely explanation is local choice, not error. The workspace should explain both, not pick one.

## Automation notes (not yet eligible: 0 of 3 runs)

- **Discovery** could later be partly automated: Open States bill sources, a news search API, and the
  legislature's meeting feed. **Choosing source types and judging relevance stays manual** until it has
  been proven 3 times.
- The **D-list** is a good structured artifact for automation to fill and a person to settle.
