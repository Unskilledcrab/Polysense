# Security and reliability

**Goal:** a site people can trust to be up, correct and safe, run by one person. **Approach:** mature,
well-maintained, permissively licensed components; defense in depth; few moving parts. **Checklist
target:** OWASP ASVS Level 2.

## Threat model

| Who | Wants to | How | Main controls |
|---|---|---|---|
| Partisan actors | Discredit us, or slip in biased content | Deface pages; take over an editor account; flood us with fake challenges; social-engineer editors | Admin behind Cloudflare Access + MFA; audit log; two-person rule; Turnstile + rate limits; append-only revisions |
| Data poisoners | Get false content published | Plant misleading text or instructions in documents we ingest (prompt injection) | Documents are untrusted data; AI calls have no tools or web access; deterministic gate; human support check |
| Opportunists | Exploit the app or its dependencies | Known vulnerabilities, supply-chain attacks, leaked secrets | Locked, hashed dependencies; scanning; secret scanning; minimal dependencies; non-root container |
| Anyone | Take us offline when it matters | Traffic spikes or DDoS on news days and votes | Cloudflare CDN and WAF; cacheable pages; health checks |
| Us (mistakes) | — | Data loss, bad migrations, leaked personal data | PITR + tested restores; staging; reviewed migrations; never storing addresses; error-report scrubbing |

## Controls

### Software choices and supply chain

- **Mature, permissive components only.** Each needs an entry in [dependencies.md](dependencies.md)
  covering purpose, license, maintenance and security history.
  - No AGPL or other copyleft-network licenses in the application. **pdfplumber/pypdf, not PyMuPDF.**
  - Prefer the standard library and Django built-ins.
- **Lockfiles and CI checks:**
  - `uv.lock` with hashes, installed with `uv sync --locked` in CI and builds;
  - Dependabot updates, grouped and reviewed (not auto-merged);
  - pip-audit or OSV scanning on every pull request;
  - CodeQL for Python.
- **GitHub settings:** secret scanning with push protection; branch protection with required checks.
- **Container:** pinned `python:3.13-slim` digest; non-root user; read-only file system where possible;
  image vulnerability scan.
- **Front-end assets:** htmx and pdf.js are vendored with integrity hashes. No third-party scripts, ads
  or trackers.

### Application hardening

- `manage.py check --deploy` must pass in CI.
- HTTPS only, with HSTS. Secure, HttpOnly, SameSite cookies. A strict **Content-Security-Policy** (built
  into Django 6). Referrer-Policy and Permissions-Policy set.
- Django's defaults stay on: CSRF protection, template auto-escaping, ORM parameterization, clickjacking
  protection. **No raw SQL built from strings.**
- **Editor and admin tools:**
  - on a separate hostname behind **Cloudflare Access** (single sign-on with hardware keys);
  - **plus** Django MFA (django-allauth: WebAuthn or TOTP);
  - short sessions;
  - every editorial action written to an **audit log**.
- **Public forms:** Cloudflare Turnstile, rate limits, size limits and strict validation.
- Uploads are not accepted from the public during the pilot.

### Data protection

- **Separate database roles:**
  - a migration role, used only at deploy;
  - a runtime role that **cannot UPDATE or DELETE revisions** (enforced by REVOKE plus a trigger).
- **Personal data:**
  - **addresses are never stored or logged**, and error reports are scrubbed;
  - challenge emails are deleted when the challenge closes;
  - no personal data in test fixtures (public landmark addresses only).
- **Secrets** live only in the platform's secret store and are rotated on staff change. The Anthropic API
  key sits in its own workspace with a **spend limit**.
- **Evidence bucket:** versioning or object lock where supported. Snapshots are checked against their
  SHA-256 hash.

### AI-specific

- Every ingested document is treated as **untrusted data**:
  - drafting calls have **no tools and no web access**, and return text and citations only;
  - the system prompt says source content is data, not instructions.
- AI output goes through the deterministic gate and **human review** before anything is published.
- The model version and prompts are pinned. Raw requests and responses are stored by hash so they can be
  audited.

### Reliability

- Use **generally available (non-beta)** API features on critical paths.
- **Backups:** managed point-in-time recovery, a nightly encrypted logical dump to R2, and a **quarterly
  restore drill**.
- **Staging first;** migrations reviewed; health checks; uptime monitoring and alerts.
- **Graceful degradation:**
  - if a data source fails, show the last verified data with its "checked at" time;
  - if the geocoder fails, offer a manual district picker.
- **Feature freeze** in the busiest weeks of the session.

## Incident response (minimal)

1. **Detect:** alert, report or challenge.
2. **Contain:** take the feature offline or put content into a quarantine status.
3. **Fix:** in staging, then production.
4. **Write it up publicly** if users were affected. Content errors go through the corrections process.
   Vulnerabilities are handled under [SECURITY.md](../../SECURITY.md).
