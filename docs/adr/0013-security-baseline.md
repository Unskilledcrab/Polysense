# 0013: Security baseline and dependency policy

- **Status:** Accepted
- **Date:** 2026-11-08

## Context

The owner requires secure, reliable software. A civic information site is an attractive target for
defacement, account takeover and data poisoning, and it must stay up on news days.

## Decision

- **Checklist target:** OWASP ASVS Level 2.
- **Dependency policy:**
  - every dependency is listed in [dependencies.md](../architecture/dependencies.md) with its purpose,
    license, maintenance and security history;
  - mature, widely used, permissively licensed components only;
  - **no AGPL or other copyleft-network licenses** in the application;
  - prefer the standard library and Django built-ins.
- **Supply chain:**
  - hash-locked `uv.lock`, installed with `uv sync --locked`;
  - Dependabot, with every update reviewed;
  - pip-audit or OSV scans and CodeQL;
  - secret scanning with push protection, and branch protection;
  - a pinned base image digest, run as non-root.
- **Hardening:**
  - `check --deploy` in CI; HSTS, a strict CSP, secure cookies;
  - editor tools behind Cloudflare Access plus Django MFA, with an audit log;
  - Turnstile and rate limits on public forms.
- **Data:**
  - separate migration and runtime database roles;
  - the runtime role can't UPDATE or DELETE revisions;
  - addresses never stored or logged;
  - secrets only in the platform secret store.
- **AI:** documents are untrusted, drafting has no tools or web access, and there is a gate plus human
  review.
- **Reliability:**
  - generally available API features on critical paths;
  - point-in-time recovery, nightly encrypted dumps and quarterly restore drills;
  - staging before production;
  - uptime monitoring.

Full detail: [security.md](../architecture/security.md).

## Consequences

Some convenience libraries are off-limits (PyMuPDF, for its license). Slightly more CI setup in exchange
for a much smaller attack surface.
