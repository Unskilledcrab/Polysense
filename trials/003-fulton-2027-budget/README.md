# Trial 003: Decision workspace, Fulton County's 2027 budget

| | |
|---|---|
| **Process** | A. Bill workspace ([runbook v0.3](../../docs/process/runbooks/bill-workspace.md)), adapted for a county budget. Every adaptation is in [log.md](log.md) |
| **Decision** | The Fulton County Board of Commissioners adopts the county's budget for calendar year 2027. A re-created four-member Budget Commission must submit the proposal "by no later than November 15" (2026-11-15, a Sunday). The Board must adopt "no later than the second regular meeting in January" (2027-01-20 on the proposed 2027 schedule). Sources: S01 (P-005), S07b (P-041) |
| **Kind** | 6, county decision (an upcoming decision; residents can still take part) |
| **Done by** | AI agent (Claude Code), by hand, 2026-11-08 |
| **Verified by** | Not yet. Round 1 of the [verification protocol](../../docs/process/verification-protocol.md) ran on revision 1 (independent checkers run by the coordinating session; no human in the loop, [ADR 0014](../../docs/adr/0014-ai-only-verification.md)): the blind source check found no wrong facts, calibration caught 12/12 planted errors, and **the bias panel failed** (3/5, 3/5, editor 3/5). Revision 2 answers every finding ([verification.md](verification.md)); the next full round has not run |
| **Owner's role** | Feedback on the user experience ([review.md](review.md)) |
| **Status** | **Revision 2, drafted after round 1; not verified.** The key document, the proposed 2027 budget, **did not exist yet** on 2026-11-08. The page says so, and must change through append-only revisions as the proposal, hearing notice and vote arrive (see "How to update"). Revision 1 is kept at `verification/round1/workspace.r1.md` |

## Why this decision (step 0)

Scored with the [topic-selection rubric](../../docs/policies/topic-selection.md) as of 2026-11-08. The scout's
scores ([upcoming-decisions-2026-11.md](../../docs/research/upcoming-decisions-2026-11.md)) were re-checked
against primary records; the total is unchanged.

| Criterion | Score | Reason |
|---|---|---|
| Everyday impact | 3 | Every resident and property owner. The 2026 General Fund was $1,057,100,578 (P-054), about 48 cents of each dollar for justice and safety (P-053), and the jail is under a 2025 agreement with the U.S. Department of Justice (P-048) |
| Input still possible | 3 | Public comment at regular Board meetings on 2026-11-18, 12-02 and 12-16 (P-036, P-039, P-067); a public hearing must come before adoption (P-047); the vote is due by the second regular meeting in January 2027 (P-005) |
| Evidence available | 2 | The county code, Legistar votes, last year's budget book and the offices' own 2027 presentations are public. The proposal itself, the hearing date and the Budget Commission's written 5% instruction are not |
| Public confusion / attention | 3 | Six justice officials held a news conference and said they would skip the hearings (P-069, P-076); commissioners dispute the process (P-072, P-073); a resolution to change it failed and was filed (P-024, P-025) |
| Likely to move | 3 | The county code requires adoption by the January deadline (P-005) |
| **Total** | **14 / 15** | |

- **Category:** money and taxes; safety and courts.
- **Exclusions checked:** not a local act or honorary resolution. It is about a budget, not about any candidate for office. The page names officials only by their role and documented statements, and mentions no party.
- **Other candidates:** see the scout report. PSC Docket 57171 is Trial 002.

## Status and what is not published yet (as of 2026-11-08)

