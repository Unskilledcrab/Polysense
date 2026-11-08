# Log: Trial 003 (Fulton County 2027 budget)

Times are UTC on 2026-11-08. They measure **agent** wall-clock time, which isn't comparable to the time a person
would need. Steps overlapped: retrieval and discovery ran together, and the gate ran after every drafting pass.

| Step | Start → end | What happened | Friction / failures |
|---|---|---|---|
| Setup | 06:57 → 06:59 | Read the brief, CLAUDE.md, runbook v0.3, style guide v0.4, Trial 001's files and the tools README. Checked the proxy (open) | — |
| 0. Choose and justify | 06:59 → 07:00 | Re-scored the scout's pick with the rubric: 14/15, same as the scout (README). Categories: money and taxes; safety and courts | The rubric's "likely to move" is automatic for a budget the code says must be adopted by a deadline |
| 1. Find sources | 06:59 → 07:16 | Walked the Legistar Web API: Board events July 2026 to Feb 2027 → agenda items for 2026-08-05, 08-19, 10-21, 11-04 → roll calls → attachments. Found the **2027 meeting calendar item (26-0686)** the scout missed, and two budget-process discussion items. Found 43 department "2027" budget-hearing files (decks and updated versions) on the open data portal through the Socrata Discovery API. 11 web searches (leads only). Checked leads: the AJC (paywalled), Capital B (about the 2025 budget), the Sheriff's Office press-release page (nothing after July 2026), the Clerk's pages (public comment rules) and the FGTV and "Watch Past Meetings" pages (no usable static text). **Voices counted before drafting:** for the process, one commissioner (Ellis) plus the Board's own resolution; against it, three justice leaders (Gammage, Johnson, Labat) and one commissioner (Barrett) plus her resolution. Searched again for the vice chair, other commissioners and the leaders' letter; found nothing more. Discrepancies D1-D11 written (README) | The runbook's source list is for bills (versions on legis.ga.gov, fiscal note, O.C.G.A. sections). A county budget needed a different list (see "Where the runbook didn't fit", 1). The scout's notes were good leads but two were wrong or stale: the 2027 calendar was posted (D2), and the "4-1" vote count is not what the roll call shows (D1) |
| 2. Snapshot | 07:00 → 07:15 | 36 snapshot runs with `trials/tools/snapshot.py`: 33 sources retrieved, 2 blocked (Census QuickFacts and FindLaw: Cloudflare challenges), 1 transient 404 (S01, kept as evidence and retried once). Derived text: `pdf_markup.py` for the two resolutions; pdfplumber (`scripts/pdf_text.py`) for agendas, schedules and the budget book; a stdlib extractor (`scripts/pptx_text.py`) for four PowerPoint decks. `verify_hashes.py`: 33/33 OK | **S01 returned HTTP 404** from one server node (gasmp-legweb4) although the scout got it at 06:46; a retry 33 s later reached gasmp-legweb5 and matched the scout's hash. **Census QuickFacts and FindLaw** served Cloudflare challenges; recorded as blocked, not reloaded in a browser. The **Census Data API** now needs a key; not requested. The **Internet Archive capture** request (runbook 2.5) was denied by this session's permission system; not retried. The open data portal's own catalog endpoint returned nothing; the shared `api.us.socrata.com` endpoint worked |
| 3. Passages | 07:16 → 07:29 | 84 passages in `passages.spec.yaml`, built by `build_passages.py` with 0 errors. They hold the **full text** of County Code Sec. 2-261 (as amended) and Sec. 2-263, the full text of Resolution 26-0729, every roll call cited, and the whole Legistar history of 26-0729, so absence claims can be checked. Visual checks: S01 pp. 2-3 (strike/underline), S09b p.4 (signed scan, OCR text), p.77 (stacked table headings), p.78 (fund-balance rows); S01b (signed copy) pp. 2, 3 and 7 against S01 | `pdf_markup.py` caught the strikes but its line grouping misplaced bold words ("WHEREAS," moved to the end of a line), so unmarked passages come from a prefixed plain extraction and only the struck section from `--clean`. The builder hard-codes "Trial 001 (HB 1009)" as the title of passages.md; replaced by `scripts/retitle_passages.py` (header only). Trimmed two copyrighted news excerpts to the sentences cited |
| 4. Draft | 07:29 → 07:40 | `workspace.md` (30-second version + Layers 1-5, 3 options, 6 cards, "Questions to weigh"), `home.md` (4 items), `page.yaml` (slug `fulton-budget-2027`). A test render of the prototype with Trials 001 and 003 together worked (scratch only; phone screenshot checked) | Several template fields assume a bill (see below). One pronoun ("he" for the chairman) had no source; replaced by the role |
| 5. Deterministic gate | 07:32 → 07:44 | First run: PASS with 19 warnings; after fixes, **0 errors, 0 warnings** (234 units). Reading grade 6.6 / 7.8 / 7.4 / 6.3 for the 30-second version and Layers 1-3. Hashes 33/33. Step 5 checklist in `verification.md` | One quote failed: the source breaks "DECISION-MAKING" across a line. The checker's "several sentences share one marker" warning fires on "U.S.", "a.m.", middle initials and "Jr."; reworded rather than ignored. The number check needs the year or the outlet name ("FOX 5") inside a cited passage, so several sentences gained a second citation; sentences that carry our capture date ("On Nov 8, ...") are tagged `{analysis}` because no passage holds that date |
| Records | 07:36 → 07:47 | `sources.yaml`, README (status table, D-list, hosts), `verification.md`, `review.md`, this log; derivation scripts copied to `scripts/` and re-run to confirm the derived files reproduce exactly | — |

