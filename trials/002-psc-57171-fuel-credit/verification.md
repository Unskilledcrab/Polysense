# Verification: Trial 002 (PSC Docket 57171)

Method: the AI-only [verification protocol](../../docs/process/verification-protocol.md). There is no
human in the loop ([ADR 0014](../../docs/adr/0014-ai-only-verification.md)). The checkers were independent
agents run by the coordinator; the drafter ran only the deterministic gate. Copies of each round's inputs and
raw reports are in [verification/round1/](verification/round1/), [verification/round2/](verification/round2/),
[verification/round3/](verification/round3/), [verification/round4/](verification/round4/),
[verification/round5/](verification/round5/) and [verification/round6/](verification/round6/); revision 6.3, before the
reader-first update, and revision 7 with its round 7 reports are in [verification/round7/](verification/round7/).

**Status: revision 7.4 (fixes from the third targeted re-check; last fix pass this cycle).** Round 7 checked revision 7,
the reader-first update, and **passed**. Revisions 7.1 to 7.4 made only the fixes the coordinator listed after round 7 and
after each of three targeted re-checks; none found an unsupported claim, and the third found 2 partial wordings and no high
balance issue. No card's claim, evidence label or side has changed since round 7. See "Round 7", "Revision 7.1", the three
targeted re-checks and "Revision 7.4" at the end. (Revision 6.3 passed round 6 and its targeted re-checks.)

