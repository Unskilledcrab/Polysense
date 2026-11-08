# Runbook: bill decision workspace (manual), v0.2

**Purpose:** turn one bill into a clear, fully cited decision workspace (the 30-second version plus
Layers 1–5) by hand. **Output:** `trials/NNN-<bill>/`.

**Automation gate:** not passed (0 of 3 runs). See
[prove-then-automate.md](../prove-then-automate.md).

**Time:** record the start and end of every step in `log.md`.

## Step 0: Choose and justify

1. Score the candidate with the [topic-selection rubric](../../policies/topic-selection.md).
2. Write the scores and a one-paragraph rationale in `README.md`.
3. Exclude local acts, honorary resolutions and anything about a candidate for office.

## Step 1: Find sources (primary first)

Find each of these, in priority order:

1. **Bill text, every version** (as introduced, committee substitutes with LC numbers, as passed,
   enrolled) from legis.ga.gov.
2. **Status history and votes** from legis.ga.gov, cross-checked with Open States or LegiScan.
3. **Fiscal note**, if one exists (Department of Audits and Accounts / OPB).
4. **Current law** being changed (the O.C.G.A. sections named in the bill).
5. **Hearing records:** agendas, video timestamps, written testimony.
6. **Documented arguments from more than one side:** testimony, sponsor and opponent statements, and
   analyses from organizations across the spectrum (for example GBPI and the Georgia Public Policy
   Foundation). Label where each organization stands.
7. **Neutral context:** state agency reports and nonpartisan research.
8. **News coverage** for timeline and context. Use it as secondary support only, never as the only source
   for a fact.

For each source, record the id, title, publisher, **type** (`primary-government`, `secondary-research`,
`advocacy`, `news`, `local-government`), URL, status and why it's needed.

Rules added after Trial 001:

- **Search-engine summaries are for discovery only.** Never cite them, and never write a fact from them.
- **Write a discrepancy list (D1, D2, …)** wherever reports disagree (dates, votes, committees,
  exceptions). Settle each one from a primary source in step 2. If a local source conflicts with state
  law, explain the difference (for example, a district starting early) rather than picking one.
- **Produce a "hosts needed" list** for retrieval. A bill's evidence usually spans the legislature, the
  governor's office, agencies, universities, news sites and journals.

## Step 2: Snapshot

1. Download each source into `_raw/` (git-ignored) with `trials/tools/snapshot.py`. It saves the exact
   bytes, the response headers, the visible text, and prints the `sha256` and `retrieved_at`.
2. **legis.ga.gov:** find the bill with the public search form in a headless browser (Session 2025-2026
   = id 1033; "Doc. Number" field). Load `legislation/<id>` and save the JSON the page itself requests
   (`api/legislation/detail/<id>`). Version PDFs download directly from
   `api/legislation/document/<session>/<versionId>`. Don't copy the site's hidden API key into our code.
3. If a site returns a bot check instead of content, re-load it in a headless browser and hash the
   rendered text. Record the `method`.
4. Record everything in `sources.yaml`, then run `verify_hashes.py`.
5. Request an Internet Archive capture where allowed, and record the archive URL.
6. If a source can't be fetched, mark it `not-retrieved` or `rejected` with the reason. **Never
   substitute another document.**

**Bill PDFs: strike-through and underline matter.** Plain text extraction merges deleted and inserted
words ("restricted prohibited"). Run `pdf_markup.py`, which writes the default markup, `--clean` (as
enacted) and `--old` (prior law). Spot-check one rendered page visually.

## Step 3: Extract passages

Choose passages in `passages.spec.yaml` (source, file, locator, exact start and end words). Then run
`build_passages.py` to copy the spans mechanically, so there are no typing errors.

1. Split each source into numbered passages: `P-001` and onward, unique across the trial.
2. Use natural units: a bill section or subsection, a table row, or a paragraph.
3. Record the source id and a locator (page and line, or section number) for each passage.
4. Government records may be copied in full. For copyrighted analyses, copy only the passages you will
   cite, keeping each excerpt short.
