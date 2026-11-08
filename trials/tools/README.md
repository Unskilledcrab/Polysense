# Trial tools

Small helpers for the manual trials. They are the seed of the future pipeline and deterministic gate
([ai-pipeline.md](../../docs/architecture/ai-pipeline.md#3-deterministic-gate-code-not-models)). Each
tool does one mechanical job. Judgment stays with the runbook and the verification protocol.

| Script | Job | Dependencies |
|---|---|---|
| `snapshot.py TRIAL ID URL` | Fetch a source; save exact bytes, headers and visible text in `_raw/`; print the sha256 and `sources.yaml` fields | httpx, beautifulsoup4 |
| `pdf_markup.py BILL.pdf [--only-marked\|--clean\|--old]` | Recover strike-through (deleted) and underlined (inserted) words in Georgia bill PDFs; output the markup, the as-enacted text, or the prior law | pdfplumber |
| `build_passages.py SPEC.yaml` | Copy hand-chosen spans exactly from snapshot text into `passages.md` (fixes and redactions must carry a note) | PyYAML |
| `check_citations.py PAGE PASSAGES` | Every sentence cited or tagged; passage IDs exist; quotes verbatim; numbers found in cited passages; loaded words outside quotes | stdlib |
| `verify_hashes.py SOURCES.yaml [--print]` | Every retrieved file in `_raw/` matches its recorded SHA-256 | PyYAML |
| `readability.py PAGE` | Flesch–Kincaid grade per section; flags the 30-second version and Layers 1–3 above 8.5 | stdlib |
| `check_verifier.py REPORT PASSAGES [KEY]` | Machine-check an independent verifier's evidence spans; list flags; score its catch rate against planted errors | stdlib |
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
