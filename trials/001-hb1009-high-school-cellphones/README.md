# Trial 001: Bill workspace #1, HB 1009 (2026), high school personal-device ban

| | |
|---|---|
| **Process** | A. Bill workspace ([runbook](../../docs/process/runbooks/bill-workspace.md)) |
| **Bill** | HB 1009, 2025–2026 session, Georgia General Assembly |
| **Done by** | AI agent (Claude Code), by hand |
| **Reviewer** | Project owner |
| **Status** | **Blocked at step 2 (retrieval).** Steps 0–1 are complete. Steps 2–8 have not started. |

## Why this bill (step 0)

Scored retrospectively with the [topic-selection rubric](../../docs/policies/topic-selection.md), as
of when the bill was live in the 2026 session:

| Criterion | Score | Reason |
|---|---|---|
| Everyday impact | 3 | Reported to affect every public high school student and family in Georgia |
| Input still possible | 3 then / 1 now | Hearings and votes were ahead in Feb–Mar 2026. Now the state decision is made, and local districts carry it out (reported start: the 2027–28 school year) |
| Evidence available | 2 | Research on school phone limits exists but is mixed. Georgia's K-8 ban only took effect on 2026-07-01. No fiscal note found yet |
| Public confusion / attention | 3 | Heavy coverage, and reports disagree on dates and exceptions (see the discrepancies below) |
| Likely to move | 3 | Reported to have passed the House 145–20 and the Senate 52–0, and to have been signed |
| **Total** | **14 / 15** | |

**Other candidates considered:**
- **HB 1000:** a one-time income tax rebate, with a fiscal note of about $1.1B reported. Its arguments
  are thinner (unanimous votes). Better for Trial #2.
- **HB 1199:** a 60-day gas tax suspension. Passed within days, so there are few records.
- **SB 33:** property tax caps plus a local sales tax option. Complex and fiscal-heavy, a strong Trial #2
  candidate.

**Why HB 1009 for the first trial:**
- It has **argument types that differ in kind:**
  - factual: does banning phones improve learning?
  - predictive: will enforcement work?
  - values: emergency contact and student autonomy versus focus.
- It's a short bill.
- Several reports conflict, which tests whether the process catches errors.
- Its implementation is still ahead.

**Not covered in 2027:** this is a training and evaluation case, not pilot content.

## Discrepancies found during discovery (to settle from primary sources)

| # | Question | What reports say | Settle with |
|---|---|---|---|
| D1 | When does it take effect? | 2027–28 school year (WABE, AJC, Savannah Magazine) **vs** 2026–27 (a Cairo High School page) | Enrolled bill text |
| D2 | When was it signed? | 2026-05-05 (AJC caption) **vs** 2026-05-06 (one report) | Governor's press release; legislature status page |
| D3 | Which committees heard it? | Senate Children and Families (AJC); the House committee or subcommittee isn't named | Legislature status page and meeting records |
| D4 | Which devices are covered? | Phones, tablets, smartwatches, headphones (reports) | Definitions section of the bill |
| D5 | What exceptions apply? | IEP, Section 504 and medical plans; districts may set policies for exceptions and off-campus instruction | Bill text |
| D6 | Votes | House 145–20 on 2026-02-24; Senate 52–0 on 2026-03-23 | Roll calls |
| D7 | Is there a fiscal note? | None found | OPB / Department of Audits fiscal-note lists |

## How to resume

1. **Allow these hosts for this repository's environment** (cloud environment menu → Edit → Network
   access → Allowed domains):
   - **Required:** `www.legis.ga.gov`, `legis.ga.gov`, `gov.georgia.gov`, `opb.georgia.gov`,
     `www.audits2.ga.gov`, `www.gadoe.org`, `web.archive.org`.
   - **For documented arguments and context:** `www.wabe.org`, `www.atlantanewsfirst.com`,
     `savannahmagazine.com`, `news.emory.edu`, `gbpi.org`, `www.georgiapolicy.org`,
     `georgiarecorder.com`, `www.gpb.org`.
   - **For research papers:** `www.nber.org`, `www.thelancet.com`.
   - AJC (`www.ajc.com`) is paywalled. Use it only for leads.
2. Run steps 2–8 of the runbook. Update `sources.yaml` statuses, fill in `passages.md`, draft
   `workspace.md`, run the tools, and log every step in `log.md`.
3. The owner reviews using `review.md`.

## Files

[sources.yaml](sources.yaml) · [passages.md](passages.md) · [verification.md](verification.md) ·
[review.md](review.md) · [log.md](log.md). `workspace.md` will be created at step 4.
