# Manual trials

Each folder is one **manual, end-to-end run** of a process we plan to automate later. The AI agent does
the work by hand from a runbook in [docs/process/runbooks/](../docs/process/runbooks/), and the project
owner reviews it. Reviewed trials become the **gold data** that automation must match. See
[prove-then-automate.md](../docs/process/prove-then-automate.md).

| Trial | Process | Subject | Status |
|---|---|---|---|
| [001](001-hb1009-high-school-cellphones/) | A. Bill workspace (#1) | HB 1009 (2026), high school "bell-to-bell" personal device ban | **Passed checks (round 4)**, revision 5. Planted-error catch rate 13/13. Owner reviewed the experience. Prototype in [prototypes/hb1009/](../prototypes/hb1009/) |
| [002](002-psc-57171-fuel-credit/) | A. Decision workspace (adapted to a regulatory case) | PSC Docket 57171: how Georgia Power credits data-center-heavy Real-Time Pricing revenue against fuel costs (vote 2026-12-15) | **Passed checks (round 6)**, revision 6.3, after accuracy fixes and three targeted re-checks. Five parties; the hardest trial so far (rounds 1 to 4 failed the fairness panel). Planted-error catch rate 12/12. Must be revised after the Dec 2 briefs and the Dec 15 decision |
| [003](003-fulton-2027-budget/) | A. Decision workspace (adapted to a county budget) | Fulton County 2027 budget, and the dispute over how it is being prepared (proposal due 2026-11-15) | **Passed checks (round 3)**, revision 3.3, after accuracy fixes and three targeted re-checks. Planted-error catch rate 12/12. Must be revised when the proposal is posted (due by 2026-11-15) |

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
  "when a page is done" rule, and six tool fixes, all logged with the trial that taught them.

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
