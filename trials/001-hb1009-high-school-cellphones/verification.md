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

## Round 3 (revision 3, `verification/round3/workspace.r3.md`)

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate | **Pass.** 154 units (107 cited, 31 analysis, 14 editorial, 2 computed), 0 errors, 0 warnings. Reading grade: 30-second 8.1, L1 5.6, L2 7.6, L3 7.3 |
| 2 | Blind source check (fresh agent) | **100 supports, 7 partial, 0 does not support, 0 wrong span.** 8 tags questionable, 0 invalid; 14 framing issues (3 medium, 11 low). **154/154 evidence spans grounded**. All numbers, dates, vote counts and quotes correct |
| 3 | Bias panel | **Advocates pass; 2 high-severity editor issues.** Supporter's advocate 4/5. Critic's advocate 4/5. Standards editor: neutrality 4/5, clarity 4/5 (24 issues: 2 high, 8 medium, 14 low; 15 flip-test asymmetries) |
| 4 | Calibration | Carried over from round 1 (same verifier prompt and method) |

Both advocates reached the pass mark for the first time. The round still can't count as a pass: the editor
found two high-severity problems, and the source check left flags to resolve. Revision 4 fixes them, and
round 4 re-checks it with the whole panel (protocol layer 5).

### What round 3 found, and what we did

S = supporter's advocate, C = critic's advocate, E = standards editor, SC = blind source check.

| # | Finding (who raised it) | Disposition in revision 4 |
|---|---|---|
| 1 | **High.** The Florida study's findings were worded unevenly: "significant" and "led to" for the rise in suspensions, plain "rose" and "fell" for the gains (S, E) | **Fixed.** One wording rule for all three findings: "led to" for test scores and suspensions (the study was designed to estimate causal effects), the authors' own "suggest" for absences, and "significant" dropped everywhere, because readers hear it as "large" |
| 2 | **High.** "Districts can add" the guaranteed phone access an expert described, though the ban has no emergency exception (E, SC) | **Fixed.** Districts can add faster parent alerts, but they can't allow students' own phones in an emergency unless a plan or another law requires it |
| 3 | The 13-senator amendment vote appeared without the final votes, its grades 9–12 limit or the 20 House "no" votes; the voice count left out the student (S, C, E, SC) | **Fixed.** The intro gives 145–20 and 52–0, says the amendment covered high school students and that no senator voted against the final bill, and counts the student who testified |
| 4 | Option B's benefits quoted officials while its costs had no opposing voice (C, E) | **Fixed.** Officials' quotes left Option B. Both lists now use the same kinds of evidence: the Florida study, the law's text, and each side's parents (87.7%, 71.2%, 69.1% and 60.3% expected benefits; 74.8% feared losing emergency contact) |
| 5 | Option A used the Florida study only for the gains a ban might bring; its first "Who pays" item read like a benefit; a supporter's quote was cut to back the status quo (C, S) | **Fixed.** Option A now lists avoiding the short-term rise in suspensions as a benefit, names the distraction cost the law describes, and quotes Sen. Harrell in full |
| 6 | The missing fiscal note under "Who pays" implied a cost no source estimated; "no money" lacked budget context (S, SC) | **Fixed.** The fiscal note moved to "What's uncertain", with the storage choices that drive cost. "Sets aside no money" stays under "Who pays" and in card 5, now noting that state funding would come through the budget, which we didn't review |
| 7 | Card 5's label: split it like card 4 (S); add the lack of funding and the disparity for Black students (C) | **Fixed.** "Supported on new duties and early suspensions (the law's text; one Florida working paper); uncertain on staff workload (not measured)". The funding and "especially among Black students" points are in the card |
| 8 | Woods's report of teachers' support had no caveat, while Adams's lockdown account did (C, E, SC) | **Fixed.** Card 5 says no source surveyed teachers, so neither Woods's account nor Adams's concern has been measured. The statement left Option B |
| 9 | Card 6 recast critics as a claim about maturity, buried parental choice, and left the health and schoolwork reasons unanswered; "many parents" had no denominator (C, S, E, SC) | **Fixed.** Retitled in the documented speaker's terms ("should be allowed to keep phone access"). The summary names the 45.5% parental-choice reason. "What challenges it" adds the plan exception and school-provided laptops. "Who disagrees" adds the 70.8% majority |
| 10 | Add the 13 senators to card 6 as speakers (C) | **Not adopted.** No source gives their reasons. Crediting them with an argument would break the anticipated-argument rule (the round-2 editor's finding) |
| 11 | Relabel card 2 "Not supported so far" and card 3 "Unverified" (C) | **Not adopted.** In our vocabulary, "Uncertain" means little evidence yet, with the reason stated; "Mixed" is the label for evidence pointing both ways. The labeling rule is now printed above the cards, and card 3's label says "statements only" |
| 12 | The English researchers' policy conclusion was cut (C) | **Fixed.** Their second sentence is quoted in full |
| 13 | "Didn't measure test scores" and "didn't measure friendships" rested on a summary's partial outcome list (SC) | **Fixed.** The page now says the summary reports no such results and that we didn't read the full paper. That point moved from "What challenges it" to "What's missing" |
| 14 | Card 1: "only" wasn't in the source, and its first-year finding sat under "What's missing" while card 5 filed the matching point as a challenge (S, E, SC) | **Fixed.** No "only"; the first-year finding is now under "What challenges it" |
| 15 | The Florida summary never says what the bans covered; "larger effects in middle and high schools" may apply to every effect, including suspensions (C, E, SC) | **Fixed.** Added P-067 and P-068 so the whole abstract is archived. The study notes say the summary doesn't describe the bans' scope or which effects were larger. The ambiguous finding left the benefit and rebuttal fields |
| 16 | Each study got uneven caveats: no strengths for the English study, no design note for the Florida one (E, SC) | **Fixed.** English: 1,227 students, 30 schools, a standard well-being questionnaire. Florida: "quasi-experimental", explained in plain words |
| 17 | Side labels ("supports" vs. "concerned about"); titles named the policy differently and hedged only the critics' prediction (E) | **Fixed.** "Supports the law" and "Opposes the law". Every title says "the ban", and predictions use "will". Card 5 keeps Adams's own "can" for conflict |
| 18 | "Worries" was the only emotional verb in an "In short" line, and Adams's wording was softened (E, C, S) | **Fixed.** "Said", and the reporter's own words: "put the burden on teachers and administrators to police phones, a task that can invite conflict" |
| 19 | Supporters' survey reasons rebutted a concern (60.3% "physical health and safety" vs. emergency contact) (C, E, SC) | **Fixed.** Removed from card 4; it stays among Option B's expected benefits. Survey reasons are no longer used as rebuttals on either side |
| 20 | Card 4 framed uncertainty only around the law's alert systems, and listed the law's own requirement as a challenge (S, SC) | **Fixed.** The label and "What's uncertain" now cover both school alerts and students' own phones; the requirement left "What challenges it" |
| 21 | Supporter points the passages support: the law's review-and-adjust duty, the authors' "initial adjustment period" reading, the social and mental-health expectations (S) | **Fixed.** Card 5 and Option B carry them |
| 22 | The labeling rule was never shown (E) | **Fixed.** A collapsed "How to read these cards" section above the cards gives the rule, the card order and the study notes |
| 23 | Accuracy details: "exempt", "hold or wear" without the law's purpose clause, "replaces" (it expands), "he" for a senator whose gender no passage gives, "is unknown" and "no source" without "we reviewed", "short breaks", a definition called a goal, the K-8 deadline's tense, "must now also cover" for programs only newly named (SC, E, C) | **Fixed.** Each is now worded as its passage supports |
| 24 | Plain language: undefined "dual enrollment", "amendment" and "margin of error"; jargon in visible tags; long sentences in Layer 4; subgroup precision misstated (E, SC) | **Fixed.** Defined in place; Layer 4 sentences split. Margins of error are given separately for each group |
| 25 | Supporters' cards always come first (C, E) | **Kept**, with the ordering rule stated: the order is not a ranking |
| 26 | Card 2's summary states its null result while critics' summaries carry no rebuttals; the result repeats four times (S) | **Kept.** Every "In short" line gives the claim's evidence status (card 4: no safety data; card 5: workload not measured). For card 2, that status is the null study |
| 27 | Less in-school phone time isn't evidence for better mental health (C) | **Kept.** It is the first step the claim relies on; the null well-being result sits right below it |
| 28 | Researchers are listed as voices only when their study cuts against the law (E) | **Kept.** The English authors made an explicit policy statement (P-043). The Florida summary makes none (P-067, P-038 to P-040), so there is no matching statement to list |
| 29 | Every concern is raised by one ordinary person and answered by officials (E) | **Kept for this trial, and disclosed** on the page. The runbook (v0.3) now has the drafter count voices per side before drafting and search for more on the thin side |
| 30 | The law's purpose is missing from card 1 and the 30-second version (S) | **Kept.** The law's own text isn't a side's speaker; Layer 2 states it |

## Round 4 (revision 4, `verification/round4/workspace.r4.md`)

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate | **Pass.** 180 units (114 cited, 44 analysis, 19 editorial, 3 computed), 0 errors, 0 warnings |
| 2 | Blind source check (fresh agent) | **108 supports, 6 partial, 0 does not support, 0 wrong span.** 8 tags questionable, 0 invalid; 12 framing issues (3 medium, 9 low). **180/180 evidence spans grounded.** No wrong number, date, vote count or quote |
| 3 | Bias panel | **Pass.** Supporter's advocate 4/5. Critic's advocate 4/5. Standards editor: neutrality 4/5, clarity 3/5, **no high-severity issue** (28 issues: 8 medium, 20 low; 15 flip-test asymmetries) |
| 4 | Calibration | Carried over from round 1 |

**Round 4 passes** under the protocol's "when a page is done" rule. Revision 5 makes only accuracy fixes
and fixes for breaches of written rules. A targeted blind re-check covers the 53 changed or new units.
Everything else from round 4 is an open item, listed below.

### Fixed in revision 5 (accuracy and written rules)

| Finding (who raised it) | Fix |
|---|---|
| Three Harrell quotes relied on a passage heading to name the speaker; the passage text said only "she said" (SC) | P-045 and P-046 widened to include the article's sentences that name her. Card 3 quotes her sentence in full |
| The "no fiscal note" claim cited only the list's 2026 heading (SC) | Added P-069, the list's whole 2026 section. The claim now points to it |
| One Florida working paper was stated in the page's own voice ("In Florida, bans led to…") (SC) | Each mention now says what the study found, for the gains and the suspensions alike |
| The failed amendment also dropped "breaks" from the school-day definition (SC) | Option C now says so |
| Overstatements: "attendance gains found" (the authors say "suggest"); "younger students" for ages 12 to 15; "the article" and "the source" for single excerpts; "Emory" (no passage names it); the K-8 conclusion; the effective date given as the high school deadline (SC) | Each reworded to what its passage supports |
| Voice tally left out the public health professor (SC) | Added |
| A pro-law sentence sat in card 1's "What challenges it" field (SC, C, E) | Removed; the English study's missing test-score results stay under "What's missing" |
| Woods's words narrowed; "said … would burden" firmer than "worries"; "all" dropped from "all emergency communication"; "calling parents" for "notifying"; the page called phones a distraction in its own voice; "Schools" for high schools; adopting vs. reviewing a policy; Cairo's plan undated; "how far off it could be" (S, C, E) | Each fixed to match its passage |
| Written-rule breaches: subgroup figures before the base rate in Option B; "expected" vs. "said" for the same survey question; "support" vs. "argued against"; loaded "even" and "suffer"; undefined jargon (IEP, recreational, steady state, Documented, claim type); "critics" vs. "supporters" (S, E) | Base rates first; "cited" for both sides; "spoke for" and "spoke against"; neutral words; terms defined |
| Card 1 was the only "In short" line with no limit (C, E; style guide: same form on every card) | Adds "There are no Georgia results yet" |

### Open items for the next full round

These need a card, label or balance change, so they wait for the next revision and a full panel. Where
reviewers disagreed, both views are listed.

1. **Card 5's label** carries the Florida study without the "with limits; no Georgia results yet" wording
   that card 1 uses for the same study; card 5 also lacks the study's Florida-only and scope caveats
   (S, E).
2. **Contradictions to resolve:**
   - Option A's Harrell quote: in full (round-3 supporter) or trimmed to the factual part (round-4
     critic and editor).
   - Card 4's label: include "whether students' own phones make emergencies safer" (round-3 supporter)
     or drop it because no source claims it (round-4 critic).
   - The law's emergency-communication requirement in card 4: a safeguard to list (round-4 supporter)
     or not a finding against the claim (round-2 critic, round-3 source check).
   - "Use" vs. "hold, wear or use" in the 30-second version: "hold or wear" overstated the law without
     its purpose clause (round-3 source check); "use" understates it (round-4 critic).
   - Card 2's in-school phone time: support for the claim (round-2 and round-4 supporter) or only the
     rule doing what it says (round-3 and round-4 critic).