| Item | Status on 2026-11-08 | Where we looked |
|---|---|---|
| Proposed 2027 budget | Not published. Due no later than 2026-11-15 (a Sunday) | Budget page (S09, P-045): newest documents are FY2026 |
| Public hearing on the budget | No notice posted. Last year's was held at a meeting on 2025-12-03 (P-047) | Public Notices page (S08, P-042): newest notice is the Budget Commission's October hearings. Notices also run in the county's legal newspaper, which we did not check |
| 2027 meeting schedule | A **proposed** schedule is attached to item 26-0686 (S07b); its record says approved with the 2026-10-21 consent agenda but its status is "Agenda Ready" (S07) | Legistar |
| Agenda for 2026-11-18 | Not posted; Legistar's event list ended at 2026-11-04 | Legistar Web API |
| Budget Commission's written 5% instruction | Not found | Notices, budget page, Legistar, open-data decks |
| Justice leaders' letter (2026-10-07) | Not found on a primary page | Legistar, Sheriff's press-release page, web search |

## How to update (append-only)

Published content is never edited in place (CLAUDE.md, invariant 6). When something arrives, snapshot it, add
passages, and publish a new revision with a change class and a reason, keeping revision 1 as it is:

1. **The proposal (by 2026-11-15):** snapshot the proposed budget book and any Budget Commission presentation;
   replace "not published" statements; add the 2027 totals and the justice offices' amounts; re-check card 4
   (does the proposal list requests above 5%?).
2. **Hearing notice:** add its date, place and how to speak to Layer 1 and home.md.
3. **2026-11-18, 12-02 and 12-16 agendas and votes:** re-pull from the Legistar Web API (events, event items,
   votes) and add any budget actions.
4. **Final 2027 schedule:** confirm or correct the 2027-01-20 deadline (D2).
5. **Adoption:** move the page from "upcoming" to "what happened", like Trial 001.

## Discrepancies, settled from primary records

| # | Question | Answer | Source |
|---|---|---|---|
| D1 | How did the Board vote on 2026-08-05 to re-create the Budget Commission? | The Center for Civic Innovation reported "4-1", with Commissioner Arrington abstaining and Commissioner Barrett "absent". Legistar's roll call shows 4 yes, **no** "no" votes, 1 abstention (Arrington) and Barrett as "Did Not Vote", which Legistar records separately from "Absent". So the primary record is 4 yes, 0 no | S02d (P-020) vs. S22 (P-082) |
| D2 | When is the January 2027 adoption deadline? | The code says "no later than the second regular meeting in January". The scout found no 2027 calendar, but item 26-0686 has a **proposed** 2027 schedule: second regular meeting 2027-01-20 (first: 01-06). Its record says approved with the consent agenda on 2026-10-21, yet its status is "Agenda Ready". Treated as the expected date, not final | S01 (P-005), S07 (P-040), S07b (P-041) |
| D3 | Is the draft attached to the Legistar matter (labeled "7.30") the text that was adopted? | Pages 2, 3 and 7 of the signed copy match it line for line (visual comparison; the scan has no text layer). The signed copy also underlines "budget commission" on p.2 lines 19 and 23, a markup-only difference. Page 7 shows adoption on 2026-08-05. Pages 1, 4-6 and 8 were not compared | S01, S01b |
| D4 | What exactly is the 5% cap, and where is it written? | **Unsettled.** Barrett's resolution says requests may "not exceed 5% of their 'base budget'" and broader needs may be described "in narrative form"; FOX 5 says "5% above current allocations". We found no written instruction from the Budget Commission. The cap is not in Sec. 2-261 or 2-263 (full text archived) | S04d (P-029), S21 (P-077), S01 (P-005 to P-015) |
| D5 | What is the chief probate judge's name? | FOX 5's story says it was "updated since publication to correct Judge Kenya Johnson's name"; a search summary still showed an older name. We use Chief Probate Judge Kenya Johnson | S21 (P-078, P-080) |
| D6 | What did the justice leaders' letter say? | **Unsettled.** Atlanta News First calls it "a detailed letter to the Board of Commissioners outlining their requests"; FOX 5 calls it a "joint letter". No primary copy found, so their positions come from the two news reports | S20 (P-069), S21 (P-076) |
| D7 | Is the proposal due "on" or "by" 2026-11-15? | The code says "by no later than November 15". Barrett's failed draft speaks of the proposal "that will be presented to the Board of Commissioners on November 15, 2026", which is a Sunday. Last year's proposal was "provided on November 15, 2025, presented on November 19, 2025". So: due by 11-15; the date it is presented to the Board is not set in any source | S01 (P-005), S04d (P-031), S09b (P-046) |
| D8 | How did Resolution 26-0729 "fail" on 2026-10-21? | One yes (Barrett), one no (Ellis), two "Did Not Vote" (Pitts, Arrington), two "Absent" (Thorne, Abdur-Rahman): it failed without a majority either way. On 2026-11-04 the motion to approve got no vote, and a substitute motion to file passed 4 to 1 (Arrington did not vote) | S04b (P-025), S04e (P-033), S04f (P-034) |
| D9 | How can residents comment? | The Board's public comment rule (agenda item 26-0748) mentions comments "in-person, via emails or via Zoom or other electronic media (i.e., phone call)"; the Clerk's page lists "two options ... in person and virtually via Zoom". Both are official; the page tells readers to ask the Clerk how to send a written comment | S05 (P-036), S11 (P-067) |
| D10 | "Budget Committee" or "Budget Commission"? | The 2026-11-04 agenda item 26-0766 says "Update on the Budget Committee"; the code says "budget commission". Presumably the same body; the page uses the code's name and quotes the agenda's | S05 (P-038), S01 |
| D11 | Did the Budget Commission's hearings happen, and did the justice offices attend? | **Unsettled.** The notice and agenda show hearings scheduled 2026-10-12 to 10-22, with the justice offices on 10-13, 10-14 and 10-22; FOX 5 reported the leaders would not attend. No minutes found. The offices' presentations were posted to the open data portal on 2026-10-09 | S08 (P-042), S08b (P-043, P-044), S10 (P-055 to P-058), S21 (P-076) |

