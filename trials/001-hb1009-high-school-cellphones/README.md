# Trial 001: Bill workspace #1, HB 1009 (2026), high school personal-device ban

| | |
|---|---|
| **Process** | A. Bill workspace ([runbook v0.3](../../docs/process/runbooks/bill-workspace.md)) |
| **Bill** | HB 1009, 2025–2026 session, Georgia General Assembly. Signed 2026-05-05 as Act 395; takes effect 2026-12-31 |
| **Done by** | AI agent (Claude Code), by hand |
| **Verified by** | The AI-only [verification protocol](../../docs/process/verification-protocol.md); no human in the loop ([ADR 0014](../../docs/adr/0014-ai-only-verification.md)) |
| **Owner's role** | Feedback on the user experience ([review.md](review.md)) |
| **Status** | **Revision 6.3: last fixes after the targeted re-check; re-check pending** (2026-11-08). Revision 6.1 **passed checks (round 6)**: 0 unsupported claims and 0 wrong or ungrounded spans, both advocates 4/5, no high-severity editor issue, planted errors caught 10 of 10. Revision 6.2's targeted re-check found nothing that misstates a passage; revision 6.3 takes its two medium flip-test points and a few wording fixes, with no change to any card claim, evidence label or side. The deterministic gate passes on 6.3. Earlier revisions are archived in [verification/round5/](verification/round5/) (5 and 6), [verification/round6/](verification/round6/) (6.1) and [verification/round6-targeted/](verification/round6-targeted/) (6.2). Rounds 1 and 2 failed the bias panel; rounds 3 to 6 passed it. Open items and the revision notes are in [verification.md](verification.md). Owner reviewed the experience on 2026-11-08 ([review.md](review.md)). Prototype: [prototypes/hb1009/](../../prototypes/hb1009/) |

## Why this bill (step 0)

Scored retrospectively with the [topic-selection rubric](../../docs/policies/topic-selection.md), as
of when the bill was live in the 2026 session:

| Criterion | Score | Reason |
|---|---|---|
| Everyday impact | 3 | Every public high school student and family in Georgia |
| Input still possible | 3 then / 1 now | Hearings and votes were ahead in Feb–Mar 2026. Now districts carry it out (high schools must start no later than 2027-07-01) |
| Evidence available | 2 | One Florida study, one English study, a parent survey; no fiscal note found |
| Public confusion / attention | 3 | Heavy coverage; reports disagreed on dates (see D1) |
| Likely to move | 3 | Passed 145–20 and 52–0; signed |
| **Total** | **14 / 15** | |

**Other candidates:**
- HB 1000 (one-time tax rebate) and SB 33 (property tax caps) suit Trial #2, the budget-heavy trial.
- HB 1199 (gas tax suspension) passed within days, so it left few records.

**This is a training and evaluation case.** It is not 2027 pilot content.

## Discrepancies, settled from primary records

| # | Question | Answer | Source |
|---|---|---|---|
| D1 | When does it take effect? | The Act takes effect 2026-12-31. High schools must comply "no later than July 1, 2027". Cairo High School chose to start in 2026-27, so the conflicting reports were both right | S01 (P-007, P-019), S19 (P-054) |
| D2 | When was it signed? | 2026-05-05, as Act 395 | S02 (P-024) |
| D3 | Which committees? | House Education; Senate Children and Families | S02 (P-023) |
| D4 | Which devices? | Smartphones, smartwatches, tablets, headphones and similar; e-readers were removed from the example list | S01 (P-004, P-021) |
| D5 | Exceptions? | Only when an IEP, Section 504 or medical plan explicitly requires a device, and only as the plan requires | S01 (P-013) |
| D6 | Votes? | House 145–20 (2026-02-24). Senate amendment failed 13–38; passage 52–0 (2026-03-23) | S02 (P-025, P-026) |
| D7 | Fiscal note? | None found on OPB's 2025–2026 list, which has a 2026 section. Absence there isn't proof that none exists | S10c (P-062) |

## Files

| File | What it is |
|---|---|
| [sources.yaml](sources.yaml) | 15 retrieved sources with SHA-256 hashes (raw files in `_raw/`, git-ignored), plus sources covered elsewhere, blocked or rejected |
| [passages.spec.yaml](passages.spec.yaml) → [passages.md](passages.md) | 73 hand-chosen passages, copied mechanically. They cover the bill's full enacted text, the prior-law wording the page compares against, and the Florida study's whole abstract |
| [workspace.md](workspace.md) | The page: 30-second version + Layers 1–5. Every sentence cited or labeled |
| [home.md](home.md) | Home-page items built from this trial |
| [verification.md](verification.md) + [verification/](verification/) | Every verification report, the planted-error copy and its answer key, dispositions |
| [log.md](log.md) | Step-by-step log with times, friction and rules learned |
| [review.md](review.md) | The owner's feedback on the experience |