3. **Option B** benefits run about 50 words against about 130 for costs; its "What's uncertain"
   sentence lumps unequal evidence together (S, E).
4. **Voices:** "Supporters say" vs. a single named teacher in the summaries; officials answer the only
   opposing voice; card order always puts supporters first (C, E). Runbook v0.3 now asks the drafter to
   count voices before drafting.
5. **Field filing:** the expert's quote sits in "What challenges it" while the English researchers sit
   in "Who disagrees"; card 6's survey reasons sit in "What supports it"; card 6's "What's uncertain"
   repeats its summary (E).
6. **Plain language:** Option B and card 2 read near grade 11; opposing cards' summaries run 34–37 words;
   the study notes run about 200 words (E).
7. **Missing points the passages support:** the breadth of "access" and open-ended consequences
   (card 5); the plan exception's narrowness (card 6); the absences explanation (card 1); a statewide
   rule with no waivers (Option B); larger effects in middle and high schools (card 1) (C, S).
8. **Documented arguments** show speaker and source, but not the date and venue on the card; dates
   are in the source drawer (E).

## Targeted re-checks (revision 5, current `workspace.md`)

Fresh blind agents checked only the changed units (protocol: "When a page is done", step 2). Inputs and
reports are in [verification/round5-targeted/](verification/round5-targeted/).

| Check | Units | Result |
|---|---|---|
| Targeted re-check 1 (revision 5 draft) | 53 changed or new | 33 supports, 1 partial, 0 does not support, 0 wrong span; 3 tags questionable; 53/53 spans grounded |
| Fixes | 17 units | Card 2's summary no longer credits "supporters" with parents' figures, and cites the passages for England and well-being. "Students can use phones in class" (the law bans access, not phones in the room). "No measured results from Georgia high schools yet". The K-8 date was a "no later than" deadline. The amendment rewrote what "bell-to-bell" means. The student's point is paraphrased without quote marks. "Adopt, and revise as needed". Missing citations added |
| Targeted re-check 2 (the 17 fixed units) | 17 | **11 supports, 0 partial, 0 does not support, 0 wrong span; 6 tags valid; 17/17 spans grounded** |

**Status: passed checks (round 4).** Revision 5 is published as checked.

### Open items for the next full round (added by the targeted re-checks; all low severity)

9. "District" for "each local school system and public school"; "now" for a law that takes effect
   2026-12-31 (lines 27, 56).
10. "Student devices are banned" is looser than the law, which bans student *access* (line 42).
11. Option A's Florida sentences say "students" where only students in districts without limits would
    be affected; keep the two sentences parallel (lines 74-75).
12. "How far off the result is likely to be" can read as the typical error; a 95% margin is how far off
    it could plausibly be (line 96).
13. "The one study we found" can read as the only study (two appear); name the comparison groups
    (line 124).
14. "Georgia teens" is broader than the public high school students the law covers (line 130).
15. Card 3's challenge could note that districts had to adopt K-8 policies by 2026-01-01 (line 140).
16. Card 6's summary drops "allowed" from "should be allowed to keep access" (line 172).

## Revision 6: reader-first update (2026-11-08, checks pending)

