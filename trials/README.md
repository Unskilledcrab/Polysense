# Manual trials

Each folder is one **manual, end-to-end run** of a process we plan to automate later. The AI agent does
the work by hand from a runbook in [docs/process/runbooks/](../docs/process/runbooks/), and the project
owner reviews it. Reviewed trials become the **gold data** that automation must match. See
[prove-then-automate.md](../docs/process/prove-then-automate.md).

| Trial | Process | Subject | Status |
|---|---|---|---|
| [001](001-hb1009-high-school-cellphones/) | A. Bill workspace (#1) | HB 1009 (2026), high school "bell-to-bell" personal device ban | **Passed checks**, revision 6.4: 292 of 292 sentences checked, gate passing. Prototype in [prototypes/hb1009/](../prototypes/hb1009/) |
| [002](002-psc-57171-fuel-credit/) | A. Decision workspace (adapted to a regulatory case) | PSC Docket 57171: how Georgia Power credits data-center-heavy Real-Time Pricing revenue against fuel costs (vote 2026-12-15) | **Passed checks**, revision 7.7: 560 of 560 sentences checked, gate passing, fairness review 4 of 5 with no high issue (4 medium notes open). Must be revised after the Dec 2 briefs and the Dec 15 decision |
| [003](003-fulton-2027-budget/) | A. Decision workspace (adapted to a county budget) | Fulton County 2027 budget, and the dispute over how it is being prepared (proposal due 2026-11-15) | **Passed checks**, revision 5.4: 459 of 459 sentences checked, gate passing, fairness review 4 of 5 with no high issue (2 medium notes open). Must be revised when the proposal is posted (due by 2026-11-15) |

The status column is a summary. The current record for each trial is its generated `status.json`
(`trials/tools/status.py`), which lists the gate results, the sentence counts and the open fairness notes.

## What checking costs now (2026-11-13)

The reader-first cycle used about 13.5 million tokens for three pages, nearly all on the top tier. The lean flow
in the [verification protocol](../docs/process/verification-protocol.md) replaced it. Measured on these three pages:

| Step | Last cycle | Lean flow |
|---|---|---|
| Follow-up check of changed sentences, three pages | about 750k, top tier | about 110k, then 86k and 83k, on Sonnet |
| Planted-error test | about 1.1M, top tier | about 150k on Sonnet (52 of 52 caught, 0 false alarms on 60 clean sentences) |
| Fairness review, two pages | two advocates and an editor per round | one reviewer, about 98k on Sonnet |
| Building and qualifying the new tools, whole cycle | | about 1.33M delegated tokens (Haiku about 702k, Sonnet about 630k), none on the top tier |

**Known limit.** Given only a sentence and its cited passages, Sonnet re-found 9 of 55 subtle overstatements
that the top tier had flagged, and Haiku 35 of 55 with 10 false alarms in 60. So a new page still gets one
whole-page read at first publication.

## What the three trials showed (2026-11-08)

| | 001 HB 1009 (a law) | 002 PSC 57171 (a five-party utility case) | 003 Fulton budget (a county decision still ahead) |
|---|---|---|---|
| Full rounds to pass | 4 | 6 | 3 |
| Unsupported claims found | 0 in every round | 1 in round 1, then 0 | 0 in every round |
| Planted errors caught | 13 of 13 | 12 of 12 | 12 of 12 |
| Follow-up re-checks (findings) | 2 | 3 (6, 1, 0) | 3 (6, 3, 0) |
| What failed rounds | Uneven cards; then over-correction | Missing points per side; one-sided numbers; one summary line; labels | Label rules; then a lean toward the current process |

- **Accuracy was the easy part.** After round 1, no fact-check found an unsupported claim in any trial; the
  rounds were spent on fairness and clarity.
- **Fairness converges, slowly for complex cases.** Each round's findings moved from missing arguments, to
  structure (whose numbers are shown, who gets the last word), to single lines. A narrow revision passed
  each time only the editor's issue remained.
- **Checking the checker works.** The fact-checker caught all 37 planted errors across the three trials.
- **What the trials changed:** the style guide (v0.5), the runbook (v0.5), the verification protocol's
  "when a page is done" rule, and about ten fixes to the tools and the prototype, each logged with the trial
  that taught it.

## Folder layout

```
NNN-slug/
  README.md         what was attempted, why (rubric scores), status, how to resume
  sources.yaml      every source: id, type, URL, status, retrieved_at, sha256, archive URL
  passages.md       numbered passages (P-###) with locators
  workspace.md      the output (every sentence cited or tagged)
  verification.md   verification protocol results + dispositions of every finding
  verification/     raw verifier reports, planted-error copy and answer key
  review.md         the owner's feedback on the user experience
  log.md            step-by-step log: time, friction, failures, rules learned
  _raw/             downloaded source files (git-ignored; hashes live in sources.yaml)
```

## Tools

See [tools/README.md](tools/README.md).
