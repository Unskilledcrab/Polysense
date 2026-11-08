# Verification: Trial 001 (HB 1009)

Method: the AI-only [verification protocol](../../docs/process/verification-protocol.md). There is no
human in the loop ([ADR 0014](../../docs/adr/0014-ai-only-verification.md)). Raw reports are in
[verification/](verification/).

## Round 1 (revision 1, `verification/round1/workspace.r1.md`)

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate (`check_citations.py`, `verify_hashes.py`, `readability.py`) | **Pass.** 92 units, 0 errors, 0 warnings (after two checker fixes: American quote punctuation, number words). 15/15 hashes match. Reading grade 5.4–7.0 on target sections |
| 2 | Blind source check of the real page (fresh agent, page + passages only) | **58 supports, 9 partial, 0 does not support, 0 wrong span.** 12 tags questionable, 0 invalid; 13 framing issues. **92/92 evidence spans grounded** (machine-checked). Every number, date, vote count, quote and attribution matched the passages |
| 3 | Bias panel | **Fail.** Supporter's advocate 3/5. Critic's advocate 3/5. Standards editor: neutrality 3/5, clarity 4/5 (35 issues: 3 high, 14 medium, 18 low; 12 flip-test asymmetries) |
| 4 | Planted-error calibration (13 hidden errors, fresh blind verifier) | **13/13 caught (100%)**, 92/92 evidence spans grounded. The code-only gate alone caught 1/13 |

### What round 1 found, and what we did

Three checkers working independently converged on the same problems, which is evidence they were real.

| # | Finding (who raised it) | Disposition in revision 2 |
|---|---|---|
| 1 | Argument cards didn't share one template; the missing or renamed fields were on the cards critical of the law (editor, critic) | **Fixed.** Every card now has the same fields in the same order: In short · Who says it · What supports it · Who disagrees · What challenges it · What's missing · What's uncertain |
| 2 | 4 critic cards vs. 2 supporter cards, including an unattributed "anticipated" card; documented supporter arguments had no card (supporter, editor) | **Fixed.** 3 + 3 documented cards. Added "Phone limits are already working in Georgia". Removed the anticipated cost card; its substance moved into Option B "Who pays" |
| 3 | Emergency-contact card had no real support; the Emory expert's suggestion was filed under "What the law says"; her "valid and understandable" line was cut (critic, editor, source check) | **Fixed.** Support now cites the law's text, the lockdown account and her concession. Her proposal sits under "What challenges it", and we note the law doesn't require it (now checkable against the full bill text) |
| 4 | Selective quotes: Hilton's basis ("districts reported improved focus and less isolation…") and Harrell's full tradeoff quote were dropped (supporter) | **Fixed.** Both restored where they belong |
| 5 | Subgroup percentages without base rates (supporter, editor, source check) | **Fixed.** The overall split (70.8% support, 29.2% oppose, margin about ±4 points) is now stated before any subgroup figure |
| 6 | Claims about what the law doesn't say rested on passages that skipped bill lines (all three panelists, both source checks) | **Fixed.** Added passages P-056–P-061, so passages now cover the entire enacted text (lines 1–106). "No funding" is now worded as "the law itself doesn't set aside money; any state funding would come through the budget, which we didn't review" |
| 7 | Uncertainty lines presumed benefits ("how much … will improve") (critic, editor, source check) | **Fixed.** "Whether, and how much, … will change", each with a reason |
| 8 | "The state decision is final" and a district-only "What you can do" favor the status quo (editor, source check) | **Fixed.** Added a neutral action: tell your state lawmakers what you think, whether you support the law or not |
| 9 | Option A lacked its strongest case for and against; options had different fields (editor, critic) | **Fixed.** Option A now names the kept access and contact (with the 74.8% figure) and the avoided duties, plus the distraction cost Woods described. All options have "What happened" |
| 10 | Option B "Who pays" omitted lost direct contact and the Florida suspension rise (critic) | **Fixed.** Both added, plus the recurring duties and the missing fiscal note |
| 11 | Overstatements: "all day", "would have allowed phones", "some schools", dropped "public", 30-second version omitted the exception, "call" vs. "contact the school directly" (source check, editor) | **Fixed** |
| 12 | Argument from silence: "That study measured well-being and phone use, not test scores", based only on abstract excerpts (both source checks) | **Fixed.** Added P-064 (the abstract's listed outcomes). The page now says the summary *lists* well-being and phone use as outcomes, and that we read summaries only |
| 13 | Labels not on one rubric: card 1 "Supported, with limits" vs. card 4 "Mixed" on the same Florida study (editor, critic) | **Fixed.** Both Florida-based cards are "Supported, with limits (one Florida study)", with the specific limit named |
| 14 | Emergency card labeled pure "values" though its core is factual (critic, editor) | **Fixed.** "Factual, with a values question", graded "Supported, with limits (the law's text; no data on crisis alerts)" |
| 15 | Mental-health card's support slot held a non-supporting finding (in-school phone time) (critic, editor, source check) | **Fixed.** Moved to "What challenges it", in the same sentence as "no evidence of better well-being" |
| 16 | Jargon: Act 395, transitions, fiscal note, cross-sectional, epidemiologist, state waivers, working paper (editor) | **Fixed.** Each is defined in place or replaced with plain words |
| 17 | Verb asymmetry: "argued", "called" vs. "said" (editor) | **Fixed.** "Said" for every speaker |
| 18 | Mental-health label: the critic proposed "Not supported by current evidence" | **Partly adopted.** Kept the vocabulary's "Uncertain" but added the reason on the card: "no benefit found in the one study reviewed". One cross-sectional study can't establish "contradicted" |
| 19 | Fiscal-note search couldn't be checked; the only passage showed a "2025" heading (both source checks) | **Fixed.** Added P-062 (the list's 2026 section heading). The sentence is tagged with the search method and says a missing entry doesn't prove there's no estimate |

## Round 2 (revision 2, current `workspace.md`)

*Pending: fresh blind source check, supporter's advocate, critic's advocate and standards editor.*