## Where the runbook didn't fit (v0.3 was written for bills)

The three biggest gaps come first.

1. **The decision's key document doesn't exist yet, and the runbook has no "not yet published" path.** Steps 3 to
   5 assume a text to extract and explain. Here the proposal is due 2026-11-15 (a Sunday), the hearing isn't posted,
   and the 2027 schedule is only proposed. I adapted by: (a) a README status table of what is not published, where
   we looked and when; (b) writing "not published" as dated, positive statements about what each page showed
   ("the newest notice was for October's hearings") rather than bare absence claims, since the style guide allows
   absence claims only with the whole text archived; (c) an append-only "How to update" plan in the README. Gaps:
   the capture date lives in `sources.yaml`, not in any passage, so dated sentences can't be cited and fail the
   number check unless tagged `{analysis}`; and the runbook needs a change-tracking step (re-snapshot and diff the
   budget page, notices page and Legistar events on a schedule).
2. **The source list, types and ingestion steps are bill-specific.** Step 1's priority list (bill versions,
   legis.ga.gov status, fiscal note, O.C.G.A. sections) and step 2's legis.ga.gov browser method don't apply. The
   county analogues were: the code section and its amending resolution (Legistar attachments), Legistar matter
   histories and roll calls (Web API: events → items → votes; **no tallies are given**, so every count is
   `{computed}`), last year's budget book, public notices, meeting schedules, and the offices' own requests (open
   data portal decks, PowerPoint files the tools couldn't read). The type vocabulary has no "primary, local
   government" value, so county records are typed `primary-government` and the `local-government` type went
   unused. A county-budget runbook (as the scout suggested) should list these sources, the API chains and the
   formats.
3. **Documented arguments in a local dispute come mostly from news, and one side's reasons are mostly votes.** The
   justice leaders' letter is not published, so their positions come only from two news reports, against the
   rule that news is "secondary support only, never the only source for a fact". The Board majority's reasons for
   keeping the 5% limit appear nowhere in writing; they may be in meeting video (S16), which the runbook has no
   method for (transcription, timestamps as locators). The page says plainly that the process side is thinly
   documented and why. The runbook's "count voices per side" rule worked, but it should add "votes are the base
   rate; reasons may only be on video".

Other places it didn't fit or needed a choice:

4. **Option template.** For a budget, the status quo is not "no change": the code makes the proposal the operating
   budget until adoption, so "adopt as presented" is the default path (Option A). The field "What happened" became
   "Where it stands" for all options, because two of three are still ahead. "Reject" is not a real option on the
   record and is explained, not listed.
5. **Card sides.** "Supports / opposes the law" didn't fit a dispute with three positions (the process, its critics,
   and the justice offices' funding asks). Sides used: "supports the current process", "critical of the current
   process", "county justice leaders".
6. **30-second version and Layer 2 labels.** "What it does" was kept (the budget "will set how much the county can
   spend"), but "What would change" became "What's different this year", plus "Not published yet" and "The 2026
   budget, for scale" sections, because the 2027 contents are unknown.
7. **Layer 1 order.** Kept Trial 001's order (What happened, Coming up, What you can do) for template consistency.
   For an upcoming decision, "Coming up" first may read better; worth a usability test.
8. **Bot-check fallback vs. the brief.** Runbook step 2.3 says to reload bot-checked pages in a headless browser;
   the brief says never to work around a block. I treated Cloudflare challenges as blocks. The runbook should say
   which is which (a site that needs JavaScript to render vs. an anti-bot challenge).
9. **Internet Archive captures (step 2.5)** were denied by the session's permission system. The runbook should say
   who requests captures (the agent, or the coordinator after review).
10. **Transient errors.** "One request per source" met a load-balanced server that returned 404 from one node. I kept
    the failed response, retried once after a pause, and recorded both. The runbook should allow one retry for 404
    or 5xx responses, never for 403 or challenges.
11. **Scanned PDFs.** The signed resolution has no text layer and there is no OCR tool here; I compared three pages
    visually and cite the draft text. The runbook needs a rule (OCR as a dependency, or visual comparison of named
    pages, recorded).
12. **Per-household numbers.** The style guide asks for human-terms conversions, but no population or household
    source was reachable (Census blocked; its API needs a key). I used "cents of every General Fund dollar"
    instead. The project needs one archived population/household table per county.
13. **Election-adjacent timing.** The leaders' "wait until January" ask is about who votes on the budget (incoming
    board members). The page reports their reason and describes no election, but there is no rule for this case.

## Shared tool changes needed (described, not made)

- `build_passages.py`: take the title and intro from the spec instead of hard-coding Trial 001's; allow a
  per-file option for the line-number prefix (it can't be used on agendas, where lines start with real numbers).
  A JSON mode (passages from fields, not raw text) would make roll-call passages readable.
- `snapshot.py`: add PowerPoint/Word types to the extension map (the decks were saved as `.bin`), and write text for
  PDFs and decks (this trial's `scripts/pdf_text.py` and `scripts/pptx_text.py` could move to `trials/tools/`).
- `check_citations.py`: add single capital initials, "Jr.", "U.S." and "a.m./p.m." to the abbreviation list; consider
  a `{captured: date}` tag for dated "as of" sentences.
- `prototypes/hb1009/template.html`: the header says "TRIAL 001" for every trial.
- Dependencies: no new project dependency. pdfplumber (listed) and the standard library did all extraction. The
  system tools `pdftoppm` (page images for visual checks), `pdftotext` (exploration only) and headless Chrome (a
  screenshot of the local test render) were used outside the project's code, and fetched no source.

## Rules learned

1. **Primary vote records beat summaries, even reputable ones.** A civic group's "4-1" was really 4 yes, 1 abstain
   and 1 "did not vote" (D1). Legistar roll calls give names and values but no totals; count them and show the work.
2. **Check what a scout says is missing.** The 2027 calendar "not posted" was attached to a 2026-10-21 consent item
   (D2). Search the meeting records, not only the website.
3. **A failed request isn't always a block.** Keep the failed response, retry once for 404/5xx, and never for 403 or
   challenges.
4. **Write "not yet published" as what each page showed on a date.** It is checkable; "no X exists" is not.
5. **For a fast-changing local record, archive whole histories** (Legistar histories, roll calls), so "no later
   action" can be checked.
6. **County resolutions carry bill-style strike/underline markup.** `pdf_markup.py` works on them, but check bold
   words' order and compare against the signed copy.

## Automation notes (not yet eligible: 0 of 3 runs)

- **Legistar Web API pulls** (events → agenda items → roll calls → attachments) were deterministic and fast; a good
  first candidate once three manual runs agree. **Change watchers** for the budget page, the notices page, Legistar
  events and item 26-0686's status would catch the proposal, the hearing notice and the final schedule.
- **Open-data discovery** (Socrata catalog for "Budget Presentation_2027") is easy to automate; reading the decks
  needs the PowerPoint extractor.
- **Manual for now:** deciding which records matter, settling discrepancies, choosing passages, and writing the cards.
