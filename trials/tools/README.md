# Trial tools

Small helpers for the manual trials. They are the seed of the future pipeline and deterministic gate
([ai-pipeline.md](../../docs/architecture/ai-pipeline.md#3-deterministic-gate-code-not-models)). Each
tool does one mechanical job. Judgment stays with the runbook and the verification protocol.

| Script | Job | Dependencies |
|---|---|---|
| `snapshot.py TRIAL ID URL` | Fetch a source; save exact bytes, headers and visible text in `_raw/`; print the sha256 and `sources.yaml` fields | httpx, beautifulsoup4 |
| `pdf_markup.py BILL.pdf [--only-marked\|--clean\|--old]` | Recover strike-through (deleted) and underlined (inserted) words in Georgia bill PDFs; output the markup, the as-enacted text, or the prior law | pdfplumber |
| `build_passages.py SPEC.yaml` | Copy hand-chosen spans exactly from snapshot text into `passages.md` (fixes and redactions must carry a note); warns when an end string repeats soon after its match | PyYAML |
| `check_citations.py PAGE PASSAGES` | Every sentence cited or tagged; passage IDs exist (also those named in tag notes); quotes verbatim; each number found as a whole number in the cited passages (rounding allowed for 2+ significant digits); loaded words and party labels outside quotes | stdlib |
| `verify_hashes.py SOURCES.yaml [--print]` | Every retrieved file in `_raw/` matches its recorded SHA-256 | PyYAML |
| `readability.py PAGE` | Flesch–Kincaid grade per section; flags the 30-second version and Layers 1–3 above 8.5. Card labels and other `<!-- nocheck -->` lines aren't scored; it reports the longest card label instead | stdlib |
| `check_verifier.py REPORT PASSAGES [KEY]` | Machine-check an independent verifier's evidence spans; list flags; score its catch rate against planted errors | stdlib |
| `ledger.py sync\|seed\|pending\|status TRIAL` | The claims ledger (`claims.jsonl`): one verdict per sentence, kept until the sentence or a cited passage changes. Lists what needs a check | stdlib (PyYAML for `page.yaml` visuals) |
| `psc_docket.py list DOCKET` · `get TRIAL SID DOCUMENT_ID --title …` | Georgia PSC dockets: list a docket's filings; snapshot one filing with every attached file and its PDF text, and print the `sources.yaml` entry. Titles are given by role, because filings name private people | httpx, beautifulsoup4 (pdfplumber for PDF text) |
| `candidates.py TRIAL` | Stage 4 of the source funnel: from `candidates.yaml`, include short documents whole and keyword-search long ones for each question the page must answer; writes `_raw/candidates.md` for the drafter. No model | PyYAML |
| `check_privacy.py TRIAL` | Privacy gate (invariant 7): no street address in a page's tracked files unless `page.yaml` lists it under `public_addresses`; no name from the git-ignored `_raw/private_names.txt`. Part of `status.py`'s gate | stdlib (PyYAML optional) |
| `desk.py index\|watch\|queue\|impact\|brief` | The desk: one row per page (`registry.json`), whether watched sources changed (`watch.json`), what needs doing next (`queue.json`), which sentences a changed source touches, and a one-screen brief for an agent. Tests: `test_desk.py` | PyYAML (httpx and beautifulsoup4 for `watch --fetch`) |
| `judge.py emit\|ingest\|quiz\|score …` | Pair checks: small batches (a sentence plus only its cited passages) for a cheap model; verifies answers and spans in code; builds and scores blind planted-error quizzes | stdlib |
| `lint_balance.py lint\|surface TRIAL` | Balance prompts (verbs, values guide, undated unknowns, caveats, grades, terms) and the balance surface for one fairness reviewer | stdlib (PyYAML for terms) |
| `status.py TRIAL [...]` | Writes `status.json` and the page's status fields from the gate and the ledger | stdlib |
| `render_prototype.py TEMPLATE OUT TRIAL [TRIAL ...]` | Build the self-contained, clickable prototype from one or more trials. Each trial needs a `page.yaml` (slug, title, header chips, short source labels, optional bill-markup view) | PyYAML |

### `extract/`: text from non-HTML filings

Adopted from Trials 002 and 003, where regulatory and county records came as Word, Excel, PowerPoint, zip
and PDF files that `snapshot.py` saves without text. Each writes plain text that `build_passages.py` can
copy spans from; run them with `python3 -I` on files in `_raw/`, and record each derived file in
`sources.yaml`. Standard library only, except `pdf2txt.py` (pdfplumber, already approved).

| Tool | Does |
|---|---|
| `extract/pdf2txt.py IN.pdf OUT.txt [--margin X]` | A PDF's text layer as `pN: LINE text` lines; reports pages with no text layer (scans can't become passages) |
| `extract/docx2txt.py` | Word documents |
| `extract/xlsx2txt.py` | Excel workbooks, cell by cell |
| `extract/pptx2txt.py DECK.pptx > DECK.txt` | Slide text, in slide order |
| `extract/safe_unzip.py ZIP [OUTDIR]` | Lists, or safely extracts, a multi-file filing: flat names only, no paths or symlinks, size-capped, into a new folder |

Inputs are archived public filings; the XML parsers rely on the standard library's expat, which limits
entity-expansion attacks. Treat extracted text as untrusted data, like any source.

Run Python tools with `uv run --with <deps> python3 trials/tools/<tool>.py …`. The JavaScript in
`prototypes/` needs no build step.

## Self-tests (2026-11-08)

- `check_citations.py` on a throwaway fixture caught:
  - a non-verbatim quote;
  - an uncited sentence;
  - a misplaced marker;
  - an unknown passage ID;
  - a number missing from its source;
  - a loaded word.
- `check_citations.py` alone caught only **1 of 13** planted errors in Trial 001's calibration copy (a
  wrong vote count, as a warning). That's why the independent verifier exists.
