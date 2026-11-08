# Trial 002: Decision workspace #2, PSC Docket 57171 (how Real-Time Pricing revenue is credited to fuel costs)

| | |
|---|---|
| **Process** | A. Decision workspace, adapted from the bill runbook ([runbook v0.3](../../docs/process/runbooks/bill-workspace.md)) to a regulatory case. Every adaptation is in [log.md](log.md) |
| **Decision** | Georgia Public Service Commission (PSC) Docket 57171, "RTP Revenue Credit and Allocation Methodology": how Georgia Power splits money from customers on Real-Time Pricing (RTP), including data centers, between the fuel account that standard-rate customers' fuel charge pays and the base account. Final decision scheduled for 2026-12-15; the case must end by 2026-12-31 |
| **Done by** | AI agent (Claude Code), by hand, 2026-11-08 |
| **Verified by** | Not yet. The deterministic gate passes (see [verification.md](verification.md)). The [verification protocol](../../docs/process/verification-protocol.md) (blind source check, bias panel, planted-error calibration) is to be run by independent checkers, as the brief directs |
| **Owner's role** | Feedback on the user experience ([review.md](review.md)) |
| **Status** | **Drafted, revision 1 (2026-11-08).** Gate: `check_citations.py` PASS (269 units, 0 errors, 0 warnings); `readability.py` 30-second 6.2, Layer 1 5.6, Layer 2 6.2, Layer 3 8.0; `verify_hashes.py` PASS (65 files, 35 retrieved sources). Independent checks not run. The record keeps moving: see "How to resume" |

## Why this decision (step 0)

Scored with the [topic-selection rubric](../../docs/policies/topic-selection.md) as of 2026-11-08. The scout
scored it 13; my score is 12 because the evidence is thinner than it first looks (key figures are blacked out).

| Criterion | Score | Reason |
|---|---|---|
| Everyday impact | 2 | Touches the fuel charge on every standard-rate Georgia Power bill, but the parties' per-bill estimates are about a dollar or two a month (Staff's $1.35, P-066/P-067; Georgia Power's $1.48 and $1.65 per $100 million, P-142/P-143) |
| Input still possible | 3 | Public-witness hearing 2026-11-12; written comments until 24 hours before the 2026-12-15 vote; committee meeting 2026-12-10 |
| Evidence available | 2 | Five parties' sworn testimony and the company's own workpapers, but the public copies black out Georgia Power's Option 1 and Option 2 estimates, most of Staff's table columns and GAM's dollar figures; no hearing transcript is posted; two filings are scans with no text layer |
| Public confusion / attention | 2 | News coverage of data-center costs; the mechanism (fuel versus base credit) is technical and easy to misread as "data centers don't pay fuel costs" |
| Likely to move | 3 | Fixed decision date set by order; the fuel-case order requires the case to end by 2026-12-31 |
| **Total** | **12 / 15** | |

**Why this one, among the scout's list:** it stresses the process in ways HB 1009 (Trial 001) did not: a contested
regulatory case with sworn testimony instead of a bill; more than two sides with crossing alliances; redacted figures;
a dense money topic that must read at grade 6–8; and a live record that changes before the vote. The scout's other
picks: the Fulton County 2027 budget (Trial 003) and school-board device policies (same subject as HB 1009).

**This is a training and evaluation case.** It is not 2027 pilot content.

## Discrepancies, settled from primary records

