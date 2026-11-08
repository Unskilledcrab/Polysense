# Ingestion

> Every connector below is **done by hand first** (trials B and C, and source retrieval in trial A), and
> automated only after it passes the [automation gate](../process/prove-then-automate.md). The schedules
> below apply to *after* automation.

## Connectors (pilot)

| Connector | Source | Output | Schedule once automated |
|---|---|---|---|
| Legislative bulk import | Open States per-session data (CC0) | Bills, versions, actions, votes, people, committees | Daily, plus hourly during session |
| Legislative cross-check | LegiScan dataset/API (CC BY 4.0) | Discrepancy report | Daily |
| Meetings | Open States events; fallback legis.ga.gov `/api/meetings` | Meetings + agenda PDF snapshots | Every 30–60 min during session |
| Agenda bill extraction | Agenda PDFs (pdfplumber) | AgendaItems with extraction method | On new or changed agenda; human confirms |
| Bill text | legis.ga.gov version PDFs | Snapshots + passages | On new version |
| Fiscal notes | DOAA/OPB PDFs | Snapshots + passages | Linked by hand for covered bills |
| Boundaries | Census TIGER (2025 SLD) + Reapportionment Office shapefiles | DivisionBoundary rows with plan and validity | On new plan only |

## Snapshot procedure (every fetch)

1. Fetch with timeouts, retries with backoff, and an identifying User-Agent. Respect `robots.txt` and the
   site's terms.
2. Store the bytes in the evidence bucket under their `sha256`, with the URL, `fetched_at`, HTTP status
   and headers.
3. If the hash is unchanged since the last fetch, record a "checked" event only. If it changed, create a
   new snapshot and trigger **SourceWatch**, which marks dependent content "source changed: re-review".
4. For key primary documents, request an Internet Archive capture as an independent witness.

## Reconciliation and freshness

- **Disagreements** between Open States, LegiScan and legis.ga.gov go into a discrepancy queue for a
  person to resolve. **Nothing is published on a disputed fact.**
- **Every opportunity carries `last_verified_at`.** Once it is older than its freshness target, the item
  is automatically downgraded to "unverified: check the official source".
- **Freshness targets:**
  - hearings: 2 hours during session;
  - bill status: 6 hours;
  - rosters: verified by hand after the swearing-in and every change.

## Failure handling

- A failing connector raises an alert and keeps serving the last verified data with its "checked at"
  time.
- **Never fall back to model-generated data.**
- Since legis.ga.gov's token scheme is undocumented, it is used **only** as a fallback, and an alert
  fires on token failure.