- `verify_hashes.py` caught a deliberate hash mismatch.
- `readability.py` scored plain text at grade 3.9 and dense legalese at 32.4.
- `pdf_markup.py`'s strike and underline detection was checked against rendered images of bill pages 2–3.

## Fixes from Trials 002 and 003 (2026-11-08)

- **Numbers:** `check_citations.py` matched numbers as substrings, so "4" passed because "2024" was in the
  passage. It now needs a whole number. "05" in a date equals "5". A rounded figure with 2 or more
  significant digits passes ("$1.48" for 1.4806, "$220 million" for $220,454,272). An identifier such as
  26-0729 must appear whole. The page's own labels ("Option 1", "Layer 2") are skipped. On a fixture with
  6 planted wrong numbers it warned on all 6, but only 5 when the passage also held a date with "05": a
  1-digit number can still match by coincidence, so the source check stays the real test. On the three
  trials it found 4 real gaps (Trial 003 names Resolution 26-0729 while citing only the resolution's text,
  which doesn't carry the number) and no false alarms.
- **Card labels:** `render_prototype.py` split a card's meta line on "·", so an evidence label that used
  "·" lost everything after it without warning. Evidence is now read whole, and an unknown field stops
  the build.
- **Reading grade:** `readability.py` scored card labels as prose, and missed sentences that end inside
  quote marks or brackets (`said "no." Then`). Both raised grades on pages that quote testimony. Labels
  are now left out (the longest is reported), and those sentences count. Targeted sections moved by 0 to
  0.5 grades on the three trials, and every one still passes. Trial 002's Layer 4 went from 11.9 to 9.6.
- **Passage ends:** `build_passages.py` cut a span at the first match of its end string, which silently
  cut Trial 002's P-058 short. It now warns (`CHECK`) when the end string occurs again within 400
  characters, and `end_occurrence: N` picks a later match. Of 326 passages in the three trials, 6 drew
  the warning, and none was cut short: each repeat starts the next item. Those were marked checked
  with `end_occurrence: 1`.
- **Snapshots:** `snapshot.py` saved Word, Excel, PowerPoint and zip files as `.bin`. It now takes the
  extension from the content type, or else from the served file name or URL. It prints the served file
  name and which `extract/` tool makes the text, and it exits 1 on an empty body.
- **Absence claims:** `check_citations.py` now warns on an unscoped claim that no party (or no record,
  filing or source) disputes, asks, says or proposes something. Such a claim covers sources we couldn't
  read; the style guide asks for the whole text. Trial 002's round 5 source check flagged 10 of these, and
  the warning finds the same lines. Scoped wording passes ("no party whose testimony we read …",
  "no archived record …"). There were no false alarms on Trials 001 and 003.

- **Reader-first pages (style guide v0.6, usability feedback):**
  - `render_prototype.py` reads a new section, "What it means for you", and a `kind` in `page.yaml`
    (law, regulatory-case or budget; anything else stops the build). It turns inline **bold** into
    segments, so readers never see asterisks. It dates home items (it warns when an item has no
    "Mon D, YYYY" date), and it warns when Decide, Input or Observe labels anything outside "What you can
    do", because those are the reader's powers. Self-test: a misspelled kind stopped the build; an undated
    home item warned; the current pages render with 13 bold segments and no asterisks.
  - `readability.py` grades "What it means for you" as a targeted section and warns on the new length
    budgets (30-second version, "If you" lines, "In short", values lines, Layer 3 intro). It also prints
    CHECK lines for "both", "neither" or "either" standing alone. These are prompts, not warnings, because
    "no data on either" is fine when the two things are named in the same sentence.
  - `check_citations.py` now also checks passage IDs named inside a tag's note (`{analysis: P-007 sets …}`);
    an unknown ID is an error. The Trial 001 drafter found the gap. Self-test: a note naming P-999 failed; the
    three current pages still pass with 0 errors and 0 warnings.