## Round 1 (revision 1, `verification/round1/workspace.r1.md`)

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate (`check_citations.py`, `verify_hashes.py`, `readability.py`) | **Pass.** 269 units (226 cited, 16 analysis, 23 editorial, 4 computed), 0 errors, 0 warnings. 66 hashed files across 35 retrieved sources match (revision 1's README said 65; a miscount). Reading grade: 30-second 6.2, L1 5.6, L2 6.2, L3 8.0 |
| 2 | Blind source check of the real page (`source-check.json`) | **197 supports, 28 partial, 1 does not support, 0 wrong span.** 4 tags questionable, 0 invalid; 25 framing issues (3 high, 12 medium, 10 low). **269/269 evidence spans grounded.** No wrong dollar amount, rate, date, time or phone number; blacked-out figures handled correctly |
| 3 | Bias panel | **Fail.** Advocate for Georgia Power's and GAM's positions 3/5 (`panel-utility.json`). Advocate for PIA Staff's and GIPL's positions 3/5 (`panel-morecredit.json`). Standards editor: neutrality 3/5, clarity 3/5 (39 issues: 4 high, 21 medium, 14 low; 18 flip-test asymmetries) (`panel-editor.json`) |
| 4 | Planted-error calibration (fresh blind verifier on a mutated copy, `source-check-cal.json`) | **12/12 planted errors caught.** Its flags on unplanted text (lines 28, 132, 146, 210, 118, among others) matched other checkers and are triaged below |

Both advocates said the same thing from opposite sides: some of their parties' strongest passage-backed
arguments were missing, often where the page quoted the other side's rebuttal. The editor found that the
evidence labels did not follow the printed rule. The source check found three misstated positions (GIPL in
card 1, GAM in card 3, a party named without a passage).

### What round 1 found, and what we did

SC = blind source check; CAL = the calibration verifier's flags on unplanted text; U = advocate for Georgia
Power's and GAM's positions (MA = missing argument, UW = wording, IM = imbalance, LD = label); M = advocate
for PIA Staff's and GIPL's positions (same codes); E = standards editor (E1–E39 issues, F1–F18 flip tests).
Line numbers (L) refer to revision 1. Cards are numbered as in revision 2.

| # | Finding (who raised it) | Disposition in revision 2 |
|---|---|---|
| 1 | **High.** Card 1 gave GIPL Georgia Power's principle that real-time customers should carry "only costs they can change"; GIPL wants them to carry pipeline, coal and overhead costs (SC L146 high, CAL L146, M UW1 high) | **Fixed.** Each party gets its own reason in its own sentence: Georgia Power's unavoidable-cost and fixed-price points (P-130), GIPL's dispatchable-resources and costs-and-benefits points (P-100). "In short" says "each for its own reason" |
| 2 | **High.** Card 3 listed GAM under "Who says it" for Georgia Power's winter-peak plan, wrote "no part" for GAM's "No specific portion", and cut GAM's second half; Ly's name was cited to the wrong passage (SC L171 high, CAL L171, E10, M IM7, U UW9, U MA5) | **Fixed.** GAM is no longer a speaker on card 3. Its P-110 sentence is quoted with "specific" wherever it applies: card 3 "What challenges it" (next to P-127, since that plan books real-time money to fuel to cover new fees), cards 4 and 5 "Who disagrees", Layer 2 and the Layer 4 intro. Card 4 carries both halves of P-110. Ly is cited to P-153. GAM's description of the plan's effect (P-113) stays under card 3's "What supports it"; see contradiction C1 |
| 3 | **High.** "Resource Supply Management" and "five parties" had no passage; P-002 names only Jim Clarkson (SC L28, L132 does-not-support and high, CAL L28 and L132, E6) | **Fixed.** New P-162, the filing's document page, whose company field reads "Resource Supply Management". The page uses the full name only and no longer equates it with the "RSM" of the fuel-case order (P-017 keeps "RSM" as written). "Five parties" became the list of filers. P-002 dropped |
| 4 | **High.** The $322 million and $1.35 come from page 7, which Staff's Oct 7 corrections changed; no caveat where used; the corrections were described too broadly; "wiped out"; "fuel rate … $1.35 a month" mixed a rate and a bill amount (SC L118, CAL L118, E3 high, E34, U UW7, U IM5, M UW6) | **Fixed.** Plan C says the figures are on page 7, that the corrections changed a line on that page, and that we show them as first filed because the corrected page is a scan; "eliminated" is quoted; the $1.35 is a bill amount for a home using 1,000 kWh and covers the fuel charge only. Everywhere else the corrections are listed exactly (Tables 2, 3, 4 and 7; lines on pages 7 and 20). Read by eye in revision 1 (README D5), the page-7 correction changes "$329 million" to "$328 million", not these two figures, but a scan can't be checked by machine, so the caveat stays |
| 5 | **High.** Evidence labels did not follow the printed rule: matched labels on opposing cards graded side facts; "Uncertain" was never used; "a party's own figures" could earn "Supported"; "with limits" and "credible" undefined (E1 high, SC L138 questionable, CAL L138, U LD1–LD5, M LD1–LD4) | **Fixed.** One printed rule: the label grades the card's main factual premise, which it names. "Supported" needs the case record or parties other than the claimant, with no party disputing; "Mixed" means conflicting accounts with some record support; "Uncertain" means the deciding numbers are blacked out, missing or not yet known; a second part gives the status of the size or prediction. Six cards are now "Mixed on the premise …; Uncertain on size"; GAM's card is "Supported that a bigger fuel credit leaves less real-time money for base costs; Uncertain whether and how much base rates would rise". "With limits" and "credible" are gone. Label choices that the advocates disputed are in C2–C5 |
| 6 | **High.** Option 1 typed "mostly values" and Option 2 "factual and values"; card 2's "gap" and "under-counted" took Option 2's premise as the baseline (E2 high, F8, U LD1, E38) | **Fixed.** Both cards are "factual and values" and end with mirrored values sentences. Staff's figures are "would have added" and "the difference", not a gap or an under-count |
| 7 | **High.** Speaker, filing and date given only on two cards (E4 high, F7, CAL L183) | **Fixed.** Every "Who says it" names the witnesses, the filing and its date (P-041, P-117, P-059, P-152, P-153 and the witness passages), then quotes them |
| 8 | "Commissioners are elected" though Wise was appointed; "Wise joined" though he served 1994–2018 (SC L16, L17, E29, CALF L17) | **Fixed.** "The PSC describes its commissioners as elected" (P-031), then Shaw's 2019 appointment and 2020 election, Wise's 2026 appointment, and that Wise returned, first elected 1994 and stepped down 2018 (P-033 to P-035) |
| 9 | "The big customers' own prices would not change" stated as fact for every plan; baseline power is billed at standard rates (SC L9, L79, E5) | **Fixed.** Attributed to GIPL for its plan (P-105); Georgia Power said the same of its 2010 change (P-046); and a tagged analysis notes that a later base-rate change would reach real-time customers' baseline power (P-043). Removed from the 30-second version |
| 10 | Staff's claim that standard-rate customers "keep paying" the left-out costs stated as fact (SC L103, CALF L103) | **Fixed.** "PIA Staff says customers on standard rates absorb …" (P-063); Georgia Power's and GAM's view sits in the same plan (P-124, P-110) |
| 11 | GAM's condition dropped; its "options" are the two take-or-pay choices only (SC L109, CAL L109, E8, E11, F1) | **Fixed.** The same sentence appears in Plans B, C and D: "GAM says that if a change is needed, either take-or-pay option would 'adequately address' the concerns" (P-111). Plan A gives GAM's lean |
| 12 | Staff's fallback had two triggers; the page gave one (SC L116) | **Fixed.** "If the PSC prefers a demand-based split, or treats old and new fees differently" (P-086) |
| 13 | "Other parties" for GIPL's "intervenors" (SC L112, L174, CAL L112, L174, E32) | **Fixed.** "Intervenors", defined in Layer 1. The Oct 15 hearing and the Layer 2 opening use the order's and Georgia Power's word too (P-009, P-053) |
| 14 | No passage says public witnesses will be heard on Nov 12; P-027's condition dropped; the legal notice's phone numbers were for the Sep 1 hearing; P-030 doesn't say how to submit (SC L10, L46, L47, L48, CAL L10, E30) | **Fixed.** "No filing we read says whether public witnesses will be heard on Nov 12", with the PSC's public-witness contact numbers (new P-163). P-027's condition is in full. The disability note is tied to the Sep 1 notice. The page says "public comment", quotes the 24-hour rule, and says the page does not say how to send one |
| 15 | **High (U).** "PIA Staff found" plus the page's own "So base rates would not necessarily rise" sided with Staff and ignored the PSC order's mechanism and Staff's own caveat (SC L210 questionable, CAL L210, U UW1 high, U MA9, E17, F3) | **Fixed.** The analysis sentence is gone. Staff "estimates". Card 7's "What's uncertain" quotes Staff's "direction and magnitude" sentence (P-090) and the order's regulatory-asset rule (P-016) |
| 16 | The 30-second version led with data centers in the page's voice and ended on GAM's concern (SC L8, E26, M UW13) | **Fixed.** It names the whole customer group first and attributes data centers to GIPL (P-097); item 2 adds Staff's point that a base rate case would decide the size and direction (P-090) |
| 17 | "Day-ahead" and "hour-ahead" not in any passage; RTP-HA's 5 MW minimum missing; "any business customer"; "big" overstates a 250 kW threshold; kW measures demand, not use (SC L59, CAL L55, L59, E39) | **Fixed.** New P-164 (Staff's footnote spelling out RTP-DA and RTP-HA); the 5 MW minimum; "a demand of at least 250 kilowatts". "Big" dropped from the title, 30-second version and home items |
| 18 | P-131 rejects "Table 1" without naming coal rail car leases or overhead; "Georgia Power agrees these costs are not in the credit" overstated (SC L77, L108, L194, E35) | **Fixed.** P-065 now spans the whole of Table 1, header row included, so Georgia Power's column ("Exclude from RTP credit calculation") supports each cost. Card 6: "Georgia Power asks the PSC to decline Staff's recommendations, which include these costs" |
| 19 | "These are the four plans in the record" overstated completeness and was a factual claim tagged editorial (SC L96, CAL L96, E7) | **Fixed.** "Today's method and the three plans in the testimony we could read", with the scanned filing named in the tag |
| 20 | "None of the four parties … archived" understated GAM's closeness to today's method; GIPL reports that Georgia Power "argues that the current method is fair" (SC L101, CAL L101, CALF L101) | **Fixed.** "Whose testimony we could read"; GAM's view and conditional acceptance; GIPL's report about Georgia Power (P-099) |
| 21 | Georgia Power "counted $264 million", which is one part from its own analysis; the total is blacked out (SC L102, M UW12, E17, F2) | **Fixed.** "Georgia Power estimates that one part …, capital substitution, came to $264 million in 2025" (P-052); card 7 says GAM's 2025 total is blacked out (P-112, P-114) |
| 22 | Plan A dropped "marginal" and "pool sales or purchases" (SC L100, CAL L100) | **Fixed.** P-048's words are quoted, and "marginal replacement fuel cost" is glossed |
| 23 | Jargon: peak vs. energy splits, transmission split, strike, AIER, replacement fuel cost, embedded costs, day/hour-ahead, proposed orders, Administrative Session, monthly access charge, base rates (SC L108, L30, L150, CAL L116, E22, E23, E39) | **Fixed.** Defined in place or in "Words used on this page": base rates, base rate case, peak demand and energy use, marginal replacement fuel cost, very large customers, intervenors, proposed orders, Administrative Session, strike, AIER (as another way to measure marginal cost), RTP-DA and RTP-HA, the access charge |
| 24 | Only GAM's stake was described; Newsome is PSC staff, not an outside expert (SC L207, L134, U UW12, E13, F6) | **Fixed.** A roles list opens Layer 4 for every party, each sourced: Georgia Power and its witnesses' jobs (new P-165 to P-167), PIA Staff's role in the order (P-006) and its witnesses, GIPL with its congregations and witness, GAM's members "under a variety of rate schedules" and witness, and Resource Supply Management's witness |
| 25 | Staff's missing arguments: its 2010 reply (P-073), its double-recovery and energy-rate reasons (P-083, P-084), "no financial exposure" (P-090), growth (P-077, P-079), the public totals (P-150), the company's benefit under today's method (P-091), its role (P-006) (M MA1–MA4, MA11–MA13, M IM1, M UW3) | **Fixed.** Card 2 (P-073 after Georgia Power's "since 2010" point; P-077 growth; P-079), card 4 (double recovery and "To eliminate this risk"), card 7 (P-090), card 6 (totals as `{computed}`), Plan A (pointer to Staff's return-on-equity estimate), Layer 4 roles (P-006). "No financial exposure" is not repeated in Layer 2 |
| 26 | GIPL's missing arguments: the full data-center claim (P-097), its "two critical factors" (P-099), its pipeline plan with no card (P-104, P-102, P-096), its reason for changing (P-155), its reply on scope (P-136), P-148 behind its conditional advice (M MA5, MA6, MA8–MA10, M UW2, UW8–UW10, M IM8, E14, E15, F11, SC L128) | **Fixed.** Card 6 quotes P-097 in full, with Georgia Power's answers (P-054, P-121). P-099 is in Plan A and card 7. New card 5 for GIPL's pipeline plan. P-155's reason is given wherever Georgia Power's account of the change appears. P-136 and P-107's recommendation are in "Also raised". P-148 is cited with P-096 |
| 27 | Georgia Power's missing arguments: cost causation (P-050, P-051), customer protection and the infrastructure analogy (P-127, P-126), building on Staff's 1-CP and "cannot be reconciled" (P-056, P-125), the fair-share disclaimer and "would undermine" (P-121), price risk (P-042), $102 a year with scope (P-054), the full critique of Staff's guidelines (P-120, P-118), the last step of its take-or-pay argument (P-130), feasibility of reports (P-132), its agreement to the review (P-053) (U MA1–MA4, MA7, MA10–MA12, U UW4, UW5, UW10, U IM4, U IM6, E16, F4, F10, F16, SC L55) | **Fixed.** Cards 1 and 3 (P-050, P-051, with P-051 after Staff's "partial solution" challenge), card 4 (P-127, P-126, P-123 and P-125 quoted), card 3 (full P-126), card 6 (P-121 in full, P-120, P-054 commitment), Layer 2 (P-042, P-054 with a scope note, P-132, P-053), Plan C (P-120, P-118), card 1 (P-130's last sentence). Partly for U MA4: P-045's "driver of new economic activity" sentences are not used (they concern why real-time pricing exists, not the credit) |
| 28 | GAM's missing points: both halves of P-110; the public $214 million and $264 million (P-114) (U MA5, MA6, U UW3, F13) | **Fixed.** Card 4 quotes both halves of P-110; card 7 gives both figures and says only the 2025 total is blacked out |
| 29 | Partial agreement credited one way only (U MA8, U IM3, U UW6, M IM2, E11, F1) | **Fixed.** GIPL's agreements with Georgia Power (P-099 "I agree with this concept"; P-101 "addresses one problem"; P-096 recovering new fees from large customers) and with Staff (coal, overhead, a pipeline share) are shown in Plans A–D and cards 3 and 7. Georgia Power and Staff both count new pipeline fees in the credit (P-065), shown in Plans B and C. "Only one party backs Option 2" is deleted |
| 30 | Georgia Power quotes cut so they blunt or flip its meaning: P-054, P-121, P-120, P-126, P-130 (U UW2, UW4, UW5, U IM6) | **Fixed.** Each is quoted through the cut sentence; P-054's freeze is no longer used against GAM |
| 31 | Caveats on Staff's estimates repeated, upward-only, and without the direction; Georgia Power's figures carried none; "found" and "counted" vs. "estimates" (U UW8, U IM2, U IM5, M UW4, M IM4, E17, E38, F2, F3) | **Fixed.** One caveat wherever Staff's take-or-pay figures appear: "Staff says its data were incomplete, so these figures may be too low." Card 2's "What's uncertain" is neutral. Every party's figure is "estimates" or "said", and Georgia Power's per-$100 million examples are marked "not forecasts" |
| 32 | Replies handled unevenly: Staff's card answered every Georgia Power challenge, Georgia Power's card answered none (U IM1, M IM1) | **Fixed.** One printed rule: under "What challenges it", the card's own side's documented reply follows a challenge when there is one. Card 3 now carries Georgia Power's replies (P-051, P-056); card 2 carries Staff's 2010 reply (P-073); P-145 is worded "PIA Staff had said", since it came before the rebuttal |
| 33 | The base-side argument credited to Georgia Power alone, with GIPL's reply last (U UW11) | **Fixed.** Georgia Power and GAM together (P-124, P-110); GIPL's view follows in the same form |
| 34 | Cost study's author and age missing (U UW13, E31, CALF L87) | **Fixed.** "Georgia Power's cost study for the year ending Jul 31, 2023, as cited by GAM" (P-116) |
| 35 | "0.49 percent" reads as half of one percent of profit (U UW14, E33) | **Fixed.** "About half a percentage point", with 10.3, 10.9 and 10.9 percent and the 9.5–11.9 percent range (P-091) |
| 36 | "We could not check" applied only to a Georgia Power claim (U UW15); give Georgia Power's own stated reason for GIPL's change (M UW9, MA9) | **Partly fixed.** The caveat stays: it is the only claim on the page that rests on a document the docket lists without a file (P-003). The page now gives the reason in Georgia Power's own footnote (P-155). Staff's absorption analysis is labeled as Staff's estimate, and P-145 is Staff's statement about its own testimony, so neither needs the same caveat. Transcript page numbers are not printed on the page; they are in P-155 |
| 37 | Title named only the fuel charge (U UW16) | **Fixed.** "… and your power bill" (page and `page.yaml`) |
| 38 | Questions to weigh used Georgia Power's or GAM's framing (Q1 "help pay", Q2 "now … later", Q3 new fees only); add a who-gains question and an optional hidden-numbers question (SC L216, U Q, M Q, M IM5, E18, E19) | **Fixed** for Q1–Q3, using the editor's and the advocates' rewrites. **Adopted in a neutral form:** Q4 asks how each plan affects homes compared with businesses, since GAM (P-116) and GIPL (P-099, P-102) both say customer groups pay different shares. **Not adopted:** the hidden-numbers question; blacked-out figures are flagged card by card, and the commissioners see the confidential versions, so it concerns public access more than the decision |
| 39 | Layer 2 opening ran 2–5 times its 80-word budget, with the explainer and glossary between the decision and its list (E20) | **Fixed.** 60 words, then five one-line items; "How real-time pricing works", "Words used on this page", "Fuel side versus base side" and "Also raised" follow as subsections |
| 40 | "In short" lines ran 39–48 words with grade 8–11 first sentences (E21) | **Fixed.** Each is one claim of 19–24 words plus one short counterpoint (30–41 words in all). **Partly** for the rest of Layer 4: it reads at grade 11.9 by the tool (10.2 with quote-aware sentence counting) because the advocates asked for full verbatim quotes; speaker sentences were split from quotes to help |
| 41 | 30-second items should be one or two sentences of ≤ 25 words (E27) | **Partly fixed.** Items run 42–54 words. Item 3 keeps three sentences, because the source check required the public-witness and comment-rule wording and the editor the date-change note |
| 42 | Date-change notes only in Layer 1; the computed deadline would move with the vote (E28) | **Fixed.** Notes in the 30-second version and Layer 1; "If the PSC moves the vote, the deadline moves too" |
| 43 | "Option B … uses Option 1" (E24) | **Fixed.** Plans A–D; Option 1 and Option 2 only for the take-or-pay choices, as the record names them; `page.yaml` chips renamed |
| 44 | No plain statement that no filing gives a plan's net effect on a typical home's bill (E25) | **Fixed.** "No filing we read gives the net effect of any plan on a typical home's total bill", with the reason in its tag |
| 45 | Cards 5 and 6 titled as one party's argument stated as fact; GAM's side label cast it as the compromise (E8, E9, F12, M LD5) | **Fixed.** Titles name proposals ("Count more fuel-related costs in the credit", "Limit the shift of real-time money to fuel"); GAM's side reads "favors limiting the shift to fuel; accepts either … if the PSC finds a change is needed" |
| 46 | Plan D got its sponsor's hedged base-rate wording and a price reassurance; lacked the pipeline-fee payer and Option 1's blacked-out size; listed three congregations by name (E12, F5, F14, F18) | **Fixed.** Plans B, C and D share the same base-rate sentences (P-115, P-105, P-090) and each names who pays new pipeline fees; Plan D cites P-139; "filing with three of its member congregations" |
| 47 | Plan A's "Who benefits" named no customers (F15) | **Fixed.** "Customers on standard rates, Georgia Power and GAM say" (P-124, P-111) |
| 48 | Layer 2's pipeline item left out GIPL and GAM (F17) | **Fixed** |
| 49 | Reports had no card (E15, M IM9) | **Fixed by explanation.** Layer 4 says no card covers them because the testimony gives little reasoning beyond Layer 2's two sentences; Georgia Power's feasibility point is added (P-132) |
| 50 | Plan B's "Who benefits" held a rate-design argument (SC L110) | **Fixed.** It lists customer effects only |
| 51 | GAM's card was the best supported, including a `{computed}` $1.65 vs. $1.48 comparison no party made and the PSC order under "What supports it" (SC L208, M IM3, E36) | **Fixed.** The comparison is gone from the card; the per-$100 million examples stay only in Layer 2, marked as examples, not forecasts. P-016 moved to "In short" and "What's uncertain", worded neutrally. See C8 |
| 52 | GIPL's P-102 was listed as support for Staff's energy split (SC L184) | **Fixed.** P-102 now supports only GIPL's own card |
| 53 | "PIA Staff itself said" (M UW11) | **Fixed.** "Itself" dropped; P-064's reason ("policy and cost causation arguments") given |
| 54 | GIPL "agrees" in GAM's card (M UW7) | **Fixed.** "GIPL says"; GIPL's P-099 sits under "Who disagrees" on card 7 |
| 55 | Say what is public as well as what is hidden (M IM6) | **Fixed.** Staff's public Delta column and yearly totals, GAM's public figures, and "only PIA Staff's estimate is public" on card 2 |
| 56 | Briefs and proposed orders, and "Dollar figures are each party's own estimates" though some are actuals (SC L38, L140) | **Fixed.** Both defined; "Dollar figures come from the parties' own filings, and many are estimates" |
| 57 | Staff's Table 1 header and Georgia Power's column needed confirming (M MA7) | **Fixed.** P-065 widened to the whole table; merged cells checked in the `.docx` XML and noted on the passage. Plan C now says Option 2 is one of Georgia Power's two choices (P-058, P-065) |
| 58 | The same five commissioners vote "barring a vacancy or recusal" (SC L18, valid) | **Kept**, and listed under "Where I'm least sure" |

### Contradicting suggestions

| # | Point | Views | Choice |
|---|---|---|---|
| C1 | GAM's place on card 3 | SC: no passage shows GAM backing the winter-peak split; listing GAM implies it. U (UW9) and M (IM7): GAM's description of the plan (P-113) is legitimate support | GAM is not a speaker for card 3. Its words about the plan's effect (P-113) stay under "What supports it"; its "No specific portion" principle (P-110) sits under "What challenges it", next to P-127 |
| C2 | Card 7 (GAM) label | U: "Supported, with limits", noting Staff's concession and the public figures. M: "Supported" that less money goes to base, "Mixed" on whether base rates rise. E: use "Uncertain" where the deciding numbers are missing | Two parts: "Supported" that a bigger credit leaves less real-time money for base costs (no party disputes it), and "Uncertain" whether and how much base rates would rise. "Uncertain" rather than "Mixed", because the deciding facts don't exist yet: Staff itself says a base rate case would set the direction and size (P-090) |
| C3 | Card 2 (Option 2) label | U: "Supported" only that Option 2 raises the credit; the size is Staff's estimate alone. M: "Supported, with limits", built from Georgia Power's own data, probably too low. E: grade the card's main premise | E's rule: "Mixed" on the premise that the contracts' costs belong in the credit (Georgia Power and GIPL dispute it), "Uncertain on size (only PIA Staff's estimate is public)". Both advocates' facts are in the card body. The label doesn't say "from Georgia Power data", which U said lends Georgia Power's authority to Staff's calculation |
| C4 | Card 1 (Option 1) label | M: "Mixed" on whether Option 1 keeps the credit marginal-cost based. U: the label was fair | "Mixed on the premise" (M, E's rule): PIA Staff disputes it, and Georgia Power's own direct testimony says Option 1 "is not based on all Georgia Power generating units running" (P-058). U's undisputed point, that Option 1 raises the credit, is in Plan B and card 2 |
| C5 | Card 6 (other costs) label | M: "Supported, with limits", since P-144 shows standard-rate customers pay fuel costs net of the credit. U: no change | "Mixed on the premise", because Georgia Power says real-time customers pay toward fixed costs through the base side (P-124, P-121); "Uncertain on size", with the public totals in the card |
| C6 | Staff's $1.35 a month | U: flag it as fuel-only and from a corrected page. M: keep it | Kept, with both flags (item 4) |
| C7 | "We could not check" | U: apply it equally or print the transcript pages. M: add Georgia Power's own reason | Kept, with the reason (item 36) |
| C8 | Georgia Power's per-$100 million examples | U: Georgia Power's public figures belong on the page. M: the $1.65 vs. $1.48 comparison assumes base rates rise dollar for dollar | Both figures stay in Layer 2 as examples, "not forecasts", followed by the net-effect sentence. No comparison, and none in GAM's card |
| C9 | GIPL's P-099 | U: credit GIPL's agreement with the base-side concept. M: credit its "two critical factors" rebuttal | One sentence carries both, in Plan A and card 7 |
| C10 | Q3 | U: fine as written (new fees, very large customers). M and E: include old and new fees, storage, and peak vs. energy | M's and E's rewrite, which keeps very large new customers as one of the groups |

## Round 2 (revision 2, `verification/round2/workspace.r2.md`)

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate | **Pass.** 357 units (296 cited, 23 analysis, 34 editorial, 4 computed), 0 errors, 0 warnings, and still clean under the new whole-number check. 66/66 hashes match. Reading grade with the updated `readability.py`: 30-second 7.5, L1 6.2, L2 7.8, L3 7.7 (Layer 4 9.6) |
| 2 | Blind source check (fresh agent, `source-check.json`) | **289 supports, 7 partial, 0 does not support, 0 wrong span.** 5 tags questionable, 0 invalid; 21 framing issues (7 medium, 14 low). **357/357 evidence spans grounded.** The accuracy part of the pass rule is met |
| 3 | Bias panel | **Fail.** Advocate for Georgia Power's and GAM's positions 3/5, "close to 4" (1 high item). Advocate for PIA Staff's and GIPL's positions 3/5 (4 items marked high: 2 wording, 2 imbalance). Standards editor: neutrality 3/5, clarity 3/5 (36 issues: 1 high, 14 medium, 21 low; 21 flip-test items) |
| 4 | Calibration | Carried over from round 1 (same verifier prompt and method) |

Each side now named specific, different gaps for itself, and the editor saw a mild lean toward PIA Staff's
framing, against both Georgia Power and GIPL. The items several checkers shared came first: one label rule
applied the same way to every card, the plain fact that both take-or-pay options raise the credit, the order of
the cost-model exchange, and Plan C's unanswered critique.

### What round 2 found, and what we did

SC = blind source check (L = revision 2 line); U = advocate for Georgia Power's and GAM's positions; M = advocate
for PIA Staff's and GIPL's positions (MA = missing argument, UW = wording, IM = imbalance, LD = label); E =
standards editor (E1–E36 issues, F1–F21 flip tests). Cards are numbered as in revision 3: the new card 7 is
Georgia Power's, and GAM's card is now 8.

| # | Finding (who raised it) | Disposition in revision 3 |
|---|---|---|
| 1 | **High.** GAM's card graded a narrower premise than its own "In short" and got the page's only "Supported", while cards 2 and 6 were graded on broader, contested premises (E32 high, M IM1 high, M LD7, U LD4, F19) | **Fixed.** Every label now grades the main claim its "In short" states, in two short parts: "Main claim: … Size: …". GAM's card is "Mixed", like the others. Undisputed mechanics are stated in the text, not given their own label parts (C1, C2) |
| 2 | **High.** The page never said that today's method counts take-or-pay energy but not cost, so both options raise the credit; card 1 never said Option 1 raises it, and "leave out" read as the do-nothing choice (U IM1 high, U MA1, U UW1, U UW3, U LD1, M MA2, M IM6, E9) | **Fixed.** Layer 2's first item and Plan A say it plainly, citing all four parties' filings (P-049, P-058, P-072, P-076, P-115, P-128). Card 1 is retitled "Remove …"; its "In short" and support say Option 1 raises the credit, and its size label says the parties agree |
| 3 | **High.** Georgia Power's model claim (P-056, Aug 14) came after GIPL's challenge to it (P-103, Sep 23), so it read as the reply and had the last word; GIPL's "not supported by any available evidence" was cut (M UW11 high, M IM2 high, M UW5, M MA6) | **Fixed.** In Plan B and card 3 the two now run in date order, claim then challenge, with P-103's sentence quoted |
| 4 | **High.** Plan C's "What's uncertain" held Georgia Power's sharpest attack with no reply, plus a repeated errata note (M UW6 high, E16, F7, SC framing L133) | **Fixed.** The attack moved to card 6's "What challenges it", quoted with "new policy recommendations" and "by their own admission" (U MA6, U UW4), followed by Staff's documented answer (its matching guideline, P-062; consistency with the 2010 proposal, P-073) and GIPL's point that the PSC holds large loads "to a higher standard" (P-097, M MA11). Plan C's "What's uncertain" now lists only its blacked-out figures, as Plan B's does (U IM6) |
| 5 | "Very large customers" merged GAM's 100 MW-and-up class with the filings' "large load customers", which have no stated threshold (SC L86 questionable and medium framing, E10) | **Fixed.** "Large-load customers" for Georgia Power's and Staff's term, with a note that the passages give it no threshold; "very large" only for GAM's 100-megawatt class. Plans B and D and card 3 use the filings' term |
| 6 | "The size of the change rests on Staff's estimate alone" ignored Georgia Power's blacked-out Option 2 estimate and cited the Option 1 attachment (SC L187 partial and medium framing, M UW9) | **Fixed.** Card 2's label says Staff's estimate is the only public one and Georgia Power's is blacked out; "What's missing" cites P-138 to P-140 |
| 7 | "None asks to keep it unchanged" understated GAM; Georgia Power's view of today's method came through GIPL's paraphrase (SC L114 questionable and medium framing, E11, U UW10, F4) | **Fixed.** Plan A: "GAM proposes none: it argues against a bigger credit, and accepts either take-or-pay option only if the PSC finds a change is needed"; Georgia Power's view in its own words (P-121) |
| 8 | PIA Staff's look-back profit estimate appeared four times, sat under Plan A's "Who benefits", and was the only challenge on GAM's card, with no note on the order's regulatory-asset rule (U IM3, U UW7, U UW8, F5) | **Fixed.** Stated once, in Layer 2, as a look-back what-if, with the order's rule beside it (P-016). GAM's card now has GIPL's capital-substitution point as its challenge (P-098) |
| 9 | Missing reasons: GAM's "fully fixed costs" versus "marginal cost-based rate" and its conflation point (U MA4, U MA5, U UW5, U UW6, E24); GIPL's lambda reason (M MA10, M UW13, E30, F17); GIPL's "This shifts costs to FCR customers" (M MA5, M UW3); Staff's "simply for illustration purposes" where card 3 uses its showing (M MA9, E23, F14) | **Fixed.** GAM's reason and conflation point (P-110, P-168) are in card 3's "Who disagrees"; card 4 has the lambda half with "can contribute" (SC L214, E28). GIPL's lambda reason (P-148) is in card 5's support and, in one clause, card 4's "What's uncertain" (C8). P-099's sentence is in Plan A's "Who pays"; P-145 follows Staff's showing in card 3. Card 3's "while this plan books …" contrast, which U called an untagged inference, is gone |
| 10 | Only the "count more" side had a card on Layer 2's third question (E31, F18, U IM2) | **Fixed.** New card 7, "Keep other fuel-related costs out of the credit (Georgia Power)": the marginal-cost design (P-121), fixed costs that "do not vary with changes in demand" (P-049), base-side money (P-124), the competitive-value point (P-121); PIA Staff and GIPL disagree; their cost-variation points are the challenges. **Partly** for U IM2's second half: GAM's pipeline position has no card of its own; its reasons are now in cards 3 to 5 |
| 11 | Truncated quotes: P-120's "by their own admission" and "new policy recommendations"; GIPL's opening clause (the brief named P-098; M UW1's item is P-105 on line 98); "two critical factors" (U MA6, U UW4, E16, M UW1, M UW3) | **Fixed.** P-120 in full; P-105 with "will assist in reducing FCR rates for FCR customers, but"; "two critical factors" in Plan A and card 8; P-098 quoted from "a necessary cost of service" |
| 12 | Georgia Power's account of GIPL's switch appeared four times and supported Staff on card 4 without the "could not check" caveat (E26, F8, F15, M UW12, M IM7, SC framing L213, U IM4) | **Fixed.** In card 5 only, with the caveat; Plan D keeps a one-line pointer; removed from card 4 |
| 13 | The errata caveat covered tables the corrections don't touch; one note said "read by eye" and another "could not be read"; an internal file was named in a reader-facing tag (M UW8, SC framing L131, E15) | **Fixed.** The note lists exactly what was corrected and says the other Staff figures on the page come from pages and tables not on that list. Plan C says only that the corrected page is a scan our tools could not turn into text; the by-eye reading stays in README D5 |
| 14 | Card 6's yearly totals include costs from other cards (SC medium framing L240) | **Fixed.** The card says so and adds the one public single item Staff would count: corporate overhead, about $6 million a year (`{computed}`) |
| 15 | The glossary blurred the credit's average replacement fuel cost with lambda (SC L85 questionable and medium framing); undefined jargon in quotes: system lambda, capital substitution, winter CP, cost model, under-collected balance, tariff (SC framing L214, E22) | **Fixed.** Separate entries for the average hourly marginal replacement fuel cost, system lambda and capital substitution, plus winter CP, large-load customers, large-load cost model and under-collected fuel balance; "tariff" glossed in place |
| 16 | Plans B–D had word-for-word "Who benefits" lines, and only Plan C showed dollar figures, with no comparability note (E14, F6, SC medium framing L123) | **Fixed.** Each plan names what it adds; the Layer 3 intro says only Plan C's take-or-pay part has public estimates, because Georgia Power's Option 1 estimates are blacked out, and a hidden figure is not zero |
| 17 | Plan A's "Who pays" used Option 2 as the only yardstick (E12) | **Fixed.** "They are measured against Option 2; Georgia Power's estimates for Option 1 are blacked out" |
| 18 | Questions: Q1 assumed Staff's "serve all customers" and turned Georgia Power's principle into "enough"; Q2 made Staff's "size and direction" the arbiter; Q3 left out GAM's "none"; a values question was missing (SC L257 questionable and framing, U Q and UW9, M Q, E34, E35, E36, F21) | **Fixed.** Q1 states the fixed-cost premise with sources and asks whether payments should be matched against those costs in the fuel account or keep counting toward base costs. Q2 uses the PSC order as the neutral source. Q3 starts with GAM's question (any real-time revenue at all?) and adds the move to base rates that Staff and GIPL offer. New Q4 is the values trade-off U named. The homes-versus-businesses question now has a cited premise. **Not adopted:** M's optional hidden-numbers question, for round 1's reason (C10) |
| 19 | The lead ran 143 words, named GIPL and PIA Staff before introducing them, led with GIPL's data-center framing, gave only Staff's view on base rates, and never said no filing gives the net effect (E1–E4, F1, F2, SC L8 partial and medium framing, U UW12, M MA3) | **Fixed.** 109 words. It uses Georgia Power's own words on why the review exists (P-053), says "Larger businesses", names no other party, cites GAM's and GIPL's base-rate statements without making any party the arbiter, and says no filing gives the net effect on a typical home's bill. GIPL's data-center statement moved to Layer 2. **Partly:** the real-time-prices note (E3) stays in Layer 2, and bullets run 28–46 words, not 15–20 (C9) |
| 20 | The decision block ran about 200 words; its "Base side" item is a fixed condition, not a choice; take-or-pay contracts and pipeline fees were used before being defined; the review history was Georgia Power's account in the page's voice (E7, E8, E9, SC framing L58, L66) | **Fixed.** "What's being decided" is 40 words; four one-line items, the first two glossed at first use; the order's condition moved to "Fuel side versus base side"; the review-history sentence cut |
| 21 | Labels of 50–78 words used "premise"; "In short" lines ran two sentences (E19) | **Fixed in part.** Labels read "Main claim: … Size: …", 55–68 words with Side and Claim type (was up to 81). Each "In short" is one sentence of 21–33 words that names who disagrees (E33, F20) |
| 22 | Card 1's label counted Georgia Power's own caveat against its premise, though the same passage supports it (E18, F11, U UW2, M LD1) | **Fixed.** The label names each side's reasons, including GIPL's (M LD1); the caveat stays only in "What challenges it", beside Georgia Power's same-passage statement |
| 23 | Card 2 marked Staff's public, undisputed estimate "Uncertain" because Georgia Power's is blacked out (M LD2, M IM4) | **Fixed.** The size part names the public estimate; the printed rule now says a blacked-out figure is neither zero nor evidence against another party's figure |
| 24 | Cross-party credit in labels was uneven, and owners of blacked-out figures were named on some labels only (E21, E25, F13, U IM4, U LD2, M LD3, M LD4, M LD5, M MA1, M IM4) | **Fixed.** Card 3 credits GIPL's peak tie and says no party disputes that large-load growth drives new fees; card 4 says Georgia Power agrees the pipelines serve "all periods" (P-051, also added to card 4's support); card 5 states GIPL's "whenever" premise; every size part names the owner of a blacked-out figure |
| 25 | Plan B was the only plan whose "Where it stands" recorded no opposition (M UW4) | **Fixed.** Plans B, C and D each record partial support and opposition from the other parties |
| 26 | Only PIA Staff got a mission statement; witness titles took different forms (E17, F9, F10, U IM5, SC framing L150); keep "independent" (M UW7) | **Fixed** with one template (C3): what each party is and whom it represents, and each witness's title in the same form. PIA Staff is "PSC staff that takes part in this case as a party"; its consultants are "two principals of J. Kennedy and Associates" |
| 27 | "Its data were incomplete" moved Georgia Power's data gap to Staff (M UW2) | **Fixed.** "The data Georgia Power provided were incomplete" |
| 28 | One-sided harsh language: Staff's "unreasonably suppressed" missing (M IM5) | **Fixed.** Quoted in card 2 |
| 29 | Missing Georgia Power points: the 2010 basis (U MA2), revenue that covers the cost to serve (U MA3), its backing for GAM's premise (U MA7), cost allocation belongs in the next base rate case (U MA8), and a substantive answer on other costs (U UW11) | **Fixed.** P-046 in card 1; P-055 and P-054 in card 6's "Who disagrees", next to GIPL's subsidy point, so they no longer read as an answer on other costs alone (SC framing L238); P-124 and P-047 in card 8; P-133 in card 5, without "strays"; card 7 gives the substance |
| 30 | Missing Staff and GIPL points: the shared fallback and P-104's provisional split (M MA7), GIPL's working-copy request (M MA6), Staff's matching principle (M MA8), P-064 filed as a challenge (M UW14) | **Fixed.** Card 5's support and "What's uncertain"; Plan D's "What it does"; card 6's reply; P-064 moved to card 6's "What's uncertain" |
| 31 | Caveat parity on figures: Georgia Power's $102 a year had one caveat; $264 million was attributed to GAM without "According to GPC"; GIPL's rebuttal sat far from these figures (F3, M IM3, M MA4) | **Fixed in part.** "This is Georgia Power's projection, and no other party's estimate of it is public"; "Citing Georgia Power, GAM said …". GIPL's capital-substitution rebuttal is on card 8, not beside the $264 million in Plan A |
| 32 | Card 4 used "asserted" outside quotes; "own" marked admissions for one side (E27, F12, F15) | **Fixed.** Staff's words are in quote marks; "own" dropped |
| 33 | "What's uncertain" fields ended on a party's dismissal or lacked the values sentence (E29, F16) | **Fixed.** Card 4's gives all three views in one sentence and a values sentence; card 5 adds the values sentence; card 8's uses the PSC order and a values sentence |
| 34 | Source-check partials and tags at lines 129, 175, 199, 211, 214 and 241 | **Fixed** with each suggested fix: Staff's lean and "for now" on policy-related costs (P-089); Staff's own reason against Option 1 (P-074) in card 1; "a primary driver … their share"; Staff's double-recovery reason in card 4's summary; "can contribute"; new "What's uncertain" wording for cards 6 and 7 |
| 35 | Low framing items: committee meeting not shown to be open; "each propose" for GIPL's filed split; two large-customer descriptions side by side; card 3's support without qualifiers (SC framing L53, L63, L73, L201) | **Fixed.** "Attend or speak"; "each filed"; a note that the two descriptions may cover different groups; P-145 and GIPL's full sentence in card 3 |
| 36 | RSM and MARTA never spelled out (E5); docket URL and PSC email missing (E6); "very large customers' payments would cover" implied higher bills (E13) | **Fixed:** "three other parties"; the PSC email from P-163; "more of large-load customers' payments would be counted toward …". **Partly** for the URL: it is in the source drawer for P-001 (sources.yaml S01), because the prototype shows text as plain text and a Markdown link would appear raw |
| 37 | Layer 4 read about grade 10, with long verbatim quotes (E22) | **Partly fixed.** Layer 4 reads 9.6 with the updated tool, with 8 cards instead of 7: long quotes were split into separate sentences, and plain paraphrases lead the densest. The questions read 7.5 (were 11.8). Target sections read 6.3 to 7.7 |
| 38 | Mechanical "Supported" parts proposed for cards 1, 2, 3 and 6 (U LD1, U LD2, M LD2, M LD6), and GIPL's switch in card 4's label (U LD3) | **Not adopted** (C2, C4) |
| 39 | Add the P-116 class split to the Layer 3 intro for all plans (F5) | **Not adopted.** Layer 2 and the last question carry it once; repeating it per plan would lengthen Layer 3 without new facts |

### Contradicting suggestions (round 2)

| # | Point | Views | Choice |
|---|---|---|---|
| C1 | GAM's card label | U: keep "Supported" on the mechanical premise and add a "Mixed" part. E and M: grade the premise the card's "In short" states, or split cards 2 and 6 the same way | Graded the stated main claim: "Mixed". M's point that GAM's card also turns on values is taken: its claim type is "factual, predictive and values" |
| C2 | Undisputed mechanics as "Supported" label parts (cards 1, 2, 3, 6) | U and M: add them for their own sides' cards. E: shorter, two-part labels | Two parts only (main claim, size). The undisputed facts are stated where readers meet them: Layer 2's first item, Plan A, card 1's support, card 3's label clause, and card 6's and 7's text |
| C3 | How to describe PIA Staff | M: keep "independent evaluation" from the order. E and U: no mission statements; say it is a party | E's template for every party; "independent" is not used |
| C4 | Georgia Power's account of GIPL's switch | U: add it to card 4's label. E, M and the brief: keep it in one place | Card 5 only, plus a pointer in Plan D |
| C5 | Q1 | U: "keep counting toward base costs" and no "serve all customers". M: the matching principle and the class-share point. E: no "serve all customers" | Cited premise plus both alternatives, with Staff's matching phrased as "matched against them in the fuel account". The class-share point became its own question |
| C6 | Q2 | U: "the chance of higher base rates, which GAM and GIPL raise". M: Staff's "size and direction". E: the PSC order as the neutral source | E's version |
| C7 | Q3 | U: "or whether any specific part … at all". M: "or moved into base rates". E: any revenue, old and new, peak or energy | All three combined |
| C8 | Where GIPL's lambda reason goes | M: card 4, as the answer to GAM. E: card 5, GIPL's own support | Full reason in card 5; one clause in card 4's "What's uncertain" |
| C9 | Lead length versus content | E4: 15–20-word bullets. E3 and the brief: add the net effect and the prices note | 28–46-word bullets with the net-effect sentence; the prices note stays in Layer 2 |
| C10 | A question on blacked-out figures | M: add it. Round 1 choice: not adopted | Not adopted again: every blacked-out figure is flagged card by card, and the commissioners see the confidential versions, so it is about public access more than the decision |

## Round 3 (revision 3, `verification/round3/workspace.r3.md`)

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate | **Pass.** 402 units (326 cited, 29 analysis, 42 editorial, 5 computed), 0 errors, 0 warnings. 66/66 hashes match. Reading grade: 30-second 7.6, L1 6.3, L2 7.6, L3 7.7 (Layer 4 9.6) |
| 2 | Blind source check (fresh agent, `source-check.json`, `source-check-home.json`) | **321 supports, 5 partial, 0 does not support, 0 wrong span.** 73 tags valid, 3 questionable, 0 invalid; 16 framing issues (4 medium, 12 low). **402/402 evidence spans grounded.** The accuracy part of the pass rule is met again. `home.md`: 4 supports and 1 questionable tag (the Dec 14 deadline shown without its time, high framing); 5/5 grounded. The coordinator fixed that line (9:30 a.m.); its independent re-check rated it valid, with three low points applied in revision 4 |
| 3 | Bias panel | **Fail, but closer.** Advocate for Georgia Power's and GAM's positions 3/5 (1 high, 9 medium, 11 low; 7 missing arguments; 6 label notes). Advocate for PIA Staff's and GIPL's positions **4/5**, up from 3 (7 medium, 12 low; 9 missing arguments; 6 label notes); it sees the remaining gaps as placement, not misstatement. Standards editor: neutrality **4/5**, up from 3; clarity 3/5 (29 issues: 0 high, 14 medium, 15 low; 19 flip-test items) |
| 4 | Calibration | Carried over from round 1 (same verifier prompt and method) |

The editor and the utility-side advocate both saw the remaining lean as structural, toward PIA Staff: only the fuel
side of the dollar-for-dollar shift had numbers, the label rule could only return "Mixed", replies were given on
PIA Staff's cards but not on Georgia Power's or GAM's, and Plan C carried no challenge. The more-credit advocate's
items were mostly about where arguments sat: Staff's profit look-back missing from GAM's card, GIPL's words used for
a split GIPL rejects, and no reason given for the review. The items several checkers shared came first.

### What round 3 found, and what we did

SC = blind source check (L = revision 3 line); U = advocate for Georgia Power's and GAM's positions; M = advocate
for PIA Staff's and GIPL's positions (MA = missing argument, UW = wording, IM = imbalance, LA = label note); E =
standards editor (E1–E29 issues, F1–F19 flip tests); B = the coordinator's brief for revision 4 (items a–l). Cards
are numbered as in revision 4: the new card 6 is GAM's pipeline card, so revision 3's cards 6, 7 and 8 are now 7, 8
and 9.

| # | Finding (who raised it) | Disposition in revision 4 |
|---|---|---|
| 1 | **High.** Only the fuel side of the dollar-for-dollar shift was put in numbers, and readers would net the $1.48 and $1.65 examples (U IM1 high, U MA7, U UW4, E16, SC framing L100, B a) | **Fixed.** Layer 2 says plainly that the case is about how real-time payments are split, not the prices, so every dollar added to the fuel credit is a dollar less counted toward base revenue (P-133, P-061, P-105, P-111), and that the order records the matching change as a regulatory asset or liability for the next base rate case (P-016). Each Staff fuel-side figure now has its base-side match as `{computed}`: Plan A ($70–76 million a year stayed on the base side), card 2 ($220 million), Plan C ($126 million rising to $399 million, and $329 million as first filed). On netting, Layer 2 computes about 17 cents a month per $100 million if base rates later rose by the full amount, says the order leaves that to the next base rate case, and says no filing gives any plan's net effect |
| 2 | The 30-second version gave GAM's "upward pressure" with no size and no other view (E1, E2, F17, U UW8, M UW11, B a) | **Fixed.** "Who it affects" gives both halves (a lower fuel charge; the same dollars stop helping pay base costs), Georgia Power's per-$100 million examples ($1.48 on the fuel charge, $1.65 on base rates), GAM's "upward pressure" as GAM's, and PIA Staff's view that a later base rate case sets any change and the company could have absorbed it (P-090) |
| 3 | The label rule could only return "Mixed", because every "In short" ends with who disagrees (E8, E25, M IM7, M LA1, M LA3, U LA1 to U LA4, B b) | **Fixed.** The printed rule now grades the card's main factual premise, set apart from who disagrees. "Supported" means the record backs it and no party disputes it; "Mixed" names a disputed value or reading; "Uncertain" means the deciding facts are hidden or not yet known. Every label has a "Supported (no party disputes …)" part (including cards 2, 5 and 6 as M named them, and cards 3 and 8 as U named them), a "Mixed" or "Uncertain" part naming the dispute, and the size. Each "no party disputes" clause also appears as an `{analysis}` sentence in the card's support that lists the parties' passages, so the blind check can test it (E25). The folded card shows only the grade words |
| 4 | Replies were given on PIA Staff's cards but not on Georgia Power's or GAM's, though replies are on record (P-049, P-124); Georgia Power's rebuttal points stand unanswered because it filed last (E4, F1, M IM5, M MA9, SC framing L243, B c) | **Fixed.** One printed rule: under "What challenges it", the card's side answers a challenge with its own statement on the point when one is on record, and "had said" marks a statement filed before the challenge. Cards 2, 3, 4, 7 and 8 use "had said"; card 8 (Georgia Power) and card 9 (GAM) end on Georgia Power's Oct 27 P-124. Layer 4 says once that Georgia Power filed last, on Oct 27, and that the schedule sets no written replies before the Dec 2 briefs (P-117, P-009 to P-011) |
| 5 | GAM's card overstated the dispute ("PIA Staff and GIPL disagree") and lacked Staff's evidence (U UW1, U MA1, E7, F5, SC L263 partial and medium framing, M MA1, M IM1, M LA5, U LA1, B a) | **Fixed.** Card 9's "In short" says GIPL and PIA Staff accept a base-side effect, but GIPL says part of that money is a needed cost, not a surplus (P-098), and Staff says a later base rate case would set its direction and size (P-090). Support adds GIPL's "may increase base rates" (P-105). "Who disagrees" gives Staff's "natural consequence" and GIPL's "agree with this concept" with its two factors. "What challenges it" gives Staff's look-back with "while maintaining its targeted Return on Equity" and "well within" (P-090, P-091), the order's rule for any change made now, GIPL's P-098 and Georgia Power's P-124. Label: "Supported (no party disputes that a bigger fuel credit moves the same dollars off the base side); Uncertain (how base rates would move …)" |
| 6 | Card 3's support held a Staff rebuttal and GIPL's words for a split GIPL asked the PSC to reject; its "In short" left out GAM (U UW2, M UW1, M IM4, E6, E15, F4, F6, SC framing L205, U UW6, U LA4, M LA6, B d) | **Fixed.** Support holds only evidence that backs the premise, each with its source's own qualifier: Staff's peak showing, which Staff calls "simply for illustration purposes" (P-145); GIPL's peak tie, "though it asked the PSC to reject the winter-peak split" (P-096); GAM's description of the plan, from GAM, "which opposes crediting real-time money to pipeline fees" (P-110) (C10). The "In short" names GAM. GIPL's agreement that new large-load fees should come out of the fuel charge (U UW6) is in Plan B and in card 3's "Who disagrees" ("addresses one problem") |
| 7 | Card 8 (Georgia Power, other costs) had no support from outside its side and no direct challenge to its main claim (U IM3, U MA5, U UW5, F15, M MA2, M LA4, U LA2, B e) | **Fixed.** Support adds GIPL's backing for leaving out hedging and carrying costs as not "actual costs of fuel" (P-106), Staff's "no recommendation" on three costs with its "policy and cost causation" reason (P-089, P-064), and GAM's base-side point (P-111, P-112), after the undisputed premise that the real-time price is set on marginal cost. "What challenges it" now meets the main claim: GIPL's "neglects two critical factors" and "a necessary cost of service" (P-099, P-098), then Georgia Power's P-124. Size names Georgia Power's public $264 million and GAM's blacked-out total (C2) |
| 8 | GAM's pipeline position had no card, though the printed ordering rule ends each question with a card for keeping today's treatment (U IM4, U MA2, E23, B h) | **Fixed** with a short card 6, "Credit no real-time money against pipeline fees (GAM)": GAM's fixed-cost versus marginal-cost reason and its access-charge point (P-110, P-168), the undisputed premise (pipeline fees are fixed; new large-load customers pay access charges), and challenges from GIPL (P-148) and PIA Staff (P-083). Cards 3 to 5 point to it instead of restating GAM's case. 12 units |
| 9 | The plans weren't parallel: Plan C had no challenge and left out that GIPL backs Option 1; Plan A set $264 million beside $70–76 million a year and gave only Georgia Power's and GAM's "Who benefits"; Plan D's "Who benefits" was one bare sentence (E5, E9, F2, F3, F7, F14, U IM2, U MA6, M IM2, M MA7, SC framing L120, B f) | **Fixed.** Plan C's "Where it stands": Georgia Power offered Option 2 but prefers Option 1, as GIPL does (P-058, P-065, P-128), and Georgia Power's objections (P-127, P-125, P-118). Plan A: the $264 million is "the whole amount, not what any plan would move"; "Who benefits" keeps P-124's base-rate-case timing and says base rates are frozen through 2028 (P-054), and GIPL's "a necessary cost of service" (P-098) follows. Plan D: GIPL's own claim that Option 1 gives standard-rate customers the contracts' costs and benefits (P-100). Each plan now has the same parts: the other parties' agreements and disagreements, documented challenges, and comparable figures only |
| 10 | Layer 2's "Fuel side versus base side" gave claims without their documented counters (M MA6, M IM3, U MA3, U IM6, B g) | **Fixed.** Georgia Power's base-rate freeze and $102 are followed by GIPL's report of concerns that the method raised other customers' fuel costs (P-097); GIPL's P-098 is in Plan A and card 9. Staff's "absorbing all these costs" (P-063) is followed by the record's answers: baseline power at standard rates, fuel included (P-043, P-048, P-052), and access charges (P-071, P-168). Card 7 says P-097 reports concerns raised by parties in the fuel case, not a PSC finding (U IM6) |
| 11 | Why the PSC ordered the review was missing (M MA5, B j) | **Fixed.** Layer 1's May 28 entry gives Georgia Power's account of why PIA Staff and intervenors sought it (P-053) and GIPL's report of the concerns (P-097) |
| 12 | Card 2 didn't say Georgia Power offered Option 2 itself, or that the other proposers call today's treatment a problem (M MA8, SC framing L191, B j) | **Fixed.** "In short": Georgia Power "offered this option too but prefers Option 1, as GIPL does"; support: Staff says Georgia Power "recognized this is a problem", GIPL says Option 1 "resolves this problem", and Georgia Power's direct testimony offered Option 2 as one of its two fixes (P-072, P-100, P-058) |
| 13 | GAM's finding on who benefits (P-116) never reached the summaries (U IM5, B j) | **Fixed in part.** Layer 3's intro, which covers every plan, says GAM finds a bigger credit helps medium and large businesses more than homes; Layer 2 gives the shares (rounded to 58% and 55%) with the base-rate caveat. Not in the 30-second version (C8) |
| 14 | Questions: Q4's "keeping the real-time rate tied to marginal cost" was Georgia Power's framing; Q1 used Staff's guideline wording without Option 1; no cost-shift question (M UW2, E13, F18, E24, U and M question notes, B i) | **Fixed.** Q1 names all three choices (Option 2, Option 1, today's energy-only count). Q4 is now the cost-shift question M proposed, in neutral form: do real-time customers already pay their share of fixed fuel costs through the base side, or should the credit count those costs? Q6 asks which changes belong in this case and which in the next base rate case. **Not adopted:** U's "possibly higher" in Q2 (C6); U's question on large projects that may not arrive (P-127 is in Plan B and cards 3 and 4); M's questions on monthly reports and hidden figures (Layer 4 says why there is no reports card; round 2's C10) |
| 15 | The decision block ran about 187 words against an ~80-word budget, and its party positions repeated Layers 3 and 4 (E3, B j) | **Fixed in part.** "What's being decided" is 37 words (budget ≤ 80) and "What would change" has four items (budget ≤ 5) with no party positions: about 115 words in all. Items 1 and 2 run 29 and 17 words, not ≤ 15, because they define take-or-pay contracts and pipeline fees at first use (C11) |
| 16 | Undefined terms: regulatory asset or liability, capacity values, pool sales or purchases, kW and MW (E12, SC framing L98) | **Fixed.** Glossary entries for each; "capacity values" is glossed from P-148's own terms |
| 17 | Resource Supply Management's gap was disclosed only in Layers 3 and 4; "copy into passages" is internal jargon (E14, B j) | **Fixed.** Disclosed in Layer 1 (Sep 22–23) and at the top of Layer 2, in E14's interim wording; "copy into passages" is gone. **Not adopted:** E14's OCR, because no OCR tool is approved (log.md, tool change requests; invariant 9) |
| 18 | Source-check partials and questionable tags (SC L114, L121, L138, L173, L217, L74, L99; B k) | **Fixed** with each suggested fix: Staff's whole-plan $329 million as first filed (P-066) in the Layer 3 intro and Plan C; Plan A's Option 2 figures are "compared with today's method", and both of Georgia Power's estimates are blacked out (P-139, P-140); Plan C gives Table 5's public 2023–2025 totals (about $1.09 billion and $1.31 billion); the errata note says Staff's page-20 text could not be checked against its correction; "One reason Staff gives" (P-083, with P-081, P-084 and P-085 in the card); the baseline note says changes to either the fuel charge or base rates reach real-time customers' baseline power, and new large-load customers on one tariff have none (P-043, P-048, P-071); "none of the filings we read gives another party's estimate" |
| 19 | Staff's page-7 figures were shown as first filed, though page 7 was corrected (SC framing L136, B k) | **Fixed.** Plan C says, where the figures appear, that Staff corrected page 7 on Oct 7, that our tools could not read the corrected page, and that the figures are as first filed (P-039, P-066) |
| 20 | Smaller wording items: GAM "argues against" (U UW7, E17); the AIER gloss (M UW6, E20); "Staff's reason" (M UW4, E21); P-064 placed so Staff seemed to doubt items it recommends (M UW7); the supporters' split filed as a challenge (M UW8); "a marginal-cost credit" (M UW9); a one-sided glossary entry for capital substitution (M UW10); GAM's lambda point set against a GIPL point on another claim (U UW10); Option 2's "larger" read as larger than Option 1 (U UW11); GIPL's Oct 14 reply placed after the Oct 27 rebuttal (U UW12, SC framing L109); Georgia Power's motion paraphrased more softly than GIPL's reply was quoted (F12); the decision paragraph named only the fuel-side benefit (U UW3) | **Fixed.** GAM's own "would adequately address"; Staff's "should have proposed the associated interchange energy rate" quoted; "One reason Staff gives"; P-064 moved to card 8's support beside P-089; the split moved to card 7's "What's uncertain"; "belong in the fuel credit"; the entry now says only what capital substitution is and where it goes; card 4 gives GIPL's actual inference (only the capacity part could cover the fees at peak, P-148) (C5); "than today's method"; "Also raised" runs Oct 8, Oct 14, Oct 27; the motion quoted as "irrelevant, clearly outside the scope of this proceeding" (P-134); "the rest helps cover base costs" (P-061, P-124) |
| 21 | Uneven caveats: "could not check" only on Georgia Power's account of GIPL (U UW9, F10); Plan D repeated that account without card 5's context (M IM8); the "data were incomplete" caveat printed twice (F9); single-source notes on some parties' figures only (F8, M IM6, U IM7, U IM9) | **Fixed in part.** Card 5 now states facts only: Georgia Power cites hearing transcript pages, and the Oct 15 transcript is listed without a file; Plan D adds Georgia Power's own footnote that the change followed its data responses (P-155) and points to card 5 (C9). The "incomplete" caveat is in card 2 only. GIPL's charged line carries "a report of concerns, not a PSC finding". Staff's figures carry their own notes (as first filed; which columns are public). Mentions of blacked-out figures follow what each party blacked out and were not rebalanced |
| 22 | Card 7's "What's missing" held mostly public figures, and its totals overstated the stakes (E22, U IM8, U LA3) | **Fixed.** The field leads with what is missing (most single items, including coal costs, are blacked out, so what these costs alone would add is not public), then gives the public totals and what they include; the label's size part says there is no public estimate for these costs alone (C2) |
| 23 | Label lines of 52–65 words before the "In short" (E10); label notes on "agrees" (U LA5), GIPL's reported switch (U LA6) and Georgia Power's peak agreement (M LA2) | **Fixed in part.** The folded card shows only the grade words; the full labels run 53–67 words (C3). Card 4's label no longer says Georgia Power "agrees". Card 5's label credits Georgia Power's partial agreement; GIPL's reported switch stays in the card's body (C7) |
| 24 | Missing Georgia Power points: expected revenues "more than cover" incremental costs, real-time pricing attracts load, and real-time customers are "not similarly situated" (U MA4) | **Partly.** Card 7's "Who disagrees" carries the same point for new large customers (P-055, P-054); card 8 has the competitive-value point (P-121); card 9 has "downward pressure on rates for all customers" (P-045). Not added in full, to hold length |
| 25 | Missing PIA Staff points: Georgia Power's own words behind the double-recovery concern (M MA3); Staff's "recovered from all customers" guideline (M MA4) | **Fixed.** Card 4's support quotes P-052 ("including … FT"); card 7's "What challenges it" quotes Staff's guideline (P-062) with its cost-variation point (P-149) |
| 26 | Party descriptions are uneven: GAM's stake is described, GIPL gets only "filing with three of its member congregations" (F13) | **Not changed.** P-093 is the only passage that describes GIPL, and it names only its member congregations; we don't describe a party from outside the record |
| 27 | Top-layer clarity: appointment history (E28); no docket link, and "docket" undefined (E29); two-decimal percentages (E27); capitals changed in two quotes (E26); Dec 10's time in the label (SC framing L41); the appointment point left implicit (SC framing L18) | **Fixed**, except as noted: one appointment sentence (the line on Stan Wise's earlier service is cut); "A docket is the PSC's case file"; the URL stays in the source drawer for P-001, because the prototype shows Markdown links as raw text; shares rounded to 58% and 55%; "The direction …" and "To eliminate this risk" keep their source capitals; Dec 10's start time is in the item's text. **Not changed:** "So the same five are set to vote on Dec 15" already states the point of L18 |
| 28 | Large-load quotes used against a point about all real-time customers (SC framing L242) | **Kept.** The sentence opens "On new large customers", which states its scope |
| 29 | Home: the Dec 14 deadline without its time (SC home L6, high); line 14's jargon (SC home L14, medium); the Nov 12 "Input" label (SC home L5, low) (B l) | **Fixed.** Line 6 by the coordinator, then the re-check's three points ("Submit"; P-013 in the `{computed}` tag; Dec 10 under "Coming up"). The Oct 27 item now says, in plain words, that Georgia Power and an interfaith group want solar, wind and biomass contracts taken out of the fuel credit entirely, the PSC's advocacy staff wants them counted in full, and either way the credit grows. Nov 12 is "Observe" (watch or attend), with the call to ask about public witnesses. No Dec 9 advice: no passage gives a deadline tied to the committee meeting |
| 30 | Flip tests needing no change: named-voice counts follow the filings and are disclosed (F19); Plan C's public figures can read as "delivers more" (F16) | **Kept.** Layer 4's note on named voices; Layer 3's intro says a hidden figure is not zero |

