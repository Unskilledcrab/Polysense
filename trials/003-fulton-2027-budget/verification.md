# Verification: Trial 003 (Fulton County 2027 budget)

Method: the AI-only [verification protocol](../../docs/process/verification-protocol.md). There is no human in
the loop ([ADR 0014](../../docs/adr/0014-ai-only-verification.md)). The independent checkers were run by the
coordinating session, not by the drafting agent. Their reports are copied unchanged into
[verification/round1/](verification/round1/), [verification/round2/](verification/round2/),
[verification/round3/](verification/round3/) and [verification/round4/](verification/round4/) (sha256 below). The planted-error copy and its answer key stay with the
coordinating session. Nothing on this page may be called "verified" until a full round passes.

## Round 1 (revision 1, `verification/round1/workspace.r1.md`)

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate (`check_citations.py`, `verify_hashes.py`, `readability.py`), run by the drafter | **Pass.** 234 units (151 cited, 41 analysis, 27 editorial, 15 computed), 0 errors, 0 warnings. 33/33 hashes match. Reading grade 6.6 / 7.8 / 7.4 / 6.3 for the 30-second version and Layers 1-3 |
| 2 | Blind source check of the real page (fresh agent, page + passages only) | **128 supports, 23 partial, 0 does not support, 0 wrong span.** 10 questionable tags; 19 framing issues. **234/234 evidence spans grounded.** No wrong numbers, dates, roll calls or arithmetic |
| 3 | Bias panel | **Fail.** Supporter's advocate 3/5. Critic's advocate 3/5. Standards editor: neutrality 3/5, clarity 3/5 (36 issues: 3 high, 14 medium, 19 low; 20 flip-test asymmetries) |
| 4 | Planted-error calibration (fresh blind checker on a mutated copy) | **12/12 caught.** Its report also raised three real issues in the true text (lines 21, 44 and 183), which are used below; its other flags concern the planted errors and were not used |

Reports: `source-check.json` (2862471072895566…), `source-check-cal.json` (c10d9404e14cb1c6…), `panel-supporter.json`
(7789666b031a6d1b…), `panel-critic.json` (a93e64239d76f395…), `panel-editor.json` (e74bbd3a45fc1422…).

**What the panel agreed on.** Both advocates rated the page fair in tone but said their side was not at full strength.
The editor found imbalances running both ways, not one tilt. The three high-severity editor issues were the evidence
labels, card 6's missing county evidence, and "no reasons" claims that didn't disclose the unreviewed meeting video.

### What round 1 found, and what we did

Codes: **SC** blind source check (line), **CAL** calibration checker, **SUP** supporter's advocate, **CRIT** critic's
advocate (M = missing argument, U = unfair wording, I = imbalance, L = label), **ED** editor issue, **FT** flip test,
**COORD** the coordinator's round-1 note. Card numbers are revision 2's; revision 1's cards 2 and 3 swapped places.

