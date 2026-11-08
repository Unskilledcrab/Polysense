# Prove by hand, then automate

**Rule:** no step of the pipeline is automated until it has been done by hand, written down, measured and
shown to produce correct, clear output. The manual runs become the **gold data** that any automation must
match or beat.

## Why

- **Automating a process we don't understand hard-codes our mistakes.** A manual run shows where the real
  difficulty lies: messy sources, ambiguous claims, wording that confuses readers.
- **The manual outputs become the evaluation set.** We can measure automation against something real
  instead of trusting it.
- **For a solo builder, a proven manual process is useful on day one.** Private alpha content can be hand
  made while automation catches up.

## Who does the manual work

The **AI agent working in a session** (for example, Claude Code) does each step by hand, following the
runbook, as a person would. It uses only the tools a person would: fetching documents, reading them,
writing passages and drafts, and running small check scripts.

**Verification has no human in the loop** ([ADR 0014](../adr/0014-ai-only-verification.md)). The
[verification protocol](verification-protocol.md) checks the output with:
- a deterministic gate;
- a blind, independent source verifier whose evidence spans code confirms;
- a bias panel;
- seeded-error calibration that measures how well the verifier catches planted errors.

The project owner gives feedback on the **user experience** of the resulting pages.

## Trials

Each trial is a folder, `trials/NNN-slug/`:

| File | Contents |
|---|---|
| `README.md` | What was attempted and why (for a bill, the rubric scores), status, links |
| `sources.yaml` | Each source: id, title, publisher, type, URL, `retrieved_at`, `sha256`, archive URL, license note |
| `passages.md` | Numbered passages `P-###` with page/paragraph locators. Full text only for public government records; short cited excerpts for copyrighted works |
| `workspace.md` (or the trial's output) | The product output, with every sentence cited or tagged |
| `verification.md` | Checklist results and "where I'm least sure" |
| `verification/` | Raw reports from the verification protocol (source check, bias panel, calibration) and a summary |
| `review.md` | The owner's feedback on the user experience |
| `log.md` | Every step with time taken, friction, failures, rules learned and automation ideas |

Raw downloaded files go in `trials/NNN-slug/_raw/`, which git ignores. Their hashes live in
`sources.yaml`, so anyone can re-download a file and check it.

### Planned trials

| Trial | Done by hand | Becomes |
|---|---|---|
| **A. Bill workspace ×3**: #1 a clear everyday-impact bill; #2 budget/fiscal-heavy; #3 contested/values-heavy | Find and snapshot sources, extract passages, write the 30-second version and Layers 1–5 with citations, verify, user review, comprehension check | Drafting runbook, style guide, prompts, eval set for the AI pipeline |
| **B. Hearing calendar** | Compile one week of 2026 hearings into the Opportunity format | Ingestion spec and test fixture for the hearings feed |
| **C. District lookup** | Public landmark addresses (the Capitol, libraries, courthouses; never private homes) checked against official maps | Gold test set for geocoding and point-in-polygon |
| **D. Challenge → correction** | Simulate a challenge and handle it under the policy until it becomes a revision and correction notice | Corrections workflow runbook |

## The automation gate

A step may be automated **only when all five are true**:

1. **Repeated.** It has been done by hand **at least 3 times** from a written runbook.
2. **Fixed format.** Its inputs and outputs have a defined, stable format.
3. **Quality defined and met.** Written quality criteria exist, and the manual runs meet them.
4. **Failures known.** Failure modes are listed, each with a way to detect it.
5. **Measured against gold.** An eval set built from the manual runs exists, and the automated version
   matches or beats the manual baseline on it.

Record each pass in the step's runbook ("Automation gate: passed on YYYY-MM-DD, evidence: …").

## Shadow mode

New automation first runs **beside** the manual process for a while, with nothing published from it.
Compare its output with the manual output every time. Promote it only when it meets the bar consistently,
and keep the manual runbook as the fallback.

## Order of automation

Mechanical, low-judgment steps come first. Judgment comes last.

1. Fetching and snapshotting sources (with hashes), and importing structured legislative data
2. District lookup
3. Passage extraction
4. Hearing and calendar feeds
5. Deterministic checks (citation coverage, verbatim quotes, numbers, readability, loaded words)
6. **AI drafting**: last, in shadow mode, and never without a human support check

## What is never skipped (even once automated)

- The **independent support check**: does each passage actually support its sentence? It is done by a
  verifier separate from the drafter, with machine-checked evidence spans.
- **Calibration.** No verifier is trusted without a measured catch rate on planted errors.
- **Voting logistics.** Always official links, never generated.
- **Coverage decisions are logged** with their rubric scores and reasons.