### Contradicting suggestions (round 3)

| # | Point | Views | Choice |
|---|---|---|---|
| C1 | Where Staff's profit look-back goes | M (MA1, IM1, LA5) and the brief: add it to GAM's card as Staff's counter-evidence. Round 2's U (IM3): it appeared four times and sat under Plan A's "Who benefits" | Beside GAM's claim only: one clause in the 30-second version, one sentence in Layer 2, and card 9's "What challenges it", marked "Looking back" and followed by the order's rule for any change made now. Not in Plan A |
| C2 | The size part of cards 7 and 8 | U (LA2, LA3, IM8): the totals are company-wide, so say no public estimate covers these costs alone. M (LA4): card 8's size should be its own claim's money, Georgia Power's public $264 million and GAM's blacked-out total | Both, each where it fits: U's wording for card 7, M's for card 8 |
| C3 | How much goes in a label | U (LA1 to LA4) and M (LA1 to LA6): add cross-party agreement clauses with passages. E10 and the brief: keep label lines short; E10 would cut them to grade and size, below "In short" | Three short parts: one "Supported (no party disputes …)", one "Mixed" or "Uncertain", and the size. Who agrees with whom stays in the card's text. 53–67 words with Side and Claim type; the line stays at the top of the card, where the prototype reads it, and the folded card shows only the grade words |
| C4 | Staff's base-rate view beside every GAM claim, or once | M (UW11, LA5) and E1: give Staff's view wherever GAM's "upward pressure" appears. M (UW5): repeating the order's rule after Staff's view reads as a last word against Staff. Round 2's U: don't repeat Staff's estimate | Staff's view sits beside GAM's claim in the 30-second version, Layer 2 and card 9. The order's rule is in Layer 2's first item, card 9 and Plan C, and never closes a paragraph after Staff's statement |
| C5 | Where GIPL's lambda point goes | U (UW10): card 4 set it against a claim it doesn't answer. Round 2's M (C8): use it to answer GAM | The first challenge on GAM's new card 6; GIPL's own support in card 5; in card 4's "What's uncertain" only as GIPL's actual inference (the capacity part, at peak) |
| C6 | Q2's direction | U: "possibly higher base rates later". M, citing Staff's filing: direction and size are for a base rate case | The PSC order as the neutral source, as in round 2: "possible base-rate effects later, which its order leaves to the next base rate case" |
| C7 | Card 5's label | U (LA6): add Georgia Power's account of GIPL's switch. M (LA2): credit Georgia Power's peak agreement and Staff's pipeline share | M's agreement clause ("as GIPL says and Georgia Power partly agrees"), with Staff's share in the support. GIPL's reported switch stays in "What challenges it" and "What's uncertain": it is an account of the proponent from an untranscribed hearing, not evidence on the premise |
| C8 | GAM's who-benefits finding (P-116) in the lead | U (IM5): put it in the summaries most readers see. E (E1 to E3) and the brief: a short lead, and hold the page's length | Layer 3's intro, which covers every plan, and Layer 2, with the base-rate caveat. The 30-second version stays at 149 words |
| C9 | The caveat on Georgia Power's account of GIPL's switch | M (IM8): add card 5's caveat to Plan D. U (UW9) and F10: a caveat on one party's account only is uneven | Facts, not caveats: card 5 says Georgia Power cites transcript pages and the Oct 15 transcript is listed without a file; Plan D adds Georgia Power's own footnote (the change followed its data responses) and points to card 5 |
| C10 | Staff's "simply for illustration" in card 3 | U (UW2): move it under "What challenges it". M (IM4) and the brief (d): borrowed support only with each source's own qualifier; rebuttals under challenges | The brief's rule: Staff's words about its own showing ride with that showing in the support, as the source's qualifier. Staff's rebuttals of the plan (P-082, P-085) stay under "What challenges it" and "Who disagrees"; P-145 also answers Georgia Power's P-125 in card 4 |
| C11 | Decision-block items | E3: each item ≤ 15 words. The style guide and round 2's checkers: define take-or-pay contracts and pipeline fees at first use | Positions moved out; the two glosses stay, so items 1 and 2 run 29 and 17 words |

## Round 4 (revision 4, `verification/round4/workspace.r4.md`)

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate | **Pass.** 415 units (321 cited, 43 analysis, 43 editorial, 8 computed), 0 errors, 0 warnings. 66/66 hashes match. Reading grade: 30-second 6.8, L1 6.8, L2 8.0, L3 8.1 (Layer 4 9.9) |
| 2 | Blind source check (fresh agent, `source-check.json`, `source-check-home.json`) | **310 supports, 11 partial, 0 does not support, 0 wrong span.** 92 tags valid, 2 questionable, 0 invalid; 18 framing issues (2 medium, 16 low). `home.md`: 5 supports, 1 valid tag; 4 framing issues (3 medium, 1 low). **421/421 evidence spans grounded.** No wrong dollar figure, date, quote or party name; all arithmetic redone correctly |
| 3 | Bias panel | **Fail, on one item.** Advocate for Georgia Power's and GAM's positions **4/5**, up from 3 in every earlier round (7 missing arguments; 13 wording items, 7 medium; 5 imbalance items, 2 medium; 6 label notes). Advocate for PIA Staff's and GIPL's positions 4/5 (10 missing arguments; 15 wording items, 7 medium; 8 imbalance items, 1 high, 4 medium; 8 label notes). Standards editor: neutrality 4/5, clarity 3/5; 28 issues (**1 high**, 10 medium, 17 low) and 19 flip-test items. The one high item is the 30-second "Who it affects" line, which all three flagged from different sides |
| 4 | Calibration | Carried over from round 1 (same verifier prompt and method) |