| # | Question | Answer | Source |
|---|---|---|---|
| D1 | When is the decision, and the deadline? | Final decision "on December 15, 2026 at the regularly scheduled Administrative Session"; the calendar lists that session at 9:30 a.m. The fuel-case order requires the case to be "concluded by December 31, 2026" | S02 (P-013), S04 (P-024), S21 (P-016) |
| D2 | When does the 2026-11-12 hearing start? | The order: "Beginning after the regularly scheduled Committee Meetings". The calendar lists the committees at 9:30 a.m. and the hearing at 9:35 a.m.; the hearing's own calendar entry shows no time or place. Both are consistent; the page gives both | S02 (P-010), S04 (P-023), S03 |
| D3 | Which meeting on 2026-12-10? | The order says the "scheduled Energy Committee Meeting, which begins at 9:30 a.m.", in the hearing room. The calendar lists the combined "Facilities Protection/Telecommunication/Energy And Administrative Affairs Committees" at 9:30 a.m. Same slot | S02 (P-012), S04 (P-024) |
| D4 | Where does the case come from? | Brief: "a 2026-05-28 stipulation". Primary: PIA Staff and Georgia Power filed the stipulation 2026-05-12 (GAM joined 5/14, RSM 5/27, MARTA 5/28); the PSC adopted it by short order on 2026-05-28, whose ordering paragraph 6 creates this review; a full order followed 2026-08-04. A news story's "May 11" is unsupported | S21 (P-016, P-017, P-018), S20, S21b, S24 |
| D5 | Staff's "$329 million" | **Corrected.** Staff's First Errata (2026-10-07) changes p.7 line 9 from "$329 million" to "$328 million" and p.20 line 15 from "$93 million" to "$92 million"; Table 2's total becomes $328,278,331 (was $328,900,459); Table 4's 2024 deltas change; Tables 3 and 7 change only in blacked-out cells. Not changed: the $322 million fuel shortfall, the $1.35 a month, and Tables 5 and 6. The errata sheet is a scan with no text layer, so these values were **read by eye** from rendered pages and could not become passages. The page therefore cites only figures the errata did not change and flags the corrections (P-039) | S07 (P-066), S07b (P-039; scanned sheet read by eye) |
| D6 | Where are the meetings? | 244 Washington Street SW, Atlanta. The legal notice put the 2026-09-01 hearing in Room 110; administrative sessions are in hearing room 110 and open to the public; the 2026-12-10 meeting is in "the Commission’s hearing room". No page gives a room for 2026-11-12, so the page tells readers to call the number in the legal notice to confirm | S05 (P-021), S19 (P-032), S02 (P-012) |
| D7 | Who backs which option? | Confirmed: Georgia Power and GIPL back Option 1; PIA Staff backs Option 2; GAM accepts either. Georgia Power's direct testimony offered both without a written preference; Staff's table records "Option 1 preferred" from the 2026-09-01 hearing; the rebuttal states it | S11 (P-119, P-128), S07 (P-065, P-073), S09 (P-111), S08 (P-095) |
| D8 | Does a membership change affect who votes on 2026-12-15? | The July order's letterhead lists Tricia Pridemore; Staff's 2026-10-07 letter lists Stan Wise. Wise's PSC page: commissioner since 2026-09-05, appointed by the governor to finish Pridemore's term, serves through 2026-12-31. All five commissioners' PSC pages give terms through 2026-12-31 or later, so the same five are scheduled to vote. Election results were not examined: not needed, and the Fortune page was blocked to the scout | S02 (P-015), S07b (P-040), S17–S17e (P-033 to P-038) |
| D9 | When did legal notice run? | First published 2026-08-24 (confirmed). The order says Georgia Power "will be directed to give first notice ... not later than August 7, 2026" and, elsewhere, "no later than 30 days after filing its application"; 2026-08-24 is 10 days after the 2026-08-14 testimony. We found no filing that explains the August 7 date. Not used on the page | S05 (P-020), S02 (order text, not a passage) |
| D10 | Written-comment deadline? | "Comments must be submitted 24 hours before a Commission vote." The vote session starts at 9:30 a.m. on 2026-12-15, so the safe deadline is 9:30 a.m. on 2026-12-14 `{computed}`. The time the item itself comes up is not posted | S15 (P-030), S04 (P-024) |
| D11 | Can the public speak at committee meetings ("last 15 minutes")? | **Still [unverified].** Not on the public witness, public comments or FAQ pages. The page says we found no PSC page on it | S14, S15, S19 |
| D12 | Was Georgia Power's motion to strike decided? | Unknown. Filed 2026-10-08; GIPL opposed 2026-10-14. No order appears in the filing list as of 2026-11-08, and the 2026-10-15 transcript is listed without a file | S12 (P-134, P-158), S13 (P-136), S01b (P-003) |
| D13 | What does RSM's testimony say? | Filed 2026-09-22 per the docket (its cover says "October 15, 2026"). A 5-page scan with no text layer, read by eye only: it argues that marginal-cost (real-time) pricing should be open to existing customers' loads, not just new ones, and takes no position on Option 1 or Option 2. Not cited | S01b (P-002), S10 |
| D14 | Time of the 2026-10-15 hearing | The legal notice said "October 15, 2026, at 9:30 a.m."; the order said after the committee meetings; the calendar listed 9:35 a.m. Past event; no effect on the page | S05 (P-021), S02 (P-009), S04 |

