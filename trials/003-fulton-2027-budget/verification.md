# Verification: Trial 003 (Fulton County 2027 budget)

Method: the AI-only [verification protocol](../../docs/process/verification-protocol.md). There is no human in
the loop ([ADR 0014](../../docs/adr/0014-ai-only-verification.md)). The independent checkers were run by the
coordinating session, not by the drafting agent. Their reports are copied unchanged into
[verification/round1/](verification/round1/) (sha256 below). The planted-error copy and its answer key stay with the
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
