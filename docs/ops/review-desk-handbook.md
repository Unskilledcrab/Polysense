# Review desk handbook

This is the rubric every verifier applies. During the proof of concept, the verifiers are the independent
AI agents of the [verification protocol](../process/verification-protocol.md), with no human in the loop
([ADR 0014](../adr/0014-ai-only-verification.md)). A citation that merely **exists** proves nothing. The
verifier must confirm that it **supports** the sentence, and quote the words that do.

## What "supports" means

For each sentence with a citation, read the cited passage in the archived snapshot and choose one verdict:

| Verdict | Meaning | Action |
|---|---|---|
| **Supports** | The passage states this fact, or it follows directly without outside knowledge | Accept |
| **Partial** | The passage supports part of it, or the sentence is stronger or broader than the passage | Narrow the sentence, or add a citation |
| **Does not support** | The passage is related but doesn't establish the claim | Rewrite, re-cite or remove |
| **Wrong span** | Another passage does support it, but the wrong one is cited | Fix the citation |

Common traps:
- An **old version** of the bill cited when a substitute changed the text.
- "**May**" turned into "will".
- A **fiscal-note estimate** presented as a certainty.
- A **number** whose unit or year shifted (annual vs. total, fiscal year vs. calendar year).
- An **advocacy claim** presented as fact.
- A **quote** trimmed in a way that changes its meaning.

## Other checks

- **Tagged sentences.**
  - `{editorial}` ("what's missing", "what's uncertain"): is it fair, and is it actually true that the
    sources don't address this?
  - `{computed}`: redo the arithmetic.
  - `{analysis}`: does it follow from the cited facts?
- **Fairness.** Does every option get the same fields and its strongest case? Would someone on each side
  call their side's argument fairly stated? Is there false balance (equal weight for unequal evidence)?
- **Clarity.** Score each layer 1–5. Could a busy reader get the 30-second version in 30 seconds?
- **Status and dates.** Do status, dates and actions match the official record as of the capture time?

## Two independent passes

These need **two independent verification passes** (a blind source check and the bias panel, each run by
agents with fresh context) before publication:
- anything that names a real speaker (documented arguments);
- anything about elections or voting;
- every correction.

## Recording

Verifier reports are saved as JSON in the trial's `verification/` folder. `check_verifier.py` confirms
every evidence span against the passages. In the application, these become Review rows tied to the
revision being approved.

## Calibration

Every trial runs a verifier on a copy with hidden planted errors, and the catch rate is recorded. The
target is at least 90% with no ungrounded evidence spans. Misses become new examples in this handbook,
new planted-error types, or prompt and tool fixes.