Change class **update**. Reason: reader-first revision after usability feedback. Revision 5 is archived in
[verification/round5/](verification/round5/) (`workspace.r5.md`, `home.r5.md`, `page.r5.yaml`). The page now follows
the reader-first contract (style guide v0.6, section 9; runbook v0.6, Step 4). The changes also answer an AI lay-reader
check of revision 5 (persona: a parent of a 10th grader). That reader couldn't tell when their child's school starts,
what happens to a student who uses a phone, or what the law costs a family. They read the Arguments tab's opening
figures as leaning toward the law, and didn't know what "HB" means.

**Not changed:** every evidence label, card claim (title), card order, option title and the side each point
supports. This was checked mechanically against `workspace.r5.md`. Outside "In short", card edits are plain-language
only: a role ("State Sen."), a defined name, or a bare "it", "this" or "they" replaced by its noun.

### What changed, by part

| Part | Change |
|---|---|
| 30-second version | Leads with what the law does and spells out House Bill (HB). Names the deciders (Georgia lawmakers and the governor). Keeps the health-or-disability-plan exception and both suggested actions: ask the district, or tell lawmakers whether to keep or change the law. 90 words |
| What it means for you (new) | Four "If you…" lines: high school students, their families, school staff, and families of K-8 students. Each says what changes and when. The students line gives the consequences; the families line says the law sets aside no money and doesn't require families to pay. "Decided now, and decided later" explains Dec 31, 2026 (the law takes effect) versus July 1, 2027 (the latest start), and that each district or school picks its start date. "Not known yet" has 5 dated items, each with its reason. There is no "Where the money comes from", because no source describes money moving |
| Layer 1 | New order: What you can do; Who decides, and how to reach them; Coming up; What happened. "Who decides" lists the General Assembly, the governor, and each district and public school in one format. It gives only the routes the sources give (handbooks, websites, encouraged workshops and town halls) and says the sources give no phone or email. "Your state representative and senator" became "state lawmakers in the General Assembly". Committee steps and Act 395 are explained in plain words |
| Layer 2 | Open items 9 and 10 fixed. "District" defined as the law's "local school system". Consequences now say each code of conduct sets the exact steps |
| Layer 3 | New intro (56 words) and values guide: Option A from Darryl Adams (P-047), Option B from Richard Woods (P-036). Option C has no documented argument, so it gets no line, and the guide says so. Option B's long sentences split (open item 6). The Florida study, the survey's maker and Sen. Harrell's role are defined at first mention |
| Layer 4 | The tab now opens with both sides in one news report's words (P-071), then who our sources quote. The survey split moved into "How to read these cards", still before any subgroup figure. The vote tallies left this tab; they remain in Layer 1 and Options B and C. Every "In short" was rewritten: people first, then the claim, then the evidence status (29–37 words). Study notes cut from about 200 to about 130 words. Added "Questions to weigh" (3 questions). Open item 14 fixed in card 2 |
| home.md | Items spell out House Bill 1009, run 12–22 words, and each has a dated label |
| page.yaml | Title: "Georgia lawmakers ban school-day phone use in public high schools by July 1, 2027". The code and link text spell out House Bill. Status chips: signed May 5, 2026; takes effect Dec 31, 2026; high schools start by July 1, 2027. Option chips are plain. Each reader-facing string has a citation comment. `kind` and `markup` are unchanged |
| Passages | Added P-070 (S12: who runs the survey), P-071 (S16b: both sides in one sentence) and P-072 (S16b: the high school start in school-year terms). All come from snapshots already archived; the rebuild reproduced the 68 earlier passages exactly |
| `verification/summary.json` | Status set to "Reader-first update; checks pending" (`verified: false`). The intro says the results shown are for revision 5. Unit count updated to 250 |

### Deterministic gate (revision 6)

| Check | Result |
|---|---|
| `check_citations.py` | workspace: 250 units (152 cited, 61 analysis, 34 editorial, 3 computed), 0 errors, 0 warnings. home: 6 units, 0 errors, 0 warnings |
| `readability.py` | 30-second 7.7, What it means for you 7.8, Layer 1 5.9, Layer 2 8.2, Layer 3 7.8 (Layer 4 7.9, not targeted). No budget warnings, no CHECK lines |
| `verify_hashes.py` | PASS |
| Test render | No warnings |

The independent checks (blind source check, bias panel) have not run on revision 6.

### Open items

- **Folded in:**
  - 6: sentences in Option B and card 2 split. Opposing "In short" lines now run 32–36 words and supporting ones 29–37. Study notes are shorter.
  - 9: "now" removed, and "each school district and public school" used where the duty is stated.
  - 10: "student access to personal devices is banned".
  - 14: "Georgia's public high school students".
- **Touched by the rewrite** (the next full round should confirm): 13 (card 2's summary no longer says "the one study we found" and names the comparison) and 16 (card 6's summary keeps "should be allowed").
- **Still open:** 1–5, 7, 8, 11, 12, 15.

### Where I'm least sure

1. **Values guide, Option A.** Adams's only verbatim quote is about older teachers not wanting "that confrontation". His other points (older students' independence, phones in emergencies) survive only as paraphrase. Emergency contact is the reason opposing parents cited most (74.8%), so this line may not show that side's strongest value. A verbatim lockdown quote exists in S16 and S16b, but it doesn't fit the 20-word quote and 35-word line budgets without cutting its meaning.
2. **Values guide, both lines.** "Who opposed the bill" and "who called for the ban" are our mapping of each speaker to Options A and B; neither speaker named an option.
3. **"If you…", families.** "Doesn't require families to pay for anything" is an absence claim over the law's full text. A district could still ask families to buy a pouch; that appears under "Not known yet".
4. **"If you…", K-8 families.** "Leaves that rule as it was" rests on the bill's markup (no deletions or additions in (b)(1)), not on a passage that says so.
5. **"If you…", staff.** "Your school must enforce the phone ban by July 1, 2027" joins the monitoring duty (P-014) to the start date (P-007).
6. **"If you…", students.** "At any point in the school day" paraphrases bell-to-bell (P-002); a checker may prefer "during".
7. **"Decided now".** "In place by the 2027-28 school year" combines the law's deadline with one news report's school-year framing (P-072).

## Round 5 results (revision 6, `verification/round5/workspace.r6.md`)

Inputs and reports are in [verification/round5/](verification/round5/): the page, home items and `page.yaml` strings as
checked (`workspace.r6.md`, `home.r6.md`, `page.r6.yaml`, `passages.r6.md`), and every report.

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate | **Pass.** 250 units (152 cited, 61 analysis, 34 editorial, 3 computed), 0 errors, 0 warnings; home 6 units, 0 errors, 0 warnings. Reading grade: 30-second 7.7, What it means for you 7.8, L1 5.9, L2 8.2, L3 7.8 |
| 2 | Blind source check of the page (fresh agent) | **149 supports, 3 partial, 0 does not support, 0 wrong span.** 93 tags valid, 5 questionable, 0 invalid; 11 framing issues (3 medium, 8 low). **250/250 evidence spans grounded** (`check_verifier.py`) |
| 2 | Blind source check of the home items | 4 supports, 2 tags valid; 2 low framing issues; 6/6 spans grounded |
| 2 | Display check (`page.yaml` strings) | 9 supports; 2 low framing issues; 9/9 spans grounded |
| 3 | Bias panel | **Pass.** Supporter's advocate **4/5** (7 missing arguments; wording notes 2 medium, 5 low; imbalance 1 medium, 3 low; 3 label proposals). Critic's advocate **4/5** (3 missing arguments; wording notes 1 high, 1 medium, 5 low; imbalance 2 medium, 5 low; 2 label proposals). Standards editor: neutrality 4/5, clarity 4/5, **no high-severity issue** (23 issues: 7 medium, 16 low; 14 flip-test notes) |
| 4 | Planted-error calibration (fresh blind verifier on a copy with hidden errors, `verification/round5/calibration/`) | **11 of 11 caught** |
| — | Lay-reader check (experimental, not a pass condition; [grades](verification/lay-reader/revised/grades.md)) | Revision 6 before this round: answered all six questions; listed 41 terms it couldn't place (12.0 per 1,000 words, down from 16.8). Its HB 1009 terms went to the revision 6.1 fix list (items 19 and 20 below) |

**Round 5 passes** under the protocol's "when a page is done" rule: the gate is clean; the source checks found 0
unsupported claims, 0 wrong spans and 0 ungrounded spans; both advocates rated their side 4/5; and the editor found no
high-severity issue. The critic's one high-severity note was about its own side's values-guide line; the pass rule counts
the editor's.