| # | Finding (who raised it) | Disposition in revision 2 |
|---|---|---|
| 1 | Evidence labels didn't follow one visible rule: card 2's "Supported, with limits" was undefined; card 4 got a flat "Uncertain" and card 6 "Uncertain" despite county records; card 3 (now 2) had no "uncertain" part (ED 1 high, ED 2, FT 5; SUP I3, SUP L cards 3-4; CRIT I1, CRIT L cards 1-4 and 6; SC F11) | **Fixed.** "How to read these cards" now prints one rule: "Supported" needs a record that settles the point (the rule's own text for what a rule says; for money, needs or effects, a county record not written by the claimants); statements or requests alone are "Uncertain"; multi-part claims get a label per part. All six labels were re-set by it. Card 1 adds "uncertain ... on whether all commissioners were asked for input (disputed)". Card 2 is "Supported on who serves and how they were chosen; uncertain on how much they shape the draft". Card 3 splits the noticed hearings (supported) from the comparison with past years (uncertain). Card 4 is supported on the code and the 2026 record, uncertain on the limit's terms and the draft. Card 6 is "Supported for the jail; uncertain for the courts and prosecutors". **Choice:** CRIT proposed "disputed in practice" for card 1; it appears as "uncertain ... (disputed)", because the dispute rests on a statement in a resolution that was filed, which the rule treats as a statement |
| 2 | Claim types applied unevenly (ED 13, FT 5) | **Fixed.** The rule for "with a values question" is printed, and cards 1-4 and 6 carry it |
| 3 | Card 6 left out the county's own consent-decree evidence; cited P-049 only for the half against it; "no independent data" overstated for the jail (ED 3 high, FT 7; CRIT M5, I3, L card 6; SC F17, SC 187; COORD) | **Fixed.** "What supports it" now has the jail agreement's required reforms incl. "improving staffing and supervision" (P-048), P-049's next sentence ("additional resources will be needed for consent decree compliance"), and "expected to grow" (P-050). "What's missing" is limited to the courts and prosecutors. The 48-cent share left "What challenges it" (SC F17: scale is not adequacy) |
| 4 | The Board funded justice needs above the 2026 draft: consent decree +$18,157,000; solicitor-general $14,605,358 → $16,149,063 (CRIT M6; COORD) | **Fixed** in card 6 and Option B (`{computed}` from P-054), with the fairness note that decree costs were "still being vetted" when the proposal was written (P-050) |
| 5 | "No reasons given" lines didn't disclose that the linked meeting video (P-025) and the Nov 4 budget discussion (P-038) were not reviewed; the tag "P-025 holds motions and votes only" was wrong (ED 4 high, FT 6, FT 16; SC 105; COORD) | **Fixed.** One note in the Layer 4 intro says the records link the video and we did not review it, so reasons may have been given there. The repeated lines in Option C, card 4 and the intro were removed. Recorded as S16 (not reviewed) in sources.yaml |
| 6 | "The reasons for the process are thinly documented" ignored the adopted resolution's stated purpose (SUP U 111; SC F8, SC 111) | **Fixed.** The page now says the sponsors' resolution states their reasons; what no archived record gives is a reason for the 5% limit or for filing the resolution to remove it |
| 7 | "Failed" misused: the motion to approve failed Oct 21; the resolution was filed Nov 4 (SC 20, 117, 160, F3; COORD) | **Fixed** everywhere (Layer 1, the Option C heading and chip, the Layer 4 intro, card 4) |
| 8 | Oct 7 timeline: "skip" read as shirking; the written 2027 requests (P-080), the offices' hearing slots (P-043, P-044) and Ellis's same-day reply were missing (CRIT U 18 high, CRIT M1; ED 6, FT 2; SC F2; SUP M4, SUP U 18; COORD) | **Fixed.** "Said they would not present in person"; FOX 5's report of written requests; hour-long slots for all six offices on Oct 13, 14 and 22; "Ellis answered their objections in both news reports" |
| 9 | 30-second version gave only the critics' position, named only the critic commissioner, and ran about 135 words (SUP U 8; ED 5, ED 16, FT 1; SC F1; COORD) | **Fixed.** One clause per side, no commissioner named for either; 3 items, about 80 words |
| 10 | Gammage's "Each year we are underfunded" sat in the 5% card, where it doesn't address the limit (SUP U 160, SUP I4; CRIT verdict; FT 11; SC F14; COORD) | **Fixed.** Moved to card 6 |
| 11 | The 4-1 filing vote sat under "Who disagrees" as if it were an argument (CRIT U 162; SC F15; COORD) | **Fixed.** Removed from the card; the vote stays in Layer 1 and Option C |
| 12 | Ellis's "no talk of an early vote" sat under "Who disagrees" on the January card (CRIT U 174; ED 26; FT 13; SC F16) | **Fixed.** Moved to "What's uncertain" |
| 13 | Ellis's standard-committee sentence was cut to its second half; his openness claim was narrowed (SUP M3, U 150, U 135; COORD) | **Fixed.** The whole sentence is quoted in card 2 (two quoted parts, every word kept); card 3 says "open meetings that all commissioners and their staff can attend" |
| 14 | Labat's "hijack" quote appeared twice (SUP U 148; FT 12) | **Fixed.** Card 1's "Who disagrees" now uses the coalition's objection to presenting "to a two-commissioner panel rather than the full board" (P-076); "hijack" appears once, in card 2 |
| 15 | The critics' main objection was never stated in their terms; the code's depth of the two members' role (P-013) and the first half of Ellis's FOX 5 sentence ("solely responsible for drafting recommendations") were missing (CRIT M2, U 147, I4, I6; COORD) | **Fixed.** In the 30-second version, card 1 "Who disagrees", and card 2 "In short" and "What supports it" (Ellis's FOX 5 point stays a paraphrase, not a quote) |
| 16 | Self-selection should lead the critics' card, with Barrett's "only they qualify" quote there; Johnson spoke "on behalf of the coalition" (CRIT M3, U 148, L card 3) | **Fixed.** Card 2 "In short", "Who says it" (both Barrett quotes; "Johnson spoke for the group") and "What supports it" (the two sponsored the rules; neither needed a nominating vote) |
| 17 | The 1953-2018 budget-commission history (P-002, P-003) was never cited; a third supporter card was proposed (SUP M2, I2; COORD) | **Partly fixed.** The history is now in Layer 1, Layer 2 ("How the budget is drafted") and card 1. **Not adopted:** a separate third card, because the history and the stated purpose come from the same resolution; splitting one documented argument into two cards would pad that side |
| 18 | Support beyond the two members was not shown: Pitts and Thorne, not on the commission, voted yes three times; Thorne's district had no other eligible member (SUP M1, I1 high; COORD) | **Fixed.** Card 2 "What challenges it" (the eligibility point is tagged `{analysis}`), and Layer 1 now names every vote |
| 19 | Membership rules shown only through their 2027 result; eligibility stated as absolute (SUP M5; SC 58, 149) | **Fixed.** Layer 2 gives the general rules: selection each cycle at a noticed meeting (P-009), eligibility and the fallback when no one is eligible (P-010), and the vice-chair rule (P-011) |
| 20 | Code subsection picked only facts that fit the critics: the code neither allows nor forbids a cap; Resolution 26-0729's proposed ban was filed; P-012 (the county manager and CFO solicit departments' proposals) was missing; "neither code section" implied only two sections exist (SUP M6, U 76, L card 4; ED 20, FT 20; SC 76) | **Fixed.** "Neither Section 2-261 nor Section 2-263 mentions a cap on requests, either to allow one or to forbid one", followed by the proposed ban and its filing, and P-012 under "How the budget is drafted" |
| 21 | The code builds in accountability to the full Board: P-007's purpose, P-013's reporting duty, the Nov 4 update item (SUP M7) | **Fixed** in card 1; P-038 is cited for the agenda listing only, not for what was said |
| 22 | The 2026 proposal's own "1% decrease across all agencies and departments" (P-050) is context for the GFOA quote (SUP M8; ED 24; SC F18; CRIT verdict) | **Fixed** in Layer 2, labeled a spending cut, not a limit on requests |
| 23 | Fiscal context for the limit, without making it anyone's stated reason (SUP M9, I5) | **Fixed.** Card 4 "What challenges it" says no source gives the commission's reason, then cites "many unmet requests" (P-049) and the reserve margin (`{computed}`) |
| 24 | The limit was described too broadly ("requests" instead of requests for extra money); both descriptions come from critics; no source disputes it exists; "limit" vs. "cap" (SUP U 52; ED 19; CRIT U 52) | **Fixed.** "Requests for extra money", the two describers named, "no source we reviewed disputes that the limit exists"; "limit" in the page's voice, "cap" in the resolution's |
| 25 | Barrett's charge that the commission broke its duty needed the resolution's outcome and the absence of any record either way (SUP U 127) | **Fixed** in card 1 ("What challenges it" and "What's missing") |
| 26 | "Critics say" credited arguments only Resolution 26-0729 makes; "hidden" in the page's voice (SUP U 159, I4; SC 159; ED 23, FT 19; SC F13) | **Fixed.** "Resolution 26-0729 says ..."; the justice leaders' concern is reported separately; "hidden" removed |
| 27 | GFOA's words narrowed from "spending cuts or caps" to "caps"; "effectively" dropped from "impossible" (SUP U 160; ED 24; SC 160) | **Fixed.** "Spending cuts or caps" and "effectively" are quoted |
| 28 | The resolution's policy case (legacy spending; Pew; the -4% to +43% spread) was missing (CRIT M4; COORD) | **Fixed** in card 4, attributed to the resolution |
| 29 | Card 4 turned the critics' "only ... in narrative form" into a mitigation ("may still"), and ended with an author-drawn rebuttal (CRIT U 163 x2, I2). **Contradiction:** SUP wanted the narrative-form point shown as a partial answer (SUP U 103, L card 4) | **Fixed, with a choice.** The point appears once, in "What challenges it", in the resolution's own framing ("but only 'in narrative form'"). The public defender's figures moved to "What supports it" ($4,720,447, about 3.3 times 5% of its 2026 budget); the rebuttal sentence was removed |
| 30 | A flat cap does not separate contractual or legally required costs from optional ones (CRIT M7) | **Fixed** as unattributed `{analysis}` in card 4 (P-064, P-062; the 2026 process sorted requests by "contractual, regulatory, statutory" drivers, P-049) |
| 31 | Ellis's "more transparent and open than anything we've had in recent years" was unchecked, while the critics' figures were flagged; the 2026 cycle also held formal hearings (CRIT M8, I5) | **Fixed.** Card 3 "What challenges it" (P-049) and "What's missing" ("We did not check Ellis's comparison ...") |
| 32 | The draft carries weight: it serves "as presented", and the 2026 adopted General Fund was within about 0.4% of the proposal (CRIT M9) | **Fixed** in card 1 "What challenges it" and Option B (`{computed}`) |
| 33 | Barrett objected by Aug 19 (removed from the consent agenda; the only no vote) (CRIT M10) | **Fixed** in Layer 1 |
| 34 | Critics' public-interest framing (P-069; Labat's "unfair to county agencies and taxpayers") (CRIT M11) | **Partly fixed.** P-069's warning is in card 6. **Not adopted:** FOX 5's paraphrase of Labat, since card 2 quotes Labat's own words |
| 35 | "Jail crowding" softened the source's "overcrowding"; "what they describe as" hedged only the critics (CRIT U 88; ED 22, FT 3) | **Fixed.** "Citing ... jail overcrowding" |
| 36 | "Noted" (a verb that presumes truth) for Ellis only (ED 21; FT 4; SC F10) | **Fixed.** "Said" for every speaker |
| 37 | The jail agreement was softened in paraphrase (ED 12; FT 8; SC F7) | **Fixed.** Quotes "to address constitutional violations at the Fulton County Jail" and "mandated the County implement specific reforms" |
| 38 | "Observe" pointed only to one side's materials; "what you want the budget to pay for" assumed more spending (ED 7; FT 9) | **Fixed.** Pointer to both resolutions; "fund, cut or keep the same", plus "what you think of how it is being prepared" |
| 39 | District 4 is vacant, so "tell your district commissioner" doesn't work there (ED 8) | **Fixed.** District 4 residents are pointed to the chairman, who serves at large |
| 40 | Options lacked their strongest case: Option A's "Who benefits" restated the process rationale; Option C's "Who pays" argued it cost nothing; Option B covered only additions (ED 9, 10, 11; FT 10) | **Fixed.** Conditional, record-based cases in both directions; "spent no money" moved to Option C's "What it does"; Option C's costs: less than four weeks to gather requests, about $222 million in 2026-cycle extra requests, "no money for program additions" |
| 41 | The adopted measure was "the Board's resolution", the failed one "Barrett's resolution" (FT 17; CRIT I8) | **Fixed.** "Resolution 26-0460 (Ellis, Abdur-Rahman; adopted Aug 5, 2026)" and "Resolution 26-0729 (Barrett; its motion to approve failed Oct 21, and it was filed Nov 4)" |
| 42 | Supporter cards always came first (CRIT I7) | **Partly fixed.** Cards are paired by topic (1-2, 3-4). Within each pair the case for the current process comes first, then its challenge, and the rule is printed. Alternating which side leads was not adopted: a fixed rule is easier to check than an alternating one |
| 43 | Card 5 was labeled a criticism of the process; card 6 named a speaker group (SUP I6, L card 5; ED 27) | **Fixed.** Sides now name positions: "asks for a January vote", "asks for more justice funding" |
| 44 | The funding question had only one side (ED 27) | **Fixed.** The Layer 4 intro says no 2027 source we archived argued against more money for justice offices |
| 45 | Questions to weigh: Q1 undersold the design and left out how members are chosen; Q2 set "limited" against "see every request"; Q3 left out the reason for waiting; no funding question (SUP; CRIT; ED 14, 15; FT 18; SC F19; COORD) | **Fixed, with a choice.** Q1: "staff with one commissioner from each half of the county", the full Board votes either way, "how should they be chosen?". Q2: parallel wording with "larger needs described in writing". Q3: the funding question ED proposed, which also covers SUP's optional savings question. **Choice:** the brief allows 2 or 3 questions; the timing question was dropped (CRIT proposed refining it, SUP accepted it) because card 5 has no documented opposing side |
| 46 | Relative dates ("next year", "last year") (ED 34) | **Fixed.** "In 2027", "the 2026 cycle", "the 2026 budget message" |
| 47 | "Last year's proposal" cited the adopted budget book (S09b), not the proposal (ED 25; SC 71, 77, 139, 161, 187; COORD) | **Fixed.** P-046 was widened to the book's statement that it reprints "the budget message submitted to the Board as part of the Fiscal Year 2026 Proposed Budget"; sentences say "the budget message sent with the 2026 proposal" or "the 2026 budget book" |
| 48 | Joined statements: the appendix (P-049) and the $158M/$64M request totals (P-051) read as one (ED 25; SC 161, 77) | **Fixed.** Split; the totals are "beyond the requests already in the plan" and came from "departments, agencies and outside groups" |
| 49 | Quote fidelity: a straight apostrophe where P-028 has a curly one; lowercase quotes cited to all-capital P-027 (ED 29) | **Fixed.** Exact characters; the transparency quotes now cite P-024 |
| 50 | P-042 cited for the January deadline; P-031 (a filed resolution) for the Nov 15 duty (ED 30) | **Fixed.** Deadlines cite P-005 alone; where a year is added, the sentence is tagged `{analysis}` because the code gives dates without years. P-031 is cited only when describing the resolution |
| 51 | "No later than Jan 20, 2027" stated an expected date as firm (ED 31) | **Fixed.** "Expected by Jan 20, 2027 (deadline: the second regular meeting in January)"; home.md matches. The coordinator had already changed the page.yaml chip |
| 52 | "Nov 15 falls on a Sunday" was left hanging (ED 32) | **Fixed.** The 2026 cycle's deadline also fell on a weekend (Saturday, Nov 15, 2025); that proposal was provided that day and presented Nov 19 (P-046); the code has no weekend rule |
| 53 | The Layer 4 intro repeated Layer 1's votes (ED 33) | **Fixed.** One sentence points to What happened |
| 54 | "What's uncertain" lines named uncertainty without a reason (ED 35) | **Fixed.** Each now gives one |
| 55 | "In short" lines and labels ran long (ED 36) | **Partly fixed.** Labels shortened. "In short" stays two or three short sentences (30-41 words), because style guide section 8 requires the claim and then its evidence status |
| 56 | Cards near grade 10-12; undefined jargon (ED 18; COORD) | **Fixed.** Layer 4 grade 10.6 → 8.6. Defined in place: operating budget, capital requests, minimum reserve, base budget (P-086, the county's own definition), consent decree, at large, abstain, solicitor-general, probate court (P-087) |
| 57 | The decision block ran about 99 words before its list (ED 17; COORD) | **Fixed.** 64 words, then 5 one-clause items; eligibility and the limit's sourcing moved to subsections |
| 58 | Calibration flags: "led by the vice chair" (the agenda shows only "(Abdur-Rahman)"); "no date for the public hearing had been posted" (only one page checked); the presentations "describe these needs" though none mentions jail crowding (CAL 21, 44, 183; SC 21, 44, 183, F5; ED 28) | **Fixed.** "A discussion item under the vice chair's name"; "On Nov 8, ... its Public Notices page had no budget hearing date"; card 6 now separates county records (the jail) from the offices' own accounts |
| 59 | "Filing ends work on an item" turned one record into a general rule (SC 21; ED 28) | **Fixed.** "Filing set the resolution aside without approving it, and its record shows no later action" |
| 60 | "Writes the draft" and "to draft the county budget" overstate the commission's role ("to assist in the development"; P-012 keeps preparation with the county manager and CFO) (SC 8, 16, 42) | **Fixed.** "Helping draft", "help develop", citing P-005 and P-007, and P-012 in Layer 2 |
| 61 | "The jail" in the 30-second version had no jail passage (SC 8) | **Fixed.** The tag now cites P-060 (the sheriff's jail staffing) with P-054 |
| 62 | "Already serves" before the plan exists; "not public" broader than one page (SC 85, 86, 128) | **Fixed.** "Once submitted"; "not on the county's budget page on Nov 8" |
| 63 | "Changes ... before the vote on Jan 21" (SC 94) | **Fixed.** "The Board adopted the amended budget on Jan 21" |
| 64 | "The public would see every request" went beyond P-030 (SC 103; SUP U 103) | **Fixed.** "The proposed budget would list every request, rather than leaving larger needs to be described 'in narrative form'" (P-029, P-030) |
| 65 | Definite counts: "voted four times"; "three justice leaders ... criticizing it" (Gammage spoke on funding) (SC 109, 111) | **Fixed.** The count sentence was replaced; "two justice leaders and one commissioner criticizing it, and a third justice leader on funding" |
| 66 | The vice chair "had not answered" read as evasive; her role came from uncited passages (SC 111; SC F9) | **Fixed.** "Atlanta News First said it was still waiting to hear back from the vice chair" (P-073, P-021) |
| 67 | A filed proposal presented as a live choice (SC 81) | **Fixed.** "The main choices are to adopt the proposal as it is or to change it first"; Option C is introduced as filed |
| 68 | A factual comparison inside an `{editorial}` tag (SC 141) | **Fixed.** "Whether the Nov 15 draft will list requests above the 5% limit"; the 2026 comparison moved to cited sentences in card 3 |
| 69 | The tag "none of the archived statements discusses it" was false (SC 162) | **Fixed.** "None of Ellis's archived statements, or Resolution 26-0460, does" |
| 70 | Votes were named inconsistently, and only Ellis on the Oct 21 losing side (SC F4; FT 14) | **Fixed.** Every member's vote is named on every roll call |
| 71 | The sheriff's line excludes the $50 million consent-decree line (SC F6) | **Fixed** in Layer 2 |
| 72 | Barrett's resolution under card 3's "Who disagrees" overstated the disagreement (SC F12) | **Fixed.** The card says the resolution does not say the meetings were closed; its disagreement is about disclosing requests |
| 73 | Only justice offices listed as affected (FT 15) | **Fixed.** Libraries and senior services added |
| 74 | Voices and space favor critics; keep the disclosure, add the video caveat, don't pad (FT 16) | **Fixed** as the editor proposed |
| 75 | The page's own summary sentences affirmed supporters and qualified critics (CRIT I2) | **Fixed.** Every card's "In short" now ends with the same kind of evidence-status sentence |
| 76 | "The full Board decides" was restated about seven times, the critics' matching point never (CRIT I6) | **Fixed.** Fewer restatements; the draft's weight and the panel objection now appear |
| 77 | No option reflects the critics' main objection (who drafts; to whom departments present) (CRIT I9) | **Not adopted as an option.** No formal proposal is on the record, and the page must not invent one. The objection appears in the 30-second version, cards 1-2 and question 1 |

## Revision 2: deterministic gate (run by the drafter)

| Check | Result |
|---|---|
| `check_citations.py` on workspace.md | **Pass**: 311 units (202 cited, 57 analysis, 35 editorial, 17 computed), 0 errors, 0 warnings |
| `check_citations.py` on home.md | **Pass**: 6 units, 0 errors, 0 warnings |
| `readability.py` | **Pass**: 30-second version 7.4, Layer 1 8.0, Layer 2 7.9, Layer 3 7.4; 0 sections over target. Layer 4 is 8.6 (cards 8.5-10.1) and Layer 5 10.2, neither targeted |
| `verify_hashes.py` | **Pass**: 33/33 |
| Passages | 86, rebuilt by `build_passages.py` (header now from the spec). Added: P-086 (the county's definition of "base budget"), P-087 (probate court services). Widened: P-046 |
| Other step-5 checks | Same fields on every option and card (script check); decision block 64 words; 30-second version about 80 words; no party labels; no gendered pronoun for a named person; loaded words only inside quotes ("hijack", "shenanigans") |

## Where I'm least sure (revision 2)

1. **The 2027-01-20 deadline** comes from a schedule labeled "Proposed" whose record says it was approved with the
   consent agenda but still shows "Agenda Ready" (D2).
2. **The 5% limit's terms** come only from its critics (Resolution 26-0729 and FOX 5), worded differently (D4).
3. **Reasons for the limit and for filing Resolution 26-0729** may be in the Oct 21 and Nov 4 meeting video, which
   was not reviewed (S16). The page now says so.
4. **Thorne's ineligibility** is inferred: P-021 says Ellis was the only eligible northern member, and P-035 puts
   Thorne in District 1. Tagged `{analysis}`.
5. **"Additional Property Taxes (Consent)"** is read as a consent-decree tax line from its label. Its removal is
   confirmed against the rendered page; the reason for it is not in our sources.
6. **Consent decree = the 2025 Justice Department agreement** rests on adjacent sentences in the budget book (P-048).
7. **Definitions tagged `{analysis}`** (the solicitor-general prosecutes misdemeanors; abstain; at large; minimum
   reserve; operating budget; capital requests) are plain-language readings, not quotes.
8. **Calendar arithmetic:** Nov 15, 2025 as a Saturday is computed from Nov 15, 2026 being a Sunday (P-039).
9. **Card 4's comparison** of the public defender's $4,720,447 with 5% of its 2026 budget uses the 2026 budget as the
   yardstick; the 2027 base budget is not published.
10. **News paraphrases** remain paraphrases: the January request, Ellis's "no talk of an early vote", Ellis's
    "solely responsible for drafting recommendations" and the justice leaders' 5% concern.

## Open items for the next full round

- A third supporter card built from the 1953-2018 history (row 17): not adopted; reconsider if the panel still finds
  the supporter side under-argued.
- Card order (row 42), "In short" length (row 55), and the timing question dropped from "Questions to weigh" (row 45).
- Layer 4 reads at grade 8.6; cards 2, 3, 4 and 6 are 9.5-10.1, mostly because of long verbatim quotes.
- When the proposal is published (due Nov 15), several "uncertain" labels (cards 1-4) can be re-tested against it.

## Round 2 (revision 2, `verification/round2/workspace.r2.md`)

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate on revision 2, run by the drafter | **Pass.** 311 units (202 cited, 57 analysis, 35 editorial, 17 computed), 0 errors, 0 warnings. 33/33 hashes match. Reading grade 7.4 / 8.0 / 7.9 / 7.4 for the 30-second version and Layers 1-3 |
| 2 | Blind source check of the real page (fresh agent, page + passages only) | **189 supports, 13 partial, 0 does not support, 0 wrong span.** 100 valid and 9 questionable tags; 15 framing issues. **202/202 evidence spans grounded** |
| 3 | Bias panel | **Fail.** Supporter's advocate 3/5 (no high items). Critic's advocate 3/5 ("Each fix is a sentence or a phrase. With them made, I would rate the page 4 to 5"). Standards editor: neutrality 3/5, clarity 3/5 (34 issues: 1 high, 9 medium, 24 low; 16 flip-test items) |
| 4 | Planted-error calibration | Not part of round 2's reports |

Reports (copied unchanged into `verification/round2/`): `source-check.json` (c1513bd14bd2e29b…), `panel-supporter.json`
(5550c48bc7524c5b…), `panel-critic.json` (47b2b7cf4a9c59ff…), `panel-editor.json` (d49ec934da582231…).

**What the panel agreed on.** The editor and the critic found that the cards were mostly fair, but the framing around
them leaned mildly toward the current process: Board votes with no stated reasons were used as rebuttals only against
the critics, the page supplied a "tight finances" motive for the limit, "neither allows nor forbids" settled a disputed
legal question, and card 4 dropped the critics' best figure from the very sentence it quoted. The supporter found
card 1 stated the supporters' case weakly: the sponsors' own reasons were missing, and the 0.4% figure hid larger
changes and the fact that the 2026 draft came from the county manager alone. All three asked for Barrett's full
sentence, for per-part labels that follow the printed rule, and for the 1953-2018 commission's different makeup.

### Minutes search (revision 3 sourcing step)

Checked 2026-11-08, 09:19 to 09:22 UTC. The coordinator asked for official minutes or summaries of the Oct 21 and
Nov 4 Board meetings that record why commissioners kept the limit or filed the resolution.

- **Legistar meeting pages** (MeetingDetail.aspx?LEGID=6398 and 6399): "Published minutes: Not available" for both.
  Each links a "Post Agenda" PDF.
- **Legistar Web API events list** (S05d, archived): minutes status "Final" (Oct 21) and "Post Agenda" (Nov 4);
  `EventMinutesFile` is null for both. No Nov 18 event is listed yet.
- **The Clerk's "post agenda minutes"** (S05b for Oct 21, S05c for Nov 4; archived in full with `snapshot.py`):
  they record movers, seconders and votes on 26-0729, and list the budget updates (26-0735 on Oct 21, 26-0766 on
  Nov 4) only as "DISCUSSED", with no summary. **They record no reasons.** The Board ratified the Oct 21 minutes on
  Nov 4 (item 26-0746, P-091). The Nov 4 minutes are marked tentative, with ratification set for Nov 18 (P-090).
- **Per-item minutes notes** (event items API): empty for 26-0729, 26-0735 and 26-0766. The only notes are "Meeting
  went into Recess / Meeting Reconvened" on the executive-session items. Not archived, because they are empty.
- **The county's Agendas & Minutes page** links the Nov 4 agenda and points to Legistar for minutes. Not archived.

Result: the page now cites the minutes (P-088 to P-093, P-096) for the motions and votes, and says they give no
reasons. Reasons, if any were stated, would be on the meeting video (S16), which was not transcribed: there is no
approved tool for that. Nothing was inferred.

### What round 2 found, and what we did

Codes as in round 1, plus **SC F** (a source-check framing issue, by line). Line numbers are revision 2's. **In
revision 3, cards 3 and 4 swapped places**: the 5% limit card is now card 3, and the openness card is card 4. Card
numbers below are revision 2's unless marked "r3".

| # | Finding (who raised it) | Disposition in revision 3 |
|---|---|---|
| 1 | Card 4 quoted P-029's "27 of 41" but dropped "and 14 of them received budget increases that exceeded 5%" (ED 170 high; CRIT M1; FT 170; COORD) | **Fixed.** Card 3 (r3) "What supports it": "...asked for more than 5% for 2026, and 14 of them got increases above 5%." **Contradiction:** SUP I2 wanted the resolution's unchecked figures moved to "Who says it"; ED and CRIT wanted them in "What supports it". **Choice:** "What supports it", attributed ("It says ..."), labeled "Needs above 5%: Uncertain (claimants' figures)", with "We did not check the resolution's figures against budget books". Card 6 also lists the offices' own accounts under "What supports it", so this keeps the cards consistent. ED's suggestion to repeat the figure under Option C was not adopted, to avoid repetition |
| 2 | Votes with no stated reasons were used as rebuttals only against critics: card 1 "The Board did not adopt that resolution" (136), card 2's Pitts/Thorne votes and the Thorne inference (148), card 4 "and the Board filed it" (172), the preamble's "the process stayed in place after each one" (118) (ED 136, 148; CRIT U136, U148, I1; FT 136, 148; SC F136; COORD) | **Fixed.** All four removed. The Layer 4 preamble now states the rule: votes are listed under What happened, and the cards don't use them as evidence for either side, because the records don't give each member's reasons. Card 1's challenge is now attributed to its sponsor ("Commissioner Barrett's Resolution 26-0729 says ..."). **Contradiction:** SUP U9 asked to add the Oct 21 and Nov 4 votes to card 4's "Who disagrees", noting that no reasons are recorded. **Choice:** not adopted. The style guide (section 8) allows only statements in "Who disagrees", so the card says "No one in the sources we reviewed disputed this directly" and notes that the minutes record votes but no reasons. For the same reason, SUP M3's Aug 19 approval (4 to 1) stays under What happened only |
| 3 | The page supplied its own "tight finances" motive for the limit (172) (ED 172; CRIT U172, I2; FT 172; SC 172 questionable; SC F172; SUP I4; COORD) | **Fixed.** Deleted, with the two fiscal sentences that followed it ("many unmet requests"; the $2.8 million margin). The fiscal facts stay in Layer 2. Nothing replaces them |
| 4 | "The code neither allows nor forbids a cap" decided a disputed legal question (SC 172 questionable; SC F172; CRIT U172; COORD) | **Fixed.** Card 3 (r3) "What's uncertain": "Whether a cap fits the code's rule on requests; the code mentions no cap, and no source we reviewed settles it." Label: "Conflict with the code: Uncertain (the code mentions no cap)". SUP L4 had proposed the old wording; the coordinator's neutral "mentions no cap" was used |
| 5 | "Once submitted, the proposal serves as the operating budget" overstated P-005: the proposal governs "the following year" (SC 25, 43, 90, 95, 136 partial; SC F136; ED 95; SUP I1; COORD) | **Fixed.** Stated once, in Layer 2 "How the budget is drafted", in the code's words, plus "The proposal covers 2027, so this applies only to 2027 spending before the Board adopts a budget" (`{analysis}`). Removed from Layer 1, the decision block, the Layer 3 intro, Option A and card 1. The prototype chip "In force once submitted" became "Due by Nov 15" (page.yaml) |
| 6 | Oct 7: in Atlanta News First, Ellis answered Barrett, not the justice leaders; the entry gave the refusal and the reply but not the objections (SC 18 partial; SC F18; ED 18; CRIT U18; FT 18; COORD) | **Fixed.** The entry now gives the objections (the two-commissioner panel; the 5% limit), the refusal and the written requests, then "In Atlanta News First's report, Commissioner Barrett criticized how the commission was set up, and Commissioner Ellis responded" and "FOX 5 reported that Ellis addressed the officials' concerns". Layer 4's "Ellis's replies" became "Ellis's statements" |
| 7 | The 30-second version left out the 5% limit; the two sides had different actors; "too much say" was softer than the critics' words; the full Board's role was missing (CRIT U8 high; ED 8 x2; FT 8; SUP U1; COORD) | **Fixed.** "A Budget Commission that includes two commissioners is helping draft it, and the full Board decides." "Supporters say it gives one commissioner from each half of the county an early role; critics say it lets those two take control of the process." "Critics also object to a 5% limit on departments' requests for extra money." **Contradiction:** ED asked to cut item 1 to about 25 words; the coordinator, CRIT and SUP asked to add content. **Choice:** add, in short sentences. Item 1 is 67 words; the section is 114 words at grade 7.9 |
| 8 | Card 1 stated the supporters' case weakly and without the sponsors' own reason (SUP M1, U2, U3; COORD) | **Fixed.** In short: the sponsors say the commission adds "earlier and ongoing input" from Board members, balanced between north and south; the full Board alone still adopts the budget. "Who says it" quotes both P-004 clauses ("structured to ensure balanced representation ..."; "the full Board's exclusive authority to adopt ..."). "What supports it" adds that staff still prepare the budget documents (P-012) |
| 9 | Card 1 quoted the full-Board purpose (P-007) but not P-013's "interests and priorities of their respective district grouping" (SC F134; SUP M1) | **Fixed.** Both quotes now sit side by side in card 1's "What supports it", where either side can read them |
| 10 | Card 1's 0.4% was a net figure that hid larger changes, and the 2026 draft came from the county manager alone (SUP I1; SC 136 note; SC F136; COORD) | **Fixed.** "In the 2026 cycle, the budget book lists changes to the proposal dated Jan 7, adding $20,597,446, and Jan 21, taking out $16,648,868. So the adopted General Fund ended within about 0.4% of the proposal, which the county manager had drafted alone." **Found while fixing:** the Jan 7 changes were the chief financial officer's recommended revisions, which the Board approved with the rest on Jan 21 (budget book pp. 62 and 66-67, not quoted). So the page no longer calls them "the Board's changes", in card 1 or Option B. **Contradiction:** CRIT M5 wanted the 0.4% moved to card 2's "What supports it". **Choice:** kept in card 1, as the coordinator directed, so the fact appears once among the cards |
| 11 | The 1953-2018 commission's only Board member was the chairman, who serves at large; the new one has two district commissioners (CRIT M4, I2, I5; ED 132 medium; SUP M4) | **Fixed.** It opens card 1's "What challenges it". The precedent no longer repeats in card 1's In short. **Contradiction:** SUP M4 read the same fact as support (an elected official sat on the old commission too). **Choice:** stated as a plain fact, where both readings can use it. The sponsors' history claim stays in "Who says it" |
| 12 | Card 4's label didn't follow the printed rule: values part unnamed, "known only from critics", parts unlabeled (COORD; CRIT L card 4; SUP L4, L5; ED 166, 154) | **Fixed.** Card 3 (r3) labels each part: the code requires all requests and the 2026 draft listed extras (Supported: code text; budget book); conflict with the code (Uncertain); exact terms (Uncertain: no written instruction; undisputed); needs above 5% (Uncertain: claimants' figures); effect on the draft (Uncertain: not out); sound budgeting (a values question). **Contradiction:** SUP asked to add "known only from critics" to the In short. CRIT said that contradicts line 53 and P-077/P-080: the limit is in FOX 5's own narration, and Ellis is among the report's sources. ED proposed "known only from critics and a news report based on their news conference". **Choice:** the label says "no written instruction; undisputed", and Layer 2 names who describes the limit (Barrett's resolution and a FOX 5 report). It does not say "only from critics" |
| 13 | "A uniform cap on requests is poor budgeting" sharpened P-028, which is about spending cuts and caps (SC 168 partial; ED 168; SUP U8, I5) | **Fixed.** The In short quotes "not the best method for controlling costs in the long run" and says "spending caps". It keeps the evidence-status sentence the style guide requires, reworded: "The code's text and the 2026 record support part of this; the draft will show the rest" |
| 14 | The rent example ($76,981, about 0.3% of the office's budget, within 5%) and "the descriptions of the limit mention no exceptions", an argument from silence built by the page (SUP I3; ED 170 low; FT 172; SC 170 tag) | **Fixed.** Both removed. **Contradiction:** CRIT M8 wanted stronger legally driven examples (the sheriff's "Consent Decree Compliance" and "Court Ordered Contract"), with a caveat. **Choice:** the sheriff's items appear only under "What's uncertain" ("Whether the limit covers costs tied to court orders or the jail agreement ..."), with no argument attached. Note that every item on that slide is ranked 1, so the page does not call them "top-ranked" |
| 15 | Card 4's challenges lacked context: the limit covers only requests above the base budget (SUP M7) | **Fixed.** "The limit covers only requests for extra money above the base budget. The base budget is the money needed to keep up past spending, including staff." SUP M7's second point (the resolution calls caps a method "for controlling costs") was not used as a challenge, because it would hint at a reason no one stated (SUP I4 itself says not to infer one) |
| 16 | Needs above 5% are still visible (SUP M5, I3; CRIT verdict) | **Fixed** once, in card 3 (r3) "What challenges it": "Requests above 5% can still be seen: the public defender's full request is in its posted slides." The public defender's figures stay in "What supports it" as the claimants' figures |
| 17 | "but only 'in narrative form'" carries the critics' emphasis (SUP U10) | **Not adopted.** It is the resolution's own framing and the round-1 fix for CRIT U 163. This round, CRIT and ED both asked for "only ... in narrative form" in question 2 as well |
| 18 | The 2026 cycle took, reviewed and published every request without a cap (CRIT M2) | **Partly fixed.** Card 3 (r3) "What supports it" quotes "All submissions were received and carefully reviewed," next to the appendix. The ranking order ("contractual, regulatory, statutory" first) is not used, because it was the basis of the removed "no exceptions" argument (row 14) |
| 19 | The resolution calls its change a clarification of the code, not a new ban (CRIT M3) | **Fixed** in Layer 2: "Resolution 26-0729 says it would 'clarify' the code by adding a ban ..." (P-083, P-030) |
| 20 | The resolution's "unprecedented restriction" was dropped (CRIT M9) | **Fixed** in card 3 (r3) "Who says it". It is covered by the card's uncertain labels |
| 21 | The FY2027 eligibility rule parallels the January request (CRIT M10, low) | **Not adopted.** No one made that argument; it would be built by the page |
| 22 | The hearing notice was used only against critics, though it shows departments presented to the commission (CRIT M6, I3, L card 2) | **Fixed.** Card 2 "What supports it" quotes the notice ("Special called meetings of the Fulton County Budget Commission"), and its label adds "Hearings held by the commission: Supported (county notice)". The hearing-slot sentence stays in "What challenges it" |
| 23 | Card 2 lacked the public-interest side of the objection (CRIT M7) | **Fixed.** "Sheriff Patrick Labat called the arrangement unfair 'to county agencies and taxpayers,' FOX 5 reported." ANF's "court delays, jail conditions and public safety" stays in card 6 |
| 24 | Barrett's "shenanigans" quote dropped "some of the current" and "in a way that I feel is inappropriate" (SC 145 partial; SC F145; ED 145; CRIT U145; SUP U7; FT 145) | **Fixed.** The whole sentence is quoted |
| 25 | "Only those two" holds for the 2027 cycle; the rules choose members again each cycle (SUP U4, M3) | **Fixed** in card 2: "What supports it" says "for the 2027 budget", and "What challenges it" adds "The rules choose the two commissioners again before each budget cycle." Barrett's paraphrase in the In short is unchanged, because adding "for the 2027 budget" would change her words |
| 26 | Ellis's FOX 5 statement was split across the critics' fields (SUP U5) | **Fixed.** Card 2 "What supports it" now cites the code for the drafting role (P-013, P-015). Ellis's statement appears whole under "Who disagrees" |
| 27 | "directly and continuously" dropped "with the county manager and chief financial officer" (SUP U6) | **Fixed** |
| 28 | Card 2 omitted that two of the commission's four members are staff (SUP M2) | **Fixed** in "What challenges it" |
| 29 | Card 2's title was pure judgment; card 1's pairs a value claim with a fact (CRIT U140) | **Fixed.** "Two commissioners, seated under rules they sponsored, have too much say" |
| 30 | Only the critics got the news-report caveat; "their case rests on news reports" overstated it (CRIT U149; ED 149; FT 149) | **Fixed.** "... so their statements come from news reports". Card 4 (r3) adds "His statements, like the officials', come from two news reports." |
| 31 | Card 3's "Who disagrees" opened with what the critics did not say (CRIT U159; SC F159) | **Fixed.** Card 4 (r3) leads with the resolution's transparency aim and Judge Johnson's objection; the clarification comes last |
| 32 | "Notices and posted decks back that" overstated the records (ED 156; SC 156 questionable) | **Fixed.** "A public notice and posted slides are consistent with that" |
| 33 | Card 3's label left the values part, the draft and the 2026 record out (ED 154; SUP L3; CRIT L card 3) | **Fixed.** "More open than past years: Uncertain (his statement; 2026 also had hearings). Open enough: a values question." The draft-disclosure question sits on the 5% card |
| 34 | Cards 3 and 4 had nearly the same "What's uncertain", and "the written limit aren't public yet" assumed a written limit exists (ED 95; SC 162, 174 questionable) | **Fixed.** Card 4 (r3) asks whether the commission's other meetings are publicly announced and open (P-042, P-015); card 3 (r3) keeps the draft question. The phrase about a written limit is gone |
| 35 | The commissioners report to the full Board in public (SUP M6) | **Fixed.** Card 4 (r3) "What supports it": the code requires reports to the full Board (P-013), and the Oct 21 and Nov 4 meetings each took up an update on the budget process (the minutes, P-089 and P-093). "What's missing" says the minutes do not summarize them |
| 36 | Card 1's "(disputed)" is not a printed ground, and "no record either way" understated the resolution's claim (ED 130; SUP L1; SC 137 questionable) | **Fixed.** The printed rule now includes "no record settles it". Card 1's label reads "Uncertain (no record settles it)", and "What's missing" says "Apart from that claim, we found no record ..." |
| 37 | "The code backs this design" read as outside support for rules its sponsors wrote (CRIT U132; ED 132; SC 132; SC F132; FT 132) | **Fixed.** "The code's text confirms these rules; results wait on the draft." |
| 38 | A dated status fact under `{editorial}` (SC 138) | **Fixed.** Tagged `{analysis: P-045, captured Nov 8, 2026}` |
| 39 | Cards 3, 4 and 6 claimed "with a values question" but never named it (ED 154; SUP L3) | **Fixed.** Every label names its values part, and the printed rule says so |
| 40 | Card 5's factual parts were unlabeled: January 2026 adoption; when new members take office (CRIT L card 5; ED 180) | **Fixed.** "2026 vote in January: Supported (budget book). When new members take office: Uncertain (not in our sources)". The gap is also in "What's missing" |
| 41 | Ellis's "no talk of an early vote" belongs in "What challenges it" (SUP U11) | **Not adopted.** It does not dispute the request to wait; it bears on when the vote will be. It stays in "What's uncertain" |
| 42 | Card 6: county records back the jail agreement's costs, not "overcrowding"; "underfunded" generalized Gammage; the label's basis was incomplete (ED 192; CRIT L card 6) | **Fixed.** "Justice officials are asking for more money, citing ... [P-070] County records back added costs for the jail agreement; other needs rest mainly on the offices' own accounts." Label: "Courts' and prosecutors' 2027 needs: Uncertain (mainly their own accounts)" |
| 43 | "warned" for the justice officials, "said" for others (ED 193; FT 193) | **Fixed.** "said" |
| 44 | "Justice leaders" is the coalition's own name (FT 97; SC F97) | **Fixed.** "Justice officials" (Atlanta News First's term) throughout; Layer 4 notes once that "The six officials call themselves 'Justice Leaders.'" |
| 45 | The Layer 4 tally left out Resolution 26-0729 as the critics' stated reasons, and put Gammage under funding (CRIT U118; FT 118) | **Partly fixed.** The tally now says the sources include Resolution 26-0729, "which states its sponsor's reasons". Gammage stays "on funding", because both of his quotes are about funding (P-071, P-077) |
| 46 | Arrington's seconds on Barrett's motions went unnamed (CRIT I6; FT 21) | **Fixed** in What happened (Oct 21 and Nov 4) |
| 47 | The supporters' card came first in both pairs (CRIT I4; FT 124; FT 8) | **Fixed.** The second pair now starts with the critics' card. The printed ordering rule says so |
| 48 | Repetition: "as presented" five times; the $2.8 million and $73.6 million figures three times each; the precedent four times (ED 95; CRIT I5) | **Fixed.** "As presented" appears once; $2.8 million twice (Layer 2, Option A); $73.6 million twice (Layer 2, card 6); the precedent three times, the old makeup twice |
| 49 | Option C's "Who pays" implied a cost; "$222 million ... beyond the plan" misstated P-051; "no money" (ED 113; CRIT U113; FT 113; SC 113 questionable; SC F113) | **Fixed.** "Who pays" keeps only the time cost, reworded because P-031 frames it as departments' chance to submit: "Departments and budget staff would have had less than four weeks to prepare and add every omitted request." The $222 million and "no money" sentences are gone; the 2026 figures stay in Layer 2 |
| 50 | Option C's "Who benefits" named no one (ED 112) | **Fixed.** Board members and the public; departments whose needs go beyond 5%; then the resolution's own claim |
| 51 | "Beyond the requests already in the plan" (SC 85 partial; ED 85) | **Fixed.** "Beyond the extra requests it had already considered" |
| 52 | Option B's "Where it stands" described the precedent, not the status (ED 103) | **Fixed.** It opens with the 2027 status, then "Last cycle" |
| 53 | "Additional Property Taxes (Consent)" read as a tax cut (SC 103; SC F103; ED 17) | **Fixed.** "On the revenue side, it dropped a proposed $32,170,000 line of 'Additional Property Taxes' and raised the regular property tax line by $18,535,960" (`{computed}`) |
| 54 | Options A and B never named the people who use courts and jail services (CRIT U104) | **Fixed.** "and the people who use their services" in Options A and B |
| 55 | "Whether the vote comes in December or January" left out earlier dates (SC F98) | **Fixed.** "When the Board will vote; the code sets only a latest date, ..." |
| 56 | "it changed what is asked for and shown", about a resolution that was filed (SC 110 questionable) | **Fixed.** "it would have changed" |
| 57 | The Layer 2 opening ran about 187 words against a budget of about 80 (ED 53; COORD) | **Fixed.** About 90 words in all: a 17-word decision block, a one-sentence "Not published yet" and three one-sentence items. The member list and the consensus rule moved to "How the budget is drafted". The limit's sourcing is now one clause in item 3 ("..., Commissioner Barrett's resolution and a FOX 5 report say"), so the limit is not stated as bare fact |
| 58 | Card summaries ran 38-55 words and evidence headers 29-51 words (ED 166 clarity; COORD) | **Partly fixed.** In shorts are 33-50 words (card 3 is the longest). **Contradiction:** ED asked for headers of 15 words or fewer; SUP, CRIT and the coordinator asked for a label on every part. **Choice:** one short label per part, separated by periods so each reads as a short sentence. Headers are 23-51 words; card 3's is the longest. The renderer splits the meta line on "·", so "·" is not used inside the label (checked with a test render) |
| 59 | "Consent decree" was never plainly defined; "consent agenda" was defined after its first use (ED 17; COORD) | **Fixed.** "In general, a consent decree is a legal settlement that a court approves and can enforce." "Consent agenda" is defined at first use (Aug 19). The "Consent Decree" budget line now points to the jail agreement section |
| 60 | Undefined jargon: "noticed", "substitute motion", "deck", "predictive" (ED 63) | **Fixed.** "Publicly announced"; a definition of substitute motion; "slides" or "presentations"; "about what will happen" |
| 61 | "Asked" was softer than "restricted" and "directive" (CRIT U53) | **Fixed.** "were directed" |
| 62 | Question 1 opened with one side's main point and used the sponsors' framing; the 1953-2018 model and a timing question were missing (COORD; ED 202; CRIT Q; SUP Q; FT 202) | **Fixed.** Q1 now opens with the question and lists "staff alone, staff with the chairman (the setup until 2018), staff with two commissioners (one from each group of districts), or the whole Board". New question: "Should the Board adopt the budget in December, or wait until January?" CRIT's draft ("... when incoming members can take part?") was not used, because no archived source says when new members take office |
| 63 | Q2's "described in writing" softened "only ... in narrative form" (ED 203; CRIT Q) | **Fixed.** "described only in narrative form" |
| 64 | An optional openness question (SUP Q) | **Not adopted.** Q2 covers what the draft shows, and no other checker asked for it |
| 65 | The reading pointers led only to the offices' own materials (ED 38) | **Fixed.** New Observe item: the 2026 adopted budget, with its budget message and General Fund schedule |
| 66 | "Open data website" was not in the cited passages (SC 38, 158 partial) | **Fixed.** New passages P-094 and P-095 carry the catalog permalinks on data.fultoncountyga.gov; the page now names the site |
| 67 | "Requests for extra money" needed P-029 (SC 20 partial) | **Fixed** in workspace.md and home.md |
| 68 | "The code doesn't say ..." about weekends, when only Section 2-261 was checked; the 2025 deadline needed P-047 (SC 25 questionable, SC 25 note) | **Fixed.** "Section 2-261 doesn't say ..."; the tag says other sections and state law were not checked; P-047 added to the computed tag |
| 69 | The solicitor-general tag misdescribed P-062 (SC 18 note) | **Fixed.** "P-062 cites ABA benchmarks for misdemeanor prosecution among the office's challenges" |
| 70 | Card 4 didn't say its missing supporters' case might trace to the unreviewed video (ED 173; SUP I4) | **Fixed.** Card 3 (r3) "What's missing": "The minutes give no reasons for the limit, and we did not review the meeting video, where reasons may have been given." |
| 71 | Find official minutes or summaries recording reasons; don't infer (SUP I4; COORD sourcing step) | **Done.** See "Minutes search" above: the minutes record no reasons. Archived as S05b, S05c and S05d |
| 72 | "Atlanta News First is still waiting to hear back from" the vice chair could read as evasive (SC F120, low) | **No change.** The checker rated it acceptable, because it explains why she is not quoted |
| 73 | Notes with no change needed: P-079 is a paraphrase (SC 118); District 4's vacancy may change (SC 35); the consent decree money sits in Non Agency (SC 192) | **Noted.** The Non Agency point is now used in card 3 (r3) "What's uncertain"; the others need no change |

## Revision 3: deterministic gate (run by the drafter)

| Check | Result |
|---|---|
| `check_citations.py` on workspace.md | **Pass**: 329 units (211 cited, 69 analysis, 33 editorial, 16 computed), 0 errors, 0 warnings |
| `check_citations.py` on home.md | **Pass**: 6 units, 0 errors, 0 warnings |
| `readability.py` | **Pass**: 30-second version 7.9, Layer 1 8.2, Layer 2 8.3, Layer 3 7.2; 0 sections over target. Layer 4 is 8.9 and Layer 5 10.5, neither targeted |
| `verify_hashes.py` | **Pass**: 36/36 (new: S05b, S05c, S05d) |
| Passages | 95, rebuilt by `build_passages.py`. Added: P-088 to P-093 and P-096 (the Oct 21 and Nov 4 minutes), P-094 and P-095 (catalog permalinks) |
| Prototype render (test, to scratch) | Renders; every card's evidence label parses whole |

## Where I'm least sure (revision 3)

1. **The 2027-01-20 deadline** comes from a schedule labeled "Proposed" (D2). Unchanged.
2. **The 5% limit's terms** come from Barrett's resolution and a FOX 5 report, worded differently; there is no written
   instruction (D4).
3. **Reasons for the limit and for filing Resolution 26-0729** are not in the minutes (D12). They may be on the video
   (S16), which was not reviewed. The Nov 4 minutes are tentative until Nov 18.
4. **"As presented" applies only to 2027 spending before adoption** is the drafter's reading of P-005 ("for the
   following year"), which follows the source check. Tagged `{analysis}`.
5. **The Jan 7, 2026 changes** were the chief financial officer's recommended revisions, approved on Jan 21 (budget
   book memos, not quoted). The page avoids saying who made them.
6. **The consent decree definition** is general. Whether a court approved this agreement is not in our sources, so
   the page says "In general".
7. **Definitions tagged `{analysis}`** (solicitor-general; abstain; substitute motion; at large; operating budget;
   minimum reserve; capital requests; consent decree) are plain-language readings, not quotes.
8. **Card 3's comparison** of the public defender's $4,720,447 with 5% of its 2026 budget uses the 2026 budget as the
   yardstick; the 2027 base budget is not published.
9. **News paraphrases** remain paraphrases: the January request, Ellis's "no talk of an early vote", Ellis's "solely
   responsible for drafting recommendations", and the officials' 5% concern.
10. **The new card order** (critics first in the second pair) is a fairness choice, not a rule from the style guide.

## Open items for the next full round (after revision 3)

- Re-check the Nov 4 minutes after the Nov 18 meeting (ratified or changed), and the Nov 18 agenda (README, step 3).
- The meeting video (S16) needs an approved transcription tool before anyone can say what was said on Oct 21 or Nov 4.
- Evidence headers are longer than the editor's 15-word target (23-51 words; card 3 is the longest), because every part now has
  its own label. See whether the panel finds them clearer or harder to scan.
- Layer 4 reads at grade 8.9, mostly because of long verbatim quotes, which the checkers asked to keep whole.
- When the proposal is published (due Nov 15), re-test the "uncertain" labels on cards 1 to 4 against it.

## Round 3 (revision 3, `verification/round3/workspace.r3.md`)

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate on revision 3 (run by the coordinating session, with the tools as updated after revision 3) | **0 errors.** 4 warnings from the new whole-number check: lines 84, 137, 157 and 158 name "Resolution 26-0729" but cite only the resolution's text, which doesn't carry the number |
| 2 | Blind source check, workspace.md | **205 supports, 6 partial, 0 does not support, 0 wrong span.** 115 valid and 3 questionable tags; 12 framing issues (5 medium, 7 low). **329/329 evidence spans machine-checked as grounded** |
| 2 | Blind source check, home.md | **3 supports, 1 partial**; 2 valid tags; 3 framing issues. 6/6 grounded |
| 3 | Bias panel | **Pass.** Supporter's advocate 4/5. Critic's advocate 4/5. Standards editor: neutrality 4/5, clarity 4/5 (21 issues: 0 high, 7 medium, 14 low; 14 flip-test items) |
| 4 | Planted-error calibration | Not part of round 3's reports |

**Round 3 passes** under rule 1 of "When a page is done" ([verification protocol](../../docs/process/verification-protocol.md)):
0 unsupported claims, 0 wrong spans and 0 ungrounded spans; both advocates at 4/5; no high-severity editor issue;
0 gate errors (the 4 warnings came from a check added after revision 3, and are fixed below). Under rule 2, revision
3.1 fixes only accuracy flags and breaches of a written rule, as the coordinator listed them. Everything else is an
open item. Nothing on this page is called "verified".

Reports (copied unchanged into `verification/round3/`): `source-check.json` (829b84f9fbb6d7c5…),
`source-check-home.json` (9d3527e2d8f3a28d…), `panel-supporter.json` (2153ae20bb85a23d…), `panel-critic.json`
(ad2c3f01908d77c2…), `panel-editor.json` (df486ebb20dddd6b…). The coordinator's note mentioned six reports; the folder
held these five when checked (10:58 and 11:05 UTC).

### Accuracy fixes in revision 3.1

Line numbers: "r3" is revision 3 as checked; "r3.1" is this revision. One deletion (item 2 of "What's different this
year") moves every later line up by one.

| # | Flag (who raised it) | r3 line | r3.1 line | Fix |
|---|---|---|---|---|
| 1 | A reporter's paraphrase inside quote marks, read as the speaker's words (SC 136, 146, 194 partial; SC F136, F146, F194; ED 194; FT 146; coordinator) | 136, 146, 194 | 135, 145, 193 | Quote marks removed, and each sentence now opens "[Outlet] reported that ...". Line 145 gives FOX 5's own wording: Labat "criticized the two-commissioner arrangement as unfair to county agencies and taxpayers" |
| 2 | The net 0.4% hid the gross changes, and "So ..." drew an inference (SC 137 questionable; SC F137; ED 137; SUP I1; coordinator) | 137 | 136 | The changes to the county manager's proposal dated Jan 7 (+$20,597,446) and Jan 21 (−$16,648,868); "Those changes included $18,157,000 more for consent decree costs" (computed); "Together, they left the adopted General Fund $3,948,578, or about 0.4%, above the proposal" (computed); and what that shows, tagged `{analysis}`: "the proposal's total largely held, though some of its lines changed by millions of dollars". The point stays in card 1's "What challenges it" |
| 3 | Card 3's challenges went beyond P-029, P-077 and P-086, and generalized from one office's slides (SC 161 partial x2 and questionable; SC F161; CRIT U161 x2; ED 161; coordinator) | 161 | 160 | "The resolution and FOX 5 describe the limit as applying to requests for extra money." "For the 2026 budget, the county described its 'base budget' as the current funding needed for past spending patterns, including staff." "Outside the draft, at least one office's requests above 5% are public: the public defender's posted slides list $4,720,447 in requests." The tag says "above 5%" is measured against the 2026 budget |
| 4 | P-004 is the Board's adopted determination ("WHEREAS, the Board determines"), not the sponsors' words (SC 133 partial; coordinator) | 133 | 132 | "Resolution 26-0460, which the Board adopted, says ..." [P-004, P-017]. "Who says it" still names the sponsors |
| 5 | "The county had posted neither ..." was broader than the two pages checked (SC 46 questionable; SC F46; coordinator) | 46 | 46 | "On Nov 8, neither the county's budget page nor its Public Notices page showed the 2027 proposal or a hearing date." |
| 6 | "Only the full Board can adopt or change the budget" was listed as new (SC F51; ED 51; CRIT U51; SUP verdict; FT 51; coordinator) | 51 | deleted | Deleted; line 44 already says the full Board decides. The old item 3 is now item 2 (line 51, text unchanged) |
| 7 | "Raised the regular property tax line" could read as a tax increase (SC F104; coordinator) | 104 | 103 | Added: "Taken together, total property-tax revenue ended $13,634,040 below the proposal." {computed: $815,496,192 + $32,170,000 = $847,666,192 proposed; $834,032,152 adopted; difference $13,634,040 (P-054)} |
| 8 | "The draft will show the rest" overstated what the draft can settle (ED 157; FT 157; SUP U157; coordinator) | 157 | 156 | "... the rest depends on an unsettled reading of the code, the claimants' own figures, values or the coming draft." |
| 9 | Line 18: "addressed" is FOX 5's verb, and neither Barrett's allegation nor Ellis's response had its substance (ED 18; FT 18; SC F18; SUP U18; CRIT U18; coordinator) | 18 | 18 | "In Atlanta News First's report, Commissioner Barrett said some current board members were trying to take control of the budget process. [P-072] In the same report, Commissioner Ellis responded that the commission's meetings are open meetings. [P-073] FOX 5 reported that, in response to the officials, Ellis said the full Board will review and vote on the final budget. [P-079, P-080]" |
| 10 | "Which the proposal should show" was uncited (ED 107; coordinator) | 107 | 106 | "How much room 2027 revenue leaves, because the proposal isn't public yet." (Option A's pattern) |
| 11 | The rubric defined "Not mainly a factual question", a label no card uses (ED 125; coordinator) | 125 | 124 | "'A values question' means that part of the claim is about what is fair or right, not mainly about facts." |
| 12 | Q1 tagged one choice with a period; Q2 gave the limit restrictive words and the other choice expansive ones (ED 203, 204; FT 203; SUP U204 and Q; coordinator) | 203, 204 | 202, 203 | Q1 tags no choice with a period ("staff alone, staff with the chairman, staff with two commissioners (one from each group of districts), or the whole Board?"); the whole Board has no period to tag. Q2: "Should departments' requests for extra money have a set limit, or should the draft list them without one?" The three views are in contradiction C6 |
| 13 | New whole-number check: "26-0729" not in the cited passages (gate warnings; coordinator) | 84, 137, 157, 158 | 83, 136, 156, 157 | P-024 (the Legistar matter record, which carries the number and the sponsor) added to each citation |
| 14 | home.md: line 5 cited the October hearing notice; line 14 left "file" undefined (SC home 5 partial; SC home F5, F14; coordinator) | home 5, 14 | home 5, 14 | Line 5 cites P-036 (the public comment rules) instead of P-042. That left no cited passage carrying "2027", so the gate warned; "the 2027 budget" became "the county budget", which P-036's "County related opinions" covers. Line 14 adds "Filing set the resolution aside without approving it." `{analysis}` |

Also changed: the status lines (3 and 4, not checked) now say revision 3.1, accuracy fixes after round 3, targeted
re-check pending, and three rounds of checks.

### Revision 3.1: deterministic gate (run by the drafter)

| Check | Result |
|---|---|
| `check_citations.py` on workspace.md (with the new whole-number check) | **Pass**: 332 units (211 cited, 70 analysis, 33 editorial, 18 computed), 0 errors, 0 warnings |
| `check_citations.py` on home.md | **Pass**: 7 units (4 cited, 2 analysis, 1 computed), 0 errors, 0 warnings |
| `readability.py` | **Pass**: 30-second version 7.9, Layer 1 8.0, Layer 2 7.9, Layer 3 7.3; 0 sections over target. Layer 4 is 8.1. Card labels are no longer scored (6; longest 74 words, line 154) |
| `verify_hashes.py` | **Pass**: 36/36 |
| Passages | 95, unchanged |
| Prototype render (test, to scratch) | Renders. It reads the status as "revision 3", taking the first number of "3.1" |

### For the targeted re-check

Changed sentences, by line in revision 3.1. workspace.md: 18 (three sentences), 46, 103 (the new last-but-one
sentence), 106, 124 (one sentence), 132 (first sentence), 135 (first sentence), 136 (all but the first and last
sentences), 145 (third sentence), 156 (both cited sentences: a citation added to the first, the last rewritten), 160
(first, second and fourth sentences), 193 (last sentence), 202 (first sentence) and 203. Citation added only: 83
(first sentence), 136 (last sentence) and 157 (first sentence). Deleted: revision 3's line 51. home.md: 5 (first
sentence) and 14 (new middle sentence).

### Open items (feed the next revision; not changed in 3.1)

Codes are as before. A code's number is the line in that report, so revision 3's line (for example, ED 159 or
SUP U169); a line named in the text is revision 3.1's.

- **Source check:** "filed" can read as "submitted" in Layer 3 (lines 90, 108) and Layer 4 (line 126) (SC F91).
  Card 3's 3.3x comparison should say in the sentence that the 2026 budget stands in for the "base budget" (SC F159).
  The public comment rules leave out the residency check and the 60-minute cap (SC F32; SC home F5). home.md's Nov 4
  item doesn't say one member did not vote or that the Oct 21 motion failed (SC home F14), and its line 9 could cite
  P-005 directly (SC home 9). Not flagged but the same point as fix 3: Layer 2's "base budget" bullet (line 84) uses
  the 2026 definition in the present tense.
- **Supporter's advocate:**
  - Card 2 leaves out the code's checks: the duty to seek every commissioner's priorities and report to the full Board,
    the general eligibility rules, selection at a noticed meeting entered in the minutes, and the Board's adoption of
    the rules and approval of the members (M5, M6, I1).
  - Card 3's challenges leave out 2026 record facts: unmet requests and a thin reserve without any limit; the Board's
    power to add money; the old process's 1% across-the-board cut; and the resolution's proposed "all budget
    requests" wording (M1 to M4, I3). Present none of them as anyone's reason for the limit.
  - Card 1 could use the Oct 21 and Nov 4 updates as the reporting duty in action (M7, optional label L1). Card 4
    could add the noticed-meeting selection rule (M8).
  - Card 4's In short narrows "Those meetings are open meetings", and "his statement alone" is sharper than card 6's
    "rest mainly on" (U169). Card 5 could put Ellis's "no talk of an early vote" in its In short (I7).
  - Card 2's label could add "Whether the rules were written to seat the two: Uncertain" (L2).
- **Critic's advocate:**
  - Card 2 shortens P-013's role for the two commissioners (revenue and expenditure assumptions, other elected
    officials' requests, preparing the recommended budget); its label could add "Their role under the code:
    Supported" (M1, L card 2).
  - In 2026 the Board raised four of the six protesting offices and consent decree funding above the proposal, in a
    year when all extra requests were printed; this fits P-028's "make adjustments" reason (M2). The scale of the 2026
    requests ($158 million, $64 million) could go in card 3's support (M3). The sheriff's 2027 slides tie staffing
    and overtime to the consent decree (P-060, P-061), for card 6 (M4).
  - Single hedge words quoted alone ("clarify", line 83; "effectively", line 156) and the shortened Pew advice
    (line 157) make the critics read as more tentative (U84, U157, U158, I3).
  - Layer 4's count should say "six justice officials, through two quoted speakers" (I2).
  - A question on whether departments should present to the commission or the full Board (Q).
- **Standards editor:**
  - One rule for "What supports it": claimants' own figures sit there on cards 3 and 6, while Ellis's statements stay
    under "Who says it" (159, FT 195). The supporter's I5 agrees.
  - Card 4's label leaves an uncertain part unlabeled: "Other commission meetings announced and open" (167, FT 167).
    The critic's L card 4 agrees.
  - Card 2's "What's missing": the sources don't say why the other northern commissioners were ineligible (150).
  - Q3's "wait" casts January as the delay; an openness question is missing (205).
  - Card 3's challenges don't answer its values claim, and the Pew advice as quoted is about cuts (161, FT 87).
  - Card 3's 74-word header, and about 500 words of method notes before the first card (155).
  - The rubric should say adopted Board findings count as Board records (FT 60). Card 2 could note the operating-budget
    rule, limited to 2027 before adoption (FT 149). Card 5's claim type could read "a values question, about timing"
    (125).
  - Readability by the editor's own estimate is near grade 9-10; round dollar figures and split semicolons (50). The
    project tool gives 7.3 to 8.1.

### Contradicting suggestions, logged side by side (not resolved)

| # | Topic | One view | The other view(s) |
|---|---|---|---|
| C1 | How to describe the commission's drafting role | **CRIT** (U8, I1): "is helping draft" (line 8), "helps draft" (line 50) and "help prepare" (line 146) follow the sponsors' "assist" framing; under the code the commission "shall submit" the proposal (P-005, P-006, P-015), and Ellis called it "solely responsible for drafting recommendations" (P-079). State both | **SUP** (U148): line 147's "the commission alone drafts" turns Ellis's statement into exclusive power, a concession in the rebuttal field; quote P-079's own words |
| C2 | Card 3's first label part, "Code requires all requests" | **SUP** (L card 3): narrow it to the code's words, "the budget requests of all of the county's departments"; "all budget requests" is the filed resolution's proposed wording (P-030) | **CRIT** (L card 3): keep the conflict Uncertain, but give its reason as the code's "shall include ... all" requirement beside its silence on caps |
| C3 | The 30-second version (line 8) | **SUP** (U8): the supporters' case is below full strength; "brings Board members' input in earlier, balanced between north and south, while the full Board keeps the final say" | **ED** (8, FT 8): match strength by softening the critics' paraphrase ("gives those two too much say") and trim to about 45 words. **CRIT** (U8): add the critics' reason for objecting to the limit, and that no source gives the limit's reason |
| C4 | Card 4's "More open than past years" label | **SUP** (L card 4): drop the rebuttal; "Uncertain (his statement only; no record compares the years)" | **CRIT** (L card 4): add to it; "(his statement; the 2026 cycle also had formal hearings and printed all extra requests in an appendix, P-049)" |
| C5 | "But only 'in narrative form'" in card 3's challenges (line 160) | **SUP** (U161): use "Departments may still describe broader needs in narrative form" there | **CRIT** (U161): the sentence turns the critics' grievance into a rebuttal; if it stays, say no source shows whether the narratives will be in the draft |
| C6 | Q2 wording (changed in 3.1 under fix 12) | **ED** (204): drop "only in narrative form", or attribute it; "held to a set limit, or ... every request listed" | **SUP** (U204, Q): restrictive words for one choice, expansive for the other; suggested "limited to a set share of current funding, with larger needs described in narrative form, or should the draft list every request, whatever its size? How should either choice account for the money available?" **CRIT** (Q): accepted as written ("describes the limit in the critics' own terms"). Revision 3.1 gives both choices neutral verbs and drops the narrative-form detail |
| C7 | "The resolution does not say the meetings were closed" (card 4, line 171) | **CRIT** (U172): it argues against the critics in their own field; restate the dispute neutrally or move it to "What challenges it" | **ED** (FT 172): keep the parallel by adding to card 2 "His quoted remarks don't address the eligibility rules", or drop card 4's sentence |
| C8 | Option C's "Who pays" (line 113) | **CRIT** (U114): the time cost is overstated; "Budget staff would have had 25 days to add any omitted requests that departments chose to submit" | **ED** (114): Option C has only one cost; add one from the record (listing a request doesn't fund it; "many unmet requests") and mark C as no longer pending |
| C9 | Where the 2026 reserve margin ($2.8 million) goes | **ED** (97, 197): it appears only as a benefit of Option A; add it to Option B or the Layer 3 intro, and to card 6's challenges | **SUP** (M1): add the 2026 fiscal facts, the thin reserve among them, to card 3's challenges, without presenting them as anyone's reason for the limit. Round 2 took the margin out of card 3 because it sat next to a supplied motive (CRIT U172, ED 172) |
| C10 | What card 1's 0.4% shows (line 136) | **SUP** (I1): it cuts both ways (it also supports "earlier and ongoing input"); explain it both ways or move it to neutral context | **CRIT** (verdict): it is evidence that whoever drafts largely sets the budget. **ED** (137): state the inference as `{analysis}`; if that is the point, it also belongs in card 2's support. Revision 3.1 keeps it in card 1, as the coordinator directed, with the gross changes and an `{analysis}` of what the figures show |

### Where I'm least sure (revision 3.1)

1. **Line 136's `{analysis}`** ("the proposal's total largely held, though some of its lines changed by millions of
   dollars") is a reading of P-054, and the panel disagrees about what the figure shows (C10).
2. **Line 18 gives one half of Ellis's FOX 5 statement** (the full Board will review and vote). The other half,
   "solely responsible for drafting recommendations", stays on card 2, because its wording is contested (C1).
3. **home.md line 5** now says "the county budget", not "the 2027 budget", because no public comment passage names
   the 2027 budget.

## Targeted re-checks (revisions 3.1 to 3.3, run by the coordinating session)

Each re-check was a fresh blind agent that read only the page (or home items), the passages and a list of changed
units, and checked only those units. Spans were machine-checked with `trials/tools/check_verifier.py`. Reports are
copied unchanged into `verification/round3-targeted/`, `round3-targeted-2/` and `round3-targeted-3/`, each with
its list of changed units.

| Re-check | Units | Result | What it raised, and what we did |
|---|---|---|---|
| 1, on revision 3.1 | 25 page + 2 home | 15 of 15 cited units supported, 10 of 11 tags valid, 1 questionable; 27/27 spans grounded (8323d8852d8b6354…, 752205ee52302fb7…) | **Line 46 (questionable):** "neither page showed … a hearing date" was contradicted by P-042, which lists the commission's October hearings. Fixed in 3.2: "the newest notice … was for the commission's October hearings". Also fixed in 3.2: "net spending changes" (line 136); the planned draw on savings, $57,102,996 to $73,637,614, beside the 0.4% figure, with the analysis narrowed to the spending total; "the limit's exact terms" added to card 3's analysis (line 156); P-080 added for the FOX 5 attribution (line 160); "Base Budget" capitalized as in P-086 (line 160). Logged as open: the one-word quotes "clarify" (line 83) and "effectively" (line 156) |
| 2, on revision 3.2 | 7 | 3 supports, 1 partial, 3 tags valid; 0 unsupported; 7/7 grounded (2607f563fb86bb72…) | **Line 136 (partial):** P-054 is the General Fund schedule, so the changes are General Fund changes. Fixed in 3.3 ("net General Fund spending changes", "General Fund spending total"). Also fixed in 3.3: line 84 quoted "base budget" in lower case against P-086 and used the 2026 definition in the present tense (now dated to 2026, as line 160 is); "Not published yet" became "Not posted yet" (line 46). Logged as open: line 160 sits the 2026 definition next to the 2027 limit (it is dated) |
| 3, on revision 3.3 | 4 (+ Trial 002's home deadline) | 2 supports, 2 tags valid; 0 partial, 0 questionable; 4/4 grounded (08e7af968d66611d…) | **Passed.** Low notes, logged as open: line 46 could say "Budget Commission" at first mention in Layer 2; line 136's "proposal dated Jan 7" reads as the proposal's date |

The protocol's targeted re-check is done: no re-check found an unsupported claim, and the last one found nothing
to fix. **Status: Passed checks (round 3), revision 3.3.**


## Revision 4: reader-first update (change class: update; checks pending)

Reason: reader-first revision after usability feedback ([style guide v0.6](../../docs/product/content-style-guide.md),
section 9; [runbook v0.6](../../docs/process/runbooks/bill-workspace.md), Step 4). The coordinator also passed on the
unclear terms a lay reader (a Fulton homeowner) found in the Summary, Options and Arguments tabs. Revision 3.3, which
passed checks in round 3, is kept in `verification/round4/` (`workspace.r3.3.md`, `home.r3.3.md`, `page.r3.3.yaml`).
No checker has seen revision 4; the drafter ran only the deterministic gate.

### What changed, by part

| Part | Change |
|---|---|
| Status lines | Revision 4, "reader-first update; checks pending" |
| 30-second version (89 words) | "What's being decided" names the decider and defines it in cited words: Fulton County's Board of Commissioners, a chairman and six district seats (P-014, P-035). It also defines the Budget Commission: two commissioners and two staff, who must propose the budget by Nov 15 (P-005, P-008). "Who it affects" says Fulton County residents and property owners, and that property tax is the county's main income (P-103, P-101). The two sides' views were replaced by a stated fact: "The commission's makeup and a 5% cap on requests for extra money are disputed." This is the style guide's section 1 option ("give none and state the facts"), and it takes the line out of round 3's contradiction C3 |
| New: What it means for you | Three "If you" lines: people who live in Fulton County, people who own property there, and people who use or work in the county courts or jail. Each line says the 2027 effect is not known yet, as of Nov 8, 2026, and why. Then three folded parts: **Where the money comes from** (5 steps: the General Fund, property tax as its biggest source, how the tax billing is figured, what the fund pays for, the 2026 draw on savings); **Decided now, and decided later** (the budget now; the property tax rate later, with the 2026 hearing on Sept 30 as the precedent, P-098; mid-year changes need a Board vote, P-006); **Not known yet** (four dated items, each with its reason) |
| Layer 1 | Reordered: What you can do, Who decides and how to reach them, Coming up, What happened. **Who decides, and how to reach them** (new) lists every Board seat the same way from the Nov 4 roll call (P-035). It gives the Budget Commission's members and how its two commissioners were chosen (P-008, P-011, P-021), and the Clerk's office phone as the sources print it (404-612-8200, P-039; 404.612.8200 for accommodations, P-067). It says our sources give no selection method, term or direct contact for any commissioner, and it has no Decide, Input or Observe labels. **What you can do:** <br>- the public comment rules (residency check; a 60-minute cap, with the rest at the end of the meeting; P-036); <br>- the Zoom wording, and that the Clerk's page doesn't say where to submit (P-067); <br>- what the Clerk to the Commission does (P-097); <br>- the Public Notices page's address (P-100) and the Commission District Finder (P-099); <br>- "Tell your district commissioner" became "Tell Board members", with "This page can't look up your district"; <br>- what the budget message and the General Fund table are. <br>**Coming up:** the consent agenda is defined at its first use on the tab; "Agenda Ready" is explained; the 2025 proposal is credited to the county manager (P-047); "the code" became "Fulton County's code of laws"; the legal newspaper is said to be unnamed in our sources. **What happened:** "moved", "seconded" and "failed" are explained (1 yes among 6 members; our sources don't give the voting rule), and the "Budget Committee" item is linked to the Budget Commission as likely (README, D10) |
| Layer 2 | "Who decides" moved to Layer 1. The Board is named and defined, and "Budget Commission" is used at first mention (re-check 3's note). New: the resolution says the commission set the limit at 5% of each department's "base budget" for 2027 (P-029), and the limit's exact wording and date aren't known. "The code" became "the county code" or "Section 2-261 of the county code". "Filed" is explained |
| Layer 3 | Intro of 60 words, defining the Board and the Budget Commission and saying what filing means. New values guide (`### If you care most about…`), as its first sub-section: one line for Option C, then an `{editorial}` line saying Options A and B have no documented argument in our sources, and that the guide doesn't recommend an option. Option C's heading says "set aside on Nov 4". Option C no longer uses the word "authority". Option A names the six justice officials. Option structure unchanged |
| Layer 4 | The preamble names the quoted speakers (Johnson, Labat, Barrett and Gammage) and the two resolutions' sponsors, and defines the Budget Commission and the county code (P-017). "Ratified" became "approved", and "tentative" is explained. The six justice officials are listed (P-076), and "filed" reads "filed, or set aside". Every "In short" was rewritten (40 words or fewer): first what the claim would mean for people, then the claim in its side's own terms, then its evidence status. Card 2's support names the sponsors (Ellis and Abdur-Rahman). Card 3 defines the jail agreement at its first mention on the tab (P-048). Card 5's gap reads "who the incoming board members are or when they take office", without "elected". **No evidence label, card claim, card order or side changed** |
| home.md | Every item is 25 words or fewer and dated in its label, with the Board of Commissioners and the Budget Commission named. "Submit requests before 10 a.m." (P-067), and "Filing set the resolution aside" |
| page.yaml | Title "Fulton County commissioners adopt the 2027 budget by their second January meeting" (12 words), with plain link text and chips. Each reader-facing string has a citation comment. The source chip "Legistar" became "Board record". `kind` is unchanged |
| Passages | P-097 to P-103, all from snapshots already archived (S08, S09b, S11, captured 2026-11-08): what the Clerk does; the Board's Sept 30, 2026 hearing on the proposed millage rate; the district-finder and Public Notices links (from the HTML); property tax as the county's primary revenue and when the 2026 rate was to be set; how the tax billing is figured; and "the needs of our residents". 102 passages, 0 errors |

### The values guide: why one line

The format needs a documented, verbatim quote from someone who backs the option.

- **Option C** (Resolution 26-0729) has one: its sponsor, Commissioner Barrett, proposed it "to ensure transparency and
  informed decision-making in the development of the Fulton County budget" (P-024, P-032). The quote is the
  resolution's title, not spoken words, so the line says "proposed it" rather than "said".
- **Options A and B** are about the Board's vote on a proposal that isn't public. No archived statement argues for
  adopting the proposal unchanged, or for changing it. Ellis's statements back the commission process, not either
  vote. The justice officials ask for more money and a January vote, not for one of these options. Treating either
  as backing an option would attribute an argument nobody made (CLAUDE.md, invariant 4). So those options get no line,
  and the page says so.

### Round 3 open items this revision touches

- **Folded in, as asked:** what "filed" means, wherever it appears (Layer 3 intro, Option C heading, Layer 2, Layer 4,
  home); the public comment rules (P-036).
- **C3 (the 30-second version's wording):** the line no longer gives either side's view, so all three panelists' notes
  lapse. This is a change of approach, and the next full round should look at it.
- **C1 ("helps draft" against "the commission alone drafts"):** the 30-second version and "Who decides" now use the
  code's duty ("must propose", P-005). Layer 2's "helps draft" and card 2's line 147 are unchanged, so C1 stays open.
- **Lay reader's terms in the top layers and on home:** all addressed in place, except the template items the
  coordinator is fixing (the Options lede, the label legend, "trial" and "sha256", and the folded "How to read these
  cards").

### What the sources couldn't support

- **The size or direction of any household tax change.** The proposal wasn't public on Nov 8, and the 2027 tax rate
  isn't set. Our sources show only how the 2026 rate was handled.
- **How Board members are chosen, and how long their terms are.** The page doesn't call them elected.
- **A direct phone or email for any commissioner or for the Budget Commission.**
- **Where to submit a Zoom comment or a request to speak.** The Clerk's page doesn't say; the page points to the
  Clerk's phone.
- **The legal newspaper's name.**
- **How many yes votes a motion needs.**
- **Whether the agenda's "Budget Committee" is the Budget Commission.** The page says "likely".
- **Who the incoming board members are, and when they take office.**
- **Any documented argument for Option A or Option B.**

### Revision 4: deterministic gate (run by the drafter)

| Check | Result |
|---|---|
| `check_citations.py` on workspace.md | **Pass**: 390 units (244 cited, 91 analysis, 36 editorial, 19 computed), 0 errors, 0 warnings |
| `check_citations.py` on home.md | **Pass**: 6 units (4 cited, 2 analysis), 0 errors, 0 warnings |
| `readability.py` | **Pass**: 30-second version 7.5, What it means for you 7.2, Layer 1 8.0, Layer 2 8.2, Layer 3 8.0; 0 sections over target. No budget warnings and no CHECK lines. Layer 4 is 8.3 (not targeted); the longest card label is 74 words (card 3, unchanged) |
| Length budgets | 30-second version 89 words. "If you" lines: 45, 44 and 37 words, with first sentences of 23, 20 and 16. Values line 32 words (quote 14). Layer 3 intro 60. "In short" lines 40, 40, 40, 40, 40 and 38. Home items 24, 20, 25 and 25 |
| `verify_hashes.py` | **Pass**: 36/36 |
| Test render (to scratch) | Renders version 4, with no Decide-label or undated-home-item warnings and no asterisks in the page data |

### Where I'm least sure (revision 4)

1. **"If you own property in Fulton County"**, the line most likely to be questioned:
   - The first sentence gives the reason the coordinator set (the proposal isn't public). The fuller reason, that the
     rate is a separate decision, comes in the third sentence.
   - That third sentence calls the "proposed millage rate" of the Sept 30, 2026 notice (P-098) "the tax rate".
   - It reads "months after its budget vote" from two dates in different sources (P-046, P-098).
2. **"If you use or work in the county courts or jail":** the "work in" part rests on staffing requests and shortages
   (P-064, P-066, P-070), not on a source that speaks about county workers.
3. **"If you live in Fulton County":** "the jail, courts, libraries and other services" are 2026 General Fund lines
   (P-054). The 2027 list isn't known.
4. **The Option C values line quotes a resolution's title,** not a spoken statement. A checker may hold it to the
   format's "said".
5. **No values lines for Options A and B.** A checker might read Ellis as backing A, or the justice officials as backing
   B. I treated both as inferences.
6. **"Where the money comes from", step 3,** turns P-102's "tax digest value" into "the value of taxable property".
7. **"Who it affects" cites the 2026 budget message** ("the needs of our residents", P-103) for a 2027 page.

## Round 4 results (revision 4, `verification/round4/workspace.r4.md`)

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate on revision 4 (run by the drafter) | **Pass.** 390 units, 0 errors, 0 warnings; home.md 6 units, 0 and 0; no budget or CHECK lines; hashes 36/36 (see Revision 4 above) |
| 2 | Blind source check, workspace.md | **241 supports, 3 partial, 0 does not support, 0 wrong span**; 145 valid and 1 questionable tag; 12 framing issues (2 medium, 10 low). Grounding: 390/390 units OK (`check_verifier.py`, run by the drafter for this record) |
| 2 | Blind source check, home.md | **4 supports**, 2 valid tags; 3 framing issues (low). 6/6 grounded |
| 2 | Display check, page.yaml | **6 supports, 1 partial** (the title); 3 framing issues (1 medium, 2 low). 7/7 grounded |
| 3 | Bias panel | **Fail.** Supporter's advocate **3/5**. Critic's advocate 4/5. Standards editor: neutrality 3/5, clarity 3/5 (23 issues: **1 high**, 8 medium, 14 low; 15 flip-test items) |
| 4 | Planted-error calibration | A copy with planted errors was checked (`round4/calibration/`). Per the coordinator, its checker also flagged the values-guide gap on its own. Not used for this revision |

Reports (copied unchanged into `verification/round4/`): `source-check.json` (3ab22a025bd5f808…),
`source-check-home.json` (3e3c865fa6f1d4a7…), `display-check.json` (81b376c16da4d481…), `panel-supporter.json`
(13e5de0105b51416…), `panel-critic.json` (0e09f7296c3e7926…), `panel-editor.json` (17469034ca7df85b…). The page
and home items the round checked: `workspace.r4.md` (c00564fc501c11ae…), `home.r4.md` (24461b1f0ecc2b52…),
`page.r4.yaml` (f6cb0bec06954d5f…).

**Why the round failed.** A pass needs both advocates at 4/5 or better and no high-severity editor issue. The
supporter's advocate gave 3/5, and the editor found one high issue. Both came from the values guide: its one line
was Commissioner Barrett's for Option C, which gave "transparency" to that side alone, although Commissioner Ellis
claims transparency for today's process (P-073) and Resolution 26-0460 states its own purpose (P-004, P-016). The
source check's framing notes flagged the same gap (medium). The source checks found no unsupported claim.

**What each checker found.**

- **Source check (SC), workspace.md:** 3 partial and 1 questionable, all in the reader-first lines: "wasn't public
  then" says more than P-045 (line 14); the Sept 30 tax-rate hearing "came" and "held" when P-098 only notices it
  (lines 15 and 29); the property-tax line's first sentence gave only part of the reason (line 15). Framing notes:
  the values guide (medium), the property-tax line (medium), and 10 low notes ("isn't public" elsewhere, the general
  consent-decree definition, "to address conditions" softening P-048, District 4 and the vice chair, uneven outlet
  attribution, the Oct 7 letter, Option C's cost, the reserve figure only under Option A, "six offices" in card 2,
  unexplained terms).
- **Source check, home.md:** no flags; 3 low framing notes ("Submit requests" is vague; name the two county staff;
  "file" can read as "submit").
- **Display check, page.yaml:** the title read as done or certain and dropped "regular" (partial); the vote chip also
  drops "regular" (minor); Option C's chip is styled "bad".
- **Supporter's advocate (3/5):** the values guide (high); card 3's first label part and card 4's rebuttal inside its
  label (medium); card 3's challenges lack the Board's power to fund above the draft and the fiscal half of P-049
  (medium); card 1's challenge was twice card 2's (medium); "the commission alone drafts" and "Neither needed a
  nominating vote" (medium); 8 missing arguments; 5 low wording notes; 3 label disagreements.
- **Critic's advocate (4/5):** card 3's In short plays down the critics' case (medium); card 4's "does not say the
  meetings were closed" (medium); Option C's only cost is overstated (medium); card 6 lacks the sheriff's, the
  solicitor-general's and the county's own words; the 2026 deficit appears five times without the 2023 to 2025
  actual results (medium); 4 label disagreements.
- **Standards editor (3/5, 3/5):** the values guide (high); 8 medium issues: "unprecedented" treated unlike Ellis's
  comparison; card 3's case against; the "if right" hedge hidden in tags on cards 3 and 4; the property-tax and
  courts lines; why the other district 1 to 3 commissioners were ineligible; density (about 600 words before card 1);
  undefined terms. 14 low issues and 15 flip-test items.

## Revision 5: what changed (change class: update; checks pending)

Reason: fixes after verification round 4 (values guide quoted one side). Revision 4 is kept in `verification/round4/`.
No checker has seen revision 5; the drafter ran only the deterministic gate. Line numbers: "r4" is revision 4 as
checked; "r5" is this revision. The v0.6 content contract is kept: no section, card or option was added, removed or
reordered, and no evidence label, card claim or side changed except as items 9 and 10 say.

| # | Item | r4 line | r5 line | What changed |
|---|---|---|---|---|
| 1 | Values guide (blocked the round) | 132-133 | 132-135 | One line for each side with a documented argument, in card order (card 1 supports today's process; cards 2 and 3 are critical; card 6 asks for more justice funding). **Today's process:** Ellis, "who sponsored today's process, which Option C would change, said that process is "more transparent and open than anything we’ve had in recent years."" (P-073, P-016, P-024). **Option C:** Barrett's line, unchanged. **Court funding:** Solicitor-General Gammage, "one of six justice officials asking for more money, said they see "a lack of proper investment in the entire ecosystem of the courts."" (P-077, P-069, P-070). The two transparency claims now sit side by side. Closing `{editorial}` line: no archived source argues for Option A over Option B; the officials' request for a January vote has no line because our sources quote no one making it (P-070 is the reporter's summary); the guide doesn't recommend an option. Lines are 35, 32 and 34 words; quotes 11, 14 and 12 words. The brief listed Barrett's line first; card order puts today's process first (runbook Step 4), so it comes first |
| 2 | Accuracy flags | page.yaml 6; ws 15, 29 | page.yaml 6; ws 15, 29 | Title: "Fulton County commissioners must adopt the 2027 budget by their second regular January meeting". The vote chip also says "2nd regular January meeting" (display check, line 10, minor). Line 15: "a tax-rate hearing was set for Sept 30, after the January budget vote". Line 29: "scheduled a hearing on the proposed tax rate for Sept 30"; its tag now says the rate was "planned for after the budget" |
| 3 | "wasn't public then" (line 14) | 14 | 14 | Removed. The line now gives the dates: "A proposal is due by Nov 15; the Board must adopt a budget by its second regular January meeting, expected Jan 20, 2027." [P-005, P-041] |
| 4 | Property-owner line | 15 | 15, 36 | First sentence (24 words): "Whether your 2027 county property tax changes isn't known as of Nov 8, 2026; the proposal wasn't posted, and the tax rate comes later." Then "Bills also depend on property values." [P-102] and the Sept 30 sentence (item 2). "Not known yet" (line 36) gives the same reasons and drops "isn't public" (the source check's minor note) |
| 5 | Option C's "Who pays" | 156 | 158 | "Budget staff would have had 25 days to add any omitted requests that departments chose to submit." The tag quotes P-031's "the opportunity to submit" |
| 6 | "Neither needed a nominating vote" | 189 | 191 | "Neither Ellis nor the vice chair needed a nomination by their district group; the Board approved the item naming them on Aug 19." [P-021, P-022, P-009] Outcome only, no counts or reasons, so the votes-as-evidence rule holds |
| 7 | "the commission alone drafts" | 190 | 192 | "FOX 5 reported that Ellis, addressing the officials' concerns, noted that the commission is solely responsible for drafting recommendations and that the full Board will review and vote on the final budget." P-079's own words, without quote marks, because they are the reporter's |
| 8 | Courts and jail line | 16 | 16 | "What the jail, courts, prosecutors and public defender get for 2027 isn't known as of Nov 8, 2026. Six of their leaders asked for more money. What the draft funds isn't known. Adoption is due by the second regular January meeting, expected Jan 20, 2027." The officials' reasons moved off the line to fit 45 words; they stay in Option A and card 6 |
| 9 | Card 3 | 197-205 | 199-207 | **Challenges:** state law lets the Board adopt "greater or less than the sums presented in the proposed budget" (P-047), and in 2026 the Board adopted more than the draft for four of the six justice offices (computed from P-054); as the resolution quotes them, the finance officers' association and Pew speak of across-the-board spending cuts or caps, not limits on requests (P-028). **Label:** new part "Unprecedented: Uncertain (the resolution's statement; we didn't check past years)". **What's missing:** "We did not check past years' budget instructions for earlier limits on requests." **Who says it:** the resolution's "true picture" argument (P-028) |
| 10 | Card 4 | 209-217 | 211-219 | Label: "October hearings announced, some offices' slides posted: Supported (county records). More open than past years: Uncertain (Ellis's statement; we didn't check past years)". No label carries a rebuttal; "2026 also had hearings" was already in "What challenges it". New there: "The Oct 21 update was added to that day's agenda at the meeting itself, and the minutes don't record what either update said." (P-089, P-093; the duplicate under "What's missing" was removed). The unsourced pronouns for Ellis ("his") became "Ellis's" |
| 11 | "In short", cards 3 and 4 | 199, 211 | 201, 213 | Card 3: "If the claim is right, the draft won't show departments' full requests." ... "The code and budget book back part; the rest is unsettled." Card 4: "If the claim is right, other commissioners and their staff can follow the commission's work." ... "Records back the announced hearings; the "recent years" comparison rests on Ellis's statement." 40 and 38 words |
| 12 | Card 2, ineligibility | 192 | 194 | "Our sources don't say why the other two commissioners from districts 1 to 3 were not eligible." The tag gives P-010's grounds |
| 13 | Card 6, documented reasons | 237 | 239 | The county's words: it entered the agreement "to address constitutional violations at the Fulton County Jail," which "mandated the County implement specific reforms" (P-048). The sheriff's slides: a staffing plan "to stabilize staffing and strengthen compliance," overtime "to align with operational realities and Consent Decree mandates" (P-060), and a "Consent Decree Compliance" request (P-061). The solicitor-general's slides: "Caseload Growth without proportional staffing" and outside advice that prosecutors "should not carry workloads so large/complex that they endanger fairness, accuracy or the timely processing of charges" (P-062). Each quote checked against its passage. The slides' "ABA", "ASGs" and "Marsy's Law" were left out: the sources don't define them |
| 14 | The 2026 draw on savings | 24, 108, 139, 179, 239 | 24, 108, 241 | Kept in the Summary money steps, in Layer 2 and as card 6's challenge. Removed from Option A's "Who benefits" and from card 1 (item 15). Layer 2 now gives, once, the 2023 to 2025 actual results from the same table: revenue topped spending by about $3.0 million, $12.3 million and $13.0 million, and savings grew from $224,363,249 to $252,638,514 (P-054) |
| 15 | Layer 4 preamble; card 1 | 161-163, 179 | 163-165, 181, 272-274 | The voice count and the labeling rule stay. The minutes, ratification and video notes moved to a new Layer 5 subsection, "Notes on the minutes and meeting video", unchanged except that "that resolution" became "Resolution 26-0729" (its antecedent stayed in Layer 4). The preamble's two visible paragraphs went from 302 to 245 words (602 to 545 with the folded "How to read these cards"; counted without citations and tags). Card 1's "What challenges it" went from 131 to 72 words (card 2's is 69): one rounded takeaway, with the line-item example in the tag |
| 16 | Definitions on each tab | many | many | **Summary:** the 5% limit (30-second version: "a limit on requests to 5% above current funding"); "filed" (its definition now follows the first use, line 81); the General Fund (step 1, with the "specialty funds", P-045); savings ("fund balance", step 5); the Clerk to the Commission (line 44, P-097); "eligible" (line 64, P-010). The courts line names the offices instead of "justice offices". **What's decided:** the Budget Commission at first mention (line 85); "eligible" (line 98); the "Consent Decree" line as the jail agreement's (line 106). **Options:** savings (Option A); the 5% limit and the six officials (Option A's "Who pays"); the jail agreement and the General Fund (Option B). **Arguments:** the 5% limit, "filed" and the six justice offices (preamble, line 165); the General Fund (card 1); "eligible" (card 2); the jail agreement and "consent decree compliance" in P-048's words (card 3); savings (card 6). **page.yaml:** the source chips "Res. 26-0460" and "Res. 26-0729" now read "Resolution …" |
| 17 | Dollar figures in "Where the money comes from" | 20-24 | 20-24 | Steps 1 and 2 already gave the 2026 General Fund total ($1,057,100,578) and property tax ($834,032,152), with citations; the lay reader saw the fold closed, and the template now opens it. Both now say "in the 2026 budget". No 2027 figure is projected |
| 18 | home.md | 5, 14 | 5, 14 | Act now (25 words): "...; the Board adopts the budget." [P-014] defines the Board. What happened (24 words): "voted to file, or set aside, a resolution to end a limit holding departments' requests to 5% above current funding." [P-024, P-025, P-077]; the separate "Filing set the resolution aside" sentence is gone. No "Res." Items: 25, 20, 25 and 24 words |

Also changed: the status line (revision 5, "fixes after round 4; checks pending") and the "How this page was made" line.
Layer 3's reading grade rose to 8.9 after the first draft of these changes; splitting Option A's and Option B's new
sentences brought it to 8.4.

### Revision 5: deterministic gate (run by the drafter)

| Check | Result |
|---|---|
| `check_citations.py` on workspace.md | **Pass**: 415 units (267 cited, 93 analysis, 37 editorial, 18 computed), 0 errors, 0 warnings |
| `check_citations.py` on home.md | **Pass**: 5 units (4 cited, 1 analysis), 0 errors, 0 warnings |
| `readability.py` | **Pass**: 30-second version 7.7, What it means for you 7.1, Layer 1 8.0, Layer 2 8.3, Layer 3 8.4; 0 sections over target; no budget warnings; no CHECK lines. Layer 4 is 8.7 (not targeted; 8.3 in revision 4), mostly from the verbatim quotes added under items 9 and 13. Longest card label 84 words (card 3, with the new "Unprecedented" part) |
| Length budgets | 30-second version 90 words. "If you" lines 45, 45 and 45, first sentences 22, 24 and 18. Values lines 35, 32 and 34 (quotes 11, 14 and 12). Layer 3 intro 60. Layer 2 "What's being decided" 80 with its labels. "In short" lines 40, 40, 40, 38, 40 and 38. Home items 25, 20, 25 and 24 |
| `verify_hashes.py` | **Pass**: 36/36 retrieved, 0 problems |
| Passages | 102, unchanged; no new passages were needed |
| Test render (to scratch) | Renders version 5 with no warnings: no Decide-label or undated-home-item warnings and no asterisks in the page data. The template's Sources tab shows only the sources, so the Layer 5 notes moved under item 15 are in workspace.md but not on the prototype page (the template is outside this revision's scope) |

### Open items after revision 5 (round 4 suggestions not taken)

Codes: **SC** source check (F = framing note), **SCH** home check, **DC** display check, **SUP** supporter's
advocate, **CRIT** critic's advocate (M = missing argument, U = unfair wording, I = imbalance, L = label), **ED**
editor issue, **FT** flip test. Numbers are revision 4 lines.

- **Source checks:**
  - "isn't public yet" remains in Option A's tag and in three "What's uncertain" fields (r5 lines 141, 143, 151, 243) (SC F14).
  - The general consent-decree definition may suggest a court approved this agreement (SC 112, F112).
  - District 4 residents are also represented on the commission by the vice chair (SC 47, F47).
  - Atlanta News First quotes name no outlet while FOX 5 paraphrases do (SC F188).
  - The Oct 7 letter is stated as fact (SC F78).
  - Undefined: "FY", whether the county manager and CFO are appointed, the public defender, the superior court clerk (SC F201).
  - home.md: "Submit requests" is vague, and the residency check and 60-minute cap are missing (SCH F5). The two county staff are unnamed (SCH F9). The Nov 4 item leaves out the 4-to-1 vote and the failed Oct 21 motion (SCH F14).
  - Option C's chip is styled "bad" (DC F14).
- **Supporter's advocate:**
  - The code says "the budget requests of all of the county's departments", not "all budget requests" (M1, L card 3, carried from round 3 C2).
  - Card 3 could show the 2026 fiscal context: "many unmet requests", "no additional resources available", the $158 million and $64 million in extra requests, and the $2.8 million margin (M3, I3).
  - The 2026 proposal's 1% across-the-board cut, against "unprecedented" (M4).
  - The eligibility rules are general (M6): partly met, since card 2 now states them.
  - Card 1 could note the Oct 21 and Nov 4 updates as the reporting duty in action (M8).
  - Card 4's "In short" stays with commissioners and staff, not the public (U211).
  - Card 1's "would" (U175). Card 3's "but only 'in narrative form'" (U203, round 3 C5).
  - Card 2's "In short" says two commissioners, not a four-member commission (U187). Card 2's heading (U183): a card claim, so out of scope.
  - The GFOA and Pew quotes are secondhand (I6).
  - The 30-second version's tag names only objectors (I7). Option A's timing line lacks Ellis's "no talk of an early vote" (I7, ED 141).
  - Card 1's label could name the resolution as the source of doubt (L card 1).
  - Q2 and Q3 wording (Q).
- **Critic's advocate:**
  - The GFOA's stated reason and Pew's alternative, quoted more fully (M1).
  - The duty-to-solicit charge on card 3 (M2).
  - The Board's 2026 raise for the solicitor-general (about 10.6% above the draft) as a Supported part of card 3's support (M3, L card 3).
  - The sheriff's attrition point (M4).
  - The solicitor-general's ABA benchmarks, 400-case limit and Marsy's Law (M5): left out because the sources don't define the terms.
  - Card 3's In short, evidence sentence (U199): partly met by the item 11 trim; see R4-C3.
  - Card 4's "The resolution does not say the meetings were closed" (U214, round 3 C7).
  - Line 80's "1 yes vote among the Board's 6 members" (U80).
  - Card 2's "critics say" (U187, FT 187).
  - Card 6's "seeking more money" (U235).
  - Option A's "Who pays" rests on news claims while county records back it too (I): partly met, since item 14 removed the county figure from "Who benefits".
  - The missing written 5% instruction bears on openness, for card 4's challenges (I).
  - The vice chair seat isn't re-chosen each cycle (I).
  - Card 3's conflict reason (L card 3). Card 4's "Other commission meetings open: Uncertain" part (L card 4): item 10 narrowed the Supported part to the October hearings but did not add this part. Card 6's solicitor-general increase as its own Supported part (L card 6).
  - Q1 (who hears requests) and Q4 (jail-agreement costs) (Q).
  - The courts line could say whether the limit covers jail-agreement costs (optional).
- **Standards editor:**
  - Card 3's challenges could add the county's money position (203): the brief chose P-047 and P-054.
  - Card 2's label could narrow to "Who serves in 2027" (189).
  - Option B's four computed sentences of line-item arithmetic (161).
  - Undefined: county manager and CFO on the Summary and Layer 2 (146); solicitor-general on Layer 4; GFOA and Pew.
  - The card-pairing sentence (167). Q3's "wait" (247). Q2's bundled choices (246).
  - "fits" in card 3's "What's uncertain" (205), and a bracket citation of P-061 for the quoted request names (205).
  - Barrett "is listed as not voting" (76, FT 76).
  - "cap" and "limit" are both used (8): new text says "limit", older sentences keep "cap".
  - Mark the remaining meeting dates as scheduled (70).
  - The accommodation line's label and phone format (52).
  - Option C's documented limits on its benefit (156, FT 155).
  - Card 5 could note that the draft serves as the operating budget until adoption (227).
  - Attribute the jail agreement's aim in Layer 2 (112).
  - The "A values question" wording against the legend (167).
  - FT 189: card 2 could say the full Board adopted the rules on Aug 5.
  - FT 214: Johnson "objected to" against "said".
  - FT 51: an Observe link to the meeting videos.
  - FT 192: Barrett's quotes also come from a news report.
- **Carried from earlier rounds and still open:** the Nov 4 minutes ratification and the Nov 18 agenda (README,
  step 3); the meeting video needs an approved transcription tool; re-test the "Uncertain" labels when the proposal
  is published.

### Contradicting suggestions, round 4, side by side (not resolved unless the brief chose)

| # | Topic | One view | The other view(s) | Revision 5 |
|---|---|---|---|---|
| R4-C1 | The values line for today's process | **SUP** (values guide): Resolution 26-0460's purpose, "earlier and ongoing input" with "balanced representation" (P-004) | **ED** (132): show both transparency claims in parallel lines. **CRIT** (values guide): the one line was fair; add a pointer for the justice officials' values | Ellis's transparency claim (P-073), parallel to Barrett's unchanged line; the officials get a line (Gammage). P-004 stays on card 1 |
| R4-C2 | The Board's 2026 increases above the draft (P-054) | **SUP** (M2, I3): a challenge on card 3; a limit on requests doesn't limit what the Board funds | **CRIT** (M3, L card 3): support on card 3, with its own Supported part (the solicitor-general, about +10.6%) | The brief's item 9: a challenge ("four of the six justice offices"). The critic's label part is open |
| R4-C3 | Card 3's "In short", evidence sentence | **CRIT** (U199): "The code's text and the 2026 budget book back the rule and past practice; the effect waits on the draft." | **ED** (199): "Records back part; the rest is unsettled." | "The code and budget book back part; the rest is unsettled." (fits 40 words with the item 11 hedge) |
| R4-C4 | Card 4's "More open than past years" label | **SUP** (L card 4): "his statement alone; no record compares the two years" | **ED** (FT 200): label it like the resolution's "unprecedented". Round 3 **CRIT** (C4): add the 2026 hearings and appendix | "Ellis's statement; we didn't check past years", parallel to card 3; the 2026 points stay in the card's body |
| R4-C5 | Card 1's 2026 amendment figures | **SUP** (I4): move the Board's changes to "What supports it"; keep the 0.4% as the challenge | **ED** (161, FT 179): one rounded takeaway; move the arithmetic to the evidence layer | Trimmed in place (item 15); moving the figures would change which side they support |
| R4-C6 | Card 4's "does not say the meetings were closed" | **CRIT** (U214): reword it as the resolution's concern, or drop it | **ED** (FT 214): add a matching caveat to card 2, or drop card 4's | Unchanged (round 3 C7) |
| R4-C7 | The courts and jail "If you" line | **SUP** (if-you lines): end at "isn't known yet", or add a cost clause | **ED** (16): add the "when", then move the request or say how much the draft funds isn't known. **CRIT** (if-you lines): fair; optionally, whether the limit covers jail costs | The editor's version (item 8); the officials' reasons moved off the line to fit |
| R4-C8 | Where the 2026 deficit and reserve go | **CRIT** (I): add the 2023 to 2025 actual results once, in Layer 2, or cut repetitions | **SUP** (M3): the fiscal context belongs on card 3. Round 3 **ED** (C9): add the margin to Option B and card 6 | Actual results once in Layer 2; removed from Option A and card 1 (item 14) |
| R4-C9 | Option C's "Who pays" | **CRIT** (U156): "25 days to add any omitted requests departments chose to submit" | **ED** (156): add documented limits on Option C's benefit to "What's uncertain". Round 3 **ED** (C8): add a cost from the record | The critic's wording (item 5); the editor's addition is open |

Round 3's C1 ("helps draft" against "the commission alone drafts") is now partly settled: card 2 uses P-079's own
words (item 7), and Layer 2's "helps draft" is unchanged. C2 (card 3's first label part) is open again (SUP L card 3).

### What the sources couldn't support (revision 5)

- **A named speaker's words for the January-vote request.** Only Atlanta News First's summary exists (P-070), so that
  side has no values line, and the page says so.
- **Definitions of "ABA", "ASGs" and "Marsy's Law"** from the solicitor-general's slides (P-062). Those points were
  left out, and the ABA's advice is called "outside advice".
- **A source's own definition of the General Fund.** The page uses "biggest fund" (P-052), "specialty funds" (P-045)
  and what the fund pays for (P-054).
- **Whether a court approved the jail agreement.** The sources call it an agreement with the Department of Justice
  (P-048); the general consent-decree definition in Layer 2 is ours.
- **Why the other two district 1 to 3 commissioners were ineligible**, now said on card 2.
- **When the 2027 tax rate will be set.** Only the 2026 pattern is archived (P-098, P-101).
- **Who defends the 5% limit, and why it was set.**
- **The GFOA and Pew originals.** Only the resolution's quotes are archived (P-028).

### Where I'm least sure (revision 5)

1. **Today's-process values line** uses Ellis's transparency claim (P-073), not Resolution 26-0460's purpose (P-004),
   which the supporter's advocate called the side's strongest value. With Barrett's line kept as it was, parallel
   transparency claims were the brief's way not to give transparency to one side. A checker may prefer P-004. A
   critic may read the order (today's process first, by card order) as favoring the commission's side; the brief's
   list put Barrett first.
2. **Gammage's line** paraphrases "what we believe to be" as "they see" and quotes 12 of his 27 words. "Court funding"
   is my reading of "proper investment in the entire ecosystem of the courts". The six officials are named in full
   only later on the Options tab (Option A's "Who pays"), because the 35-word cap left no room in the line.
3. **The closing line's January-vote note.** A checker may count Atlanta News First's report as enough for a line, or
   treat Ellis's "no talk of an early vote" (P-073) as the other side of that question.
4. **The courts and jail line** no longer gives the officials' reasons. "Six of their leaders" covers the courts,
   where only the court clerk and the probate judge asked. "What the draft funds isn't known" is terse.
5. **The property-owner line:** "the tax rate comes later" generalizes from the 2026 cycle; "wasn't posted" means not
   on the county's budget page (P-045); "Bills also depend on property values" reads P-102's "tax digest value" as
   property values.
6. **Card 3's new challenge** ("four of the six justice offices", P-054) uses the budget-book fact the critic wants as
   support (R4-C2).
7. **Card 2's Aug 19 approval** states a vote's outcome on a card, despite the rule that cards don't use votes as
   evidence. It gives no counts or reasons, as the editor suggested for a similar sentence (FT 189).
8. **The Layer 5 notes** (item 15) don't show on the prototype page, because the template renders only the source
   list there. Their main points also appear in "Not known yet" and on cards 3 and 4.
9. **Item 14:** the 2023 to 2025 actual results appear once, in Layer 2. Card 6 keeps the 2026 deficit as a challenge
   without them beside it.
10. **Layer 4 reads at grade 8.7** (8.3 in revision 4). It isn't a targeted section, but the editor asked for less
    density; the new material is mostly verbatim quotes.
