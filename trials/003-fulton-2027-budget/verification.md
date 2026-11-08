# Verification: Trial 003 (Fulton County 2027 budget)

Method: the AI-only [verification protocol](../../docs/process/verification-protocol.md). There is no human in
the loop ([ADR 0014](../../docs/adr/0014-ai-only-verification.md)). The independent checkers were run by the
coordinating session, not by the drafting agent. Their reports are copied unchanged into
[verification/round1/](verification/round1/) and [verification/round2/](verification/round2/) (sha256 below). The planted-error copy and its answer key stay with the
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