Both advocates now meet the bar, so the coordinator asked for a narrow revision 5, as in Trial 001's last round:
line 9, the source check's accuracy flags, and six wording fixes the checkers agree on. Card claims, evidence
grades, card order and plan structure are unchanged. Every other finding is logged below as an open item.

### What revision 5 changed

Codes as in round 3 (SC = blind source check, L = revision 4 line; U and M = the two advocates; E = editor issues,
F = flip tests). Revision 5 keeps revision 4's line numbers in `workspace.md`.

| # | Finding (who raised it) | Change in revision 5 | Lines (revision 5) |
|---|---|---|---|
| 1 | **High.** The 30-second "Who it affects": GAM "warns" (the page's only alarm verb); "The PSC's advocacy staff" lent the regulator's name to one party and answered GAM last; "could have absorbed it" dropped Staff's 2023–2025 look-back; GIPL was absent; no sense of each plan's size (E1 high, E2, E3, F1, U UW1, U IM2, M UW1, M IM1 high, SC L9 partial, SC framing L9) | Facts only, with no party argument or rebuttal: both halves of the shift; every plan we could read would raise the credit, by amounts partly blacked out; Georgia Power's per-$100 million examples; parties disagree on whether, and how much, base rates would rise later, and the next base rate case decides (P-115, P-105, P-090, P-016). "Fuel credit" is glossed at first use. The absorption claim stays in Layer 2, now "Looking back at 2023–2025", and in card 9. The 30-second version is 149 words, as before | 8, 9, 103 |
| 2 | Source-check partials and questionable tags (SC L8, L27, L52 questionable, L53, L105, L110, L121 twice, L139, L146, L206 questionable, L268) | L8 cites P-042 and P-043 for "hourly". L27: "A legal notice of the case was first published". L52: the "Observe" label is gone; the FAQ's "Proceedings are open to the public" is quoted where it applies (administrative sessions), and we say we found nothing on committee meetings. L53 cites P-003. L105 keeps the two access-charge groups apart. L110: GIPL said rate design is outside this case "but still recommended" new pricing "as quickly as possible". L121: "GAM proposes no change of its own" (P-110, P-111, P-115), and Georgia Power's double negative is quoted with its hedge. L139: "If the PSC adopts a change, …". L146 keeps GIPL's condition from P-100. L206's tag says what P-082 and P-157 dispute. L268 quotes Georgia Power's double negative with its hedge | 8, 27, 52, 53, 105, 110, 121, 139, 146, 206, 268 |
| 3 | Home: "Observe" implied the public may attend the Nov 12 hearing; no way to submit a comment; "either way the credit grows" left out the base side; GIPL named only as "an interfaith group" (SC home framing L5, L6, L15) | Nov 12 moved from "Act now" to "Coming up" with no power label; it gives the calendar time (9:35 a.m., P-023) and the PSC's number to ask whether the public may speak (P-163). The Dec 14 item adds the number to ask how to submit a comment (P-163). The Oct 27 item names Georgia Interfaith Power & Light, calls PIA Staff "a party in the case", and adds that the same dollars come off the base side (P-061, P-115) | home 5, 9, 15 |
| 4 | "The record also shows" answered Staff in the page's own voice (E7, F2, M UW2, SC framing L105) | Each reply is attributed: Georgia Power on baseline power (P-043, P-048, P-052); PIA Staff (one tariff, P-071) and GAM (100 MW and up, P-168) on their access charges. Staff's "absorbing all these costs" is quoted | 105 |
| 5 | The 17-cent net per $100 million was computed only for the case GAM argues (E6, F3, M IM3) | Both bounds in one `{computed}` sentence: about $1.48 a month less if base rates don't change, about 17 cents more if they later rise by the full amount; where the result falls is left to the next base rate case | 102 |
| 6 | The Layer 3 intro gave GAM's reading without line 106's caveat (E8, F4, M IM2) | The caveat is added, as line 106 words it (C6) | 116 |
| 7 | "The PSC's advocacy staff" (E1, F1, U UW1) | Gone from line 9; home line 15 says "PIA Staff, a party in the case". Line 19's formal introduction already says Staff takes part "as a party" | 9; home 15 |
| 8 | GIPL quotes trimmed into concessions: P-098 (M UW3), P-105 (M UW7, E25, F9), P-099's "While I agree with this concept" (U UW5) | P-098 in full in Plan A and card 8; P-105 in full in card 9; card 8 quotes P-099 from "While I agree with this concept" | 122, 268, 278 |
| 9 | "Staff corrected page 7" overstated the correction (M UW13) | "corrected one line on page 7" (P-039); "Our tools could not read the correction" | 138 |

### Open items (round 4, not changed in revision 5)

| # | Open item (who raised it) | Note |
|---|---|---|
| O1 | Every card header shows "Supported" and then "Mixed" or "Uncertain", and the "Supported" item is chosen differently from card to card; "predictive" is missing on cards 2 and 3 (E9) | A label-rule change; brief item 4 holds grades. See C1–C3 |
| O2 | Label clauses the advocates propose: card 3 (U LA1; M LA3, an "Uncertain" part on the unreviewed cost model), card 4 (M LA1), card 5 (U LA5; M LA2), card 6 (U LA2; M LA4), card 7 (M LA7), card 8 (U LA3; M LA5), card 9 (U LA4; M LA6), cards 1 and 2 (U LA6; M LA8 says keep them) | Grade changes. See C1–C3 |
| O3 | Card 9's "In short" runs 60 words and carries two rebuttals (E11, F8, U UW7); "accepts" and "prefers" frame Staff's arguments as concessions (M UW8) | Changes card summaries |
| O4 | Reply rule: Georgia Power answers for GAM on card 9 (E26, F10, SC framing L280), and the order's sentence there acts as a reply (M UW9); card 7's Staff reply restates its guideline (E26); card 6 leaves Staff's double-recovery challenge unanswered though Georgia Power's P-123 is on record (U IM4); card 7's GIPL P-097 should read "had said" (U UW12) | See C4 |
| O5 | Staff's estimate that absorbing its plan would have cut Georgia Power's profit rate by about 0.49 points a year (P-091) is missing from Layer 2 and card 9 (E16, U UW11, M MI5); Plan A's "Who benefits" could carry it (E19) | All three reviewers agree. First candidate for the next revision |
| O6 | Card 3's borrowed support carries each source's qualifier and card 4's doesn't (U UW6, SC framing L218); GIPL's words don't belong in card 3's support (M UW14); P-145 is trimmed, and Staff's illustration used a summer peak (M UW15, SC framing L206) | See C5 |
| O7 | Missing Georgia Power and GAM points: GAM's class shares in card 9 (U MI1); Georgia Power's guideline critique in cards 2 and 4 (U MI2); Georgia Power's support for card 6 (P-052, P-124, P-123) and GAM's "other components" (P-110) (U MI3); Georgia Power's baseline answer and "not similarly situated" in cards 7 and 8 (U MI4); Georgia Power "supported and agreed to" the review, and GAM joined the stipulation (U MI6); Georgia Power's economic-development reasons (P-045) (U MI7); GAM's cards are the thinnest on support (U IM1) | Content additions |
| O8 | Missing PIA Staff and GIPL points: Staff's three guidelines and their grounding (M MI1); why the review exists, in the 30-second version (M MI2; it is in Layer 1's May 28 entry); the double-recovery premise in Georgia Power's own words (M MI3); "prices don't change" as a challenge in card 8 (M MI4); card 6's challenge from P-071 and P-055 (M MI6); the order's own scope text (M MI7, F15); GIPL's reported switch as support in card 4 (M MI8); the $322 million balance tied to GIPL's second factor (M MI9); Staff's mandate from P-006 (M MI10) | Content additions |
| O9 | Monthly reports: Georgia Power's reasons against them (P-132) never appear, and no question covers reports (E5, F17, U MI5, M question note) | One sentence in Plan C and one question |
| O10 | Layer 2: line 101 cites Georgia Power's scope statement (P-133) for the page's own sentence (E15, SC framing L101); "either an asset or a liability" makes the order look neutral on direction (U UW2); line 104 drops GIPL's "apparent" and lacks card 7's caveat, and only Georgia Power's $102 carries a single-source note (E17, F5, U UW4); the base-rate freeze reads as fact (SC framing L122); line 106's caveat overstates what GAM's comparison needs (U UW3) | See C6 and C7 |
| O11 | Card 7's "not a finding by the PSC" caveat applies to GIPL only (M UW5) | See C7 |
| O12 | Plans: one shared "Who pays" sentence for Plans B–D, and "By PIA Staff's estimates" on the base-side matches (E20, E22, F12); GAM's pipeline view in Plans B–D (E21); Plan A's "What's uncertain" leads with GIPL's concession and leaves out Staff's double-recovery point (M UW4); Plan A's "Who benefits" alone carries a rebuttal and the freeze (U UW10); Plan B's "Who pays" omits what stays on the fuel charge (M IM8); Plan C's Georgia Power critiques have no pointer to cards 4 and 7 (M IM7); only Plan B has a challenge in "What's uncertain" (F18); Plan D repeats GIPL's member congregations (F13); Georgia Power's account of GIPL's switch appears three times (F11); "unknown" should be "not public" (SC framing L130, L146); repeat "a hidden figure is not zero" in Plans B and D (M IM4) | Plan structure; brief item 4 |
| O13 | Each side says the other's estimates get the stronger treatment (U IM3; M IM3, M IM4) | See C8 |
| O14 | Card 2: "fixes" and "than today's method" (E23, F7, U UW8, M UW11, SC framing L194); shared points counted for Option 2 only (U IM5); card 2 alone carries the base-side match in its support (M UW10) | Card 2's support field |
| O15 | Cards 7 and 8 answer the same question unevenly (M IM5); "already pay" in card 8's "In short" and in Q4 (M IM6) | Card claims; brief item 4 |
| O16 | Decision block: "Count both, or neither?" leaves out keeping today's count (E4, SC framing L61); 113 words against about 80, and the RSM note (E14) | Next revision |
| O17 | Questions: Q2's "now" against "possible" (E27; U question note); Q5's "homes compared with businesses" (E28); Q4's "already" (M); the order's scope text in Q6 (M); Q3 on projects that may not arrive and on access charges (U); Staff's guidelines as policy (U); a reports question (E5, M); hidden evidence (M); Q1's order of options (U) | Next revision |
| O18 | Clarity: Layer 4 reads at about grade 11; terms left undefined (Return on Equity, interchange energy rate, hedging, carrying costs, cost causation, rate design, rebuttal); long rubric sentences (E10, SC framing L184, L280); Southern Company Services not tied to Georgia Power (SC framing L156); "Table 1" unexplained (SC framing L265); "Georgia Power's name for" in the capital substitution entry (U UW13, F16); P-103 in the passive (M UW12); Staff's $1.35 can read as a forecast (SC framing L138); the legal notice calls the 1-800 number "inside Georgia only" (SC framing L47) | Next revision |
| O19 | One party's view is missing (Resource Supply Management; E12); no links (E13, SC L53); GIPL's stake isn't described (F14) | OCR needs an approved tool; the prototype shows Markdown links as raw text; P-093 says nothing about GIPL's stake |
| O20 | Home: no "dates can change" note on the computed deadline (SC home framing L6); GAM's view is absent from the Oct 27 item (SC home framing L15) | Next revision |

### Contradicting suggestions (round 4)

| # | Point | Views, side by side | Choice now |
|---|---|---|---|
| C1 | Card 9's label | U (LA4): grade the direction "Mixed" (Georgia Power and GIPL point the same way as GAM; only Staff hedges), and keep "Uncertain" for size and timing. M (LA6): keep "Uncertain" on the base-rate effect, and add a "Mixed" part on whether the base-side money is a surplus | No change (brief item 4) |
| C2 | Card 6's label | U (LA2): add the marginal-cost half to "Supported", name Georgia Power as agreeing in the "Mixed" part, and give the $264 million as size. M (LA4): take access charges out of "Supported" and add a "Mixed" part on whether access charges or the credit should cover new fees | No change |
| C3 | Card 8's size part | Round 3's M (LA4) asked for Georgia Power's public $264 million, which revision 4 used. Round 4's M (LA5): that figure isn't tied to these costs, so say no public estimate. Round 4's U (LA2): card 6's size should match card 8's $264 million | No change; the advice conflicts across rounds and sides |
| C4 | Who answers a challenge on GAM's cards | E (E26, F10) and SC (framing L280): never let a third party answer for a side; use GAM's own words (P-112) or end on the challenge. U (IM4): card 6 should carry Georgia Power's answer (P-123) to Staff's double-recovery challenge. M (UW9): the order's sentence belongs in "What's uncertain" | No change |
| C5 | Borrowed support in cards 3 and 4 | U (UW6) and SC (framing L218): qualify card 4's borrowed support the way card 3's is, or move card 3's qualifiers to "What challenges it". M (UW14): GIPL's words don't belong in card 3's support at all. Round 3's brief: support carries each source's own qualifier | No change |
| C6 | The caveat on GAM's class-share reading | E (E8, F4) and M (IM2): add line 106's caveat ("assumes base rates later absorb the full amount") to the Layer 3 intro. U (UW3): that caveat overstates; the comparison holds if any part reaches base rates, and homes pay more overall only if about 95% does (55.36 / 58.07) | The brief's choice: added to line 116 as line 106 words it. U's correction to both lines stays open (O10) |
| C7 | The "not a finding by the PSC" caveat on GIPL's P-097 | E (E17, F5) and U (UW4): add it at line 104 too, and keep "apparent". M (UW5): drop it, or apply the same caveat to Georgia Power's outcome claims (P-055) | No change |
| C8 | Which side's estimates get the stronger treatment | U (IM3): Georgia Power's $102 and $264 million carry caveats and rebuttals, while Staff's figures get no single-source note. M (IM3, IM4): Georgia Power's per-$100 million numbers are the page's base for calculation, Staff's figures carry stacked caveats, and the redactions leave Staff's plan as the only one with a visible base-side cost | No change, except that line 102 now gives both bounds (row 5 above) |
| C9 | What the 30-second version holds | E (E1): the shortest fix is to drop party views. M (IM1, MI2): say three of the four parties we could read propose a bigger credit, and why the review exists. U (IM2): give each side's core claim a clause, or keep to facts | Facts only (row 1 above). Why the review exists stays in Layer 1 (O8) |

## Round 5 (revision 5, `verification/round5/workspace.r5.md`): passed

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate | **Pass.** 420 units (325 cited, 44 analysis, 43 editorial, 8 computed), 0 errors, 0 warnings. 66/66 hashes match. Reading grade: 30-second 6.6, L1 6.8, L2 8.2, L3 8.2 (Layer 4 10.0) |
| 2 | Blind source check (fresh agent, `source-check.json`, `source-check-home.json`) | **315 supports, 10 partial, 0 does not support, 0 wrong span**; 82 tags valid, 13 questionable, 0 invalid; 16 framing issues (5 medium, 11 low). `home.md`: 8 supports, 1 valid tag; 4 framing issues (2 medium, 2 low). **429/429 evidence spans grounded.** No wrong number, date, time, phone number or quote |
| 3 | Bias panel | **Pass.** Advocate for Georgia Power's and GAM's positions 4/5 (9 missing arguments; 10 wording items, 3 medium; 6 imbalance items, 4 medium; 5 label notes). Advocate for PIA Staff's and GIPL's positions 4/5 (7 missing arguments; 11 wording items, 5 medium; 9 imbalance items, 5 medium; 5 label notes). Standards editor: neutrality 4/5, clarity 3/5; 23 issues (**0 high**, 12 medium, 11 low) and 14 flip-test items |
| 4 | Calibration | Carried over from round 1 (same verifier prompt and method) |