## Dates on the page, and when we saw them

All seen on **2026-11-08** (snapshots 07:00–07:09 UTC). Dates and times can change; re-check before each one.

| Date | What | Source |
|---|---|---|
| 2026-11-12 | Hearing on Georgia Power's rebuttal, after the 9:30 a.m. committee meetings (calendar: 9:35 a.m.) | S02 P-010; S04 P-023 |
| 2026-12-02, 4:00 p.m. | Briefs and proposed orders due | S02 P-011 |
| 2026-12-10, 9:30 a.m. | Energy Committee considers the case, in the hearing room | S02 P-012; S04 P-024 |
| 2026-12-14, 9:30 a.m. | Safe deadline for written comments `{computed}` | S15 P-030; S04 P-024 |
| 2026-12-15, 9:30 a.m. | Final decision at the Administrative Session | S02 P-013; S04 P-024 |
| 2026-12-31 | Case must be concluded | S21 P-016 |

## How to resume (the record keeps moving)

1. Run the independent [verification protocol](../../docs/process/verification-protocol.md) on revision 1 (not done here, by instruction).
2. Before 2026-11-12: re-snapshot S03, S04 and S04b for a time, room or agenda. After it: look for the transcript.
3. After 2026-12-02: snapshot the briefs and proposed orders (new sources). They will be the parties' final positions; write
   revision 2 with a change class and reason (append-only; [ADR 0006](../../docs/adr/0006-append-only-revisions.md)).
4. Before 2026-12-10 and 2026-12-15: re-check S04c and S04d for agendas. After 2026-12-15: snapshot the order and publish a
   "What happened" revision.
5. If an OCR tool is approved (see log.md), turn the scanned errata (S07b) and RSM testimony (S10) into passages and
   replace the read-by-eye notes in D5 and D13.

## Files

| File | What it is |
|---|---|
| [sources.yaml](sources.yaml) | 35 retrieved sources (65 hashed files; raw files in `_raw/`, git-ignored), plus not-retrieved and rejected leads |
| [passages.spec.yaml](passages.spec.yaml) → [passages.md](passages.md) | 160 hand-chosen passages, copied mechanically; every one is cited on the page |
| [workspace.md](workspace.md) | The page: 30-second version + Layers 1–5. Every sentence cited or labeled |
| [home.md](home.md) | Home-page items built from this trial |
| [page.yaml](page.yaml) | Display settings for the prototype |
| [extract/](extract/) | The four small extractors that turned `.docx`, PDF and `.xlsx` files into the text the passages are copied from (stdlib, or pdfplumber for PDFs); they regenerate the derived text byte for byte. Candidates for `trials/tools/` |
| [verification.md](verification.md) | Step 5 deterministic checklist and "where I'm least sure" (no independent checks yet) |
| [log.md](log.md) | Step-by-step log with times, friction, every place the runbook didn't fit, and tool change requests |
| [review.md](review.md) | The owner's feedback form on the experience |
