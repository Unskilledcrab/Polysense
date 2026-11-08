# Runbook: decision workspace (manual), v0.6

**Purpose:** turn one public decision into a clear, fully cited decision workspace (the 30-second version
plus Layers 1–5) by hand. **Output:** `trials/NNN-<slug>/`. Written for bills (Trial 001), then adapted for
a regulatory case (Trial 002) and a county budget (Trial 003); see "Other kinds of decisions" below.

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
   analyses from organizations with different views (for example GBPI and the Georgia Public Policy
   Foundation). Record where each one stands on *this decision*, not an ideological label.
7. **Neutral context:** state agency reports and nonpartisan research.
8. **News coverage** for timeline and context. Use it as secondary support only, never as the only source
   for a fact.

For each source, record the id, title, publisher, **type** (`primary-government`, `secondary-research`,
`advocacy`, `news`, `local-government`, `party-filing` for any party's filing in a contested case), URL, status and why it's needed.

Rules added after Trial 001:

- **Search-engine summaries are for discovery only.** Never cite them, and never write a fact from them.
- **Write a discrepancy list (D1, D2, …)** wherever reports disagree (dates, votes, committees,
  exceptions). Settle each one from a primary source in step 2. If a local source conflicts with state
  law, explain the difference (for example, a district starting early) rather than picking one.
- **Produce a "hosts needed" list** for retrieval. A bill's evidence usually spans the legislature, the
  governor's office, agencies, universities, news sites and journals.
- **Count documented voices on each side before drafting.** Trial 001's sources quoted four officials and
  a student for the law but only one named critic, so every concern was raised by an ordinary person and
  answered by officials. If one side is thin, search for more before drafting: members who voted no,
  amendment sponsors, organizations, and teacher, parent or student groups. If none can be found, say so
  on the page.

## Step 2: Snapshot

1. Download each source into `_raw/` (git-ignored) with `trials/tools/snapshot.py`. It saves the exact
   bytes, the response headers, the visible text, and prints the `sha256` and `retrieved_at`.
2. **legis.ga.gov:** find the bill with the public search form in a headless browser (Session 2025-2026
   = id 1033; "Doc. Number" field). Load `legislation/<id>` and save the JSON the page itself requests
   (`api/legislation/detail/<id>`). Version PDFs download directly from
   `api/legislation/document/<session>/<versionId>`. Don't copy the site's hidden API key into our code.
3. If a page needs JavaScript to show its content, load it in a headless browser and hash the rendered
   text; record the `method`. An anti-bot challenge, a 403 or a paywall is a **block**: record it and move
   on, never work around it. Retry once, after a pause, for a 404 or 5xx error, and keep both responses.
4. Record everything in `sources.yaml`, then run `verify_hashes.py`.
5. Request an Internet Archive capture where the session allows it, and record the archive URL. If the
   request is denied, record that.
6. If a source can't be fetched, mark it `not-retrieved` or `rejected` with the reason. **Never
   substitute another document.**

**Other file types.** Word, Excel, PowerPoint, zip and PDF filings: extract their text with
`trials/tools/extract/`. A scanned page with no text layer can't become a passage (no OCR tool is approved);
read it by eye only if needed, record that, and don't state its contents as facts. Public versions of
filings are often **redacted**, and the text layer silently drops blacked-out words. If a passage reads as
if words are missing, render the page to an image and look for black bars; mark a redaction with a `fix`
and a note ("[blacked out in the public version]").

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
6. Official pages can carry party labels or family details. Choose spans that leave them out.

## Step 4: Draft

Follow the [content-style-guide](../../product/content-style-guide.md) and the
[experience template](../../product/experience.md):

1. **30-second version** (3 lines, ≤ 90 words). Name who decides and say in a few words what that body is.
2. **What it means for you.** 2–4 lines that start "If you …" (style guide, section 9). For a decision
   that moves money, add `### Where the money comes from` (≤ 5 numbered steps), and for every decision
   `### Decided now, and decided later` and `### Not known yet` (each item says why, dated if it can change).
3. **Layer 1: Participate.** `### What you can do` (the reader's own powers: Decide only for a direct vote),
   `### Who decides, and how to reach them` (every decider listed the same way, with public contact routes
   from the sources), `### Coming up`, `### What happened`. For a past session, the last is "what happened".
4. **Layer 2: Decision.** At most 80 words, plus at most 5 "what would change" bullets.
5. **Layer 3: Options.** An intro of ≤ 60 words, then `### If you care most about…`: one values line for
   each side with a documented argument, in card order, built only from a verbatim quote (style guide,
   section 9). Then the
   options, including the status quo. For each option: who benefits, who pays, what's uncertain.
6. **Layer 4: Arguments.** Documented arguments first, with speaker, venue, date and a verbatim quote.
   Anticipated arguments are labeled and unattributed. Every card uses the same fields in the same order
   (In short · Who says it · What supports it · Who disagrees · What challenges it · What's missing ·
   What's uncertain) and follows the one-bar rules in the style guide, section 8. "In short" leads with
   what the argument would mean for people (≤ 40 words). Mark the claim type
   and the evidence label, and print the labeling rule above the cards.
7. **Layer 5: Evidence.** Every source with a type badge, capture date, hash prefix and "why this source".

Every sentence ends with a passage citation `[P-###]` or a tag (`{analysis}`, `{editorial}`,
`{computed: …}`). Reader-facing strings in `page.yaml` (title, link text, chips) are content too: keep
them plain, and put the supporting passages in a comment (`title: "…"  # [P-013, P-144]`).

### Other kinds of decisions

The steps above hold for every decision; the records and some labels change. Trials 002 and 003 used:

| | Bill | Regulatory case (PSC) | Local budget (county) |
|---|---|---|---|
| **Core record** | Bill versions, status, votes | Opening order, scheduling order, filing list, sworn testimony, corrections, motions, transcripts, briefs | Code sections and amending resolutions, agenda items, roll calls, budget books, notices |
| **Who decides** | Legislature, governor | The commissioners, at a set session | The board, by a set deadline |
| **How people take part** | Hearings, contacting members | Public witness, written comment before the vote | Public comment at meetings, the budget hearing |
| **Common formats** | PDF with strike/underline | Word, zip, Excel, scans | PDF, PowerPoint, meeting video |

What carried over from those trials, to use with judgment:
- **Decisions still ahead.** Say what isn't published yet as what each page showed on a date, and keep a
  dated status table in the README. Set re-check dates before each scheduled step. Before quoting a number
  from testimony, look for later corrections.
- **Who decides** goes in Layer 1 when the decision is still ahead. Options still ahead use "Where it
  stands" in place of "What happened".
- **More than two sides.** Label each card by the position it favors, count voices per question, and name
  alliances that cross.
- **Parties' own numbers.** Attribute every estimate, give every party's figures the same caveats, and
  treat a blacked-out number as unknown, not zero. Say so when only one option has public numbers. Every
  party's filing has the same source type (`party-filing`), including a regulator's own advocacy staff.
- **Votes and reasons.** Roll calls show how members voted, not why. If reasons may be in meeting video you
  didn't review, say that rather than "no reasons were given".

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
- Review the rules this trial touched against the tenets: keep, simplify or retire them
  ([how rules change](../prove-then-automate.md#how-rules-change)).
- List automation candidates and what would be needed for each to pass the gate.

## History

| Version | Date | Change | Trial |
|---|---|---|---|
| v0 | 2026-11-08 | Created from research | — |
| v0.1 | 2026-11-08 | Step 1: search summaries are discovery-only; discrepancy list; "hosts needed" list | [001](../../../trials/001-hb1009-high-school-cellphones/log.md) (steps 0–1) |
| v0.2 | 2026-11-08 | Step 2: snapshot tool, legis.ga.gov browser method, bot-check fallback, strike/underline extraction. Step 3: passage spec + builder. Step 6: AI-only verification protocol replaces human review | [001](../../../trials/001-hb1009-high-school-cellphones/log.md) (steps 2–6) |
| v0.4 | 2026-11-08 | Generalized from bills to decisions: blocks vs. pages that need JavaScript, one retry for 404/5xx, text extractors for other file types, passages that leave out party labels, and "Other kinds of decisions" (records by kind, decisions still ahead, more than two sides, parties' own numbers, votes and reasons) | [002](../../../trials/002-psc-57171-fuel-credit/log.md), [003](../../../trials/003-fulton-2027-budget/log.md) |
| v0.6 | 2026-11-08 | Step 4: reader-first content contract (What it means for you; Who decides, and how to reach them; values guide; plain page.yaml strings) | Usability feedback |
| v0.5 | 2026-11-08 | Redactions: the text layer drops blacked-out words; check a rendered page and mark them | [002](../../../trials/002-psc-57171-fuel-credit/verification.md) (round 5) |
| v0.3 | 2026-11-08 | Step 1: count documented voices per side before drafting. Step 4: follow the argument-card rules in the [style guide](../../product/content-style-guide.md) (section 8) | [001](../../../trials/001-hb1009-high-school-cellphones/verification.md) (verification rounds 1–3) |
