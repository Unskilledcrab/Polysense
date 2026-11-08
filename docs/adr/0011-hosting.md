# 0011: Hosting — Render + Cloudflare + R2

- **Status:** Proposed (confirm in P1)
- **Date:** 2026-11-08

## Context

One maintainer. We need managed Postgres with PostGIS and point-in-time recovery, one container image
with web and cron processes, a CDN and WAF, zero-trust access to the editor tools, and cheap object
storage.

## Decision

- **Render:** web service, cron jobs, and managed PostgreSQL (confirm that the PostGIS extension and
  point-in-time recovery are available on the chosen plan).
- **Cloudflare:** DNS, CDN caching, WAF, rate limiting, Turnstile, and **Access** in front of the editor
  hostname.
- **Cloudflare R2:** the evidence archive and backups, with versioning or object lock where supported.

## Alternatives

Fly.io with Neon or Crunchy Bridge, or a single VPS. These are cheaper or more flexible, but more to
operate.

## Consequences

Low operations work. There is some vendor lock-in, limited by keeping a plain Dockerfile and standard
Postgres.
