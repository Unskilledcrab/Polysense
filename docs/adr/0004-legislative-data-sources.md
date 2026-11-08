# 0004: Legislative data — Open States primary, LegiScan cross-check, legis.ga.gov fallback

- **Status:** Accepted
- **Date:** 2026-11-08

## Context

legis.ga.gov has no documented API. Its SOAP and REST services are undocumented, and the REST token
scheme was reverse-engineered by Open States. Open States maintains a Georgia scraper actively, publishes
public-domain and CC0 data, and has bulk downloads, though its free API quota is small. LegiScan offers
30k queries a month under CC BY 4.0. See [georgia-data-sources.md](../research/georgia-data-sources.md).

## Decision

- **Primary:** Open States / Plural bulk data and API for bills, versions, actions, votes, people,
  committees and events.
- **Cross-check:** LegiScan. Discrepancies go to a human queue, and nothing disputed is published.
- **Fallback:** legis.ga.gov `/api/meetings` for hearings and version PDFs, used only when needed, with
  an alert if the token fails.
- **Rosters** are verified by hand after the swearing-in and after every change.

## Consequences

We depend on Open States' upkeep, which is mitigated by the cross-check and fallback. LegiScan data
requires attribution wherever it's displayed.
