# Roadmap (as of 2026-11-08)

Every phase follows the rule **prove by hand, then automate**
([process/prove-then-automate.md](../process/prove-then-automate.md)). Dates are targets for one
developer working with AI assistance. Exit criteria matter more than dates.

| Phase | Dates | What happens | Exit criteria |
|---|---|---|---|
| **P0: Docs + Trial #1** | 2026-11-09 → 11-20 | Planning docs; first manual bill workspace; name shortlist | Docs reviewed; Trial #1 reviewed by the user; runbook v1; style guide v1; name candidates through a knockout search |
| **P1: Manual trials + site skeleton** | 11-23 → 12-18 | Trials A#2 (budget/fiscal-heavy) and A#3 (contested/values-heavy), B (hearing week), C (district gold set), D (challenge → correction); comprehension round 1; a Django skeleton that publishes **hand-made, reviewed** workspaces through the revision log | Every trial logged; round 1 passed or fixes made; publishing works end to end |
| **P2: Automate proven steps only** | 12-21 → 2027-01-08 | Legislative import (checked against Trial B), district lookup (checked against Trial C's gold set), snapshot and passage extraction (checked against trial passages). Drafting stays manual | Each automated step passes the automation gate |
| **Private alpha** | **2027-01-11** | 15–30 testers; 2–3 hand-made workspaces; rosters verified by hand after swearing-in | Testers can complete journeys 1–4 |
| **Shadow AI drafting** | Jan → Feb | The AI pipeline runs beside the manual process, compared against the trial gold data. Never published directly | Meets the trial-derived bar ([ai-pipeline.md](../architecture/ai-pipeline.md)) |
| **Public beta (gated)** | ~2027-02-01 or later | Public launch | Cleared name, policies, second reviewer, insurance, fiscal sponsor, 3 verified workspaces, comprehension pass |
| **Session** | Feb → ~Apr 2 | About 1 workspace a week; re-reviews when substitutes appear; feature freeze from about Crossover Day | Metrics in [pilot-2027-session.md](pilot-2027-session.md) |
| **Veto window** | ~Apr 3 → mid-May | Every passed bill tracked; "What happened" | Every outcome recorded |
| **Retrospective** | May → Jun | Public pilot report with error rates, comprehension results and hours; Jul 1 "now law" items | Report published |
| **Platform + local** | Jul → Sep 2027 | Upgrade to Django 6.2 LTS; manual trials for one county and its school district (Legistar, City Scrapers ATL), 2–3 state agencies, and PSC public-comment items | Local trials pass the gate |
| **Local elections** | Sep → Nov 2027 | Workspaces for local referenda such as SPLOST, but **only in jurisdictions that actually vote in 2027** (Atlanta's city elections are in 2025 and 2029) | Election items carry official links only |
| **2028 cycle** | Jan → Nov 2028 | Session round 2 (carryover bills); new boundary plans if districts are redrawn; workspaces for constitutional amendments; candidate content only with an editorial team and counsel review | Set in a 2028 plan |

## Not scheduled (until there's evidence they're needed)

Accounts and notifications, a separate JavaScript front end, a background job queue, Spanish (planned
after the pilot), donor data, video transcription, a statewide local-meetings aggregator, and candidate
comparisons.