5. Do not edit passage text. Fix extraction glitches only, and note the fix.

## Step 4: Draft

Follow the [content-style-guide](../../product/content-style-guide.md) and the
[experience template](../../product/experience.md):

1. **30-second version** (3 lines).
2. **Layer 1: Participate.** What opportunities existed or exist, with dates. For a past session, this is
   written as "what happened".
3. **Layer 2: Decision.** At most 80 words, plus at most 5 "what would change" bullets.
4. **Layer 3: Options.** Include the status quo. For each option: who benefits, who pays, what's
   uncertain.
5. **Layer 4: Arguments.** Documented arguments first, with speaker, venue, date and a verbatim quote.
   Anticipated arguments are labeled and unattributed. Each card has the claim, what supports it, what's
   missing, what challenges it and what's uncertain. Mark the claim type.
6. **Layer 5: Evidence.** Every source with a type badge, capture date, hash prefix and "why this source".

Every sentence ends with a passage citation `[P-###]` or a tag (`{analysis}`, `{editorial}`,
`{computed: …}`).

## Step 5: Verify (deterministic checklist → `verification.md`)

- [ ] Every sentence is cited or tagged (`trials/tools/check_citations.py`)
- [ ] Every quoted span appears verbatim in its cited passage
- [ ] Every number and date appears in a cited passage, or is `{computed}` with the arithmetic shown
- [ ] Every cited passage id exists
- [ ] Every source in `sources.yaml` has a sha256, and the file in `_raw/` matches it
- [ ] Reading grade for the 30-second version and Layers 1–3 is 6–8, with outliers listed
- [ ] Jargon watch-list terms are defined or removed
- [ ] Loaded-word list scan is clean outside quotes
- [ ] Structural symmetry: every option has the same fields, and its strongest case for and against
- [ ] Length budgets are met
- [ ] "Where I'm least sure" lists the claims most likely to be wrong, and why

## Step 6: Independent verification (no human in the loop)

Run the [verification protocol](../verification-protocol.md):
1. **Planted-error copy.** Make a copy of `workspace.md` with 10+ hidden errors of varied types. Move
   the answer key outside the repository while verifiers run.
2. **Five fresh agents in parallel:**
   - a blind source check on `workspace.md`;
   - a blind source check on the planted-error copy;
   - a supporter's advocate;
   - a critic's advocate;
   - a standards editor with a flip test.

   Each reads only the page and `passages.md`.
3. Run `check_verifier.py` on each source-check report: grounding of every evidence span, then the catch
   rate against the key.
4. Triage every flag. Fix the text, or show with passage text why the flag is wrong. Log both.
5. Re-run the deterministic gate and a fresh blind source check on the revised text.
6. Write the results into `verification.md` and `verification/summary.json`.

Then render the prototype (`render_prototype.py`) so the owner can give feedback on the user experience
(`review.md`).

## Step 7: Comprehension check

When a usability round is scheduled, run the [comprehension tasks](../../product/usability-testing.md)
on this workspace.

## Step 8: Retrospective (`log.md`)

- Record the time per step, what was hard, and any failures.
- Record the rules learned, then update this runbook, the style guide and the policies. Bump the runbook
  version.
- List automation candidates and what would be needed for each to pass the gate.

## History

| Version | Date | Change | Trial |
|---|---|---|---|
| v0 | 2026-11-08 | Created from research | — |
| v0.1 | 2026-11-08 | Step 1: search summaries are discovery-only; discrepancy list; "hosts needed" list | [001](../../../trials/001-hb1009-high-school-cellphones/log.md) (steps 0–1) |
| v0.2 | 2026-11-08 | Step 2: snapshot tool, legis.ga.gov browser method, bot-check fallback, strike/underline extraction. Step 3: passage spec + builder. Step 6: AI-only verification protocol replaces human review | [001](../../../trials/001-hb1009-high-school-cellphones/log.md) (steps 2–6) |
