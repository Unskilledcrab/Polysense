# Dependencies

**Rule:** every dependency is listed here before it is added. The bar:
- **actively maintained;**
- **widely used;**
- **a permissive or compatible license** (no AGPL or other copyleft-network licenses in the application);
- **a reasonable security history;**
- **clearly needed** (the standard library or Django can't do it simply).

Re-check maintenance status when adopting and at each quarterly review.

## Planned core (adopted when code starts)

| Component | Purpose | License | Why this one |
|---|---|---|---|
| Python 3.13 | Language | PSF | Current stable; best data and PDF ecosystem |
| Django 6.x → 6.2 LTS | Web framework, admin, ORM, GeoDjango | BSD-3-Clause | Dedicated security team and LTS releases; secure defaults; built-in CSP in Django 6 |
| PostgreSQL 17 | Database | PostgreSQL License | Reliable; supports time ranges and exclusion constraints |
| PostGIS | Spatial extension | GPL-2.0, run as a database server extension | Standard for point-in-polygon. We don't distribute it, so its GPL doesn't affect our code |
| psycopg 3 | Postgres driver | LGPL-3.0, used as a library | Django's recommended driver |
| GDAL / GEOS / PROJ | GeoDjango system libraries | MIT / LGPL-2.1 / MIT | Required by GeoDjango |
| httpx | HTTP client for ingestion | BSD-3-Clause | Mature; timeouts and retries |
| pdfplumber (pdfminer.six) / pypdf | PDF text extraction | MIT / BSD-3-Clause | **Permissive alternative to PyMuPDF (AGPL)** |
| Pydantic | Validating ingested data and AI outputs | MIT | Widely used; strict schemas |
| PyYAML | Trial and source manifests | MIT | Standard |
| anthropic (Python SDK) | Claude API (built last) | MIT | Official SDK |
| django-allauth | MFA (WebAuthn/TOTP) for editors | MIT | Mature; no custom auth code |
| django-storages + boto3 | R2/S3 storage | BSD-3-Clause / Apache-2.0 | Standard |
| whitenoise | Static file serving | MIT | Simple and reliable |
| gunicorn | Application server | MIT | Standard |
| htmx (vendored) | Small page interactions | 0BSD | Tiny, no build step, works with server HTML |
| pdf.js (vendored) | Highlighting cited passages in source PDFs | Apache-2.0 | Standard viewer |
| Django REST Framework (later) | Read-only public API | BSD-3-Clause | The most mature Django API toolkit |

## Development and CI only

| Tool | Purpose | License |
|---|---|---|
| uv | Packaging and lockfile (with hashes) | Apache-2.0 / MIT |
| ruff | Lint and format | MIT |
| pyright | Type checking | MIT |
| pytest (+ pytest-django) | Tests | MIT / BSD |
| Playwright (Python) + axe-core | Accessibility smoke tests | Apache-2.0 / MPL-2.0 |
| pip-audit | Vulnerability scan | Apache-2.0 |
| just | Task runner | CC0 |
| *(none: stdlib)* | Readability screen in `trials/tools/readability.py` (Flesch–Kincaid with a heuristic syllable counter) | n/a |

## Deliberately not used (for now)

| Rejected | Instead | Reason |
|---|---|---|
| PyMuPDF | pdfplumber / pypdf | AGPL license |
| Celery / Redis / RabbitMQ | Management commands + cron | Extra infrastructure; add a Postgres-backed queue only if needed |
| A separate JavaScript front-end framework | Server templates + htmx | Another toolchain and deploy for little benefit ([ADR 0002](../adr/0002-django-monolith.md)) |
| Playwright for scraping | httpx | Heavy. Use it only if a source truly requires a browser |
| Third-party analytics or ad scripts | Privacy-preserving aggregate analytics | Privacy and security |
| textstat | A stdlib readability screen | Tried in Trial #1 on 2026-11-08: current releases download NLTK `cmudict` data from the network at runtime. That's a hidden network dependency, and it failed behind our proxy |
