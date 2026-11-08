# Review desk handbook

Reviewers are the last line of defense. AI and automated checks prove only that a citation **exists**.
Only a person can confirm that it **supports** the sentence.

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

## Two-person rule

These need a **second reviewer** before publication:
- anything that names a real speaker (documented arguments);
- anything about elections or voting;
- every correction.

Until the project has a second reviewer, such content is **not published**.

## Recording

Fill in `review.md` (one verdict per citation, clarity scores, fairness notes, time spent). In the
application, these become Review rows tied to the revision being approved.

## Calibration

Once a month, two reviewers independently check 50 random citations. The target is at least 95%
agreement. Disagreements become examples in this handbook.