## Hosts needed (runbook step 1)

Used: `webapi.legistar.com` (votes, histories, attachments), `fulton.legistar.com` and `fulton.legistar1.com`
(agendas, attachments), `www.fultoncountyga.gov` (notices, schedules, budget books), `data.fultoncountyga.gov`
and `api.us.socrata.com` (department presentations), `www.atlantanewsfirst.com`, `www.fox5atlanta.com`,
`civicatlanta.org`. Blocked or refused: `www.census.gov` and `codes.findlaw.com` (Cloudflare challenges),
`api.census.gov` (needs a key), `www.ajc.com` (paywall), `web.archive.org` (capture request denied by this
session's permissions). See [sources.yaml](sources.yaml).

## Files

| File | What it is |
|---|---|
| [sources.yaml](sources.yaml) | 33 retrieved sources with SHA-256 hashes (raw files in `_raw/`, git-ignored), plus 11 sources recorded as blocked, covered or rejected |
| [passages.spec.yaml](passages.spec.yaml) → [passages.md](passages.md) | 86 hand-chosen passages, copied mechanically. They hold the full text of County Code Sec. 2-261 and 2-263 as adopted, the full text of Resolution 26-0729, every roll call cited, and whole Legistar histories |
| [workspace.md](workspace.md) | The page: 30-second version + Layers 1-5. Every sentence cited or labeled |
| [home.md](home.md) | Home-page items built from this trial |
| [page.yaml](page.yaml) | Display settings for the prototype (slug `fulton-budget-2027`) |
| [verification.md](verification.md) + [verification/](verification/) | Round 1 results and the disposition of every finding (77 rows), the revision 2 gate, and open items. `verification/round1/` holds the checkers' reports (unchanged copies) and revision 1 of the page and home items |
| [scripts/](scripts/) | Two small helpers used to derive passage text: PDF text (pdfplumber) and PowerPoint slide text (standard library) |
| [log.md](log.md) | Step-by-step log with times, friction, failures and every place the runbook didn't fit |
| [review.md](review.md) | The owner's feedback form for the experience |
