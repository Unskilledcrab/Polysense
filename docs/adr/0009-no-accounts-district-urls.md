# 0009: No accounts; district-keyed URLs; addresses never stored

- **Status:** Accepted
- **Date:** 2026-11-08

## Context

We need to personalize by jurisdiction without profiling people, while staying cacheable and fast.

## Decision

- **No user accounts in the pilot.**
- **The address is used once:** geocoded, then point-in-polygon, then discarded. It is never stored or
  logged, and it is scrubbed from error reports.
- **Personal pages live at district-keyed URLs** such as `/my/h054-s039/`. There are only a few hundred
  House × Senate combinations, so these pages are fully cacheable, shareable and usable without
  JavaScript.
- localStorage keeps only the district key and the boundary-plan version. Topic filters are URL
  parameters.
- **ICS and RSS feeds** replace alerts.

## Consequences

Strong privacy and simple caching. We can't send notifications. Revisit accounts only with a privacy
review and a clear need.
