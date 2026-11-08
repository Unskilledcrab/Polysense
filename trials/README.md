# Manual trials

Each folder is one **manual, end-to-end run** of a process we plan to automate later. The AI agent does
the work by hand from a runbook in [docs/process/runbooks/](../docs/process/runbooks/), and the project
owner reviews it. Reviewed trials become the **gold data** that automation must match. See
[prove-then-automate.md](../docs/process/prove-then-automate.md).

| Trial | Process | Subject | Status |
|---|---|---|---|
| [001](001-hb1009-high-school-cellphones/) | A. Bill workspace (#1) | HB 1009 (2026), high school "bell-to-bell" personal device ban | Steps 0–5 done. Verification round 1 done: planted-error catch rate 13/13; bias panel failed, then revised. Round 2 in progress. Prototype in [prototypes/hb1009/](../prototypes/hb1009/) |

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