The three panel reports agree on three things: card 5's evidence label should treat the Florida working paper as card 1
does (all three, the critic included); the opponents' values line used their narrowest value (critic high; editor and
source check medium); and the accuracy flags. A label change re-runs the whole panel, so revision 6.1 gets a full
round 6. Revision 6 stays the fallback.

## Revision 6.1: what changed (2026-11-08, checks pending)

Reason: **fixes after passing round 5**. Change class **correction** for items 1–7 (accuracy) and **update** for items
8–20. Revision 6 is archived in `verification/round5/` (`workspace.r6.md`, `home.r6.md`, `page.r6.yaml`,
`passages.r6.md`). `home.md` is unchanged.

**Not changed:** every card claim (title), the card order, each card's side, the option titles, and every evidence label
except card 5's (item 8).

| # | Item | Done? | What changed |
|---|---|---|---|
| 1 | Law's second exception | Done | The 30-second version: "unless a disability or medical plan explicitly requires it, or another law does" (P-007). The same pair now appears in the students line, "Decided by the law" and Layer 2 item 4 |
| 2 | Families and staff money lines | Done | Families line: "Not known as of Nov 8, 2026: who pays for phone storage; the law doesn't say or set aside money, and we didn't review the state budget." Staff line: "…the law sets aside no money, and we didn't review the state budget." The caveat is the short form of the one in Option B and card 5, to fit the 45-word line budget; its tags carry the full wording |
| 3 | "No Georgia high school results" | Done | "Not known yet" and card 1's "In short" now say "we found no measured results from Georgia high schools"; their tags note P-046 and P-054 |
| 4 | "School day" is the bell-to-bell period | Done | Layer 2 item 1 cites P-005 and P-002; Option B's "What it does" cites P-007, P-005 and P-002 |
| 5 | Card 1 "Who disagrees" | Done | "No one quoted in our sources disputed this claim directly." Then, base rate first: "Of the 29.2% of parents who opposed a high school ban in the Emory survey, 19.8% cited "Phones needed for schoolwork" as a reason." [P-033, P-035] |
| 6 | Card 3's dates | Done | Card 3's "What challenges it" adds that districts and public schools had to adopt K-8 policies by Jan 1, 2026 (P-020, P-066); only the latest K-8 start date, July 1, 2026, hadn't passed when the high school ban was signed |
| 7 | Attribution | Done | P-047 added wherever an Adams line cites only P-048 or P-049 (Option A; cards 4 and 6, including their "In short" lines); P-044 wherever a Harrell line cites only P-045 or P-046 (Option A; cards 3 and 5, including card 3's "In short") |
| 8 | Card 5's label | Done | "Supported on new duties (the law's text); supported, with limits on early suspensions (one Florida working paper; no Georgia results yet); uncertain on staff workload (not measured)". Card 5 also gets card 1's caveats: "What's missing" adds the Florida-only data and the summary's silence on how much of the day the bans covered; "What's uncertain" adds whether Georgia high schools will see a rise in suspensions like Florida's. Claim, side and "In short" unchanged |
| 9 | Printed labeling rule | Done | "How to read these cards" defines "Supported, with limits" as card 1 and card 5 use it ({editorial: labeling rule}). It mentions no label the page doesn't use |
| 10 | Values guide | Done | One line per side, in card order, same form, from the same survey: supporters' top reason ("Improved academic focus and learning", P-034) and opponents' top reason ("No way to reach child during emergency", P-035). 31 and 35 words; quotes 5 and 7 words. A first line names the survey (P-032, P-070). The Option C line is unchanged. Woods's and Adams's quotes stay on their cards |
| 11 | Option A's case | Done | Option A "Who benefits" adds the base rate (29.2% opposed) and all five opposing reasons (P-033, P-035), the way Option B uses P-034. Option B's survey sentence now takes the same short form, since the values guide names the survey at the top of the tab |
| 12 | England study in Layer 3 | Done | After Woods's quote (Option A "Who pays") and after the 69.1% (Option B "Who benefits"): the study's two findings (less phone and social media time during school; no evidence of better well-being) and that it couldn't test cause and effect |
| 13 | Card 5's review duty | Done | The review-and-adjust duty (P-015) moved from "What challenges it" to "What supports it", as a duty, matching Option B's "Who pays" |
| 14 | Card 4: emergency protocols | Done | "What's missing" opens with the law's requirement that policies send all emergency communication through school systems coordinated by school staff (P-008, P-011), then what the law doesn't require. Placed there, not under "What challenges it" (see open item 2) |
| 15 | Card 1: middle and high schools | Done | "What supports it" adds that the summary found larger effects in middle and high schools (P-040), with the caveat that it doesn't say whether that includes suspensions |
| 16 | "If you…" lines | Done | Each line ends "Not known as of Nov 8, 2026: …" (students: your school's start date; families: who pays for phone storage; staff: how much work enforcement takes; K-8 families: what your district changes in its yearly policy update). To fit 45 words, the students line's consequences moved to "Not known yet" (which now gives the law's examples) and "Decided by the law"; the staff line's Department of Education sentence stays under "Coming up" |
| 17 | Who decides, and how to reach them | Partly | **Governor:** archived the governor's contact page (S22; P-073, P-074); the entry now gives the phone number and mailing address. **General Assembly:** the lawmaker lookup page (S21) is blocked: `snapshot.py` saved only the site's loading screen. **Districts:** no Department of Education district directory found (S23 blocked). Both keep the current sentence. No "your legislator", nothing on elections or voting |
| 18 | Questions to weigh, question 2 | Done | "How well can school alert systems and students' own phones each keep families informed in an emergency, and how could anyone know?" |
| 19 | Plain language | Mostly | See the list below. Not done: the State School Superintendent's job (no passage describes it) |
| 20 | Source labels | Done | "General Assembly bill page", "Governor's press release", "Emory parent survey", "Atlanta News First, Mar 12" and "…, Mar 17"; new "Governor's contact page" |

**Item 19, by term** (first mention on each tab: Summary, What it does, Options, Arguments):

- **Act 395:** the header code on every tab and the home card now reads "House Bill 1009 · 2026 · became law as Act 395"
  (P-024). The Summary timeline keeps its note.
- **Which plans count:** the 30-second version and students line say "disability or medical plan". "Decided by the law",
  Layer 2, Option B and card 6 name the IEP, Section 504 plan and medical plan and define IEP and 504 plans. Card 4 says
  "a student's disability or medical plan".
- **Similar devices:** the 30-second version lists phones, smartwatches and tablets; "Decided by the law" and Layer 2 list
  all four of P-004's examples.
- **The exceptions:** the students line names both.
- **The school's own systems:** Layer 2 item 3 says school staff coordinate all emergency messages, and that "all" covers
  messages from the school and between families and students. The Summary tab no longer uses the phrase.
- **One term:** "access" wherever the cited passage says access, defined from P-003 on the Summary and What it does tabs.
  "Use" remains only for the survey, the England study and health apps.
- **Bell-to-bell:** named as the law's term on the Summary, What it does and Options tabs. The Arguments tab doesn't use
  it.
- **The 2025 law:** "House Bill 340" on the Summary (K-8 line), What it does and Options tabs (P-028). Card 3 already named
  HB 340 and now adds "(K-8)".
- **The sponsor:** the Arguments tab's voice count names "the bill's sponsor, state Rep. Scott Hilton" (P-028, P-051) and,
  in the same form, every other named speaker.
- **Dec 31, 2026 and July 1, 2027:** "Decided by the law" says what applies from Dec 31 (the yearly K-12 policy, first due
  Jan 1, 2027), that the ban must start by July 1, 2027, and that the law doesn't say how the January deadline fits with
  the July start date. Layer 2 and Option A say what begins on Dec 31. The header chips now read "Law takes effect Dec 31,
  2026" and "High school ban starts by July 1, 2027".

**Sources and passages:** S22 (governor's contact page) retrieved; S21 and S23 recorded as blocked in `sources.yaml`,
with what each fetch returned. P-073 and P-074 come from the S22 snapshot through `passages.spec.yaml`; the rebuild
reproduced the 71 earlier passages exactly.

### Deterministic gate (revision 6.1)

| Check | Result |
|---|---|
| `check_citations.py` | workspace: 280 units (169 cited, 74 analysis, 34 editorial, 3 computed), 0 errors, 0 warnings. home: 6 units, 0 errors, 0 warnings |
| `readability.py` | 30-second 7.7 (90 words), What it means for you 8.3, Layer 1 6.0, Layer 2 8.4, Layer 3 7.9 (Layer 4 8.0, not targeted). No budget warnings and no CHECK lines. "If you…" lines 45, 45, 45 and 44 words (first sentences 18 to 20); values lines 31 and 35; longest "In short" 37; Layer 3 intro 60 |
| `verify_hashes.py` | PASS (15 retrieved, S22 added) |
| Test render | No warnings: version 6.1, 73 passages, 12 sources. In headless Chrome, card 5's label shows part by part ("Supported: new duties · Supported, with limits: early suspensions · Uncertain: staff workload") |

The blind source check and the bias panel have not run on revision 6.1 (round 6).

### Where I'm least sure

1. **"If you…" budgets.** Every line is at or near 45 words, so the state-budget caveat there is the short form ("we
   didn't review the state budget"), the staff line lost the Department of Education's guidance (still under "Coming
   up"), and the students line lost the consequences (now in "Not known yet"). The supporter may read the staff line as
   duties and no money with no help.
2. **Values guide.** Both lines quote the survey's own reason wording, which parents cited; the template's caption still
   says "In each side's own words". The survey ran before HB 1009 was filed (the critic's timing point), for both lines
   alike. "Top reason" is read from each table, without a test of the gaps between reasons.
3. **"Disability or medical plan"** is our plain gloss for IEP, Section 504 and medical plans in the 30-second version and
   students line; the exact names and the IEP definition sit one fold down on the Summary tab.
4. **Card 4, item 14.** The law's emergency protocol sits under "What's missing", between the supporter's request for
   "What challenges it" and rounds 2 and 3, which said the law's own requirement isn't a finding against the claim.
5. **Card 5's label** has no comma after "limits", so the template splits it part by part. Read raw, "with limits on
   early suspensions" could be parsed as "limits on suspensions".
6. **Dates.** "The law doesn't say how the January policy deadline fits with the July start date" is an absence claim over
   the full text. It sits next to "Each district or school also picks its start date, up to July 1, 2027", an inference
   the editor flagged (open item 40).
7. **England study in Layer 3** now appears twice (Options A and B), and in Option A right after Woods's quote. Both
   placements give both findings.
8. **Card 1 "Who disagrees".** A survey reason ("Phones needed for schoolwork") is the nearest thing to a dispute; it is a
   reason for opposing, not a finding about learning.
9. **"All" means every direction** (Layer 2 item 3) is our reading of "all emergency communication".
10. **Display strings changed** (code and two chips); the display check should run again.
11. **Blocked sources.** legis.ga.gov builds its pages with scripts, so `snapshot.py` can't save the lookup (S02 needed a
    browser); a browser capture might work, but the brief named `snapshot.py`. The governor's page links "Constituent
    Services" to georgia.gov, which we didn't fetch.

### Open items

**Earlier items:** 1 (card 5's label) and 15 (card 3's K-8 policy date) are done (items 8 and 6). From item 2, card 4's
emergency requirement is placed (item 14) and "use" vs. "hold, wear or use" is settled by "access" with P-003's
definition; its other bullets stay open. Item 7's larger middle- and high-school effects are on card 1 (item 15); its
other points stay open. Still open: 3, 4, 5, 8, 11, 12. Round 6 should confirm 13 and 16.

**From round 5, not taken** (S = supporter's advocate, C = critic's advocate, E = standards editor, SC = source check,
LR = lay reader). Where reviewers disagree, both views are given.

17. Card 4's label: say what is "Supported" ("that families must reach students through the school") (S). The critic
    agrees with card 4's label as printed (C).
18. Card 2's label and "In short": add the lower in-school phone and social media time, and that neither summary measured
    friendships (S). The critic agrees with card 2's label as printed (C).
19. Card 1's label: add "the summary doesn't say how much of the school day Florida's bans covered" (C).
20. Card 5's label, parts not taken: "the rise faded after year one" (S); "uncertain on staff workload and
    confrontations" (S) or "and conflict" (E).
21. Card 4's title: "…, including in emergencies" (C). A title change needs a full panel.
22. Card 4: the no-emergency-exception point is made twice (S); Adams's account is the only account of a real emergency
    in our sources (C); Gazmararian's "valid and understandable" could also go under "What supports it" (C, optional).
23. Card 4: state its values question (families' contact against protected class time) in "What's uncertain" and the
    label (E).
24. Card 6 "Who disagrees": add the student's "phones have become addictive for teens" (S). Or drop the repeated
    cigarette quote and add the survey-timing note to the 70.8% (C).
25. Card 6: the English study's lack of a difference in overall phone use, under "What supports it" (C); the health-app
    plan requirement as a burden, under "What's missing" (C).
26. Card 5: give Harrell's point in full ("teachers can focus on instruction again") (S), or drop the line or move it to a
    supporting card (E). Card 1's challenge mentions the first-year suspensions, but card 5 doesn't mention the year-two
    test gains (S).
27. Card 2: the null result appears four times (S). Round 3 kept it as the card's evidence status.
28. Supporters' teacher argument (Harrell P-045, Woods P-053) in Option B's "Who benefits" and the staff line; the 60.3%
    safety reason on a card (S).
29. Voice count: add the English study's authors (E) and the 20 House "no" votes, none quoted (C).
30. Survey timing: the Emory survey ran Oct 30 to Nov 24, 2025, before HB 1009 was filed, and asked about a school-day
    ban in general terms (C).
31. "Support isn't a benefit": the 70.8% under Option B's "Who benefits" (SC, E). Revision 6.1 mirrors it in Option A
    (item 11) instead of moving it; the supporter counts it among the choices that favor its side (S).
32. Option B lists every supporters' reason but one opponents' reason (C, E). The supporter calls Option B's "Who
    benefits" the shorter field, missing points (S). Drafter's note: Option A's "Who pays" doesn't carry the supporters'
    top reason the way Option B's "Who pays" carries the opponents'.
33. Option B: Florida's larger effects in middle and high schools (S); the first "What's uncertain" sentences give their
    reasons only in tags (E); move caveats to even out field lengths (E).
34. Option C: rules that differ by district as a cost, as in Option A (E); option titles in one form (E).
35. Option A's "What's uncertain": say Harrell's "very successful" wasn't measured (C).
36. The values-guide caption in the template, "In each side's own words, from the record", now introduces survey wording
    (drafter's note).
37. Layer 2: retitle "How the law describes distraction-free education" as "The law's stated goals" (E).
38. Questions to weigh: question 1 should add discipline and emergency response (C), or note that a fair test needs two
    school years (S); an optional fourth question on early suspensions vs. year-two gains (C).
39. Plain language beyond item 19: the State School Superintendent's job (no passage), the Georgia Department of
    Education, the Rollins School of Public Health, and "school district" at first mention on the Summary, Layer 1 and
    Layer 3 tabs (E); the word "sponsor"; "randomized experiment" (E); "the quoted passage" (SC); "In that study" on
    card 5 (SC); when lawmakers next meet, whether "breaks" includes lunch, and the "Cairo High" label (LR).
40. Summary tab: lead the K-8 line with the direction (SC, E); "Each district or school also picks its start date" is an
    inference (E); Cairo High's plan under "Coming up" though the 2026-27 year has begun (E); the fiscal-note tag should
    mention the undescribed LC 47 3887 entry (E); "Input" for asking a district (E); P-060's stated purpose (E); every
    decider in one four-field format (E); the students line could add school laptops (S); the law's purpose (P-057) in
    the 30-second version or "If you…" lines (S); the families line could add the health-app plan rule (C).
41. Home and display: home items "takes effect" and the Jan 1 / July 1 pair read alone (SC); "District policies" also bind
    schools (SC); the title and link text say "phone use" while the page now says "access"; Option C's chip uses the
    "bad" style (display check).
42. Verbs: "would cut" vs. "could spark" and "said he worries" vs. "told lawmakers" (C, E). The editor advises no change
    to the line that follows its source (P-071).

## Round 6 results (revision 6.1, `verification/round6/workspace.r6.1.md`)

Inputs and reports are in [verification/round6/](verification/round6/): the page, home items and `page.yaml` strings as
checked (`workspace.r6.1.md`, `home.r6.1.md`, `page.r6.1.yaml`, `passages.r6.1.md`), and every report.

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate | **Pass.** 280 units (169 cited, 74 analysis, 34 editorial, 3 computed), 0 errors, 0 warnings; home 6 units, 0 errors, 0 warnings. Reading grade: 30-second 7.7, What it means for you 8.3, L1 6.0, L2 8.4, L3 7.9 |
| 2 | Blind source check of the page (fresh agent) | **166 supports, 3 partial, 0 does not support, 0 wrong span.** 106 tags valid, 5 questionable, 0 invalid; 13 framing issues (3 medium, 10 low). **280/280 evidence spans grounded** (`check_verifier.py`) |
| 2 | Blind source check of the home items | 4 supports, 2 tags valid; 1 low framing issue; 6/6 spans grounded |
| 2 | Display check (`page.yaml` strings) | 9 supports; 1 low framing issue; 9/9 spans grounded |
| 3 | Bias panel | **Pass.** Supporter's advocate **4/5** (6 missing arguments; wording notes 3 medium, 4 low; imbalance 2 medium, 4 low; 2 label proposals). Critic's advocate **4/5** (4 missing arguments; wording notes 1 medium, 3 low; imbalance 1 medium, 4 low; 2 label proposals). Standards editor: neutrality 4/5, clarity 3/5, **no high-severity issue** (22 issues: 4 medium, 18 low; 11 flip-test notes) |
| 4 | Planted-error calibration (fresh blind verifier on a copy with hidden errors, `verification/round6/calibration/`) | **10 of 10 caught** |

**Round 6 passes** under the "when a page is done" rule: a clean gate, 0 unsupported claims, 0 wrong spans and 0
ungrounded spans, both advocates at 4/5, and no high-severity editor issue. So revision 6.2 makes only accuracy fixes and
fixes for breaches of written rules. No card claim, evidence label, values-guide line or side changes; a targeted re-check
covers the changed units, with the flip test on the changed "If you…" and "In short" lines.

## Revision 6.2: what changed (2026-11-08, targeted re-check pending)

Reason: **fixes after passing round 6**. Change class **correction**. Revision 6.1 is archived in `verification/round6/`
(`workspace.r6.1.md`, `home.r6.1.md`, `page.r6.1.yaml`, `passages.r6.1.md`). `home.md`, `page.yaml`, `sources.yaml` and
the passages are unchanged. The changed units are listed by line in `changed-units.txt` (scratch folder, for the re-check).

**Not changed:** every card claim, evidence label and side; the values guide; the 30-second version.

| # | Item | Done? | What changed |
|---|---|---|---|
| 1 | Start dates stated as settled | Done | "Decided later" and "Not known yet" now state only what the passages say: high schools must start no later than July 1, 2027 (P-007), policies are due by January 1 each year (P-008), and the law doesn't say how the two dates fit. "Not known yet" on learning says "must start the ban no later than July 1, 2027" (was "don't have to start until"). The students line's tag states the same three facts. The same correction goes into three tag notes on cards 1 and 5 that said the rule "isn't required until July 1, 2027" (tags only; one is on card 1's "In short") |
| 2 | K-8 line | Done | "HB 1009 keeps the school-day device ban House Bill 340 set in 2025 for public school students in kindergarten through 8th grade. The bill drops e-readers from the device examples and requires yearly policies." The first sentence names the students, so it stands alone on the home page |
| 3 | Card 3 "Who disagrees" | Done | Limited to Georgia: "No one in the sources we reviewed disputed that Georgia's phone rules are working." The tag notes that the English authors wrote about England (P-043) |
| 4 | P-013's limit | Done | "Decided by the law" and card 6 now add that access is allowed only "as necessary to fulfill the requirements" of the plan |
| 5 | Card 2's "In short" | Done | Cites P-034 (71.2% of supporting parents cited "Improved in-person social interactions") for the friendships half |
| 6 | Line 64's tag | Done | "P-024 gives Act 395, and P-028 and P-054 give the bill number" |
| 7 | "Must publish" | Done | Lines 42 and 48 (6.1's 40 and 46) cite P-014 with P-015 |
| 8 | Line 44's tag | Done | Now `{analysis: …}`. The district entry's matching tag (line 48) gets the same fix |
| 9 | The professor's safeguards | Done | The Layer 4 voice count says she named "rapid parent notification and guaranteed access to phones when needed" |
| 10 | Card 4 | Done | The law's emergency-communication duty is stated as P-011 says it ("clear protocols" for "all emergency communication" through school systems, coordinated by staff) and moved from "What's missing" to "What challenges it", next to the professor. "What's missing" now quotes her two safeguards as she said them, says the law requires neither, and says her words don't say whose phones. The reading about students' own phones stays, as ours. Claim, label and side unchanged |
| 11 | Card 2's England result | Done | "In short" says the study's strict schools "barred phone use for fun" (P-041). "What challenges it" no longer repeats the well-being result; it keeps the finding of no difference in overall phone and social media use. The result now appears once in "In short" and once in the authors' words |
| 12 | Florida's larger middle- and high-school effects | Done | Card 5 gets the same two sentences as card 1. On card 1, card 5 and the study notes, the caveat now reads "doesn't say which effects were larger: test scores, absences or suspensions" |
| 13 | Uneven caveats | Done | Adams's account loses "one family's experience" and keeps its factual caveat. Card 3 adds a matching one for Harrell: "Harrell's quoted passage doesn't name the high schools or say how their success was measured" |
| 14 | Parents line | Done | "By July 1, 2027, reaching your child during the school day means contacting the school, which must set emergency-message procedures." 45 words, with the dated "Not known" kept |
| 15 | "IEP" | Done | Spelled out at first use on each tab: Summary ("Decided by the law, exceptions"), Options (Option B "Who pays") and Arguments (card 6). Layer 2 already did |
| 16 | The 219-word bullet | Done | Split into "Decided by the law, timing", "…, the rule" and "…, exceptions", with the same sentences (plus items 4 and 15) |
| 17 | No-money repeats | Done | Kept in the parents and staff lines, "Not known yet", Option B's "Who pays" and card 5. Removed from Option B's "What's uncertain" (the fiscal-note sentence), the only other place |

Two sentences split for reading level, with the same words: "Decided later by each school district" and "Not known
yet" now give the dates as separate short sentences.

### Deterministic gate (revision 6.2)

| Check | Result |
|---|---|
| `check_citations.py` | workspace: 287 units (175 cited, 77 analysis, 32 editorial, 3 computed), 0 errors, 0 warnings. home: 6 units, 0 errors, 0 warnings |
| `readability.py` | 30-second 7.7 (90 words), What it means for you 8.2, Layer 1 6.0, Layer 2 8.4, Layer 3 7.9 (Layer 4 8.0, not targeted). No budget warnings and no CHECK lines. "If you…" lines 45, 45, 45 and 45 words (first sentences 18 to 22) |
| `verify_hashes.py` | PASS (15 retrieved) |
| Test render | No warnings: version 6.2, 73 passages, 12 sources. In headless Chrome, the three "Decided by the law" bullets and card 4's moved sentence display as intended |

### Where I'm least sure

1. **Card 4 (item 10).** The law's emergency-communication duty now sits under "What challenges it", where the supporter
   asked for it; rounds 2 and 3 had objected to listing the law's own requirement as a challenge. The critic's request to
   put "the law requires neither safeguard" right after the professor's quote is not taken; that sentence stays under
   "What's missing".
2. **Parents line (item 14).** To fit 45 words, "including in emergencies" left the first sentence; emergencies now come in
   through "which must set emergency-message procedures" (a hyphenated compound).
3. **K-8 line (item 2).** "Keeps the … ban" rests on P-006's locator ("existing law, unchanged") rather than its text;
   "requires yearly policies" compresses the change from the one-time Jan 1, 2026 deadline; the unknown, "your
   district's next policy", is terse.
4. **Card 2 (item 11).** "Barred phone use for fun" is the page's plain gloss of P-041's "recreational phone use is not
   permitted" (the study notes use it too).
5. **Item 1's tag edits** reach card 1's "In short" (tag only), so the flip test covers that line too.
6. **Item 13.** Harrell's "very successful" in Option A's "What's uncertain" still has no caveat; the field itself marks
   it as uncertain (open item 52).

### Open items

**Earlier items now done:** 40's start-date inference and K-8 direction (items 1 and 2). The rest of 17–42 stays open,
with these updates: 22's "only account of a real emergency" is moot now that the "one family's experience" note is
gone; 33's larger middle- and high-school effects are now on card 5, and still not on Option B.

**From round 6, not taken.** S = supporter's advocate, C = critic's advocate, E = standards editor, SC = source check.
Where reviewers disagree, both views are given.

43. **Card 2's label (S):** "Uncertain (statements, and one English study of 12- to 15-year-olds that couldn't test cause
    and effect; it found less phone and social media time during school but no evidence of a well-being difference, and
    reported nothing on friendships)". The critic agrees with card 2's label as printed (C).
44. **Card 5's label:** "Uncertain on staff burden (workload not measured; the duties themselves are in the law's text);
    supported, with limits, on early suspensions (one Florida working paper; the rise faded after year one; no Georgia
    results yet)" (S). Or keep the parts and add "designed to estimate cause and effect; ban scope not described" (C),
    or "designed to estimate cause and effect; rise faded after year one" (E).
45. **Card 1's label:** add "ban scope not described" (C).
46. **Legend (E, medium):** the template's legend lists Mixed, Contested and Values question, which the printed rule
    doesn't define. **Not applicable:** the page's legend shows only the labels the page uses. Card 4's values part as a
    label (E) would be a label change.
47. **Card 6's label wording:** "Not mainly factual", matching the legend, and "our sources don't measure" for
    "untested" (E).
48. **Card 4's title:** "…, even in emergencies" (C). A claim change.
49. **"If you…" lines:** the high school lines list only duties and cost-type unknowns; add the law's aim (P-057) and
    unknowns that run both ways ("whether class distractions change") (S, E). The students line should say "access"
    includes holding or wearing, the ban covers breaks, and give the consequences (C, E). "Who pays for phone storage"
    assumes a cost (S); round 5 asked for that honest unknown (SC).
50. **No-money point:** keep it only in "Not known yet" and Option B's "What's uncertain" (S); check the state budget or
    move the caveat (E). This revision keeps the six places the brief named.
51. **Teachers:** Harrell (P-045) and Woods (P-053) on teachers focusing on teaching, on card 1 and the staff line (S). On
    card 5, give Harrell's point in full (S), or drop or move it (E). Card 5's "police" and "worries" (E).
52. **Harrell in Option A:** "What's uncertain" quotes "very successful" with no caveat (C, E).
53. **Card 2:** the student's "phones have become addictive for teens" (S); the England study's scope against Georgia's
    all-access ban (S); in-school phone time under "What supports it" (SC, and earlier item 2).
54. **Options A and B's England sentence:** add "no evidence of less phone use overall" (C). The supporter notes it sits
    in both slots that favor supporters, and that Option A's avoided suspension rise lacks the fade note (S).
55. **Survey reasons:** "Fewer cited" splits the opponents' reasons (C); the 60.3% safety reason is on no card (S); the
    opponents' top reason appears twice in Layer 3 and the supporters' once (E); card 6's survey shares under "What
    supports it" (S).
56. **Card 5:** the law requires "progressive" consequences and reviews tied to "educational goals and student
    well-being" (S); the Florida abstract doesn't say what consequences its bans used (S).
57. **Card 3:** its date challenge doesn't bear on the high schools Harrell described (S).
58. **Card 1:** absences "may explain a large fraction" of the gains, so the study can't say how much came from fewer
    distractions (C); move the larger-effects caveat to "What's missing" (S).
59. **Option B:** the loss of local choice and the gain of one statewide rule, both as analysis (C). Option C: rules that
    could differ by district, as in Option A (E).
60. **Voices:** 20 House members voted no and none are quoted (C, E); the English authors count as voices (E). Modal verbs
    in the Layer 4 opening follow the news report (E).
61. **Questions to weigh:** add discipline (C) and staff workload and suspensions (E); "keep students safe and families
    informed" (E); a contact-versus-class-time question (S, optional).
62. **Values guide:** "reaching your child directly in an emergency" (S); "your child's focus and learning" (E); the
    intro and Option C notes as plain lines, not bullets (E); a note that both sides cite safety (SC). The guide doesn't
    name Option B (SC).
63. **Who decides:** one set of fields for every decider, and what the governor can still decide (E); retry archiving a
    readable General Assembly contact page (C, E).
64. **Precision:** "Public high schools must start the ban" in the 30-second version; "districts" also means schools (SC).
    Line 78's reading of "all" should say "by our reading" (SC, E). The P-060 events are "designed to promote
    distraction-free education" (SC, E). The Department of Education item has no date (SC, E). The amendment also
    dropped "transitions" and named "other public schools" (SC). "All-day" for "bell-to-bell" and P-051's "after
    districts reported" clause (SC). Kemp's quote covers several bills (SC). The fiscal-note tag should name LC 47 3887
    (E, earlier 40). Tags on "can change the law" lines should be analysis (E).
65. **Plain language:** "The same goes for" in Option B (SC, E); "In that study" on card 5 (E); Rollins tied to Emory,
    Cairo High as a public high school, "school system" (E); "standard" questionnaire (SC).
66. **Home and display:** "became law, extending … to high schools" read alone (SC home); "phone use" in the title (display
    check, and earlier 41).

## Targeted re-check results (revision 6.2, `verification/round6-targeted/`)

Revision 6.2 is archived there as `workspace.r6.2.md`, with the unit list the checkers received (`changed-units.txt`).

| Check | Units | Result |
|---|---|---|
| Blind source check of the changed units (fresh agent) | 50 | **24 supports, 26 tags valid, 0 partial, 0 does not support, 0 wrong span, 0 questionable.** 50/50 evidence spans grounded (`check_verifier.py`). Nothing misstates a passage. Framing: 1 medium (the K-8 line's e-readers sentence), 9 low |
| Editor's flip test on the changed values-guide, "If you…" and "In short" lines | 14 lines | Neutrality 4/5, no high-severity issue; 2 medium (card 5's "In short" lacks card 1's Georgia caveat on the same study; the values guide gives "learning", which both sides claim, to supporters only), 4 low |

Under the "when a page is done" rule the re-check passes on accuracy. Revision 6.3 takes the two medium flip-test
points, the medium framing note and a few wording fixes; the other low notes are open items 67–74.

## Revision 6.3: what changed (2026-11-08, re-check pending)

Reason: **last fixes after the targeted re-check**. Change class **correction**. No card claim, evidence label or side
changes. `home.md`, `page.yaml`, `sources.yaml` and the passages are unchanged. Line numbers are unchanged from
revision 6.2; the changed units are listed in `changed-units.txt` in the 6.3 scratch folder.

| # | Item | Done? | What changed |
|---|---|---|---|
| 1 | K-8 line (L17) | Done | "HB 1009 keeps House Bill 340's school-day device ban for public school students in kindergarten through 8th grade." (18 words, stands alone; no "set in 2025") "The bill drops e-readers from its list of examples, which isn't complete, and requires yearly policies." The line is 45 words |
| 2 | Card 5's "In short" | Done | Ends "We found no Georgia high school results or workload data yet." (40 words), with a tag covering both points, as card 1 does for the same study |
| 3 | Values guide, supporters' line | Done | "If you care most about your child's focus in class:" (32 words), with the survey quote unchanged. The opponents' line is unchanged (35 words) |
| 4 | Students line (L14) | Done | "Not known as of Nov 8, 2026: when your school will start the ban." To stay at 45 words, the exceptions sentence now reads "Exceptions: your disability or medical plan explicitly requires access, or another law does." |
| 5 | Parents line (L15) | Done | "By July 1, 2027, reaching your child during the school day means contacting the school, whose staff coordinate all emergency messages." (P-011(A)). The "Not known" part keeps the money point and the state-budget caveat, joined by semicolons to fit 45 words |
| 6 | Card 2's "In short" (L165) | Done | "in-person social lives" for "in-person friendships" (P-034, P-051). The card's title is unchanged |
| 7 | L180's tag | Done | Cites P-041 for England |
| 8 | "Quoted passage" (L182, L194) | Done | "The news report quoting Harrell doesn't name the high schools…"; "The news report doesn't say when two of Adams's daughters were in a lockdown, or what phone rules applied" (P-049) |
| 9 | L24's repeat | Done | Removed L24's two date sentences; "Decided by the law, timing" (L21) keeps them, so the district bullet no longer implies that districts pick the start date |

### Deterministic gate (revision 6.3)

| Check | Result |
|---|---|
| `check_citations.py` | workspace: 285 units (174 cited, 76 analysis, 32 editorial, 3 computed), 0 errors, 0 warnings. home: 6 units, 0 errors, 0 warnings |
| `readability.py` | 30-second 7.7, What it means for you 8.3, Layer 1 6.0, Layer 2 8.4, Layer 3 7.9 (Layer 4 8.1, not targeted). No budget warnings and no CHECK lines. "If you…" lines 45 words each (first sentences 18–21); values lines 32 and 35; card 2 and card 5 "In short" 40 each |
| `verify_hashes.py` | PASS |
| Test render | No warnings: version 6.3, 73 passages, 12 sources. The home card shows the four "If you…" first sentences as intended |

### Where I'm least sure

1. **Students line.** "Exceptions: your disability or medical plan explicitly requires access, or another law does." is terser
   than before; it was shortened so the brief's "when your school will start the ban" fits in 45 words.
2. **Parents line.** "Whose staff coordinate all emergency messages" states P-011(A)'s "all" and "coordinated by school
   personnel" but not the word "system"; the budget allowed no more.
3. **K-8 line.** "Its list of examples" relies on the first sentence's "device ban" for what the examples are.

### Open items (added by the targeted re-check)

67. "The July start date" in "Decided by the law, timing" calls a no-later-than deadline a start date; say "the July 1, 2027
    deadline to start the ban" (SC).
68. "Policies are due by January 1 each year" in "Not known yet" doesn't say whose policies; that bullet also repeats the
    dates a second time (SC).
69. Option B's plan sentence lacks P-013's "as necessary" limit, which "Decided by the law" and card 6 now carry (SC,
    optional).
70. The Layer 4 voice count: cite P-037 directly, name Gazmararian, and use her "concerns about emergency access" (SC).
71. The Florida caveat "doesn't say which effects were larger" presumes only some were; "doesn't say whether that holds
    for each effect" is more exact (study notes, cards 1 and 5); the study notes' tag should add P-005 (SC).
72. Card 4: "send" is firmer than the law's "Facilitate" (SC, optional). Card 2: "barred phone use for fun" can be misread;
    "where students couldn't use phones for fun" (E).
73. Flip test, low: card 5's "In short" could state the law-backed duty in our voice, as card 4 does (E); the staff line's
    unknown could run both ways ("how your workload will change") (E); the parents line's "who pays for phone storage"
    could read "whether storage costs families anything" (E).
74. Option C's chip uses the "bad" style in `page.yaml`; drop it or use a neutral style (E; earlier items 41 and 66).

The processing notes on the lawmaker lookup and the district directory (lines 46 and 48) are recorded as blocked sources
S21 and S23 in `sources.yaml`.

## Targeted re-check 2 results (revision 6.3, `verification/round6-targeted-2/`)

Revision 6.3 is archived there as `workspace.r6.3.md`, with the unit list the checkers received (`changed-units.txt`).

| Check | Units | Result |
|---|---|---|
| Blind source check of the changed units (fresh agent) | 12 | **5 supports, 4 tags valid, 1 partial, 2 questionable, 0 does not support, 0 wrong span.** 12/12 evidence spans grounded (`check_verifier.py`). No unsupported claims. The partial: the students line's "Exceptions:" sentence dropped P-013's limit, and its "disability plan" gloss was untagged. The two questionable tags: "the news report doesn't say…" claimed more than the archived excerpts (P-046, P-049) can show. Framing: 1 medium (the untagged gloss), 5 low |
| Editor's flip test on the changed values-guide, "If you…" and "In short" lines | 14 lines | Neutrality 4/5, no high-severity issue; 1 medium (card 5's "In short" put the law's enforcement duty inside "opponents say", while card 4 states its legal fact in our voice), 3 low |

## Revision 6.4: what changed (2026-11-08, re-check pending)

Reason: **fixes from the second targeted re-check**. Change class **correction**. No card claim, evidence label, side or
values-guide line changes. `home.md`, `page.yaml`, `sources.yaml` and the passages are unchanged, and line numbers are
unchanged from revision 6.3. The changed units are listed in `changed-units.txt` in the 6.4 scratch folder.

| # | Item | Done? | What changed |
|---|---|---|---|
| 1 | Students line (L14), partial | Done | "By July 1, 2027, you can't access phones or similar devices during the school day." ("with some exceptions" dropped as redundant.) "Exceptions: only the access your disability or medical plan explicitly requires, or another law requires." This sentence is now tagged `{analysis: …}`: the tag gives P-013's "as necessary to fulfill the requirements" limit and P-007's "required by law", and says "disability plan" is our plain-language term for an IEP or Section 504 plan, as at L23. 44 words |
| 2 | L182 and L194, questionable | Done | Scoped to the archived excerpts: "Harrell's remark, as reported, doesn't name the high schools or say how their success was measured." and "Adams's account, as reported, doesn't say when two of his daughters were in a lockdown, or what phone rules applied." |
| 3 | Card 5's "In short" (L201), flip test medium | Done | "Schools must enforce the ban. [P-014] Staff policing phones can invite conflict, opponents say." The Florida sentence is kept. The last sentence reads "We found no measured Georgia high school results or workload data.", matching card 1's "measured"; "yet" was dropped to stay at 40 words. Its tag adds card 1's note on Harrell's unmeasured claim (P-046) |
| 4 | Card 4's "In short" (L189), low | Done | "Phones can be critical for families in an emergency, opponents say." The attribution now comes last, as on the other cards |
| 5 | Parents line (L15), low | Done | "…means contacting the school; staff must coordinate all emergency messages." The no-money note is in parentheses so it doesn't read as an unknown: "Not known as of Nov 8, 2026: who pays for phone storage (the law sets aside no money; we didn't review the state budget)." 45 words |
| 6 | K-8 line (L17), low | Done | "The bill drops e-readers from its incomplete list of example devices and requires yearly policies." 44 words |

### Deterministic gate (revision 6.4)

| Check | Result |
|---|---|
| `check_citations.py` | workspace: 286 units (174 cited, 77 analysis, 32 editorial, 3 computed), 0 errors, 0 warnings. home: 6 units, 0 errors, 0 warnings |
| `readability.py` | 30-second 7.7, What it means for you 8.4, Layer 1 6.0, Layer 2 8.4, Layer 3 7.9 (Layer 4 8.0, not targeted). No budget warnings and no CHECK lines. "If you…" lines 44, 45, 45 and 44 words (first sentences 15–21); "In short" lines 29–40 |
| `verify_hashes.py` | PASS |
| Test render | No warnings: version 6.4, 73 passages, 12 sources |

### Open items (added by targeted re-check 2)

75. **Card 2's title** says "friendships", a word no passage uses (P-034 says "in-person social interactions"; P-051 "less
    isolation"). Titles are claims, so it stays unchanged after a passing round; change it in the next full revision
    (SC). The "In short" now says "social lives", which is still slightly broader than the sources (SC).
76. The staff line gives "the law sets aside no money" without the state guidance the page pairs it with elsewhere
    (P-016) (E).
77. The K-8 line doesn't say its changes apply from Dec 31, 2026, with the first yearly policy deadline on Jan 1, 2027; "its"
    in "its incomplete list" points to the bill, not the law's device definition (SC).
78. The parents line: the contact-the-school rule is part of each policy due January 1, so it may apply before July 1,
    2027; its tag says "each district" for "each local school system and public school" (SC).
79. L180's tag could say the English authors studied schools in England (P-041) and didn't discuss Georgia, with their
    general conclusion (P-043) on card 2 (SC, optional).
80. The supporters' values label paraphrases its quote more loosely than the opponents' label does; the editor judged it
    justified and asked for no change (E).
