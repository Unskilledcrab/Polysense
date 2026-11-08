# Log: Trial 001 (HB 1009)

Times are UTC on 2026-11-08. They measure **agent** wall-clock time, which isn't comparable to the time
a person would need. Human review time is recorded separately in `review.md`.

| Step | Start → end | What happened | Friction / failures |
|---|---|---|---|
| Setup | before 03:41 (exact time not recorded) | Asked for a repository-specific network environment so Georgia sources could be fetched | **The environments service rejected the request.** Every Georgia host returns proxy 403 (CONNECT denied); re-checked at about 03:42, still blocked. Decision: do every step that doesn't need fetching; never substitute content |
| 0 + 1. Choose, find sources | 03:42 → 03:44 | 8 web searches. Scored HB 1009, HB 1000, HB 1199 and SB 33 and chose HB 1009 (14/15). Recorded 20 sources (11 primary government, 2 research, 7 news or local) with types and purposes. Found **7 discrepancies** (D1–D7) | No reachable authoritative list of signed bills. Search summaries mix dates and outlets. The AJC is paywalled. Every primary record was unreachable, so most primary URLs are still `to-locate` |
| Tools | 03:44 → 03:46 | Built and tested `check_citations.py`, `verify_hashes.py` and `readability.py` on a throwaway fixture; all caught their planted errors | **textstat dropped:** its current release downloads NLTK data at runtime, which failed behind the proxy and is a hidden network dependency. Replaced with a stdlib formula; [dependencies.md](../../docs/architecture/dependencies.md) updated |
| Trial files | 03:46 → 03:48 | Wrote README (choice, rubric, D-list, how to resume), sources.yaml, the verification checklist, the review form and this log; updated the runbook to v0.1 | — |
| 2. Snapshot (blocked) | 03:48 | **Blocked** by network policy; trial paused and committed | — |
| *Owner reply* | ~03:58 | Owner: no human in the loop, so build our own fact- and bias-checking; public repo is fine; turn on vulnerability reporting; the owner will give UX feedback. The environment now has unrestricted network access | — |
| 2. Snapshot | 04:00 → 04:07 | Found HB 1009 (legis.ga.gov id 72304) with the site's own search form in headless Chromium; saved the bill record JSON the page loads; downloaded 3 version PDFs; snapshotted the Governor's release, OPB fiscal-note lists, Emory survey, NBER and SMART Schools abstracts, 2 Atlanta News First articles, Cairo High School page. **14 retrieved, 15 hashes verified** | legis.ga.gov's API needs a token derived from a key in the site's JavaScript; we used the browser instead of copying it. **Bill PDF extraction merged struck and new words** ("restricted prohibited", "eight twelve"); built `pdf_markup.py` and checked it against rendered page images. One school page returned a bot check; captured with a browser. WABE, LegiScan, Georgia Recorder and The Lancet refuse scripted requests |
| D-list settled | 04:02 → 04:10 | **D1** effective date: the law says high schools "no later than July 1, 2027"; Cairo High School chose to start in 2026-27 (both true). **D2** signed 2026-05-05 (Act 395). **D3** House Education; Senate Children and Families. **D4** devices listed; e-readers removed from the list. **D5** IEP/504/medical-plan exception confirmed. **D6** House 145-20 (Feb 24); Senate amendment failed 13-38, passage 52-0 (Mar 23). **D7** no fiscal note on OPB's 2025-26 list (absence not proof) | Search summary had claimed the SMART Schools study found no difference in attainment; the abstract doesn't say that, so we don't |
| 3. Passages | 04:07 → 04:15 | Hand-chose 55 passages in `passages.spec.yaml`; `build_passages.py` copied exact spans. 4 documented fixes: struck comma, page footer, link-boundary spaces, and the minor's name withheld | Hand-copying would have risked typos; spec + builder made passages reproducible |
| 4. Draft | 04:13 → 04:16 | `workspace.md`: 30-second version + Layers 1-5; 92 sentence units (67 cited, 10 analysis, 14 editorial, 1 computed). Reading grade 5.4-7.0 for the 30-second version and Layers 1-3 | American quote punctuation ("protected.") failed the verbatim check; the rule now allows one closing period/comma inside quotes, and words must still match |
| 5. Deterministic gate | 04:15 → 04:16 | `check_citations.py` PASS (0 errors, 0 warnings) after the punctuation and number-word fixes; regression fixture still fails as expected | — |
| 6. Verification setup | 04:16 → 04:17 | Made a calibration copy with **13 hidden planted errors**; the code-only gate caught **1 of 13** (as a warning). Answer key moved outside the repository. Launched 5 fresh agents: 2 blind source checks (real + calibration copy), supporter's advocate, critic's advocate, standards editor | — |
| Prototype | 04:17 → 04:26 | Built `render_prototype.py` + template (home, 5 layer tabs, evidence drawer, bill markup view, "How we checked" page). Smoke test: 7 views × 2 widths, 0 script errors, 0 horizontal overflow | — |
| Verification round 1 | 04:17 → 04:36 | Reports: supporter 3/5 (04:27); critic 3/5 (04:29); calibration **13/13 caught**, 92/92 spans grounded (04:30); editor neutrality 3/5, clarity 4/5 (04:32); blind source check of the real page 58 supports, 9 partial, 0 unsupported, 0 wrong span (04:36) | The panel **failed**, and rightly. My draft had given critics 4 cards to supporters' 2 (one of them invented as "anticipated"), cut two supporter quotes and one expert's concession, put an expert's proposal under "What the law says", and used a different template for each side's cards. The source checks found no wrong facts, only overstatements |
| Revision 2 | 04:30 → 04:39 | Drafted in the scratchpad while the last verifier finished (so its input didn't change underneath it); swapped in at 04:38. 135 units (99 cited, 21 analysis, 13 editorial, 2 computed). Added 7 passages: the full bill text, the fiscal list's 2026 heading, the English study's outcomes. Gate: 0 errors, 0 warnings | Fixed a checker false positive ("Sen." read as a sentence end) |
| Verification round 2 | 04:39 → … | Four fresh agents: blind source check, supporter's advocate, critic's advocate, standards editor. Calibration carried over (same verifier prompt) | — |

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
6. **Bill PDFs need strike-through and underline recovery.** Plain extraction silently merges deleted
   and inserted words. → `pdf_markup.py` plus a visual spot-check (runbook step 2).
7. **"The law doesn't say X" claims need the whole text archived.** Excerpts can't prove absence. →
   Passages now cover every line of a bill whenever we make absence claims.
8. **The drafter's own bias is invisible to the drafter.** I believed the first draft was balanced. Three
   independent checkers each found the same structural tilts. → One fixed card template for every
   side, base rates before subgroup statistics, and full quotes (style guide v0.1, runbook v0.2).
9. **The verifier can be measured.** It caught 13/13 planted errors, including subtle ones such as
   "on" vs. "no later than", a subgroup figure presented as everyone, and a misattributed quote. The
   code-only gate caught 1/13. Calibration is what makes AI-only verification defensible.

## Automation notes (not yet eligible: 0 of 3 runs)

- **Discovery** could later be partly automated: Open States bill sources, a news search API, and the
  legislature's meeting feed. **Choosing source types and judging relevance stays manual** until it has
  been proven 3 times.
- The **D-list** is a good structured artifact for automation to fill and a person to settle.
