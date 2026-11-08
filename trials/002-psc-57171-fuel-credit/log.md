# Log: Trial 002 (PSC Docket 57171)

Times are UTC on 2026-11-08 and measure **agent** wall-clock time, which isn't comparable to a person's time.
The brief came from a coordinating session with a scout's leads (docs/research/upcoming-decisions-2026-11.md);
every lead was re-checked on a primary page and re-snapshotted here.

| Step | Start → end | What happened | Friction / failures |
|---|---|---|---|
| Read | 06:57 → 06:59 | Brief, CLAUDE.md, runbook v0.3, style guide v0.4, Trial 001's files, the tools and their README | — |
| 0. Choose | (given) | The brief chose Docket 57171. Scored it 12/15 (scout: 13): evidence lowered to 2 for redactions and missing transcripts (README) | — |
| 1 + 2. Find and snapshot | 07:00 → 07:16 | psc.ga.gov and services.psc.ga.gov reachable. Snapshotted the docket page, the filing list (32 items), the scheduling order (.docx and signed scan), the 2026-05-28 fuel-case short order and 2026-08-04 order, the legal notice, all five parties' testimony, Staff's errata, Georgia Power's hearing-request responses, the motion to strike and response, the calendar and three event pages, the public-witness, public-comment and FAQ pages, and all five commissioners' PSC pages. Discovery search, then 4 news leads (3 retrieved, 1 blocked) | **The docket page fills its filing table with JavaScript**; snapshot of the page alone has no filings. Fetched the JSON the page requests. The first try without `pageNumber=1` returned **HTTP 200 with an empty body** and `snapshot.py` reported no error (discarded and redone). Filings are `.docx`, `.zip` and `.xlsx`, which `snapshot.py` saves as `.bin` with no text; **three key files are scans with no text layer** (the signed order, Staff's errata, RSM's testimony) |
| Reading | 07:03 → 07:18 | Read every filing used. Wrote stdlib `.docx` and `.xlsx` text extractors, a pdfplumber extractor that keeps page and margin line numbers in the `pN: LINE` form `build_passages.py` already strips, and a safe unzip (flat names, new directory). Checked the signed order scan against the `.docx` by eye (pp. 1, 3, 4, 7: match) | **Found that the scout's "$329 million" quote was corrected** by Staff's errata of 2026-10-07 ("$328 million"), and that **every Option 1 and Option 2 estimate is blacked out** in the public spreadsheets. The errata could only be read by eye. RSM's testimony (which the scout had not read) argues for wider access to real-time pricing and takes no position on the options |
| 1 (again). Count voices | 07:09 → 07:13 | Per question, not per side: take-or-pay contracts: Option 1 = Georgia Power, GIPL; Option 2 = PIA Staff; either = GAM. Pipeline fees: Georgia Power (+GAM) vs. Staff vs. GIPL's variant. Other costs: Staff and GIPL vs. Georgia Power. Base trade-off: GAM (+Georgia Power) vs. Staff, GIPL. Option 2 is thin (one party). Searched for more: news quotes one commissioner, a company spokesperson and a non-party advocacy group, but no one else argues for Option 2. Said so on the page | A news story (S24) describes a vote by party label; not used. Commissioners are deciders, not sides, so their reported remarks were left out |
| 3. Passages | 07:19 → 07:30 | 160 passages in `passages.spec.yaml`, built with `build_passages.py`, 0 errors. Fixes only for extraction glitches: page footers inside spans, footnote numbers fused to words, line-end hyphens; each noted | **Two footer fixes failed** (the footer also carries "Docket No. 57171"). **One span ended early** (P-058 stopped at the first "revenues to the fuel clause."); caught by a quote error, then audited every spec entry for repeated end strings (scratch `span_audit.py`). Business addresses were cut from witness passages; party labels and family details on commissioners' pages kept out by span choice |
| 4. Draft | 07:29 → 07:40 | `workspace.md`: 30-second version, Layer 1 with a new "Who decides", four options (A today's method, B Georgia Power, C PIA Staff, D GIPL), six cards, "Questions to weigh", Layer 5. First gate: 2 errors, 6 warnings; then 0 and 0. Self-review against passages tightened paraphrases, split definitions into tagged sentences, gave every card the same "In short" form (claim, then evidence status), reordered cards by question and filing order, and defined the abbreviations that appear inside quotes | Checker false positives avoided by wording: a middle initial ("Alicia M. Johnson") and "a.m." mid-sentence count as sentence ends. Card 3's first evidence label said no party disputes that pipeline fees are fixed; GIPL never conceded it, so the label now names who calls them fixed |
| Sources | 07:38 → 07:42 | `sources.yaml`: 35 retrieved sources, 65 hashed files (zip members hashed too), plus not-retrieved and rejected leads. `verify_hashes.py` PASS | **New type `party-filing`** (see below) |
| Archive | 07:42 | Tried to request an Internet Archive capture of the scheduling order page (runbook step 2.5) | **Denied by the session's permission policy** (public data-sharing). Not retried and not worked around. No archive URLs recorded; the availability API shows the PSC calendar has an earlier Wayback capture (2026-09-24) |
| Home, page, render | 07:42 → 07:45 | `home.md` (4 items), `page.yaml` (slug `psc-57171`). Test-rendered both trials into scratch: 6 layers, 4 options, 6 cards parse | The prototype shows the raw type `party-filing` (no label) |
| Write-up and final pass | 07:44 → 07:48 | README (rubric, D1–D14, dates seen, how to resume), verification.md (step 5 only), review.md, this log. Copied the four extractors into `extract/` and confirmed they regenerate the derived text byte for byte. Final read of the page: reworded plan summaries so they don't imply real-time customers' prices change (only how their payments are counted), narrowed two absence claims to "the public copies we found". Gate re-run: PASS | — |

## Where the runbook did not fit (and what I did)

1. **Step 1's source list is bill-shaped.** It asks for bill versions, legis.ga.gov status, a fiscal note and O.C.G.A. text.
   A PSC docket's record is different: the order that opened the case (here, a fuel-case short order), the procedural and
   scheduling order, a dated filing list, pre-filed sworn testimony from each party, errata, data-request responses, motions,
   transcripts, briefs, calendar entries, public-participation pages and the commissioners' own pages. I used that list.
   → A regulatory-docket runbook (as the scout recommended), or a "record types by decision kind" table in this one.
2. **Step 2 assumes HTML and bill PDFs.** Docket files came as `.docx`, multi-file `.zip` archives, `.xlsx` workpapers and
   scanned PDFs. `snapshot.py` saved them as `.bin` with no text, so I wrote four extractors (`extract/`). **Scans with no text
   layer cannot become passages**, and there is no approved OCR tool, so Staff's corrected figures and RSM's testimony could
   only be read by eye and are not on the page as facts. This is the biggest content gap in the trial.
3. **No source type fits party filings.** The vocabulary (primary-government, secondary-research, advocacy, news,
   local-government) would label PIA Staff "government" and the other parties "advocacy", which signals unequal authority
   when Staff is a party like the rest (P-006). I added `party-filing` for every party's filing and log it here.
4. **"Sides" are per question, not per law.** Trial 001's cards say "supports/opposes the law". Here there are five parties
   and four questions, and alliances cross (GIPL sides with Georgia Power on Option 1 and with Staff on counting more costs).
   Cards are labeled by what they favor ("Side: favors Option 1", "favors PIA Staff's pipeline plan", "accepts either of
   Georgia Power's options (GAM)"), ordered by question and then by filing order. The "count voices on each side" rule was
   applied per question.
5. **Layer 3's fields assumed a passed law.** "What happened" makes no sense for pending options, so every option has
   "Who supports it" instead (What it does · Who supports it · Who benefits · Who pays · What's uncertain). Options are whole
   packages (A–D) to avoid colliding with the parties' own "Option 1" and "Option 2".
6. **Layer 1 had no place for the decider.** Added "Who decides" with the **Decide** power level, the five commissioners, the
   mid-year appointment and each term's end date (all from PSC pages), and a line saying which dates were seen when.
7. **The evidence vocabulary assumes studies.** Here the evidence is the parties' own estimates, often blacked out. The
   labeling rule on the page defines "Supported" as backed by the case record or a party's own figures, and adds a rule
   that a blacked-out number is hidden, not zero.
8. **The 30-second version's "What it does" label** presumes an enacted law; used "What's being decided".
9. **No step for a moving record.** Staff's errata (2026-10-07) silently superseded a figure the scout quoted; briefs arrive
   2026-12-02 and the vote 2026-12-15. → The runbook needs (a) a check for errata and corrections after each testimony is
   found, and (b) a re-check schedule tied to the decision's dates (README "How to resume").
10. **No "fiscal note" equivalent.** The closest are Staff's bill estimate and Georgia Power's per-$100 million workpaper;
    both are party numbers, so both are attributed and the comparison is `{computed}`.
11. **Official pages can carry party labels and family details.** The commissioners' PSC pages print a party next to each
    name and one names a commissioner's children. Passages must be chosen to exclude them. → Add to step 3.
12. **Party-hosted copies of official records.** The only copy of the 2026-09-01 transcript I found is hosted by counsel for
    one party (S31). I did not use it. → The runbook should say how to treat such copies.
13. **Archive step** could not run in this session (permission denied). Trial 001 recorded no archive URLs either.

## Tool change requests (shared tools were not edited)

- `build_passages.py`: the generated header is hard-coded to "Trial 001 (HB 1009)" and its bill-text note. I replaced only
  the header lines of `passages.md` after the final build (no passage text touched). → Take the title and intro from the spec.
- `build_passages.py`: the end string is matched at its first occurrence after the start, which silently cut P-058 short.
  → Warn when the end string occurs again within the next few hundred characters, or allow an occurrence index.
- `snapshot.py`: (a) map `.docx`, `.xlsx` and `.zip` content types to real extensions; (b) fail on an empty body; (c) print
  the content-disposition filename; (d) optionally write text for `.docx` and PDF. Candidates to adopt from `extract/`:
  `docx2txt.py`, `pdf2txt.py`, `xlsx2txt.py`, `safe_unzip.py` (stdlib or pdfplumber only).
- `check_citations.py`: treat single capital initials ("M.") and "a.m."/"p.m." as abbreviations.
- `render_prototype.py`: add a `party-filing` label ("Party filing") to `TYPE_LABEL`; the Layer 5 parser only finds IDs
  written out in full (no ranges such as "S17b–S17e").
- An **approved OCR tool** (with a dependencies.md entry) is needed for scanned filings.

## Rules learned (proposed; not yet applied to the runbook or style guide)

1. **Check for errata before quoting a number from testimony.** A quoted figure can be superseded weeks later.
2. **A redaction is not an absence.** Say a figure is blacked out; never treat it as zero or as "not estimated".
3. **When only one option has public numbers, say so on the page** instead of comparing a number with nothing.
4. **Label every party's filing the same way**, including the regulator's own advocacy staff when it acts as a party.
5. **For multi-party cases, count voices per question** and name the crossing alliances.

## Automation notes (not eligible: 0 of 3 runs for this kind of decision)

- The PSC filing list is structured JSON (date, filer, description, attachment flag): a good candidate for a later
  docket watcher that flags new filings (briefs, errata, orders) for a manual re-check. Not built.
- File extraction (`extract/`) is mechanical and was reproducible byte for byte; it could move to `trials/tools/` after review.
