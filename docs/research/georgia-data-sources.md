# Georgia data sources (research as of 2026-11-08)

> Some sites could not be fetched from the research environment, so claims about them come from search
> summaries or from reading open-source scraper code. **[unverified]** marks items to confirm. Verdicts
> are for the 2027 session pilot.

## Summary

| Need | Primary | Fallback / cross-check | Verdict |
|---|---|---|---|
| Bills, versions, actions, sponsors, votes, committees | **Open States / Plural API v3 + bulk data** | LegiScan; legis.ga.gov | Use now |
| Hearings / committee meetings | Open States events | legis.ga.gov `/api/meetings` | Use now; bill lists come from agenda PDFs |
| Legislators and districts | Open States people (CC0) + our own PostGIS boundaries | Census Geocoder | Use now; verify rosters by hand in January |
| Fiscal notes, budget | Department of Audits and Accounts / OPB PDFs | — | Link by hand for covered bills |
| Agency comment periods | Curated by hand (about 10 agencies) | — | Later; label coverage honestly |
| Election logistics | SoS calendar PDFs, curated by hand; deep-link My Voter Page | — | Never scrape MVP; never AI-draft |
| Local meetings | City Scrapers ATL; Legistar | County sites | Later (Phase "Platform + local") |

## 1. Georgia General Assembly (legis.ga.gov)

