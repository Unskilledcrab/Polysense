# Verification protocol (AI-only, proof of concept)

**Decision ([ADR 0014](../adr/0014-ai-only-verification.md)):** during the proof of concept, no human sits
in the verification loop. The project owner gives feedback on the **user experience** only. Fact-checking
and bias-checking are done by this protocol. It must **prove itself with measurements**, never on trust.

Three ideas make it trustworthy without a human:

1. **Check against archived evidence, never against model memory.** Verifiers read only the workspace and
   its hashed passages. They have no web access and no outside facts.
2. **Make the verifier show its work in a form code can check.** Every "supports" verdict must quote the
   exact words from the passage that support the sentence. Code confirms those words exist, so a verifier
   can't invent support.
3. **Measure the verifier with planted errors.** Before trusting a verifier on real content, run it on a
   copy with known, hidden errors and count what it catches. "Several models agree" is never evidence.
   A **measured catch rate** is.

## The five layers

| # | Layer | Who or what | Catches | Pass condition |
|---|---|---|---|---|
| 1 | **Deterministic gate** | Code: `check_citations.py`, `verify_hashes.py`, `readability.py` | Uncited sentences, unknown passage IDs, non-verbatim quotes, numbers missing from sources, loaded words, hash mismatches, hard reading level | 0 errors; every warning explained |
| 2 | **Blind source check** | An independent agent with fresh context. It sees only the workspace and passages, never the drafter's notes | Unsupported or overstated claims, misattributions, wrong spans, subgroup vs. population mix-ups, timing words ("by" vs. "on"), reversed findings, bad arithmetic | Every flag resolved (text fixed, or flag shown wrong with evidence); 0 ungrounded evidence spans (`check_verifier.py`) |
| 3 | **Bias panel** | Three independent agents: a **supporter's advocate**, a **critic's advocate** and a **neutral standards editor** (who also runs the flip test) | Missing strongest arguments, unfair wording, unequal verbs or space, labels that don't follow the evidence, false balance, named minors, rule breaks | Both advocates rate their side **≥ 4/5**; no unresolved high-severity issue; every change logged |
| 4 | **Seeded-error calibration** | A second blind source verifier on a copy with hidden planted errors (numbers, dates, scope, attribution, reversed findings, wrong spans, dropped qualifiers, subgroup stats, arithmetic, loaded words) | Measures the verifier itself | Catch rate **≥ 90%** with 0 ungrounded spans; misses analyzed and the prompt or tools improved |
| 5 | **Re-verify after fixes** | Layers 1–2 re-run on the revised text | Errors introduced while fixing | Clean pass |

Results go in the trial's `verification.md` and `verification/` folder: raw JSON reports, the mutated
copy, the answer key (`mutation-key.json`, moved out of reach during the run) and the summary used by the
prototype's "How we checked" page.

## Independence rules

- Verifiers get **only** the workspace (or mutated copy) and `passages.md`. They are not given the log,
  notes, README, sources file, spec or any other report.
- The answer key for planted errors is **moved outside the repository** while verifiers run.
- Each verifier is a **new agent with fresh context**: no shared conversation with the drafter.
- Advocates argue for their side's **fair** representation and are told not to seek favoritism.
- The drafter may not mark a flag "resolved" without either changing the text or showing, with passage
  text, that the flag is wrong. Both are recorded.

## Verdicts (same definitions as the [review handbook](../ops/review-desk-handbook.md))

| Verdict | Meaning |
|---|---|
| **supports** | The passage states it, or it follows directly without outside knowledge. The verifier must quote the evidence span |
| **partial** | Part is supported, or the sentence is stronger, broader or more certain than the passage |
| **does_not_support** | Related but doesn't establish the claim |
| **wrong_span** | A different passage supports it |
| **valid / questionable / invalid** | For `{analysis}`, `{editorial}` and `{computed}` tags |

## What this protocol cannot do (stated openly)

- **It shares blind spots with the drafter.** Both are AI models. Calibration measures the verifier only
  on the error types we think to plant. Rotate and extend the mutation types every trial.
- **It can't judge what's missing from the record** beyond the passages it is given. The critic's and
  supporter's advocates partly cover this. Public challenges cover the rest after launch.
- **Advocates are simulations of viewpoints, not real constituents.** Real readers' challenges and the
  owner's feedback on the user experience are still needed. The published error rates make the
  limits visible.

## Automation status

The protocol is itself run **by hand** (agents launched in-session) under the prove-then-automate rule.
It becomes an automated pipeline stage only after it passes the automation gate: 3 trials with stable
inputs and outputs, a calibration catch rate of at least 90% on each, and documented failure modes.
