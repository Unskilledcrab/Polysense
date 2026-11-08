# Architecture overview

**Shape:** one Django application with two kinds of process (web and scheduled commands), PostgreSQL
with PostGIS, and object storage for evidence, behind Cloudflare. Chosen for **one maintainer, security
and reliability.** The alternatives are recorded in [ADR 0002](../adr/0002-django-monolith.md).

> Nothing here is built yet. Parts are built only after the matching manual process passes the
> [automation gate](../process/prove-then-automate.md). The first code will be a skeleton that publishes
> **hand-made, reviewed** content.

```
             Sources: legis.ga.gov · Open States · LegiScan · fiscal notes · Census · advocacy/news
                │  fetch → snapshot (sha256) ────────────────► R2 evidence archive  (+ Internet Archive witness)
                ▼
  ┌──────────────────────── Django app (one codebase, one container image) ─────────────────────────┐
  │  ingest/     management commands (run by hand first, then platform cron)                         │
  │  evidence/   sources, snapshots, passages                                                        │
  │  legislature/ participation/ geo/   bills, meetings, opportunities, districts                   │
  │  knowledge/  decisions, options, arguments, claims, sentences, citations                        │
  │  trust/      revisions (append-only), reviews, challenges, corrections log, coverage log        │
  │  ai/         drafting + classification (built last, shadow mode first)                           │
  │  desk/       editor & Review Desk (separate hostname, Cloudflare Access + MFA)                   │
  │  web/        public pages: server-rendered HTML + htmx; renders published revisions only         │
  └──────────────────────────────────────────────────────────────────────────────────────────────────┘
                │
                ▼
     PostgreSQL 17 + PostGIS (system of record)        Cloudflare: CDN · WAF · rate limits · Turnstile · Access
```

## Stack

| Part | Choice | Why |
|---|---|---|
| System of record | PostgreSQL 17 + PostGIS (+ btree_gist) | Relational civic data with time ranges; millisecond point-in-polygon; constraints that enforce rules |
| Application | Django 6.x, moving to **6.2 LTS** after its April 2027 release; GeoDjango | Mature security track record and secure defaults; admin for editors; one codebase for site, tools and pipeline |
| Public pages | Server-rendered templates + **htmx**; small vendored JavaScript (pdf.js for evidence, a diff view) | Fast on cheap phones, works without JS, accessible, no front-end build chain |
| Jobs | **Management commands → platform cron.** No queue until one is needed | Fewest moving parts |
| Ingestion | httpx, pdfplumber/pypdf, Pydantic | Mature and permissively licensed |
| AI (built last) | Anthropic Claude API with document citations | Sentence-level citations map to our passage IDs ([ai-pipeline.md](ai-pipeline.md)) |
| Evidence archive | Cloudflare R2 (S3-compatible); SHA-256 per object; versioning or object lock where supported | Our own authoritative copy |
| Hosting | Render (web service, cron jobs, managed Postgres with PostGIS and point-in-time recovery) behind Cloudflare | Little to operate. Confirm PostGIS and point-in-time recovery on the chosen plan ([ADR 0011](../adr/0011-hosting.md)) |
| Public API (later) | Django REST Framework, read-only | The most mature option; open data for journalists and researchers |

Every dependency is listed with its license and the reason for it in [dependencies.md](dependencies.md).
Security controls are in [security.md](security.md).

## Environments

| Environment | Purpose |
|---|---|
| **Local** | Django runs natively (uv). Docker Compose runs only `postgis/postgis:17`. Evidence is stored on local disk through django-storages |
| **Staging** | Same image as production, with its own database and bucket. Every deploy goes here first |
| **Production** | Point-in-time recovery, nightly encrypted dump to R2, quarterly restore drill |

## Repository layout (when code starts)

```
src/civic/{config,geo,legislature,participation,evidence,knowledge,trust,ingest,ai,web,desk}/
prompts/   versioned prompts + methodology text (provenance = prompt-id@git-sha)
evals/     eval sets built from manual trials + scorers
trials/    manual trial outputs (gold data)
tests/     unit, integration, accessibility smoke tests
deploy/    Dockerfile, render.yaml
docs/
```

## Key design rules

- **Public pages render from published revisions only,** never from rows being edited
  ([ADR 0006](../adr/0006-append-only-revisions.md)).
- **District identity is separate from dated boundaries,** and representation and election maps are kept
  apart ([ADR 0005](../adr/0005-divisions-and-boundaries.md)).
- **No accounts and no stored addresses.** Personal pages live at district-keyed URLs
  ([ADR 0009](../adr/0009-no-accounts-district-urls.md)).
- **Evidence is immutable.** Snapshots never change. A new extraction creates new passages
  ([ADR 0007](../adr/0007-evidence-snapshots.md)).
