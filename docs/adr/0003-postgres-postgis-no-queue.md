# 0003: Postgres/PostGIS as the only system of record; commands + cron before any queue

- **Status:** Accepted
- **Date:** 2026-11-08

## Context

We need relational civic data, time ranges (terms, boundary plans), point-in-polygon district lookup and
full-text search. We also need scheduled ingestion. The team is one person.

## Decision

- **PostgreSQL 17 + PostGIS** (with btree_gist) is the single system of record. Range types and exclusion
  constraints enforce validity. Built-in full-text search comes first; pgvector only if retrieval needs
  it.
- **No job queue at first.** Ingestion runs as Django management commands, by hand during trials, then as
  platform cron jobs.
- Add a Postgres-backed queue (for example Django's tasks framework with a database backend, or
  Procrastinate) **only** when concurrency or retries truly require it. No Redis.

## Consequences

One datastore to back up and secure. Scheduled work is simple and visible. Long or parallel jobs may
eventually need the queue, recorded in a new ADR.