Round 5 meets the pass rule in the [verification protocol](../../docs/process/verification-protocol.md) ("When a
page is done"). Revision 6 makes only the accuracy fixes the coordinator listed: the source check's partial verdicts
and questionable tags, and three written-rule items from the editor. One fix changes an evidence label (card 5), so
the whole panel re-runs in round 6 instead of a targeted re-check. Every other finding is an open item below.

### What revision 6 changed

Codes as before (SC = blind source check, L = revision 5 line; U and M = the two advocates; E = editor issues, F = flip
tests; A1–B17 = the coordinator's brief for revision 6). Revision 6 keeps revision 5's line numbers in `workspace.md`
(311 lines), so each old line number is also the new one.

| # | Finding (who raised it) | Change in revision 6 | Lines |
|---|---|---|---|
| A1 | Wise's appointment month isn't in the passages (SC L17) | "Stan Wise in 2026 … Wise has served since Sep 5, 2026" (P-033) | 17 |
| A2 | "Between those two" treated two examples as bounds; P-090 leaves direction and size to a base rate case (SC L102 questionable) | "For example, per $100 million moved, …"; "How much of the base-side change reaches base rates, and in which direction, is left to the next base rate case" | 102 |
| A3 | GIPL's hedge dropped; "subsidized" outside quotes (SC L104; E10; U UW3) | Quoted: "in apparent contravention of Georgia Power’s statements that data centers would not be subsidized by existing customers" | 104 |
| A4 | The caveat on GAM's ranking was inexact (SC L106 and L116 questionable; SC framing L106; U UW1; round 4's C6) | Both places now say: measured against each group's own bill, the ranking holds however much of the shift reaches base rates; homes come out behind in dollars only if nearly all of it does (`{computed}`: 55.36% ÷ 58.07% ≈ 0.95 by GAM's shares; $1.4806 ÷ $1.6484 ≈ 0.90 for a typical home) | 106, 116 |
| A5 | P-107 is not garbled: the words after "utilizes" are blacked out in the public PDF (SC L110; SC framing L110; E13; M UW9) | `passages.spec.yaml` gives P-107 a `fix` that marks "[blacked out in the public version]" and a note: the coordinating session checked a rendered image of PDF p.19, lines 18-19. The spec's comment and the passages header now describe this marker; `passages.md` rebuilt. The page splits the sentence and says GIPL recommends real-time pricing "that uses a method blacked out in the public version" | 110; spec and P-107 |
| A6 | "That part" implied the motion covered GIPL's p.17 recommendation; P-158 shows only the peak-price testimony (SC L111) | "the part of GIPL's testimony with its peak-price finding, along with other parts" (cites P-094) | 111 |
| A7 | "GAM proposes no change of its own" is not in P-110, P-111 or P-115; Staff's "unreasonably suppressed" applies to one feature (SC L121 twice; U UW7) | GAM's own words: no specific part of real-time revenue should be credited against pipeline fees, and a bigger credit "would put upward pressure on base rates" (P-110, P-115). Staff's phrase is scoped to counting the take-or-pay energy but not its cost | 121 |
| A8 | Georgia Power's reasons on the monthly reports were never stated (SC L162 questionable; E6; U MI6; SC framing L137) | "Georgia Power says the reports are not needed and, in some cases, 'not feasible in the requested timeframe'" (P-132); the cards cover the first three questions | 162 |
| A9 | "No party disputes" covered Resource Supply Management's unread testimony and the untranscribed Oct 15 hearing (SC L182, L194, L206, L218, L230, L242, L254, L266, L278 questionable; SC framing L182 medium; E7) | "No party whose testimony we could read disputes" in each card's support sentence and label parenthesis. The printed rule says why, once. Grades unchanged except card 5 (A10). The gate's new check on unscoped absence claims passes | 167, 178, 182, 190, 194, 202, 206, 214, 218, 238, 242, 250, 254, 262, 266, 274, 278 |
| A10 | Card 5's "Supported" premise: P-102 shows GIPL arguing against Georgia Power on it (SC L230 questionable; SC framing L230 medium) | Support quotes P-102 ("does matter where these revenues are credited"; "If the ratio of MWh to MW were the same for all classes, I would agree with Georgia Power"). "Who disagrees" now opens: by GIPL's account, Georgia Power holds that where the money is credited does not change what each group pays. **Label's first part changed from "Supported" to "Mixed"** | 226, 230, 231 |
| A11 | Staff's hedge dropped (SC L194; E15) | "is expected to keep growing through 2031" (P-079) | 194 |
| A12 | "No party asks to count" (SC L257) | "no party whose testimony we read asks to count" (adds P-131) | 257 |
| A13 | "A needed cost, not a surplus" contradicted GIPL's words (SC L276; M UW3 in part) | "GIPL calls part of that money a 'surplus' that 'is needed' and 'does not provide further surplus' to lower rates" (P-098) | 276 |
| A14 | Staff's revised returns read as the target (SC L280; E16) | "with estimated earned returns of 10.3, 10.9 and 10.9 percent" (P-091) | 280 |
| A15 | Card 8's premise: GIPL's P-094 says the peak-hour part of the real-time price is set too low, so "set on marginal cost" overstated what no party disputes (SC L266 questionable, the part left as O1 at first) | The support sentence now says no party whose testimony we could read disputes that the price is "designed as a marginal-cost price" (P-121, P-042, P-110), "though GIPL says its peak-hour part is set too low" (P-094 in the tag). The label's first part covers only the design, and its "Mixed" part adds GIPL's peak-price point. Grades unchanged (the coordinator's decision on O1) | 262, 266 |
| A16 | Card 7's premise: P-065 shows that for non-subscription renewable program expenses the over- or under-recovery is "determined in base rates per FCR-27 stipulation", so not every left-out cost is paid through the fuel charge (SC L254 questionable, the part left as O1 at first) | The support sentence and the label's first part now say "most costs left out of the credit"; a new sentence names the exception and quotes P-065. Grade unchanged, since the narrowed claim holds (the coordinator's decision on O1) | 250, 254 |
| B15 | "Every plan" left out Plan A, today's method (E1; SC framing L9) | "Every proposed change we could read would raise the credit" | 9 |
| B16 | No direction words on the bill figures; "$100 million" of what (E2; SC framing L9) | "Georgia Power estimates each $100 million cuts a typical home's fuel charge by about $1.48 a month, and adds about $1.65 if base rates rise by that much" | 9 |
| B17 | Home line 15: "fuel credit" and "base side" undefined; it read like a fight over renewable power (SC home framing L15, two medium) | Defines the fuel credit; says Georgia Power and GIPL want "its math" to leave out both the cost and the power of contracts such as solar and wind, while PIA Staff wants both counted; says fuel charges for customers on standard rates go down and the same dollars stop helping pay base costs, "the rest of the bill"; "the last written testimony on the schedule" | home 15 |

The status line (line 3) also changed.

### Open items (round 5, not changed in revision 6)

| # | Open item (who raised it) | Note |
|---|---|---|
| O1 | Two accuracy points inside flagged lines (SC L254 and L266) would change cards 7's and 8's labels | **Fixed in revision 6** as A15 and A16, by the coordinator's decision: round 6 re-runs the whole panel anyway |
| O2 | Every card but card 5 leads with "Supported", often for background facts, so the cards look equally supported; grade the "In short" premise first, or show agreed facts as "Common ground" (E8; U IM6; U LA1–LA5; M LA2–LA5) | See C1 |
| O3 | Card 8's size uses Georgia Power's $264 million while card 7 says no public estimate exists (E9, F5, M LA1) | See C2 |
| O4 | Staff's 0.49-point estimate (P-091) for Plan A and card 9 (E11, U UW9, M MI1, M IM2); carried from round 4's O5 | All sides raise it |
| O5 | GIPL's "I agree with this concept" appears three times and its two factors only once (M UW1, M UW2); U (LA2) wants the concession credited in card 8's label | See C3 |
| O6 | The 30-second version omits Staff's and GIPL's core claim, that customers on the fuel charge carry costs left out of the credit (M IM1); it runs 156 words (E3) | See C4 |
| O7 | Rebuttals sit inside Georgia Power's and GAM's own fields: Plan A's "Who benefits" (P-098), Plan B's "What's uncertain" (P-103), card 9's "In short", card 3's support qualifiers (U IM1, U UW2, U UW4, U UW5, U UW6); card 6 lacks the answers on record (P-110's "other components", P-052, P-124) (U IM2, U MI7, U MI8, E21) | See C5 |
| O8 | GIPL's data-center report appears three times and Georgia Power's answer once (U IM3, F1); E10 would cut it from Layer 2 or pair it with P-055; GIPL's basis for the "higher standard" point (M MI4); the "not a finding" caveat applies to GIPL only (M UW10) | See C6 |
| O9 | Georgia Power points: P-052 as support in card 8 (U MI1, U IM4); "not similarly situated" (U MI2); the case for the 2010 method (U MI3); "more than cover" and its commitment to existing customers (U MI4); it "supported and agreed to" the review (U MI5, F2); GIPL's "by 250%" qualifier on coal costs (U MI9) | Content additions |
| O10 | PIA Staff and GIPL points: Staff's matching guideline (M MI2); "the system has evolved" (M MI3); why GIPL calls the surplus needed (M MI5); the order's scope text (M MI6, M IM4, F6, E18); Staff's mandate (M MI7) | Content additions |
| O11 | Wording: "accepts" for Staff (M UW4); P-105 used as a concession on GAM's card (M UW5); "than today's method" (M UW6); Staff's "only a portion" caveat on the $741–824 million totals (M UW7, E17); the passive "not been allowed" (M UW8); P-094's reason (M UW9); GIPL's "whenever" qualifier in card 3 (M UW11); "fixes" (U UW8); a "cannot be checked" note on Georgia Power's claim only (U UW10); card 9's "In short" length (U UW6) | Next revision |
| O12 | Balance: redactions leave Staff's plan with the only dollar base-side figures (M IM3); Plan C's Georgia Power critiques with no Staff answer (M IM6, F11); partial agreement counted in card 8 but not card 7 (M IM7); the order's sentence and Georgia Power's last word on GAM's card 9 (M IM8, F10); "by their own admission" without the passage behind it (M IM5) | Next revision |
| O13 | Clarity: the decision block's length and its RSM note (E4); the take-or-pay mechanism is never explained, and item 1 leaves out keeping today's count (E5); reports in item 4 and a reports question (E6, E20); long quotes with acronyms in the cards (E12); P-105's "This" has no antecedent in the passage (E14); undefined terms such as the interchange energy rate, double recovery and Return on Equity (E19, SC framing L184); an inline note on Staff's page-20 correction (E22); 9:30 a.m. on Dec 14 is a safe cutoff, not the exact deadline (E23) | Next revision |
| O14 | Flip tests: a reason in cards 1 and 5's "In short" (F3); parallel card titles (F4); Plan D repeats GIPL's member congregations (F7); party descriptions and Staff's consultants' full titles (F8); "had said" for GIPL in card 7 (F9); card 4's "In short" omits GIPL (F12); "rejects" for GIPL against "opposes" for others (F13); GAM's class-share claim printed twice (F14) | Next revision |
| O15 | Source-check framing, low: GIPL's current pipeline position stated in the present tense (L228, medium); "GIPL" used before it is defined (L24); "five months earlier" (L220); Staff's $1.35 can read as a forecast (L138); P-133 cited for the page's own scope sentence (L101); "the rest of the bill" may overstate (L81); undefined procedural terms (L34); Plan D's "Who benefits" omits the large-load pipeline element (L146) | Next revision |
| O16 | Home: whether the Dec 10 committee meeting counts for the 24-hour comment rule (SC home L5 and framing L5); the Nov 12 start may slip past 9:35 a.m. (SC home framing L9) | Next revision |
| O17 | Questions: Q2's framing (U, M, E20); Q3 on who causes pipeline fees (U); the order's text before Q6 (M) | See C7 |

### Contradicting suggestions (round 5)

| # | Point | Views, side by side | Choice now |
|---|---|---|---|
| C1 | How labels show agreed facts | E (E8): grade the "In short" premise first and move agreed background to a "Common ground" note. U (LA1–LA5) and M (LA2–LA5) propose clause changes card by card, often in opposite directions (card 9: U wants GAM's support shown, M wants GIPL's and Staff's disputes; card 6: U wants Georgia Power's agreement named, M wants access charges out of "Supported") | No change beyond card 5 (brief) |
| C2 | Card 8's size part | Round 3's M (LA4) asked for Georgia Power's $264 million (revision 4 did it). Round 5's M (LA1) and E (E9, F5): say "no public estimate", as card 7 does. Round 4's U (LA2): card 6 should match card 8's $264 million | No change |
| C3 | GIPL's "I agree with this concept" | M (UW1, UW2): it appears three times, its factors once; lead with GIPL's claims. U (LA2): credit the concession in card 8's label. Round 4's U (UW5) asked to restore it at line 268, which revision 5 did | No change |
| C4 | The 30-second version | M (IM1): add Staff's and GIPL's cost-shift claim. E (E3): cut item 2 to two sentences and move the disagreement and blacked-out notes to Layer 2 | No change beyond B15 and B16 |
| C5 | Rebuttals and answers in Georgia Power's and GAM's fields | U (UW2, UW4, UW5, IM1): move opponents' rebuttals out of those fields; add Georgia Power's support and GAM's own answers to card 6 (IM2, MI7, MI8; E21). M (UW11, IM6) and F11: quote GIPL's qualifier more fully in card 3 and add Staff's answer to Plan C's critiques. Round 4's E (E26, F10): no third party answers for a side | No change |
| C6 | GIPL's data-center report | E (E10) and U (IM3), F1: keep it once, in card 7, or pair it with Georgia Power's P-055. M (MI4): add GIPL's basis for the "higher standard" point. M (UW10): drop the "not a finding" caveat or apply it to Georgia Power's claims too | No change; A3 restored GIPL's hedge at line 104 |
| C7 | Q2 | U: "a lower fuel charge against the matching drop in base-side revenue". M: whether the credit should match fuel costs with the revenue that pays them. E (E20): cost causation, bill effects now and later, or both | No change |

## Round 6 (revision 6, `verification/round6/workspace.r6.md`): passed

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate | **Pass.** 427 units (329 cited, 44 analysis, 44 editorial, 10 computed), 0 errors, 0 warnings, including the check on unscoped absence claims. 66/66 hashes match. Reading grade: 30-second 7.0, L1 6.8, L2 8.3, L3 8.3 (Layer 4 10.2) |
| 2 | Blind source check (fresh agent, `source-check.json`, `source-check-home.json`) | **322 supports, 7 partial, 0 does not support, 0 wrong span**; 97 tags valid, 1 questionable; 18 framing issues (3 medium, 15 low). `home.md`: 8 supports, 1 partial, 1 valid tag; 5 framing issues (1 medium, 4 low). **437/437 evidence spans grounded** |
| 3 | Bias panel | **Pass.** Advocate for Georgia Power's and GAM's positions 4/5 (10 missing arguments; 14 wording items, 5 medium; 6 imbalance items, 4 medium; 6 label notes). Advocate for PIA Staff's and GIPL's positions 4/5 (7 missing arguments; 12 wording items, 3 medium; 5 imbalance items, 4 medium; 5 label notes). Standards editor: neutrality 4/5, clarity 3/5; 28 issues (**0 high**, 11 medium, 17 low) and 15 flip-test items |
| 4 | Calibration | Carried over from round 1 (same verifier prompt and method) |

Round 6 passes, with card 5's changed label in place. Revision 6.1 makes only the accuracy fixes the coordinator
listed. It changes no label, card claim or structure, so a **targeted re-check** of the changed sentences follows,
not a new panel. Every other finding is an open item below.

### What revision 6.1 changed

Codes as before (SC = blind source check, L = revision 6 line; U and M = the two advocates; E = editor issues, F = flip
tests). Revision 6.1 keeps revision 6's line numbers (311 lines in `workspace.md`), so each old line number is also
the new one.

| # | Finding (who raised it) | Change in revision 6.1 | Lines |
|---|---|---|---|
| 1 | "Every proposed change we could read would raise the credit" was broader than its citations, which cover the two take-or-pay options; the fallback of moving pipeline fees to base rates (P-086, P-096) would not raise the credit (SC L9 partial; SC framing L9 medium; E13) | "Each proposed plan we could read would raise the credit through its take-or-pay option, by amounts partly blacked out" | 9 |
| 2 | P-053 speaks of "the increase in large load customers served on" real-time pricing, not customers who "moved to" it (SC L24) | "As more large customers were served on real-time prices, …" | 24 |
| 3 | GIPL's "would not change" (P-105) is about one recommendation, and GIPL also wants the real-time price changed (SC L76 partial; SC framing L76 medium) | GIPL says "a change it recommends" would not change the rates real-time customers pay, "just the RTP fuel revenue credit"; a new sentence adds that GIPL calls the price "too low during peak hours" and, while saying rate design is outside this case, recommends that Georgia Power propose new real-time pricing (P-094, P-107) | 76 |
| 4 | "A method" guessed at blacked-out text (SC L110) | GIPL's recommendation is given without the guess, and a new sentence says the words after "RTP pricing that utilizes" are blacked out in the public version | 110 |
| 5 | P-066 is a look back: if Staff's plan "had been in place during 2023 – 2025", today's fuel rate would be lower (SC L138 partial; SC framing L138 medium; E6) | "had its plan been in place in 2023–2025, a home using 1,000 kWh would now pay about $1.35 a month less in fuel charges; that figure is also on page 7, as first filed"; then "That is a look back, not an estimate of savings from adopting the plan now" | 138 |
| 6 | The errata note's reassurance was too broad: P-150 shows Staff's credited amounts in Tables 2, 5 and 7, and its profit-rate estimates may rest on corrected Table 2 totals (SC L174 questionable) | The note now says no Staff figure on the page is taken from the corrected tables, but Staff's profit-rate estimates in Layer 2 and card 9 may rest on the corrected Table 2 totals and could have changed | 174 |
| 7 | Georgia Power's condition dropped (SC L195) | Adds "when the fixed price is below the marginal fuel cost" (P-130) | 195 |
| 8 | GIPL's hedges dropped (SC L254) | Quotes GIPL: the costs "vary with sustained increases in demand", though "fixed" based on coal orders, and a 250% swing in coal use would bring "some increase or decrease" (P-156) | 254 |
| 9 | Home: "Either way, fuel charges … go down" stated an outcome as certain, though the PSC may keep today's method (P-016) (SC home L15 partial; SC home framing L15 medium) | "Either option would make fuel charges for customers on standard rates lower than under today's method, and the same dollars would stop helping pay base costs, the rest of the bill" | home 15 |

The status line (line 3) also changed. For the targeted re-check, the changed units are the sentences on these lines.

### Open items (round 6, not changed in revision 6.1)

| # | Open item (who raised it) | Note |
|---|---|---|
| O1 | Labels: 61–86 words, with "whose testimony we could read" nine times (E10); size keywords differ between cards (E21); card-by-card label proposals: card 3 (U LA1; M LA5), card 4 (M LA4), card 6 (U LA4), card 7 (U LA6), card 9 (U LA3; M LA2; E20) | Label changes; see C1–C4 |
| O2 | Card 9's "In short" gives opponents most of its 67 words (E1, F1, U UW13; M UW5 says the stitched quotes make GIPL sound self-contradictory) | Changes a card summary |
| O3 | The page's own voice answers challenges for GAM (card 9's order sentence) and GIPL (card 5's transcript note), never for Georgia Power or PIA Staff (E3, F3, M UW10) | Next revision |
| O4 | Plan C's "Who pays" lacks Plans B and D's "so base rates could rise later" (E4, F4) | Plan structure |
| O5 | Cards 1 and 5 give no reason in "In short" (E5, F5) | Card summaries |
| O6 | Length and summaries: the 30-second version is 160 words (E8); the decision block runs about 117 words against about 80 (E9); the summaries leave out Staff's and GIPL's fair-share concern (M IM1); "Who it affects" omits real-time customers' baseline power (SC framing L9, low) | See C9 |
| O7 | Clarity: Layer 4 reads at about grade 9–14 (E11); undefined terms such as the interchange energy rate, Return on Equity, double recovery and "over- or under-recovery" (E12, SC framing L184); repeated quotes and notes (E28) | Next revision |
| O8 | "Nearly all" (lines 106 and 116) hides its assumptions: today's cost shares, the per-$100 million figures, timing and carrying costs (E7, SC framing L106, U UW4, M IM2) | See C5 |
| O9 | Plan fields: GIPL's quote in Plan B's "What's uncertain" (E16, F8, U UW7); GIPL's rebuttal in Plan A's "Who benefits" (E17, F7) and the freeze stated in the page's voice there (E18, U UW5, SC framing L122); GAM's own position in the Layer 3 intro (E27); the page-20 caveat where P-157 is used (E26); corporate overhead, the one public item both supporters would add (E25); card 1's "no dollar figure" wording (E24); "than today's method" (E23); "fixes" (F10, U UW8, SC framing L194) | Next revision |
| O10 | Missing or trimmed points for Georgia Power and GAM (U MI1–MI10): the 2010 method's case, the reply to Staff's "all customers" guideline, the economic-development reasons, who agreed to the review (also F11, U UW1), the double-recovery reply on card 6 (also U IM5), GAM's conclusion and own answers, the reply to the subsidy concern (also F9, U UW3, U IM4), card 7's missing pointer to card 8 (U IM6) | Content additions |
| O11 | Missing or trimmed points for PIA Staff and GIPL (M MI1–MI7): Staff's guidelines as its own basis (also M IM4), its answer on past policy, its fallback as an answer to "driven by peak demand", GAM's table as support for card 5's premise, the 0.49-point estimate (carried from rounds 4 and 5), the grounds for GIPL's "higher standard", the reason the others disagree with GAM on card 6; "accepts" (M UW4); P-089's "may be reasonable" (M UW6); Plan C ending on Georgia Power's attacks (M UW7); P-145's purpose (M UW8); "not a finding" applied to GIPL only (M UW3); the baseline counterpoints (M UW12); card 2's base-side match in support (M UW11); the redaction penalty (M IM5) | Content additions |
| O12 | Other source-check framing, low: P-133 cited for the page's scope sentence (L101; also E15, M UW9); "had said" for Georgia Power's earlier statement in card 1 (L184); "as filed" for GIPL's power-line split (L144); the $264 million described two ways (L278); the toll-free number is "inside Georgia only" (L47); the Dec 14 cutoff is the earliest possible, and the committee takes up the case Dec 10 (L50); no docket link (L53); Wise's earlier elections (L17, also F12); "builds on" and the summer peak (L206); "asset or liability" direction (U UW2); "can" for GAM in Plan A (U UW6); GIPL's "higher standard" tense (U UW11); P-145's placement in card 3 (U UW9); "Still" (F14); Staff's reports content (F13); GIPL's description (F15) | Next revision |
| O13 | Home: whether the Dec 10 committee meeting counts for the comment deadline (home L5); arriving early to sign in on Nov 12 (home L9); "PIA Staff" unexplained on the home page, and "power" should be "energy" (home L15, low); the toll-free number and email (home framing L5) | Next revision |
| O14 | Questions: Q2's now-versus-later frame (E22, U UW14, M question note) | See C7 |

### Contradicting suggestions (round 6)

| # | Point | Views, side by side | Choice now |
|---|---|---|---|
| C1 | Card 5's first label part | Round 5's source check (L230): "Mixed", because P-102 shows GIPL arguing against Georgia Power. Round 6's editor (E2, F2): the "Mixed" rests only on GIPL's account; cite Georgia Power's own words (P-124) or grade the premise "Supported". Round 6's more-credit advocate (M LA1): "Supported", since no Georgia Power passage disputes it. Round 6's utility advocate (U LA5, U UW10): Georgia Power's own statements (P-124, P-133) should define its side | No change in 6.1 (labels frozen) |
| C2 | Card 8's use of P-094 | Round 5's source check (L266): it bears on the premise, so name it (revision 6 did, as A15). Round 6's utility advocate (U UW12, U IM2, U LA2): GIPL calls rate design outside this case (P-107), and the claim is under Georgia Power's pending motion to strike (P-134, P-158), so it should not grade the card. Round 6's editor (E19, F6): it doesn't dispute the design premise; end the support sentence at "marginal-cost price" and keep GIPL's point in the label's "Mixed" part and under challenges | No change in 6.1 |
| C3 | Card 9's label | U (LA3): name the P-098 dispute against P-124, P-111 and P-112. M (LA2): grade it "Mixed", since Staff and GIPL give conflicting accounts. E (E20): name Staff's look-back in the "Uncertain" part | No change |
| C4 | Card 8's size part | M (LA3) and E (E21, and E9 in round 5): match card 7's "no public estimate". U (LA4): card 6 should use the $264 million as card 8 does. Round 3's M (LA4) asked for the $264 million | No change |
| C5 | The "nearly all" analysis | E (E7) and the source check (framing L106): state its assumptions, once, in Layer 2. U (UW4): "only if nearly all" makes full pass-through sound like an edge case, though the order carries the whole change to the next base rate case. M (IM2): the page extends GAM's argument with its own analysis | No change |
| C6 | Rebuttals in Georgia Power's and GAM's fields | U (UW7, UW9, IM3), E (E16, E17) and F7, F8: move them to challenges or "Where it stands". M (UW7, UW8): show Staff's answers where Georgia Power's attacks appear | No change |
| C7 | Q2 | U (UW14): weigh a lower fuel charge against "the matching drop in base-side revenue". M: whether the credit should match costs with the revenue that pays them. E (E22): which split matches each cost to the customers it serves, and the bill trade-off | No change |
| C8 | GIPL's concession on the base-side argument | M (UW1, UW2): state the two factors and GIPL's "This shifts costs to FCR customers" instead of leading with "I agree". Round 5's U (LA2): credit GIPL's "I agree with this concept" in card 8's label | No change |
| C9 | The 30-second version | E (E8): cut "Who it affects" to about 55 words. M (IM1): add Staff's and GIPL's fair-share concern. Source check (framing L9): add real-time customers' baseline power | No change beyond fix 1 |

## Step 5 checklist (revision 6.1)

- [x] **Every sentence is cited or tagged.** `check_citations.py` on `workspace.md`: PASS, 430 units (331 cited,
      45 analysis, 44 editorial, 10 computed), 0 errors, 0 warnings. `home.md`: PASS, 10 units (9 cited, 1 computed).
- [x] **Every cited passage ID exists**; all 163 passages are cited. `build_passages.py`: 163 passages, 0 errors.
- [x] **Hashes**: `verify_hashes.py` PASS, 66 files across 35 retrieved sources.
- [x] **Reading grade** (`readability.py`): 30-second 7.2 (160 words), L1 6.8, L2 8.3, L3 8.4 (target ≤ 8.5). Not
      targeted: Layer 4 10.2. Card labels unchanged (up to 83 words).
- [x] **Scope held.** No label, card claim, "In short" line, heading or plan structure changed (checked line by line
      against revision 6). Changed: lines 3, 9, 24, 76, 110, 138, 174, 195 and 254, and home line 15.
- [ ] **Targeted re-check** of the changed sentences: pending (run by the coordinator).

## Where I'm least sure (revision 6.1)

1. **Line 9** now speaks only of each plan's take-or-pay option. The plans' pipeline parts also add to the credit as filed,
   but the 30-second version no longer says so.
2. **Line 76's "new real-time pricing"** reads P-107's request as a proposal for new pricing; what that pricing would use
   is blacked out.
3. **Line 174's** "may rest on the corrected Table 2 totals" is an inference from P-090 and P-150; Staff's footnote does not
   name its source table.
4. **The 30-second version** grew to 160 words (O6).

## Step 5 checklist (revision 6)

- [x] **Every sentence is cited or tagged.** `check_citations.py` (now with the check for unscoped absence claims) on
      `workspace.md`: PASS, 427 units (329 cited, 44 analysis, 44 editorial, 10 computed), 0 errors, 0 warnings.
      `home.md`: PASS, 10 units (9 cited, 1 computed).
- [x] **Quotes verbatim; numbers and dates in a cited passage or `{computed}`** (same run). The two new `{computed}`
      items are the "nearly all" thresholds at lines 106 and 116.
- [x] **Every cited passage ID exists**; all 163 passages are cited. `build_passages.py`: 163 passages, 0 errors; P-107 now
      marks its redaction.
- [x] **Hashes**: `verify_hashes.py` PASS, 66 files across 35 retrieved sources.
- [x] **Reading grade** (`readability.py`): 30-second 7.0 (156 words), L1 6.8, L2 8.3, L3 8.3 (target ≤ 8.5). Not
      targeted: Layer 4 10.2. Card labels up to 83 words (the "whose testimony we could read" scope; card 8's label also names
      GIPL's peak-price point).
- [x] **Scope held.** One evidence grade changed (card 5's first part, from "Supported" to "Mixed"). Eight labels'
      parentheses were scoped without grade changes; cards 7's and 8's first parts were also narrowed (A15, A16), and card 9's "In short" uses GIPL's own words (A13). No card order or
      plan structure changed. The test render parses, and the folded cards show "Supported · Mixed" (cards 1–4, 6–8),
      "Mixed" (card 5) and "Supported · Uncertain" (card 9).
- [x] **Length.** 427 units (revision 5: 420).

## Where I'm least sure (revision 6)

1. **Card 5's new grade** rests on GIPL's account of Georgia Power's view (P-102). No passage gives Georgia Power's own words
   on where the money is credited; its P-124 (base-side money helps pay fixed costs) is close but not the same claim.
2. **Cards 7 and 8** keep "Supported" on narrowed premises (A15, A16): the price's design, and "most" left-out costs. Both
   rest on absence of dispute in the filings we read.
3. **"Nearly all"** (lines 106 and 116) rests on two measures that differ: about 95% by GAM's class shares, about 90% for
   Georgia Power's typical home.
4. **P-107's redaction** was confirmed by the coordinating session on a rendered page image, not by this trial's tools.
5. **The 30-second version** grew to 156 words with the direction words (O6, C4).

## Step 5 checklist (revision 5)

- [x] **Every sentence is cited or tagged.** `check_citations.py` on `workspace.md`: PASS, 420 units (325 cited,
      44 analysis, 43 editorial, 8 computed), 0 errors, 0 warnings. `home.md`: PASS, 9 units (8 cited, 1 computed).
- [x] **Quotes verbatim; numbers and dates in a cited passage or `{computed}`** (same run). The eight `{computed}` items are
      revision 4's; the per-$100 million item now gives both bounds.
- [x] **Every cited passage ID exists**; all 163 passages are cited. `build_passages.py`: 163 passages, 0 errors.
- [x] **Hashes**: `verify_hashes.py` PASS, 66 files across 35 retrieved sources.
- [x] **Reading grade** (`readability.py`): 30-second 6.6 (149 words), L1 6.8, L2 8.2, L3 8.2 (target ≤ 8.5). Not
      targeted: Layer 4 10.0. Card labels 53–67 words, unchanged.
- [x] **Scope held.** No card claim, evidence grade, card order or plan structure changed. The workspace keeps revision 4's
      line numbers; 19 lines changed (the status line plus the lines in the table above).
- [x] **Length.** 420 units (revision 4: 415). About 8,670 words outside labels (revision 4: about 8,470), mostly from the
      GIPL and Georgia Power quotes restored in full.

## Where I'm least sure (revision 5)

1. **Line 116's caveat** repeats line 106's wording, which the utility-side advocate says overstates what GAM's comparison
   needs (C6).
2. **"Every plan we could read would raise the credit"** rests on each plan's take-or-pay part (P-058, P-115, P-128) and the
   Layer 3 reading of the other parts; Resource Supply Management's testimony is unread.
3. **The Nov 12 home item** now sits under "Coming up" with no power label. Whether the public may attend or speak is still
   unconfirmed by any passage, so it only says how to ask.
4. **Line 102's lower bound** assumes base rates don't change at all. The order records the base-side change as a regulatory
   asset or liability (P-016), so that bound depends on how the next base rate case treats it.
5. **O5 (Staff's 0.49-point estimate)** is agreed by all three reviewers but left out under the narrow brief.

## Step 5 checklist (revision 4)

- [x] **Every sentence is cited or tagged.** `check_citations.py` on `workspace.md`: PASS, 415 units (321 cited,
      43 analysis, 43 editorial, 8 computed), 0 errors, 0 warnings. `home.md`: PASS, 6 units (5 cited, 1 computed).
- [x] **Quotes verbatim; numbers and dates in a cited passage or `{computed}`** (same run). The eight `{computed}` items:
      the comment deadline; the 17 cents per $100 million; Staff's $126 million and $399 million projections; four
      dollar-for-dollar base-side matches (Plan A's $70–76 million a year, card 2's $220 million, Plan C's $126 million to
      $399 million and its $329 million as first filed); and Staff's yearly totals of left-out costs.
- [x] **Every cited passage ID exists**; all 163 passages are cited. `build_passages.py`: 163 passages, 0 errors.
- [x] **Hashes**: `verify_hashes.py` PASS, 66 files across 35 retrieved sources.
- [x] **Reading grade** (`readability.py`): 30-second 6.8, L1 6.8, L2 8.0, L3 8.1 (target ≤ 8.5). Not targeted: Layer 4
      9.9. Card labels 53–67 words, not scored.
- [x] **Jargon** defined in place or in "Words used on this page" (row 16).
- [x] **Loaded words and party labels**: none outside quotes (checker: 0 warnings).
- [x] **Structural symmetry.** Four plans with the same five fields, each with the other parties' agreements and
      disagreements and its documented challenges. Nine cards with the same seven fields, one three-part label rule and one
      reply rule.
- [x] **Length.** 415 units against round 3's 402: the new GAM card adds 12, and the other additions are offset by cuts of
      repeats (Who decides, the Layer 4 intro and card rules, card 3, Plan A, "How real-time pricing works"). About 8,470
      words outside labels (revision 3: about 7,750); the new card and the base-side figures account for most of the
      growth. Lead 149 words; "What's being decided" 37 words plus four items; home items 5.

## Where I'm least sure (revision 4)

1. **"No party disputes" clauses** are absence claims from the filings we read; the Dec 2 briefs may change them. Choosing
   which premise a card rests on is also a judgment (C3). Each clause is now a checkable sentence in the card's support.
2. **Dollar for dollar.** The base-side matches rest on P-061 and P-133 (the case changes the split, not the prices). How
   the base-side change reaches base rates is for the next base rate case (P-016, P-090), so the 17 cents is an
   illustration from Georgia Power's per-$100 million examples, not any plan's effect.
3. **Staff's page-7 figures** ($329 million, $322 million, $1.35) are shown as first filed; README D5's by-eye reading of the
   scanned correction gives $328 million.
4. **"Observe" for Nov 12.** The public witness page assumes people come to hearings (P-028), but no passage says this
   hearing is open to the public or that public witnesses will be heard.
5. **Card 6 (GAM's pipeline card)** is short, and its label names only GIPL's challenge; Staff's double-recovery point is in
   the body.
6. **The reply rule on GAM's card** uses Georgia Power's P-124 as the side's answer. The two share that point but not every
   position: Georgia Power's own plan also raises the credit.
7. **GIPL's current pipeline position** still rests on Georgia Power's account of an untranscribed hearing.
8. **Length.** The page grew by 13 units net. Changing the printed ordering rule, instead of adding card 6, would have held
   it at about 403.

## Step 5 checklist (revision 3)

- [x] **Every sentence is cited or tagged.** `check_citations.py` (now with the whole-number check) on `workspace.md`: PASS,
      402 units (326 cited, 29 analysis, 42 editorial, 5 computed), 0 errors, 0 warnings. `home.md`: PASS, 5 units.
- [x] **Quotes verbatim; numbers and dates in a cited passage or `{computed}`** (same run). The five `{computed}` items:
      the comment deadline, the $1.65 base-side example, Staff's $126 million and $399 million projections, Staff's yearly
      totals of left-out costs, and corporate overhead of about $6 million a year (P-150).
- [x] **Every cited passage ID exists**; all 163 passages are cited. `build_passages.py`: 163 passages, 0 errors; P-068 now
      carries `end_occurrence: 1`, after the builder's new warning that its end string repeats 297 characters later
      (checked: recommendation 1 ends there and recommendation 2 begins).
- [x] **Hashes**: `verify_hashes.py` PASS, 66 files across 35 retrieved sources.
- [x] **Reading grade** (updated `readability.py`, which skips label lines and counts sentences ending inside quotes):
      30-second 7.6, L1 6.3, L2 7.6, L3 7.7 (target ≤ 8.5). Not targeted: Layer 4 9.6. Longest card label 68 words.
- [x] **Jargon** defined in place or in "Words used on this page" (item 15 above).
- [x] **Loaded words and party labels**: none outside quotes (checker: 0 warnings).
- [x] **Structural symmetry.** Four plans with the same five fields; each records support and opposition from the other
      parties. Eight cards with the same seven fields, a Side, a claim type and a two-part label under one printed rule.
- [x] **Length budgets.** Lead 109 words. "What's being decided" 40 words plus four one-line items. "In short" lines: one
      sentence of 21–33 words. Home items: 4.

## Where I'm least sure (revision 3)

1. **Labels are judgments.** Every card's main claim is now "Mixed"; C1 and C2 show where advocates would split labels
   differently. Card 3's "no party disputes that large-load growth drives new fees" is an absence claim from our passages.
2. **Staff's page-7 figures** are shown as first filed; my by-eye reading of the scanned correction (README D5) says they did
   not change, but it can't be checked by machine.
3. **GAM's view of Georgia Power's pipeline plan** is mixed in its own words: it describes the plan's effect (P-113) and also
   says no specific part of real-time revenue should be credited against pipeline fees (P-110, P-168).
4. **GIPL's current pipeline position** rests on Georgia Power's account of an untranscribed hearing.
5. **"Large-load customers"** have no threshold in our passages; GAM's 100-megawatt "very large load" may or may not be the
   same group.
6. **Public witnesses on Nov 12**, **who votes on Dec 15**, and the **per-$100 million bill examples** carry the same limits as
   in revision 2.
7. **Card 6's public single item** is corporate overhead only; the coal-related amounts that the card is mostly about are
   blacked out.

## Step 5 checklist (revision 2)

- [x] **Every sentence is cited or tagged.** `check_citations.py` on `workspace.md`: PASS, 357 units (296 cited, 23 analysis,
      34 editorial, 4 computed), 0 errors, 0 warnings. `home.md`: PASS, 5 units (4 cited, 1 computed).
- [x] **Every quoted span appears verbatim in its cited passage** (same run).
- [x] **Every number and date appears in a cited passage, or is `{computed}` with the arithmetic shown** (same run, 0 warnings).
      The four `{computed}` items: the comment deadline (P-024, P-030), the $1.65 base-side example (P-143), Staff's Option 2
      projections rounded to $126 million and $399 million (P-079), and Staff's yearly totals rounded to $741, $824 and $732
      million (P-150). The $1.65 vs. $1.48 comparison is gone.
- [x] **Every cited passage ID exists**; all 163 passages are cited at least once. New: P-162 to P-168. Widened: P-065 (all of
      Table 1). Dropped as no longer cited: P-002, P-057, P-069, P-122. `build_passages.py`: 163 passages, 0 errors.
- [x] **Every source has a sha256 and the file in `_raw/` matches it.** `verify_hashes.py`: PASS, 66 files across 35 retrieved
      sources, 0 problems. S10's document page is now cited (P-162) and listed first, with `S10.txt` as its derived text.
- [x] **Reading grade 6–8 for the 30-second version and Layers 1–3.** `readability.py`: 7.5, 6.4, 8.1, 7.9 (target ≤ 8.5).
      Not targeted: Layer 4 11.9, Layer 5 10.2. Layer 4's figure is inflated by the tool, which doesn't count a sentence that
      ends inside a closing quote mark (counting those, about 10.2); the rest is verbatim testimony that the advocates asked for.
- [x] **Jargon defined or removed** (item 23 above). Watch-list terms do not occur.
- [x] **Loaded-word scan clean outside quotes**; no party or left-right label on the page (checker: 0 warnings).
- [x] **Structural symmetry.** Four plans with the same five fields (What it does · Where it stands · Who benefits · Who pays ·
      What's uncertain); "Where it stands" replaces "What happened" for options still ahead (runbook v0.4). Seven argument
      cards with the same seven fields, a Side, a claim type and a two-part evidence label under one printed rule. Plans B, C
      and D share the same base-rate and pipeline-payer sentences, and partial support is listed both ways.
- [x] **Length budgets.** "What's being decided": 60 words (≤ 80). "What would change": 5 one-line items. "In short": a 19–24-word
      claim plus a short counterpoint (30–41 words). 30-second items: 42–54 words. Home items: 4.
- [x] **"Where I'm least sure"**: below.

## Where I'm least sure (revision 2)

1. **Staff's page-7 figures.** The corrected page is a scan. My by-eye reading (README D5) says the correction changes a different
   figure, but the page can only say "as first filed". If my reading is wrong, the $322 million and $1.35 could have changed too.
2. **GAM's view of Georgia Power's pipeline plan.** GAM describes the plan's effect (P-113) and also says no specific portion of
   real-time revenue should be credited against pipeline fees (P-110). The page shows both without calling GAM a supporter or an
   opponent; the full GAM testimony may say more than our passages hold.
3. **GIPL's current pipeline position.** Only Georgia Power's rebuttal reports the change at the hearing (P-154, P-155), and the
   transcript has no file.
4. **"Very large customers."** GAM defines "very large load" as 100 MW or more (P-168); Georgia Power's "large load" may not use
   the same line. The page uses one plain term for both and says so.
5. **Evidence labels.** The new rule is applied by judgment: whether a premise is "Mixed" or "Supported" depends on reading each
   party's statements as disputing it. C2–C5 show where advocates would have chosen differently.
6. **Public witnesses on Nov 12.** The page now says no filing tells us; the PSC's answer by phone is the only way to know.
7. **Who votes.** "The same five are set to vote" holds barring a vacancy or recusal; it relies on PSC pages as of 2026-11-08.
8. **Bill effects per $100 million** (P-141 to P-143) are Georgia Power's examples from its storm-cost and fuel-rate changes, not
   estimates for any plan; the page says so, but readers may still compare them.
9. **Resource Supply Management's testimony** is a scan; the page names the party and witness but summarizes nothing (D13).
10. **GAM's class table** comes from a 2023 cost study whose docket number GAM gives as "44820" (probably 44280); we cite it as given.

## Revision 1: step 5 checklist (as run before round 1)

Kept for the record; the round 1 table above supersedes its claims of symmetry and balance.

- [x] **Every sentence is cited or tagged.** PASS, 269 units (226 cited, 16 analysis, 23 editorial, 4 computed), 0 errors,
      0 warnings. `home.md`: PASS, 4 units.
- [x] **Every quoted span appears verbatim in its cited passage**; every number and date in a cited passage or `{computed}`.
      The four `{computed}` items then: the comment deadline, the $1.65 base-side effect, Staff's $126 million and $399 million,
      and the $1.65 versus $1.48 comparison.
- [x] **Every cited passage ID exists**; all 160 passages were cited at least once.
- [x] **Hashes**: PASS (the count was given as 65 files; the correct count is 66).
- [x] **Reading grade**: 30-second 6.2, L1 5.6, L2 6.2, L3 8.0; Layer 4 10.0, Layer 5 9.3.
- [x] Jargon, loaded words, structural symmetry, length budgets and "where I'm least sure" were checked by the drafter; round 1
      found the symmetry and label claims did not hold (items 5–7, 29–33 above).

Suggested planted-error types for calibration on this page, beyond Trial 001's set (used in round 1): an errata-superseded
number, a redacted value presented as zero, an option attributed to the wrong party, a deadline shifted by the 24-hour rule,
a commissioner's term date, and "Option 1" and "Option 2" swapped in one sentence.

## Targeted re-checks (revisions 6.1 to 6.3, run by the coordinating session)

Each re-check was a fresh blind agent that read only the page (or home items), the passages and a list of changed
units, and checked only those units. Spans were machine-checked with `trials/tools/check_verifier.py`. Reports are
copied unchanged into `verification/round6-targeted/`, `round6-targeted-2/` and `round6-targeted-3/`, each with its
list of changed units.

| Re-check | Units | Result | What it raised, and what we did |
|---|---|---|---|
| 1, on revision 6.1 | 12 page + 1 home | 10 of 10 cited units supported, 2 of 3 tags valid, 1 questionable; 13/13 spans grounded (66667ae612b25d7a…, b22adaa5066fb3cf…) | **Line 174 (questionable):** "so they too could have changed" implied Staff revised its profit-rate estimates; P-039's corrections don't include footnote 45 (P-091). Fixed in 6.2 (split into two tagged sentences: the estimates weren't corrected, but may rest on credited amounts in the corrected Tables 2 and 7). Also fixed in 6.2: line 9 says whose estimates are public; line 110 says the blacked-out words sit between "utilizes" and "as quickly as possible"; line 138 "1,000 kWh a month" (P-067); line 195 keeps Georgia Power's conclusion from P-130; line 254 quotes GIPL's own "increased or decreased by 250%" (P-156) |
| 2, on revision 6.2 | 7 | 5 supports, 2 tags valid; 0 partial, 0 questionable; 7/7 grounded (2edca53eafb8cbb6…) | Nothing misstates a passage. One note on line 9: the 30-second version is read alone, and "PIA Staff's estimates are public" could suggest all of Staff's estimates are public, though its pipeline estimates and most single cost items are blacked out. Treated as a misstatement when read alone, so fixed in 6.3: "PIA Staff's take-or-pay estimates are public" |
| 3, on revision 6.3 | 1 | supports; 1/1 grounded (e5287b562b0a6063…) | **Passed.** Two low notes, logged as open: the sentence doesn't say Staff's public estimate covers only its own option (Option 2), and "take-or-pay" isn't glossed in the 30-second version (Layer 2 defines it) |

The protocol's targeted re-check is done: no re-check found an unsupported claim, and the last one found nothing
that misstates a passage. **Status: Passed checks (round 6), revision 6.3.**

## Revision 7: reader-first update (checks pending)

Change class **update**; reason: reader-first revision after usability feedback. Revision 6.3's page, home items and
display settings are kept in `verification/round7/` (`workspace.r6.3.md`, `home.r6.3.md`, `page.r6.3.yaml`). The
contract is the style guide v0.6 (section 9) and runbook v0.6, Step 4. The coordinator's notes from a lay-reader test
(a renter with a Georgia Power bill) were folded in. **No evidence label, card claim, card order or plan structure
changed**: the nine label lines are identical to revision 6.3's.

| Part | What changed |
|---|---|
| Title and chips (`page.yaml`) | Title: "Georgia regulators decide Dec 15 how much business real-time payments lower others' fuel charges" (14 words). Plain link text, status chips and option chips, each with a citation comment; plain source chips ("Utility testimony", not "GP testimony") |
| 30-second version | 89 words by `readability.py` (was 149–160). Names the decider and says what it is: the PSC, "five commissioners who set utility rates" (P-031, P-169) |
| What it means for you (new) | Four "If you…" lines: a home bill; a business on standard rates; a business on real-time prices; an EMC or city utility customer (from new passage P-170). Then "Where the money comes from" (5 steps, with the fuel credit described as a fuel-account entry), "Decided now, and decided later" and "Not known yet" (each item says why, dated as of Nov 8, 2026). The $1.48 and $1.65 per $100 million are labeled examples, not any plan's effect; no per-home figure is computed for a plan |
| New passages | P-169 (the PSC decides "fair and reasonable rates" for services under its jurisdiction), P-170 (what the PSC regulates: EMCs "Territory and financing only", city-owned power companies "Territory only") and P-171 (the PSC's three standing committees), all from the archived PSC FAQ (S19), built with `build_passages.py` |
| Layer 1 | Order: What you can do; Who decides, and how to reach them; Coming up; What happened. The Decide label is gone. The five commissioners are listed in one format (role; serves through), with the PSC's email and phones exactly as P-163 gives them and where the commissioners vote (P-032). The Executive Secretary's Office number is added (P-022); the Nov 12 arrival time is computed (about 9:05 a.m.); the Dec 14 cutoff is called the earliest possible. The May 28 entry gives the review's purpose in the order's words ("shall further review", "may make modifications if appropriate") and Georgia Power's own statement that it "supported and agreed to this subsequent review" (P-053). Stan Wise's Sep 5 appointment moved to the timeline, so every commissioner gets the same format |
| Layer 2 | The PSC defined at first mention on the tab (P-169). Item 1 says what take-or-pay contracts are, why counting their energy without their cost lowers the credit (P-058), and names all three choices, including keeping today's count. Parties defined at first mention on the tab, GIPL by who it is (P-093). Glossary entries added: standard rates, fuel credit ("an accounting entry, not a credit on anyone's bill"), base side, base rate case timing (P-054), double recovery, Return on Equity, interchange energy rate. The scope sentence now cites the order and both sides, not Georgia Power's rebuttal alone (round 6 O12); the base-side change is described as a drop in base revenue for the plans in the case (round 6 O12, U UW2). Long sentences split |
| Layer 3 | Intro of 60 words that defines each party. New "If you care most about…" guide: one line per plan (A to D) from a documented, verbatim quote (below). Plain wording in Plan A; "the public copy doesn't say why" where Georgia Power's estimates are blacked out (no passage gives a reason); "As filed" for GIPL's power-line split; the three policy-related costs named (P-089) |
| Layer 4 | Every "In short" rewritten (≤ 40 words): the effect on people, then the side's claim with its reason (cards 1 and 5 now give one, round 6 O5), then "Evidence: …" restating the label. GIPL described by its member congregations (P-093). Fold-ins from round 6 O12: "had said" in cards 1 and 7, card 3's summer-peak detail (P-145), no "Layer 2" references in the page text. Bare "It says" sentences now name the speaker |
| Home | Five items, each ≤ 25 words before its action, each dated, institutions defined; no Decide label; the PSC's local and toll-free numbers as P-163 gives them |

**The values guide, as written**

- Plan A: "If you care most about keeping base rates down: Jonathan Ly, GAM's witness, who argues against a bigger credit, said 'increasing the RTP fuel credit would put upward pressure on base rates.'" [P-153, P-115]
- Plan B: "If you care most about large new customers paying their pipeline costs: Georgia Power's witness panel, which backs Plan B, said it proposes that those costs 'be fully recovered from those customers.'" [P-041, P-055]
- Plan C: "If you care most about matching fuel costs with fuel revenues: PIA Staff's witness panel, which backs Plan C, said 'fuel related costs should correspondingly be matched with fuel revenues to cover those costs.'" [P-059, P-062]
- Plan D: "If you care most about keeping costs from shifting to customers on standard rates: John D. Wilson, GIPL's witness, who backs Plan D, said of today's credit, 'This shifts costs to FCR customers.'" [P-152, P-099]

**What the sources couldn't support (left out or said plainly)**

- Why Georgia Power's estimates are blacked out: no passage says; the page says "the public copy doesn't say why".
- A typical home's net for any plan, and the date of the next base rate case: no filing we read gives either.
- Whether customers of EMCs or city utilities feel any indirect effect: no passage says; their line says only that this
  case doesn't set their power company's rates.
- GIPL's members' stake or power use, and the Nov 12 hearing room: not in the passages.
- How each commissioner was chosen is left out for all five, so the format stays the same.

**Where I'm least sure (revision 7)**

1. **"If you pay a Georgia Power bill at home"**: "any proposed plan" rests on each plan's take-or-pay option raising the
   credit (P-058, P-115). "Recover the same money" is the system-wide amount; a home's share of the base side differs
   from its share of the fuel side (P-116), which is why the line says only that coming out ahead depends on the next base
   rate case.
2. **"If you run a business on standard rates"** repeats the home line's first sentence; GAM's class-share claim is
   attributed, and the page's own analysis of it is in Layer 2.
3. **"If your business pays real-time prices"**: "not your hourly prices" rests on Georgia Power's scope statement
   (P-133) and GIPL's answer that rate design is outside this case (P-107); GIPL still wants new real-time pricing
   proposed.
4. **"If your power comes from an EMC or a city-owned utility"**: "it doesn't set your power company's rates" is our
   reading of the FAQ's "Territory and financing only" and "Territory only" (P-170). The same FAQ page reports a 2011
   budget, so its list may be dated; the page uses no counts from it.
5. **Values guide, Plan A**: no party whose testimony we read backs today's method in every part. GAM argues against a
   bigger credit but accepts either take-or-pay option if the PSC finds a change is needed (P-111); the guide's closing
   line says so.
6. **Values guide, Plan B**: Georgia Power's pipeline-cost value (P-055) was chosen over its marginal-cost value (P-119,
   P-129). GIPL's plan also recovers new pipeline fees from large-load customers (P-096); the closing line says one value
   can apply to more than one plan.

**Gate (revision 7)**: `check_citations.py` PASS on `workspace.md` (512 units: 388 cited, 52 analysis, 62 editorial,
10 computed; 0 errors, 0 warnings) and `home.md` (8 units; 0 errors, 0 warnings). `readability.py`: 30-second 8.4
(89 words), What it means for you 8.4, Layer 1 7.5, Layer 2 8.2, Layer 3 8.4 (Layer 4 9.9, not targeted); no budget
warnings and no CHECK lines. `verify_hashes.py` PASS. `build_passages.py`: 166 passages, 0 errors, all cited. Test
render: no warnings about Decide labels or undated home items.

## Round 7 (revision 7, `verification/round7/workspace.r7.md`): passed

| Layer | Check | Result |
|---|---|---|
| 1 | Deterministic gate | **Pass** (revision 7, above): 512 units, 0 errors, 0 warnings; `home.md` 8 units; reading grades 8.4, 8.4, 7.5, 8.2 and 8.4 in the targeted sections (Layer 4 9.9); hashes PASS |
| 2 | Blind source check (`source-check.json`, `source-check-home.json`, `display-check.json`) | Page: 512 units, **381 supports, 7 partial, 0 does not support, 0 wrong span**; 121 tags valid, 3 questionable; 19 framing issues (4 medium, 15 low). Home: 8 units, 7 supports, 1 valid tag; 2 framing issues (low). Display strings (`page.yaml`): 8 units, 6 supports, 2 partial; 4 framing issues (1 medium, 3 low). **0 ungrounded evidence spans** (403 spans), by the coordinator's check |
| 3 | Bias panel | Advocate for Georgia Power's and GAM's positions **4/5** (7 missing arguments; 13 wording items, 6 medium; 8 imbalance items, 1 high, 6 medium; 6 label notes). Advocate for PIA Staff's and GIPL's positions **4/5** (8 missing arguments; 9 wording items, 5 medium; 10 imbalance items, 1 high, 5 medium; 4 label notes). Standards editor: neutrality 4/5, clarity 3/5; 25 issues (**0 high**, 10 medium, 15 low) and 12 flip-test items |
| 4 | Calibration (`verification/round7/calibration/`) | Caught **10 of 10** planted errors (the coordinator's count; the drafter did not open the folder) |
| – | Lay reader (experimental; `verification/lay-reader/revised/`) | Not a pass condition. Questions 1, 2, 5 and 6 answered; 3 with effort; 4 partly (how to send a comment); 39 terms it couldn't place |

**Round 7 passes**: the gate is clean, the source check found 0 unsupported claims, 0 wrong spans and 0 ungrounded spans,
both advocates rated their side 4/5, and the editor found no high-severity issue. Each advocate also marked one imbalance
item "high". The utility side's (U IM1, the "In short" lines) breaks style guide section 8 and is fixed in revision 7.1
(item 15). The more-credit side's (M IM1, card 5's label) would change a label, so it is an open item (C1).

Codes: SC = page source check, SH = home check, SD = display-string check (L = revision 7 line); U and M = the two
advocates (MA missing argument, UW wording, IM imbalance, LA label note, numbered in report order); E = editor issues
(E1–E25) and F = flip tests (F1–F12), in report order; LR = lay reader.

### Revision 7.1: what changed

Change class **correction** for items 1–14 and **update** for items 15–24; reason: "fixes after passing round 7".
Revision 7 is archived in `verification/round7/` (`workspace.r7.md`, `home.r7.md`, `page.r7.yaml`). **No card claim,
evidence label or side changed**: the nine evidence labels are identical to revision 7's. Card titles 1 and 2 change only
their "(Option 1)" and "(Option 2)" tags, and the "Side" field of those two cards is renamed the same way (item 21). Line
numbers are revision 7.1's. The changed units are listed in the coordinator's scratch folder as `changed-units.txt`
(144 changed or new units on 75 lines, 113 units removed or replaced, 1 home line, 12 `page.yaml` strings).

| # | Finding (who raised it) | Change in revision 7.1 | Lines |
|---|---|---|---|
| 1 | P-011 gives the date, not a definition of briefs (SC L38 partial) | "Briefs are due by 4 p.m. on Dec 2, 2026" [P-011]; the definition is its own `{analysis: plain-language definition}` sentence | 38 |
| 2 | "One of three committees": the calendar names a fourth (SC L71 partial; SC framing L71) | "The PSC's Energy Committee, a committee of its commissioners, …" | 72 |
| 3 | P-105's "This" is one recommendation, not GIPL's whole plan (SC L145, L328 partial) | "GIPL says a change it recommends …" at both places. In the same line 146 sentence pair, "it says … its plan" became "PIA Staff says … Staff's plan" (SC framing L330, low) | 146, 331 |
| 4 | The "homes come out behind" conclusions rest on an unstated condition (SC L148, two questionable tags; SC framing L148; E6; U UW8) | The condition is in the visible sentences: "If the later base-side change is shared among customer groups the way base revenues are today, that ranking holds …"; "On that condition, and with no added carrying cost, homes as a group come out ahead in dollars unless about 95% or more of the base-side change reaches base rates." The typical home's point (about 90%) is tied to Georgia Power's per-$100 million examples, which follow its storm-cost and fuel-rate changes (P-141). "Only if nearly all of it" is gone; the tag notes P-016 and P-105 | 149 |
| 5 | GAM's position overstated as arguing against a bigger credit (SC L158, L162 partial, L166 questionable; SC framing L158, L162; E1, E9, F1; U UW7) | Layer 3 intro: "GAM, a manufacturers' group, warns of upward pressure on base rates" (P-115). Values line: "GAM's witness, who warns about a bigger fuel credit, said …". The Plan A note no longer calls GAM's line Plan A's. Every "either take-or-pay option" sentence now follows P-111: "GAM says that if the PSC finds a change is needed, either take-or-pay choice would "adequately address" the concerns of PIA Staff and intervenors" (the passage names PIA Staff's and intervenors' concerns, not GAM's own). Card 9's "Who says it" adds that sentence (E8, F9); its title, side and label are unchanged. GAM's opposition to crediting real-time money against pipeline fees (P-110) stays in Plan A and cards 3, 4 and 6 | 159, 166, 167, 172, 180, 188, 196, 330 |
| 6 | P-055 doesn't show that new pipeline fees are covered through the credit (SC L180 partial) | "The fuel charge also would not carry new pipeline fees tied to large-load customers. [P-055, P-065] Those customers' real-time payments would cover the fees. [P-065, P-127]" | 181 |
| 7 | "Get a bigger fuel credit" reads as a bill credit (SC framing L180, L188, L196) | Plans B–D: "Customers on standard rates," then "more real-time money would count toward fuel" (Plan B adds "which lowers the fuel charge", P-144) | 181, 189, 197 |
| 8 | Line 50's tag said the FAQ doesn't mention committee meetings; the FAQ names the standing committees (P-171) | The tag now says so, and that the archived FAQ doesn't say whether the public may attend or speak; the sentence is unchanged | 51 |
| 9 | "Base rates are frozen through 2028" in the page's voice (SC framing L172; E12) | "Georgia Power says base rates are frozen through 2028." [P-054] | 173 |
| 10 | "Dec 15, 9:30 a.m." reads as the decision's time (brief; SH framing L5) | Heading "Dec 15"; the session "starts at 9:30 a.m."; a new process note says the case's time in the session isn't posted as of Nov 8, 2026 | 73 |
| 11 | Cards 3 and 5 said large-load customers would be "charged" or would "pay" pipeline fees; no plan changes what those customers pay (E5) | Card 3 and card 5 "In short": "New large-load pipeline fees would stay off fuel charges" (P-055, P-096). The cards' bodies and Plans B and D keep the passages' own account: the fees are "recovered from" those customers by counting their real-time payments toward fuel (P-127) | 257, 281 |
| 12 | Card 7's "by their own admission" has no PIA Staff passage (M UW5) | Removed: the quote now ends at "principles." Georgia Power's footnotes for it cite the Oct 15 transcript (Tr. 298-99), which is listed without a file, so no Staff passage can be cited | 309 |
| 13 | `page.yaml` (SD L7 and L14 partial; SD framing L7, L14) | `link_text`: "Larger businesses' real-time power payments and your fuel charge" [P-016, P-044, P-144]. Plan C chip: "PSC staff's plan" [P-006, P-059, P-073]. Every reader-facing string keeps a citation comment | `page.yaml` |
| 14 | 1-800-282-5813 is "outside metro-Atlanta" in one place and "inside Georgia only" in another (LR; round 6 O12) | Line 48 now attributes "inside Georgia only" to the Sep 1 legal notice that says it (P-021, P-022). Lines 46, 49 and 63 keep the public witness page's "(outside metro-Atlanta)" (P-163). Neither description is merged into the other | 48 |
| 15 | "In short" lines give the fuel side only (U IM1 high, UW5, UW6; LR "seemed to favor") | Cards 1, 2 and 4: "Fuel charges would likely fall; base rates could rise later." Card 7: "Counting costs such as coal rail car leases would likely lower fuel charges; base rates could rise later." Cards 3 and 5: "New large-load pipeline fees would stay off fuel charges; base rates could rise later." Card 6: "Fuel charges keep paying pipeline fees; real-time money keeps lowering base costs" (P-111, P-124). Card 8: "Fuel charges keep paying most left-out costs; Georgia Power says real-time customers pay their share through the base side" (attributed, since PIA Staff and GIPL dispute it). Each line is ≤ 40 words; claims and labels unchanged (two claim sentences lost one or two words: card 6 "fixed costs" → "fixed"; card 8's claim is split into its two halves) | 233, 245, 257, 269, 281, 293, 305, 317 |
| 16 | "The public copy doesn't say why" only for Georgia Power (SC framing L35; E2, F2; U UW2, IM2) | Removed at lines 35, 158, 180 and 196 (revision 7). One "Not known yet" item now covers every party: the public copies black out some figures from Georgia Power, PIA Staff and GAM and some words from GIPL, and "the copies we read give no reason for any party's blacked-out material" `{editorial: scoped to the copies we read}` | 35, 40, 159, 181, 197 |
| 17 | Uneven caveats (M UW2, UW1, UW4, IM2; U UW3, IM5; F7) | (a) Card 7's "not a finding by the PSC" note is removed; no side's statement carries one now. (b) GIPL's complaint in its own words: Georgia Power "has consistently refused" to let intervenors review the model (P-103), in Plan B and card 3. (c) GIPL's capital-substitution quote moved from Plan A's "Who benefits" to its "What's uncertain", as a short quote plus paraphrase, so no plan's benefit field ends on a rebuttal. (d) Plan C's "Where it stands" adds PIA Staff's on-record answers, marked "had said": "simply for illustration purposes" (P-145), "consistent with the 2010 rate case proposal" (P-073) and "consistent with the manner in which fuel costs are recovered" (P-084); P-127 now says "if large projects fall through". For the same evenness (F7), Plan B's adds Georgia Power's on-record answer to Staff's "partial solution": existing fees "are not driven by large load growth" (P-051) | 175, 180, 183, 188, 261, 307 |
| 18 | Values guide (SC L162; E9, E19, F3, F12; U values_guide, IM6; M values_guide, IM3) | One line per side with a documented argument, in one form, "<party>'s witness(es), who back(s) Plan X (GAM: who warns about a bigger fuel credit), said "…"", with no witness named individually. Georgia Power, Plan B: "preserve the integrity and structure of the RTP rate design" (P-131). PIA Staff, Plan C: its "all customers" guideline (P-062), as the more-credit advocate asked. GIPL, Plan D: "This shifts costs to FCR customers," with FCR defined right after. GAM: P-115. A note says Plan A has no line, that Georgia Power also says it protects existing customers (P-054, the value GIPL's line names), and that Resource Supply Management isn't covered. Lines are 31–33 words; quotes 5–12 words; no recommendation words | 163–167 |
| 19 | "What it means for you" lines don't share fields (E7, E24; LR) | Each line: direction first (≤ 25 words), then size or why it isn't known, then when, marked "as of Nov 8, 2026". To fit 45 words, the home line drops "Whether you come out ahead depends on the next base rate case" (the money steps say it) and the business line drops GAM's group comparison (C6) | 14–17 |
| 20 | Define at first mention on each tab (E10; LR unclear terms) | Summary: real-time prices (8), fuel charge and base rates (9), the fuel credit as an accounting entry (23), base rate case (25), GAM (40), GIPL (79), EMC "territory and financing" in plain words (17); docket was already defined at its first body mention (52). What's decided: real-time customers, the fuel credit (accounting entry), fuel charge, base rates and the PSC (94); "FCR customers" right after the quote (146); "the docket's filing list" → "the case's filing list" (155). Options: the parties, RTP, the fuel credit (accounting entry) and base rates (159); "FCR customers" right after the quote (165). Arguments: a "Words on these cards" paragraph (RTP, fuel credit, fuel charge, base rates, take-or-pay contracts and choices, pipeline fees) (213); "FCR customers" after the quote (331) | 8, 9, 17, 23, 25, 40, 79, 94, 146, 155, 159, 165, 213, 331 |
| 21 | "Option 1" and "Option 2" next to Plans A–D (LR) | Reader text says "take-or-pay choice 1" and "2", mapped once on each tab to the filings' "Option 1" and "Option 2" (171, 213). Card titles 1 and 2 change only that tag; their Side fields are renamed the same way; quotes and evidence labels keep "Option". Sources table "why" cells follow | 35, 171–197, 213, 229–251, 339, 357–360 |
| 22 | Title frames the decision as fuel-side only (U UW1; LR; SD framing L6; E21) | "Georgia regulators' Dec 15 decision: how real-time payments split between fuel and base costs" (14 words) [P-013, P-016, P-061, P-169]; the page's H1 matches | `page.yaml`, 1 |
| 23 | The comment item didn't say where to ask (LR question 4) | The item leads with "Send the PSC a public comment by 9:30 a.m. on Dec 14, 2026" (computed), then says the PSC's Public Comments page doesn't say how to send a comment, then gives the phones and email (P-163). The home item adds the email | 49; home 5 |
| 24 | Source labels didn't say who (LR) | "GIPL testimony", "GAM testimony", "PIA Staff testimony", "PIA Staff corrections", "Georgia Power testimony", "Georgia Power data", "Georgia Power rebuttal", "Georgia Power motion"; S10 "Filing page" (the old "Resource Supply" chip read like a typo beside the party's name). Some labels are now three words | `page.yaml` |

Other text that changed to fit a budget or a definition: the 30-second version dropped "Dates can change." (the "Coming
up" list still says so) to fit the new definitions in 90 words; the comment item dropped the Public Comments page's
"relevant feedback" description; the real-time line's "New large-load customers on one tariff have no baseline" became
"Any baseline power you have" (P-071 cited) and a tag note.

### Open items (round 7, not changed in revision 7.1)

| # | Open item (who raised it) | Note |
|---|---|---|
| O1 | Labels: cards 4 and 7 graded "Supported" on background facts (U IM7, LA1, LA2); card 3 should credit GIPL's agreement and P-127 (U LA4); card 6's size part (U LA6) and its access-charge premise (M LA3); card 8's size part (M IM6, LA2); card 9 "disputed" (M LA4); one label per card, or rename the "Supported" part (E3); "Mixed" versus the legend's "Contested" (E15) | Label changes need a full round; see C1–C4 |
| O2 | Card 9's title and side, "Limit the shift", are the page's words, not a remedy GAM proposes (E8) | Changes a card's claim |
| O3 | GIPL's peak-price finding in the basics, "Also raised" and card 8 (U UW4, IM4; E4, F5) | See C2 |
| O4 | Missing arguments for Georgia Power and GAM (U MA1–MA7): P-121 "not similarly situated", P-045, P-052 in card 8, P-046 in Plan A, P-168's conclusion, P-110's second half, P-127's second half | Content additions |
| O5 | Missing arguments for PIA Staff and GIPL (M MA1–MA8): the 0.49-point profit estimate as a Plan A cost (carried since round 4), the 2026–2031 growth in Plan A, Staff's answer in card 7, P-048 and P-058 in card 2, GIPL's reason for the peak-price finding, P-124's "(like FT)", the transparency points side by side | Content additions; Plan C's answers (M MA3, MA4) were added under item 17 |
| O6 | Card 6's challenges go unanswered though GAM's P-110 and Georgia Power's P-123 answer them (U IM3) | Round 4 and 5 views differ on third-party answers (round 6 C5) |
| O7 | Plan fields: Plan A leans toward its supporters (M IM4); Plan B's "Who pays" omits the costs that stay on the fuel charge (M IM5); Plans B and D's "Who pays" lack a blacked-out note (M IM10); Plan C's per-home benefit without a per-home cost (F6); the $102 and the data-center quote side by side in Layer 2 (M IM9, E11, F4, SC framing L146); line 147's counterpoints (M IM7); GIPL's "two critical factors" not named in Plan A and card 8 (M UW3); Georgia Power's P-058 clipped and "fixes" (U UW9, IM8); P-145 inside card 3's support (U UW10); card 3's "so" and missing P-127 reason (U UW11); P-051 used for Staff in card 4 (U UW13); card 5's "Who disagrees" through GIPL's account (E16, F8); GIPL's description caveat (F10); the reports sentence quotes one side (F11) | Next revision |
| O8 | Questions to weigh: Q2's now-versus-later frame (U, M question notes); Q3 mixes choices; no question on monthly reports (E23; M) | See C7 |
| O9 | Clarity: Layer 4 reads at grade 9.9 (E17); line 224's errata note (E17); terms still undefined: carrying costs, hedging, demand response, baseload or intermediate generation (SC framing L186), and lay-reader terms outside the brief's list (power-pool trades, large-load cost model, winter peak, embedded costs, rate schedules, Southern Company Services and the consulting firms); unlabeled bullets under "What you can do" (E22); "set to vote" (E25); vague references at line 145 ("based on its recent storm-cost and fuel-rate changes", SC framing L144) and line 175's three uses of "it" (SC framing L174); the page header's "PSC Docket 57171" chip comes before the first body definition of "docket" | Next revision |
| O10 | 30-second version: say why the review exists (M IM8); "Decided by Dec 15" and the title state a scheduled date (SC framing L1) | Next revision |
| O11 | Home: "Each plan we could read" is vague and sits under the Oct 27 item (SH framing L15); the Dec 14 deadline shown without "earliest possible" (SH framing L5); the Nov 12 hearing is on Georgia Power's rebuttal (SH L9 note) | Next revision |
| O12 | The errata flagged three times in Plan C's benefit field (M UW8); GIPL's Oct 14 answer to the motion sits far below the request (M UW9); GIPL as "a group of congregations" (SC framing L37); uneven option-chip naming (SD framing L13) | Next revision |

### Contradicting suggestions (round 7)

| # | Point | Views, side by side | Choice now |
|---|---|---|---|
| C1 | Card 5's first label part | M (IM1 high, LA1): grade it "Supported"; the only conflict is GIPL's account of Georgia Power's view. U (LA5): add "Uncertain" for which split GIPL now backs. E (E16, F8): use Georgia Power's own words (P-124) in "Who disagrees" | No change (labels frozen after a pass) |
| C2 | GIPL's peak-price finding | U (UW4, IM4, LA3) and E (E4, F5): it is under a pending motion to strike and GIPL calls rate design out of scope; drop it from the basics and card 8's label, or note the motion. M (MA6, UW7): add GIPL's reason, "because the reliability cost component was set inconsistently with the peaker method" | No change |
| C3 | "Supported" parts on background facts | U (IM7, LA1, LA2) and E (E3): grade each card's disputed premise. M: cards 1, 2, 4 and 7 are "labeled fairly and in parallel" | No change |
| C4 | Size parts of cards 6, 7 and 8 | M (IM6, LA2): card 8 should match card 7's "no public estimate". U (LA6): card 6 should cite the $264 million as card 8 does | No change |
| C5 | The "not a finding by the PSC" caveat on GIPL's data-center report | M (UW2): drop it or apply it to every side. E (E11, F4) and SC (framing L146): keep the quote once, with the caveat, or add the caveat at line 146 | Revision 7.1 removes it (brief item 17); adding a caveat for every side stays open |
| C6 | GAM's group comparison on the "If you" lines | E (E7): put it on both standard-rate lines or neither. U (if_you_lines): add it to the home line. M (if_you_lines): add the page's own computed point (a home comes out behind only if nearly all of the shift reaches base rates) to the home line | Revision 7.1: neither, because direction, size and when fill the 45-word budget; the comparison and the computed point stay in Layer 2 (line 149) |
| C7 | Q2 | U: "a likely lower fuel charge against the same dollars coming off the base side". M: add "which split best matches fuel costs with the revenue that covers them?" | No change |
| C8 | Georgia Power's values line | U (values_guide, IM6): use its marginal-cost value for Option 1 (P-119, P-129). M (values_guide, IM3): its pipeline value (P-055) is shared with GIPL's plan and with the review's purpose (P-053) | Revision 7.1 uses Georgia Power's own conclusion, P-131 ("preserve the integrity and structure of the RTP rate design"), which covers its whole plan and needs no "Option 1" in the quote; neither advocate proposed this exact quote |
| C9 | GIPL's values line | U: reword without the disputed premise and note that Georgia Power claims the same value (P-054, P-127). M: fair and strong as written | Revision 7.1: "protecting customers on standard rates", the quote kept, and a note that Georgia Power also says it protects existing customers (P-054) |

### Step 5 checklist (revision 7.1)

- [x] **Every sentence is cited or tagged.** `check_citations.py` on `workspace.md`: PASS, 541 units (408 cited, 57 analysis,
      65 editorial, 11 computed), 0 errors, 0 warnings. `home.md`: PASS, 8 units (7 cited, 1 computed), 0 errors, 0 warnings.
- [x] **Passages unchanged**: 166 passages, no new ones; all cited. `verify_hashes.py`: PASS.
- [x] **Reading grade** (`readability.py`): 30-second 7.3 (90 words), What it means for you 7.2, Layer 1 7.4, Layer 2 8.4,
      Layer 3 8.4 (target ≤ 8.5). Not targeted: Layer 4 9.9. No budget WARN lines and no CHECK lines. Longest card label
      83 words (card 8, unchanged).
- [x] **Test render**: no warnings; the page shows "version 7.1".
- [x] **Scope held**: the nine evidence labels are identical to revision 7's; no card claim or side changed; card order
      and plan structure unchanged.
- [ ] **Targeted re-check** of the changed units: pending (run by the coordinator).

### Where I'm least sure (revision 7.1)

1. **Georgia Power's values line** quotes P-131, a whole-plan statement about keeping the real-time rate's design, not
   the marginal-cost line the utility advocate proposed (C8). The value "keeping the real-time rate's design intact" is
   faithful to the quote but plainer than the testimony's "marginal cost-based rate" reasoning.
2. **P-111's "concerns"**: the brief described them as GAM's own; the passage says "PIA Staff and intervenors'
   concerns", so the page says that.
3. **The "If you" lines lost two points to the 45-word budget** (item 19, C6). The home line no longer says that coming
   out ahead depends on the next base rate case; the money steps right below it do.
4. **Item 11**: Plans B and D still say new pipeline fees "would be recovered from" large-load customers, the passages'
   own words (P-055, P-096); only cards 3 and 5 changed, as the brief listed. A reader could still take "recovered from"
   as a new charge.
5. **"Customers on standard rates."** now stands as its own cited sentence in Plans B–D's "Who benefits"; a checker may
   read a one-phrase sentence as thinly supported.
6. **The 30-second version is exactly 90 words**, at the budget, after "Dates can change." was dropped.

## Targeted re-check 1 (revision 7.1, `verification/round7-targeted/`): results

Two fresh checks of revision 7.1, run by the coordinator. Their inputs are kept in `verification/round7-targeted/`
(`workspace.r7.1.md`, `home.r7.1.md`, `page.r7.1.yaml` and `changed-units.r7.1.txt`, the list the checkers used).

| Check | Result |
|---|---|
| Source re-check of the changed units (`source-check.json`) | 150 units (137 page, 1 home, 12 `page.yaml`): **112 supports, 9 partial, 0 does not support, 0 wrong span**; 26 tags valid, 3 questionable; 0 ungrounded spans. 23 framing notes (4 medium, 19 low) |
| Flip test of the title, values guide, "If you" lines and "In short" lines (`fliptest.json`) | Neutrality 3/5: **1 high** (the values guide gave GIPL a value every party claims, "protecting customers on standard rates"), 4 medium, 3 low. The title and the four "If you" lines read the same from either side, apart from "your" on the fuel-charge half only |

Nothing misstates a passage badly enough to be "does not support", but six partials share one cause: "base rates could
rise later" didn't cite P-016, the source of "later". The high flip-test item and the partials are fixed in revision 7.2.

### Revision 7.2: what changed

Change class **correction**; reason: "fixes from the first targeted re-check". **No card claim, evidence label or side
changed**; the nine label texts are identical to revision 7.1's (and revision 7's). Codes: FT = flip test, SC = source
re-check; line numbers are revision 7.2's. The changed units are listed in the coordinator's scratch folder as
`t002-rev72/changed-units.txt` (38 changed or new units on 28 lines, 34 removed or replaced, 3 `page.yaml` strings; each
marked as a values-guide, "If you", "In short" or other unit).

| # | Finding (who raised it) | Change in revision 7.2 | Lines |
|---|---|---|---|
| 1 | GIPL's label gave one side a value every party claims (FT L165, **high**; FT L167, medium; SC framing L167) | GIPL's label is now in its own terms: "keeping costs from shifting to fuel-charge payers". Georgia Power's separate "also says it is committed …" sentence is gone. One neutral note follows the four lines: "Every party whose testimony we could read says its approach protects customers on standard rates; the parties differ on how", tagged with each party's words: Georgia Power "protect existing customers" (P-054) and "customer protection" (P-127); PIA Staff "non-RTP customers absorbing all these costs" (P-063); GIPL "shifts costs to FCR customers" (P-099); GAM "downward price pressure on non-RTP rates" (P-111) and lower costs left for others (P-112) | 165, 167 |
| 2 | "Plan A has no line" understated GAM (SC L167 questionable; SC framing L167) | "No party whose testimony we could read asks to keep today's method, Plan A, in every part. GAM's view comes closest: GAM opposes crediting real-time money against pipeline fees [P-110]. GAM accepts either of Georgia Power's proposed changes only if the PSC finds a change is needed [P-111]." GAM's values line now names the same relation: "GAM's witness, whose view comes closest to Plan A, said …" | 166, 168 |
| 3 | Card 5 gave only the new-fees half of GIPL's plan (FT L281; SC L281 partial) | "Fuel charges would likely fall and exclude new large-load pipeline fees; base rates could rise later" [P-096, P-102, P-144, P-061, P-115, P-016], which includes the direct pipeline share (P-096 item 6, P-102). P-105 is no longer cited for it. The claim sentence lost one word ("so warrant the same split") to stay at 40 words. The label is unchanged; FT's label point is open item O13 | 282 |
| 4 | Card 8's effect was Georgia Power's disputed claim (FT L317) | "Fuel charges keep paying most left-out costs; real-time money keeps helping cover base costs. [P-144, P-063, P-065, P-061, P-124] Georgia Power says its real-time rate isn't meant to cover all embedded costs, but users pay their share. [P-121]" | 318 |
| 5 | Card 9's benefit was attributed while the change cards' are in our voice (FT L329) | "Limiting the shift would mean smaller fuel-charge cuts and less chance of base rates rising later." [P-144, P-061, P-111, P-115, P-016]; the claim sentence, "GAM says real-time money now lowers other customers' base costs", is unchanged | 330 |
| 6 | Even wording (FT L14, L166, L293; SC framing L166, L293) | "your base rates" on lines 14 and 15; GAM "says" in the Layer 3 intro (and the values line no longer uses "warns"); card 6 attributes its base-side point: "Fuel charges keep paying pipeline fees; GAM says base costs stay lower" [P-083, P-144, P-111, P-112] | 14, 15, 159, 166, 294 |
| 7 | "Base rates could rise later" lacked P-016 for "later" (SC L9, L233, L245, L257, L269, L305 partial) | P-016 added to each (lines 9, 234, 246, 258, 270, 306), and to cards 5 and 9 | 9, 234, 246, 258, 270, 282, 306, 330 |
| 8 | "A group of congregations" needed P-093 (SC L79 partial) | P-093 added | 79 |
| 9 | "PSC staff's plan" can read as the regulator's own (SC page.yaml L14 partial, medium framing) | Chip "PSC advocacy staff's plan" [P-006, P-059, P-073]; the Layer 3 intro calls PIA Staff "the PSC's advocacy staff" (P-006, P-059), which also brings the intro to 60 words | `page.yaml`; 159 |
| 10 | The real-time line was vague and unscoped (SC L16 questionable; SC framing L16) | "Baseline power gets standard-rate changes, but some new large-load customers have none. [P-043, P-048, P-071] As of Nov 8, 2026, no filing we read gives your baseline's net or timing." "The lines above" is gone | 16 |
| 11 | "The earliest possible cutoff" ignored the Dec 10 committee meeting (SC L49 questionable; SC framing L49, medium) | "Dec 14 at 9:30 a.m. is 24 hours before the Dec 15 session starts; the time this case comes up isn't posted as of Nov 8, 2026." Then: "The Energy Committee takes up the case on Dec 10, and no PSC page we found says whether that counts as a Commission vote, so ask the PSC whether an earlier deadline applies." No earliest cutoff is stated | 49 |
| 12 | Low notes (SC framing) | Line 155: "Nov 8, 2026". Line 182: "which would likely lower the fuel charge". Line 176: GIPL's qualifier kept, "from an economics perspective". Line 189: Georgia Power's point that choice 2 "would depart from the marginal-cost approach in place since 2010" (P-129) now precedes PIA Staff's "2010 rate case proposal" reply. Line 214: "two take-or-pay choices, 1 and 2". Line 248: "options", not "fixes". Line 252: "take-or-pay choices … choice 2". Lines 14 and 15: "net (both changes together)". `link_text`: "… and your power bill" [P-016, P-044, P-061, P-144]. S10 label: "Resource Supply Management" | 14, 15, 155, 176, 182, 189, 214, 248, 252; `page.yaml` |
| 13 | The home comment item gave "Dec 14, 2026, 9:30 a.m." with no caveat, unlike line 49 (coordinator, after item 11) | Home line 5 now gives the rule, 24 hours before a vote (P-030), says this time is for the Dec 15 vote, and asks readers to check with the PSC whether its Dec 10 committee meeting (P-012) makes the deadline earlier; 24 words before the action; label date unchanged | home 5 |

To fit the 45-word "If you" budget with "your" and the definition of "net", lines 14 and 15 no longer say "or the
fuel-charge date" (the "Decided now, and decided later" list still says the PSC sets that date). To keep Layer 3 at grade
8.5 or below, the new GAM note is two sentences, and Georgia Power's P-129 point is a paraphrase, not a quote.

### Open items (targeted re-check 1, not changed in revision 7.2)

| # | Open item (who raised it) | Note |
|---|---|---|
| O13 | Card 5's label: add a "Supported" part for the premise it shares with card 3, and name what is mixed (FT L281; also round 7's M IM1, C1) | Label change; needs a full round |
| O14 | Card 1's "In short": GIPL's clause gives the principle without its application, "plants" narrows GIPL's "resources", and "it" is vague (FT L233; SC framing L233) | Next revision; budget is 40 words |
| O15 | Georgia Power's values label, "the real-time rate's design", is an insider value and unexplained next to the real-time line (FT L163; SC framing L163) | See C10 |
| O16 | Lines 14 and 15 could add that the order records the base-side change for the next base rate case (P-016) (FT L14, optional) | No room in 45 words |
| O17 | EMC line: "no filing we read gives any effect on your bill" hints an effect exists (SC framing L17) | Next revision |
| O18 | Line 35: the 2026 figure is a projection that readers may take as money on 2026 bills (SC framing L35) | Next revision |
| O19 | Line 159: "Each plan" could be read to include Plan A, though the paragraph names Plans B, C and D first (SC framing L159) | Next revision |
| O20 | Line 171 repeats "choices" ("two other choices …, take-or-pay choices 1 and 2"), as line 214 did (SC framing L213) | Next revision |
| O21 | Home item (Dec 14, 9:30 a.m.) has no Dec 10 caveat like line 49's (related to item 11) | Next revision; home items weren't in the brief |

### Contradicting suggestions (targeted re-check 1)

| # | Point | Views, side by side | Choice now |
|---|---|---|---|
| C10 | GAM's values line | FT: "who favors limiting the shift to fuel" (card 9's side). SC: "who says a bigger fuel credit would raise pressure on base rates" | "whose view comes closest to Plan A", matching the new Plan A note (item 2); neither wording used |
| C11 | Base-side wording in cards 6, 8 and 9 | FT: one neutral wording in all three ("real-time money keeps helping cover base costs"). SC (L293): attribute card 6's point to GAM. The brief: card 6 attributes, cards 8 and 9 in our voice | As the brief: card 6 "GAM says base costs stay lower"; card 8 "real-time money keeps helping cover base costs"; card 9 "less chance of base rates rising later" |

### Step 5 checklist (revision 7.2)

- [x] **Every sentence is cited or tagged.** `check_citations.py` on `workspace.md`: PASS, 545 units (410 cited, 58 analysis,
      66 editorial, 11 computed), 0 errors, 0 warnings. `home.md` (line 5 changed, item 13): PASS, 8 units (7 cited, 1 computed), 0 errors, 0 warnings.
- [x] **Passages unchanged**: 166, all cited; `verify_hashes.py`: PASS.
- [x] **Reading grade** (`readability.py`): 30-second 7.3 (90 words), What it means for you 7.4, Layer 1 7.4, Layer 2 8.4,
      Layer 3 8.5 (8.46; target ≤ 8.5). Not targeted: Layer 4 9.9. No budget WARN lines, no CHECK lines.
- [x] **Test render**: no warnings; the page shows "version 7.2".
- [x] **Scope held**: the nine evidence labels are identical to revision 7.1's; no card claim or side changed.
- [ ] **Second targeted re-check**: pending (run by the coordinator).

### Where I'm least sure (revision 7.2)

1. **The all-party note** says every party "says its approach protects customers on standard rates". Georgia Power and
   GAM say so nearly in those words; PIA Staff and GIPL say it by objecting to costs that customers on standard rates
   absorb (P-063, P-099). The note is tagged as analysis for that reason.
2. **"GAM's view comes closest to Plan A"** is our reading of P-110, P-111 and P-115, now in two places (the values line
   and the note).
3. **Card 6's "GAM says base costs stay lower"** is a compressed paraphrase of P-111 and P-112, shortened to fit 40 words.
4. **Line 16's "your baseline's net or timing"** and **lines 14–15's "net (both changes together)"** are terse; the
   45-word budget leaves little room.
5. **Layer 3 is at 8.46**, just under the 8.5 target.

## Targeted re-check 2 (revision 7.2, `verification/round7-targeted-2/`): results

The coordinator's fresh checks of revision 7.2's changed units. Revision 7.2's page, home items and display settings are kept
beside the reports (`workspace.r7.2.md`, `home.r7.2.md`, `page.r7.2.yaml`), with the unit list the checkers used
(`changed-units.txt`).

| Check | Result |
|---|---|
| Source re-check (`source-check.json`) | 42 units (38 page, 2 home, 3 `page.yaml`, less the skipped status line): **29 supports, 3 partial, 0 does not support, 0 wrong span**; 10 tags valid, **0 questionable**; 14 framing notes (3 medium, 11 low) |
| Flip test (`fliptest.json`) | Neutrality **4/5**, no high issue; 2 medium (GIPL's values label named a value PIA Staff states almost word for word; card 6 alone credited its base-side effect to the side), 6 low |

The three partials were wordings stronger or broader than their passages: GAM "accepts … only if" (P-111 states a condition,
not an acceptance), card 5's split extended to all pipeline fees (P-104 covers new fees, until the next rate case), and card
8's "users pay their share" (P-121 is a double negative). Revision 7.3 fixes them.

### Revision 7.3: what changed

Change class **correction**; reason: "fixes from the second targeted re-check". **No card claim, evidence label or side
changed**; the nine label texts are identical to revision 7.2's. Codes: SC = source re-check, FT = flip test; line numbers are
revision 7.3's. The changed units are in the coordinator's scratch folder as `t002-rev73/changed-units.txt` (24 changed or
new units on 15 lines, 20 removed or replaced, 2 home units, 3 `page.yaml` strings; each unit marked by type).

| # | Finding (who raised it) | Change in revision 7.3 | Lines |
|---|---|---|---|
| 1 | "GAM accepts … only if" reversed P-111's condition (SC L168 partial; SC framing L168; FT L168) | P-111 in the page's standard shape: "GAM says that if the PSC finds a change is needed, either take-or-pay choice would "adequately address" the concerns of PIA Staff and intervenors." P-110 restated with "specific", as lines 173 and 261 do. "GAM's view comes closest to Plan A" is now its own tagged sentence | 168 |
| 2 | Card 5's split read as covering all pipeline fees, with no time limit (SC L282 partial; SC framing L282) | "… so new fees warrant the same split until the next rate case" (P-104, P-096). To stay at 40 words, the effect is now card 4's "Fuel charges would likely fall; base rates could rise later" (same citations), and the evidence restatement is "Mixed on two points" | 282 |
| 3 | Card 8's "but users pay their share" turned P-121's double negative into a page-voice claim (SC L318 partial; SC framing L318) | "Georgia Power says skipping some embedded costs by design doesn't mean real-time customers don't pay their fair share." [P-121] | 318 |
| 4 | A shared value given to one side in the values guide (FT L165 medium, L164) | The cost-shift concern is named once, for both: "PIA Staff and GIPL both say today's credit shifts costs to fuel-charge payers" [P-063, P-099]. PIA Staff's line now carries a value only PIA Staff states, sharing fuel costs "based on energy usage" (P-062, its third guideline). GIPL's carries one only GIPL states, a working copy of Georgia Power's cost model for intervenors that would allow "full stakeholder participation" (P-096). Each line is 29–31 words | 164, 165, 167 |
| 5 | Card 6 alone credited its base-side effect to the side (FT L294 medium) | "Fuel charges keep paying pipeline fees; base costs could stay lower." [P-083, P-144, P-061, P-111, P-112]; each passage checked (P-083 fuel pays all pipeline fees; P-061 the rest goes to base; P-111 and P-112 that base-side money lowers others' costs) | 294 |
| 6 | The options intro gave only GAM's base-rate warning (SC framing L159, medium) | "Each plan would raise the fuel credit, the real-time pricing (RTP) money counted toward fuel, and likely lower the fuel charge." [P-058, P-115, P-144]. To stay at 60 words: "Plans B to D", and GAM's sentence reads "says base rates, the rest of the bill, would then see upward pressure" (P-115) | 159 |
| 7 | "PSC advocacy staff's plan" could still read as the regulator's own (SC framing page.yaml L14, medium) | Chip "PIA Staff's plan" [P-006, P-059, P-073], matching the heading; PIA Staff is defined on the Options tab | `page.yaml` |
| 8 | Low notes (SC framing) | Line 49: the 24-hour sum is `{computed}`, the Energy Committee clause is its own sentence cited to P-012, and "the committee meeting" replaces "that". Line 176: no comma inside the quoted "from an economics perspective". Line 189: P-129's "marginal fuel cost-based approach" quoted. Lines 14 and 15: tags no longer mention the removed fuel-date clause. Line 16: "have no baseline". Home line 5 names the case ("its Dec 15 real-time payments vote") in 25 words, so it now says "the Commission" instead of introducing "PSC". `link_text`: "… and your Georgia Power bill". S10 label gains a citation comment, `# [P-162]` | 14, 15, 16, 49, 176, 189; home 5; `page.yaml` |

Layer 3's reading grade reached 8.54 with the new values-guide quote; quoting only "full stakeholder participation" and
splitting the GAM note into shorter sentences brought it to 8.46.

### Open items (targeted re-check 2, not changed in revision 7.3)

| # | Open item (who raised it) | Note |
|---|---|---|
| O22 | Georgia Power's values line uses "asked the PSC to" while the others use "said", and its value is about a rate's structure rather than an outcome for customers (FT L163) | Next revision; see C10 and O15 |
| O23 | Card 1's "In short" gives two premises but not their conclusion, while card 2 gives one (FT L234) | Next revision; 40-word budget |
| O24 | Card 4's "In short" drops PIA Staff's hedges on double recovery ("possibility", "risk", "a portion"; P-083, P-084) (FT L270) | Next revision |
| O25 | Card 5's evidence restatement doesn't name the two mixed points (FT L282) | No room in 40 words; see O13 |
| O26 | Cards 6, 8 and 9 open with their side's cost because every card puts the fuel effect first (FT L330) | Order rule; next revision |
| O27 | "PIA Staff, the PSC's advocacy staff" in the options intro could read as speaking for the commission (SC framing L159, low) | Next revision; the 60-word budget |
| O28 | "Says … protects" in the all-party note uses Georgia Power's word for every party; "argues its approach is better for" would be even (SC framing L167) | Next revision |
| O29 | Line 79: "that case" and "the method" point back several sentences, and "raised … raised" repeats (SC framing L79) | Next revision |
| O30 | Line 16: "large-load" isn't defined on the Summary tab, and "your baseline's net" is jargon (SC framing L16) | Next revision |
| O31 | The S10 label is three words against `page.yaml`'s "one or two words" note (SC framing page.yaml L28) | As the 7.1 brief asked (party names) |

### Step 5 checklist (revision 7.3)

- [x] **Every sentence is cited or tagged.** `check_citations.py` on `workspace.md`: PASS, 549 units (412 cited, 59 analysis,
      66 editorial, 12 computed), 0 errors, 0 warnings. `home.md`: PASS, 8 units (7 cited, 1 computed), 0 errors, 0 warnings.
- [x] **Passages unchanged**: 166, all cited; `verify_hashes.py`: PASS.
- [x] **Reading grade** (`readability.py`): 30-second 7.3 (90 words), What it means for you 7.4, Layer 1 7.3, Layer 2 8.4,
      Layer 3 8.5 (8.46; target ≤ 8.5). Not targeted: Layer 4 9.9. No budget WARN lines, no CHECK lines.
- [x] **Test render**: no warnings; the page shows "version 7.3".
- [x] **Scope held**: the nine evidence labels are identical to revision 7.2's; no card claim or side changed.
- [ ] **Third targeted re-check**: pending (run by the coordinator).

## Targeted re-check 3 (revision 7.3, `verification/round7-targeted-3/`): results

The coordinator's fresh checks of revision 7.3's changed units. Revision 7.3's page, home items and display settings are kept
beside the reports (`workspace.r7.3.md`, `home.r7.3.md`, `page.r7.3.yaml`), with the unit list the checkers used.

| Check | Result |
|---|---|
| Source re-check (`source-check.json`) | 28 units (24 page, 2 home, 3 `page.yaml`, less the skipped status line): **18 supports, 2 partial, 0 does not support, 0 wrong span**; 8 tags valid, 0 questionable; 14 framing notes (3 medium, 11 low) |
| Flip test (`fliptest.json`) | Neutrality **4/5**, no high issue; 2 medium (the values note gave only PIA Staff's and GIPL's shared claim; card 8's claim read as a defensive double negative built on "skipping"), 5 low |

The two partials: GIPL's new values line dropped the conditions on its cost-model request (P-096), and "either take-or-pay
choice" narrowed P-111's "either of GPC's proposed changes" (also on five unchanged lines).

### Revision 7.4: what changed

Change class **correction**; reason: "fixes from the third targeted re-check". This is the last fix pass this cycle. **No card
claim, evidence label or side changed**; the nine label texts are identical to revision 7.3's. Codes as before; line numbers
are revision 7.4's. The changed units are in the coordinator's scratch folder as `t002-rev74/changed-units.txt` (18 changed or
new units on 15 lines, 17 removed or replaced, 1 `page.yaml` string; `home.md` unchanged).

| # | Finding (who raised it) | Change in revision 7.4 | Lines |
|---|---|---|---|
| 1a | GIPL's values line was a conditional, procedural request with its conditions dropped (SC L165 partial and medium; FT L165) | GIPL's substantive value in its own words: "If you care most about large users not shifting costs to others: GIPL's witness, who backs Plan D, said the PSC has held large loads "to a higher standard when it comes to cost shifting."" [P-152, P-097] (35 words; quote 10 words, verbatim from P-097's "holding them to a higher standard when it comes to cost shifting") | 165 |
| 1b | The note gave only one pair's shared claim (FT L167 medium; SC framing L167 medium) | "PIA Staff and GIPL both say today's credit shifts costs to fuel-charge payers; Georgia Power and GAM both say real-time money on the base side lowers other customers' base costs." [P-063, P-099, P-124, P-111, P-112], each passage checked. GAM's line keeps "keeping base rates down" | 167 |
| 2 | "Either take-or-pay choice" narrowed P-111 (SC L168 partial; also L173, L181, L189, L197, L331) | All six now say "either of Georgia Power's proposed changes would "adequately address" the concerns of PIA Staff and intervenors" (P-111's own scope) | 168, 173, 181, 189, 197, 331 |
| 3 | Card 8's claim used "skipping" and a double negative (FT L318 medium; SC framing L318 medium) | "Fuel charges keep paying most left-out costs; base rates could stay lower. [P-144, P-063, P-065, P-061, P-124] Georgia Power says the real-time rate is "not intended to recover all embedded costs" but helps cover fixed costs. [P-121, P-052, P-124]" (39 words, with the evidence restatement unchanged) | 318 |
| 4 | The options intro gave the base-side effect only as GAM's warning (SC framing L159) | "Base rates, the rest of the bill, could rise later" in our voice [P-061, P-115, P-016], as lines 9 and 24 do. GAM's warning is dropped from the intro (its values line carries it), and GAM is introduced as "a manufacturers' group, also filed testimony" [P-109, P-153] (60 words) | 159 |
| 5 | Low items (SC and FT framing) | Card 6: "base rates could stay lower". Card 5: "Evidence: Mixed on group effects, peaks", with "so new fees warrant that split" to stay at 40 words. Lines 14 and 15: "net (both changes together) or its timing", as lines 16 and 17 have; to fit 45 words, both lines drop Georgia Power's "base rates are frozen through 2028" sentence, which gave timing for the cost side only (FT L14); the freeze is still in "Decided now, and decided later". Plan D chip: "GIPL's plan" [P-093, P-095] | 14, 15, 282, 294; `page.yaml` |

### Open items (targeted re-check 3, not changed in revision 7.4)

| # | Open item (who raised it) | Note |
|---|---|---|
| O32 | Line 159: "Each plan" could be read to include Plan A, and the appositive reads like a list (SC framing L159) | Next revision; see O19 |
| O33 | The business "If you" line leaves out GAM's group comparison (P-116) (SC framing L15) | See C6 |
| O34 | Line 16: "baseline" isn't explained before it is used, and "standard-rate changes" can read as rate-design changes (SC framing L16) | Next revision; see O30 |
| O35 | Line 49 runs nine sentences and uses "Energy Committee" before line 72 explains it (SC framing L49) | Next revision |
| O36 | Home line 5: "real-time payments vote" is vague, and its computed tag makes the Commission the one crediting (SC framing home L5) | Next revision |
| O37 | Chips and labels: "PIA" may be opaque on a chip; S10 is three words and names the party, not the source type (SC framing `page.yaml` L14, L28) | Next revision |
| O38 | "Net" as a noun is hard at grade 6–8, on lines 14–16 (SC framing L14) | Next revision |
| O39 | Card 5: nothing in the line shows why it says "GIPL's testimony says" (FT L282) | Next revision |

### Step 5 checklist (revision 7.4)

- [x] **Every sentence is cited or tagged.** `check_citations.py` on `workspace.md`: PASS, 548 units (411 cited, 59 analysis,
      66 editorial, 12 computed), 0 errors, 0 warnings. `home.md`: PASS, 8 units (7 cited, 1 computed), 0 errors, 0 warnings.
- [x] **Passages unchanged**: 166, all cited; `verify_hashes.py`: PASS.
- [x] **Reading grade** (`readability.py`): 30-second 7.3 (90 words), What it means for you 7.6, Layer 1 7.3, Layer 2 8.4,
      Layer 3 8.4 (target ≤ 8.5). Not targeted: Layer 4 9.9. No budget WARN lines, no CHECK lines.
- [x] **Test render**: no warnings; the page shows "version 7.4".
- [x] **Scope held**: the nine evidence labels are identical to revision 7.3's; no card claim or side changed.
