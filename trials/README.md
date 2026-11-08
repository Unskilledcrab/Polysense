# Manual trials

Each folder is one **manual, end-to-end run** of a process we plan to automate later. The AI agent does
the work by hand from a runbook in [docs/process/runbooks/](../docs/process/runbooks/), and the project
owner reviews it. Reviewed trials become the **gold data** that automation must match. See
[prove-then-automate.md](../docs/process/prove-then-automate.md).

| Trial | Process | Subject | Status |
|---|---|---|---|
| [001](001-hb1009-high-school-cellphones/) | A. Bill workspace (#1) | HB 1009 (2026), high school "bell-to-bell" personal device ban | **Passed checks (round 4)**, revision 5. Planted-error catch rate 13/13. Owner reviewed the experience. Prototype in [prototypes/hb1009/](../prototypes/hb1009/) |
| [002](002-psc-57171-fuel-credit/) | A. Decision workspace (adapted to a regulatory case) | PSC Docket 57171: how Georgia Power credits data-center-heavy Real-Time Pricing revenue against fuel costs (vote 2026-12-15) | **Not yet passed** after 3 rounds. The fact-check found 0 unsupported claims in rounds 2 and 3, but the fairness panel is at 3 and 4 of 5 (needs 4 and 4). Revision 4 in progress. Planted-error catch rate 12/12 |
| [003](003-fulton-2027-budget/) | A. Decision workspace (adapted to a county budget) | Fulton County 2027 budget, and the dispute over how it is being prepared (proposal due 2026-11-15) | **Passed checks (round 3)**, revision 3.3, after accuracy fixes and three targeted re-checks. Planted-error catch rate 12/12. Must be revised when the proposal is posted (due by 2026-11-15) |

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
