# 0005: District identity separate from dated boundaries

- **Status:** Accepted
- **Date:** 2026-11-08

## Context

Georgia's maps were redrawn in 2023, and litigation after *Louisiana v. Callais* (11th Circuit,
2026-08-25) may bring new maps before 2028. During a transition, legislators serve under one map while
the next election uses another. AI voter tools fail most often on districts, typically by mapping ZIP
codes to districts.

## Decision

- **Division** (identity, keyed by OCD division ID) is separate from **DivisionBoundary** (a dated shape
  with `plan`, `purpose` = `representation` or `election`, a `valid` daterange and a source snapshot).
- An exclusion constraint prevents two boundaries with the same division and purpose from overlapping in
  time.
- **District lookup:**
  1. The address goes to the Census Geocoder, **for coordinates only**.
  2. Point-in-polygon runs against **our** boundaries for the relevant date and purpose.
  3. The Census district answer is used as a cross-check.
  4. A manual picker is the fallback.

  Never use ZIP codes.
- A gold test set of public landmark addresses (Trial C) must pass before lookup is automated.

## Consequences

Redistricting means loading a new plan, not migrating data. An item can say "Applies to you" only when
the lookup is certain.