- **No documented public API.** There are two undocumented layers, and both are used by Open States'
  Georgia scraper (https://github.com/openstates/openstates-scrapers/tree/main/scrapers/ga):
  - **SOAP:** `http://webservices.legis.ga.gov/GGAServices/{Service}/Service.svc?wsdl`, with
    `GetLegislationForSession`, `GetLegislationDetail`, `GetVote` and `GetMember`.
  - **REST** behind the website (`https://www.legis.ga.gov/api/...`):
    - A bearer token comes from `/authentication/token?key=…&ms=…`. The key is a SHA-512 of fixed
      strings in the site's JavaScript plus a timestamp, which has been reverse-engineered.
    - `/meetings?startDate=…` returns committee and floor meetings with `agendaUri` (PDF) and
      `livestreamUrl`.
    - `POST /Legislation/Search/250/{page}` searches bills.
    - `/legislation/document/{session}/{id}` returns version PDFs.
- **Gotchas:**
  - The token scheme can break without notice.
  - There are no published terms or rate limits.
  - Some roll calls are unpublished.
  - Which bills a hearing covers appears **only in the agenda PDF**.
  - Agenda URLs contain a malformed `https/`.
  - Special sessions use different document URLs.
- **Session rules:** convenes on the second Monday in January and runs 40 legislative days. In 2026 it
  convened Jan 12, Crossover Day was Mar 6, and Sine Die was Apr 2 under HR 998
  (https://assets.georgiacourts.gov/2/wp-content/uploads/2026/01/16182349/Week-1-Legislative-Report.pdf).
- **Verdict:** use through Open States now. Call `/api/meetings` directly only as a fallback.

## 2. Open States / Plural Open API v3

- **Endpoints:** `https://v3.openstates.org`: `/jurisdictions`, `/people`, `/people.geo` (lat/lon →
  legislators), `/bills`, `/committees`, `/events`. The key goes in the `X-API-KEY` header.
- **Georgia is actively maintained:** fixes in Oct–Nov 2026, and the 2026 special session was added in
  June 2026 (https://github.com/openstates/openstates-scrapers/commits/main/scrapers/ga).
- **Rate limits:** reported as 500/day and 10/minute, or 250/day, on the default tier
  **[unverified: check the account page]**. Prefer bulk downloads.
- **License:** bulk data is public domain; legislator data (https://github.com/openstates/people) is CC0.
- **Gotchas:** the roster is curated by hand, so it will lag after the January 2027 swearing-in. The
  House reportedly changed control in November 2026 **[unverified]**. `/people.geo` covers only state and
  federal legislators.
- **Verdict:** primary source; cache heavily.

## 3. LegiScan

- Covers all Georgia sessions, with weekly datasets (https://legiscan.com/GA/datasets).
- Free API key with up to **30,000 queries a month**. Licensed **CC BY 4.0**, so attribution is required
  (https://legiscan.com/legiscan).
- **Verdict:** cross-check and failover.

## 4. "Georgia Commons"

- A new project of Georgia Civic Data (georgiacivicdata.org): "Georgia government documents, gathered in
  one free place". It covers bills, Supreme Court of Georgia opinions, and the law and constitution.
- Its Python client `georgiacivicdata` 0.1.0 is alpha (released 2026-11-05) and licensed CC BY 4.0
  (https://pypi.org/project/georgiacivicdata/).
- Freshness, API and provenance are **[unverified]**.
- **Verdict:** don't use for the MVP. Re-evaluate in 2027; it may be a partner.

## 5. Secretary of State (elections)

- **Election calendars:** annual PDFs. There is no 2027 calendar yet; compute dates from statute and
  confirm against the PDF.
- **My Voter Page** (mvp.sos.ga.gov) is a lookup by name, county and date of birth. **No API. Never
  scrape it; deep-link it.**
- **Qualified candidates** are generated in the browser, with no export. Results (results.sos.ga.gov,
  Enhanced Voting) are a JavaScript app with no documented feed. Certified results are PDFs.
- **2026 ballot questions:** three constitutional amendments (conservation use, nonpartisan probate
  judges, 911 fund). All three reportedly passed **[unverified]**.
- **Runoffs (SB 202)** are 28 days after the election. The 2026 general runoff is **2026-12-01**.
- **Verdict:** curate by hand. The pilot needs SoS data only for special elections.

## 6. State agency rulemaking

- **O.C.G.A. § 50-13-4:** at least 30 days' notice, with the exact text and a synopsis. Adopted rules go
  to the SoS (rules.sos.ga.gov holds adopted rules only).
- **There is no central list of proposed rules.** Notices are posted agency by agency (for example the
  State Election Board's on sos.ga.gov).
- **Verdict:** after the session, curate about 10 high-salience agencies by hand, and say coverage is
  partial.

## 7. Public Service Commission (PSC)

- **Rates:** Georgia Power base rates are frozen through 2028, under the July 2025 approval and a
  December 2025 agreement. The next rate case is expected in 2028.
- **Public comment:**
  - up to 3 minutes per speaker in the first hour of each hearing day;
  - the last 15 minutes of the bi-weekly Thursday committee meetings;
  - none at Tuesday administrative sessions.
- **Documents:** PSC documents are served through `services.psc.ga.gov/api/v1/External/Public/...`.
- **Verdict:** after the session, add standing "public comment" items for PSC committee meetings.
- Source: https://psc.ga.gov/newsroom/newsreleases/psc-and-georgia-power-agreement-three-year-freeze-of-base-rates

## 8. Local government meetings (later phase)

| Body | Platform | Access |
|---|---|---|
| Atlanta City Council | Granicus IQM2 (`atlantacityga.iqm2.com`) | HTML scrape |
| Fulton County BOC | Legistar (inferred) | Legistar Web API, probably **[unverified]** |
| DeKalb County BOC | Legistar (`dekalbcountyga.legistar.com`) | Legistar Web API, probably **[unverified]** |
| Cobb County BOC | County Drupal site | HTML |
| Gwinnett County BOC | County-hosted PDFs | HTML/PDF |
| Atlanta Public Schools | Simbli/eBoard | HTML |
| Athens-Clarke | CivicPlus Agenda Center | HTML (RSS likely) |

- **Open Meetings Act (O.C.G.A. § 50-14-1):** agendas may appear only shortly before a meeting.
- **City Scrapers ATL** (https://github.com/City-Bureau/city-scrapers-atl): MIT, active through Oct
  2026, about 21 spiders.
- **Atlanta Documenters** (Canopy Atlanta) pays residents to take meeting notes, which makes them a
  partner candidate for "What happened".
- **Council Data Project Atlanta** is inactive.

## 9. District lookup

- **Google Civic Information API:** the Representatives API was shut down in April 2025. Divisions
  (OCD-IDs), elections and voterInfo remain, with tight quotas.
- **US Census Geocoder (geographies):**
  - free, no key;
  - returns county, place, the 119th congressional district, **2024 state legislative upper and lower
    districts** (matching Georgia's December 2023 maps) and unified school districts;
  - **no** county commission, city council or precinct districts.
- **Official shapefiles:** Reapportionment Office (2023 plans: SB 1EX, SB 3EX), plus county GIS for local
  districts (for example Fulton and Atlanta ArcGIS services).
- **Redistricting risk:** on 2026-08-25 the 11th Circuit vacated the 2023 Voting Rights Act ruling after
  *Louisiana v. Callais*. New maps may come before 2028
  (https://georgiarecorder.com/2026/08/26/federal-appeals-court-voids-2023-ruling-that-georgias-political-maps-violated-voting-rights-act/).
  **Store boundaries with date ranges.**
- **Commercial alternatives:** Cicero, BallotReady/CivicEngine, the Democracy Works API and the
  Ballotpedia API are sales-led. Not needed for the pilot.
- **Verdict:** self-host the boundaries, and use the Census Geocoder only for coordinates and as a
  cross-check.

## 10. Fiscal context

- **Fiscal notes:** prepared jointly by the Department of Audits and Accounts and OPB, **only when a
  legislator requests one**. Published as PDFs at https://opb.georgia.gov/budget-information/fiscal-notes
  and https://www.audits2.ga.gov/legislative-resources/fiscal-notes/.
- **Budget:** OPB reports and appropriations bills, as PDFs.
- **Open Georgia** (open.ga.gov): salaries, travel and vendor payments, updated annually.

## Biggest data risks

1. **Redistricting churn.** Version all boundaries by date, and keep representation and election maps
   separate.
2. **A fragile legislative feed.** The legis.ga.gov token is reverse-engineered and Open States' quota is
   low. Keep LegiScan on hot standby.
3. **Messy local data.** Short agenda lead times, eight or more platforms, and PDFs.
4. **No official election APIs.** A new Secretary of State in January 2027 may change vendors.
5. **Scattered rulemaking.** Comment-period coverage will be partial; say so.
