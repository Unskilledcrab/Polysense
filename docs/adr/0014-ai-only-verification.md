# 0014: AI-only verification during the proof of concept (no human in the loop)

- **Status:** Accepted
- **Date:** 2026-11-08
- **Amends:** [0008](0008-ai-drafting-and-verification.md) (human support check) and [0012](0012-prove-then-automate.md) (human reviewer in trials)

## Context

The first plan used human review: the owner checked trial output, a paid second reviewer was required
before public beta, and a "two-person rule" applied to named speakers and elections. The owner rejected
this for the proof of concept:

> "don't rely on a human. actually putting a human in the loop would be anti what we are trying to do.
> You should have your own way of fact checking and ensuring that we are aligned with the goals of being
> non bias"

The owner's role is to view the product and give feedback on the **user experience**.

## Decision

- Verification in trials is done by the [verification protocol](../process/verification-protocol.md):
  1. a deterministic gate;
  2. a blind, independent source verifier whose evidence spans are machine-checked;
  3. a bias panel (supporter's advocate, critic's advocate, standards editor with a flip test);
  4. seeded-error calibration that measures the verifier's catch rate;
  5. re-verification after fixes.
- **"Model agreement is not verification" still holds.** What counts is:
  - **source-grounded checks with machine-checkable evidence;**
  - a **measured** error-catching rate.
- The "two-person rule" becomes **two independent verification passes**: source check plus bias panel,
  each by agents with fresh context.
- The owner reviews the **experience** (prototype pages), not the facts.

## Consequences

- The process can run end to end without a person, which is what the product vision requires.
- Trust has to be earned with published numbers. Each trial publishes:
  - verifier catch rates;
  - flags found and fixed;
  - panel ratings.
- **Risks** (tracked in [risks.md](../risks.md)):
  - AI checkers may share blind spots with the AI drafter;
  - defamation and IRS exposure for published content remain;
  - the public challenge process becomes the main outside check after launch.
- **Revisit before public launch** with the measured data from at least 3 trials and legal advice.
  Adding human review then would be a new ADR, not a silent change.
