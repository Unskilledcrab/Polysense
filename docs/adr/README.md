# Architecture decision records

Each ADR records one decision: its context, what was decided and the consequences. To change a decision,
write a new ADR that supersedes the old one. Never rewrite history.

| # | Decision | Status |
|---|---|---|
| [0001](0001-orphan-restart.md) | Restart on an orphan branch; the .NET work stays on `Development` | Accepted |
| [0002](0002-django-monolith.md) | One Django app (templates + htmx); no separate JS front end for now | Accepted |
| [0003](0003-postgres-postgis-no-queue.md) | Postgres/PostGIS is the only system of record; commands + cron before any queue | Accepted |
| [0004](0004-legislative-data-sources.md) | Open States primary, LegiScan cross-check, legis.ga.gov for meetings and fallback | Accepted |
| [0005](0005-divisions-and-boundaries.md) | District identity separate from dated boundaries (representation vs. election) | Accepted |
| [0006](0006-append-only-revisions.md) | Append-only revisions of publishable bundles, with SHA-256 permalinks | Accepted |
| [0007](0007-evidence-snapshots.md) | Immutable snapshots, versioned passages, Internet Archive as witness | Accepted |
| [0008](0008-ai-drafting-and-verification.md) | AI: citations → classify → gate → support check; pinned model; evals | Accepted (amended by 0014) |
| [0009](0009-no-accounts-district-urls.md) | No accounts; district-keyed URLs; addresses never stored | Accepted |
| [0010](0010-licensing.md) | Code Apache-2.0; content CC BY 4.0; data keeps source licenses | Proposed |
| [0011](0011-hosting.md) | Render + Cloudflare + R2 | Proposed |
| [0012](0012-prove-then-automate.md) | Prove each step by hand, then automate | Accepted (amended by 0014) |
| [0013](0013-security-baseline.md) | Security baseline and dependency policy | Accepted |
| [0014](0014-ai-only-verification.md) | AI-only verification in the proof of concept; no human in the loop | Accepted |
