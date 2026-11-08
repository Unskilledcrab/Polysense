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

## Round 2 (revision 2, `verification/round2/workspace.r2.md`)

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate | **Pass.** 135 units (99 cited, 21 analysis, 13 editorial, 2 computed), 0 errors, 0 warnings |
| 2 | Blind source check (fresh agent) | **89 supports, 10 partial, 0 does not support, 0 wrong span.** 4 tags questionable, 0 invalid; 18 framing issues (4 medium, 14 low). **135/135 evidence spans grounded** |
| 3 | Bias panel | **Fail.** Critic's advocate 4/5 (pass). Standards editor: neutrality 4/5, clarity 4/5, 0 high-severity issues (25 issues: 9 medium, 16 low; 17 flip-test asymmetries). **Supporter's advocate 3/5** |
| 4 | Calibration | Carried over from round 1 (same verifier prompt and method) |

The panel failed on the supporter's side. Round 1's fixes had overcorrected in places: the page now
argued with supporters more than with critics.

### What round 2 found, and what we did

| # | Finding (who raised it) | Disposition in revision 3 |
|---|---|---|
| 1 | Option B cited the Florida study only for its cost (more suspensions), not its benefits (supporter, critic, editor, source check) | **Fixed.** Option B "Who benefits" now lists the year-two test-score gains, fewer unexcused absences and the larger effects in middle and high schools. "Who pays" keeps the suspension rise, with the source's own note that it began to fade |
| 2 | Layer 3 gave the case against the law about twice as many items as the case for it (supporter) | **Fixed.** Option B "Who benefits" adds the Florida results, the 60.3% of supporting parents who expect better safety, and teachers' reported support. Option A "Who pays" adds what Georgia would miss if Florida's results apply |
| 3 | Option A's "Who pays" was an advocate's quote; only supporters were quoted in Layer 3 (editor, critic) | **Fixed.** Option A now cites critic Darryl Adams under "Who benefits" and states its costs as tagged analysis. Woods's statement stays, worded as his call for extending the ban |
| 4 | Two of three supporter "In short" lines ended in a rebuttal; no critic line did (supporter) | **Fixed.** Every "In short" line now gives the claim and then its evidence status in the same form: card 4 says how alerts perform is unknown, card 5 says workload wasn't measured |
| 5 | Evidence labels weren't on one bar: card 4's label hedged a fact the law itself states; card 5 rested on an unmeasured claim (supporter, critic) | **Fixed.** Card 4: "Supported on the facts (the law's text); uncertain on emergency safety". Card 5 names its basis: the law's duties, the Florida suspensions, no workload data. Card 1 names its limits: a working paper, gains in year two, possibly through attendance |
| 6 | Proposed label changes: card 2 "Not supported" (critic); card 3 "Unsupported so far" (critic); card 5 "Partly supported" (supporter) | **Not adopted.** None is in our evidence vocabulary. Card 2 stays "Uncertain", because one cross-sectional study of 12- to 15-year-olds under a different policy can't show the law won't help. Card 3's label now says "reports only; no measured results" |
| 7 | Supporter arguments the passages support were missing: attendance, teachers' support, the law's stated goal, Woods's mental-health statements, the safety reason, social-media time, family workshops (supporter) | **Fixed.** Each is now on the page: Layer 2 states the law's goal (P-057) and the family workshops (P-060); cards 1, 2, 4 and 5 and Option B carry the rest |
| 8 | Critic arguments the passages support were missing or thin: no emergency exception, students with health needs but no formal plan, unfunded duties, the causal suspension finding (critic) | **Fixed.** Card 4 states there is no emergency exception. Option B "Who pays" covers the health-plan requirement and the duties without funding. Card 5 and Option B use the study's causal wording: enforcement "led to" more suspensions |
| 9 | Card 2 said no one disputed the mental-health claim, but the English study's authors did; card 1 used their quote as filler (critic, editor, source check) | **Fixed.** The authors' statement is now card 2's "Who disagrees". Card 1 says plainly that we found no evidence against it and that the English study didn't measure test scores |
| 10 | "Who disagrees" was held to different bars: Harrell's general success claim was stretched to rebut card 5; Adams's concerns were narrowed to enforcement in card 3 (editor, critic, source check) | **Fixed.** One bar: only statements that answer the card's claim. Card 5 now cites teachers' reported support. Card 3 lists all three of Adams's concerns |
| 11 | A reporter's paraphrase of Rep. Hilton appeared in quote marks; only Woods's statement carried "according to Atlanta News First" (editor, source check, supporter) | **Fixed.** Hilton is paraphrased without quote marks. The extra hedge is gone; reported statements share one form |
| 12 | Quotes split or cut: Gazmararian's two sentences were split across fields; Florida's "where student smartphone ownership is more common" was dropped; one student stood in for all student testimony (supporter, critic, source check) | **Fixed.** Her full quote appears in one place. The ownership clause is quoted. The cards say students testified in support, then cite one |
| 13 | The amendment's sponsor wasn't named, though every supporter was; listing the amendment under card 6 credited senators with reasons no source gives (critic, editor) | **Fixed.** Option C names Sen. Kemp of the 38th District, noting he isn't Gov. Brian Kemp. The amendment no longer appears as a card 6 speaker; the Layer 4 intro reports only the 13 votes |
| 14 | Layer 4 never said its named voices came almost entirely from one side (editor) | **Fixed.** The intro says the sources quote four officials for the law and one named critic, and that this reflects whom the sources quoted |
| 15 | Study limits were given unevenly; subgroup reasons were shown without their larger error or the multi-select note (supporter, source check) | **Fixed.** The intro describes both studies' designs and limits together, and says reasons within each group are less precise and can overlap |
| 16 | Tense and timing: "now apply", "replaced when signed", Cairo "started" (source check, editor) | **Fixed.** Future tense for the high school rule; the old law is replaced "when the new law takes effect on Dec 31, 2026"; Cairo "said it would begin" |
| 17 | Statements about prior law lacked prior-law text: the K-8-only rule, the newly covered programs (source check) | **Fixed.** Added P-065 and P-066 (the old wording of (c)(3) and (c)(1)). Layer 2 splits "Also new in this law" (with evidence of the change) from "Other rules in the law" (no claim it's new) |
| 18 | Overstatements: "schools must store phones", Option C's benefit without its opt-in condition, "critics say … will" for one teacher's worry, "saw no difference", "only when" for the exception (source check) | **Fixed.** Each is now worded as the source says it |
| 19 | The law's minimum rules: saying it doesn't require rapid alerts cut only against supporters (supporter) | **Fixed.** Card 4 adds that districts can add such measures because the law sets minimum rules (P-008, "at a minimum") |
| 20 | Card 4 lists the staff-run emergency channel under "What challenges it"; critics see that channel as the bottleneck (critic) | **Kept.** It's what supporters offer as the safeguard. The card's "What's uncertain" says how well it works in a crisis is unknown |

Revision 3 also brought Layer 2's reading grade back down to 7.6 after these additions had raised it to
9.3.

## Round 3 (revision 3, current `workspace.md`)

*Pending: fresh blind source check, supporter's advocate, critic's advocate and standards editor.*
