# 0012: Prove each step by hand, then automate

- **Status:** Accepted
- **Date:** 2026-11-08

## Context

The owner wants the process proven before it is automated. Automating a process we don't yet understand
would hard-code mistakes, and without a manual baseline we couldn't measure the automation.

## Decision

- The AI agent performs every future pipeline step **by hand first**, from a written runbook, with the
  owner as reviewer. Outputs go in `trials/NNN-slug/` and become gold data.
- A step is automated only after it passes the **automation gate**:
  1. done by hand at least 3 times;
  2. fixed input and output format;
  3. quality criteria written and met;
  4. failure modes catalogued;
  5. automation matches or beats the manual baseline on an eval set built from the trials.
- New automation runs in **shadow mode** first.
- Order of automation: mechanical steps first, AI drafting last.
- Never automated: the human support check, publishing approval, voting logistics, and coverage
  decisions.

Details: [prove-then-automate.md](../process/prove-then-automate.md).

## Consequences

Slower to automate, faster to correct content. Private alpha content can be hand made. The trials create
the eval data the AI pipeline needs anyway.
