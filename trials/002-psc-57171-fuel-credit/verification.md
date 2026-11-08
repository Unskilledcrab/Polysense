# Verification: Trial 002 (PSC Docket 57171)

Method: the AI-only [verification protocol](../../docs/process/verification-protocol.md). There is no
human in the loop ([ADR 0014](../../docs/adr/0014-ai-only-verification.md)). The checkers were independent
agents run by the coordinator; the drafter ran only the deterministic gate. Copies of round 1's inputs and
raw reports are in [verification/round1/](verification/round1/).

**Status: not verified.** Round 1 (revision 1) failed the bias panel. Revision 2 answers every round 1
finding below and passes the deterministic gate; it has not been through round 2.

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
