# Verification protocol (AI-only, proof of concept)

**Decision ([ADR 0014](../adr/0014-ai-only-verification.md)):** during the proof of concept, no human sits
in the verification loop. The project owner gives feedback on the **user experience** only. Fact-checking
and bias-checking are done by this protocol. It must **prove itself with measurements**, never on trust. Like the other process docs, it is working
practice from the trials and will change as they show what works.

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
| 2 | **Blind source check** | An independent agent with fresh context. It sees only the workspace, its home-page items and the passages, never the drafter's notes | Unsupported or overstated claims, misattributions, wrong spans, subgroup vs. population mix-ups, timing words ("by" vs. "on"), reversed findings, bad arithmetic | Every flag resolved (text fixed, or flag shown wrong with evidence); 0 ungrounded evidence spans (`check_verifier.py`) |
| 3 | **Bias panel** | Three independent agents: a **supporter's advocate**, a **critic's advocate** and a **neutral standards editor** (who also runs the flip test) | Missing strongest arguments, unfair wording, unequal verbs or space, labels that don't follow the evidence, false balance, named minors, rule breaks | Both advocates rate their side **≥ 4/5**; no unresolved high-severity issue; every change logged |
| 4 | **Seeded-error calibration** | A second blind source verifier on a copy with hidden planted errors (numbers, dates, scope, attribution, reversed findings, wrong spans, dropped qualifiers, subgroup stats, arithmetic, loaded words) | Measures the verifier itself | Catch rate **≥ 90%** with 0 ungrounded spans; misses analyzed and the prompt or tools improved |
| 5 | **Re-verify after fixes** | Layers 1–3 re-run on the revised text with fresh agents. The **whole** bias panel re-rates, not only the side that complained | Errors introduced while fixing; overcorrection toward the other side (Trial 001's round 2 failed this way) | Same pass conditions as layers 1–3 |

**Reader-first pages (style guide v0.6) add three checks to the same rounds:**

- **Display strings are content.** The title, link text and chips in `page.yaml` go to the blind source
  check with the page and home items, and the first sentence of each "What it means for you" line is
  checked as if read alone. Conditions ("if the PSC adopts …") and direction words must survive.
- **The values guide** ("If you care most about…") is checked by every panelist. The source check confirms
  each value is faithful to its verbatim quote and speaker. Each advocate checks that its side's line is its
  strongest documented value, at equal length and appeal. The editor runs the flip test on the lines, and a
  recommendation word ("fits", "best", "should", "choose") is high severity.
- **A lay-reader check (experimental, not a pass condition).** A fresh agent with no background, playing an
  ordinary resident, reads only what a phone shows on the Summary, Options and Arguments tabs and answers:
  what is decided and by whom; does it raise or lower my costs, by how much and when (or why that isn't
  known); where the money goes; what I can do by when; what the evidence labels mean; one argument each way;
  which terms I couldn't place; whether anything seemed to favor a side. It is graded against a key written
  before the run. Terms it can't place get wording fixes. It stands in for people between usability rounds;
  it never replaces them.

Home-page items are the most-read lines, so they get the same source check as the page. They were added in
round 3 of Trials 002 and 003, and the first check found a public-comment deadline shown without its time.

Results go in the trial's `verification.md` and `verification/` folder: raw JSON reports, the mutated
copy, the answer key (`mutation-key.json`, moved out of reach during the run) and the summary used by the
prototype's "How we checked" page.

## When a page is done

Adversarial checkers always find something more. In Trial 001, every round produced new medium and low
suggestions, and some reversed the previous round's (one advocate asked to remove a line the other had
asked to add). So "done" is defined in advance:

1. **A round passes** when: the gate is clean; the blind source check finds 0 unsupported claims, 0 wrong
   spans and 0 ungrounded evidence spans; both advocates rate their side at least 4/5; and the editor
   finds no high-severity issue.
2. **After a passing round, fix only accuracy flags and breaches of a written rule:** "partial" verdicts,
   questionable tags, wording that misstates a passage, and anything that breaks the style guide or
   CLAUDE.md (a loaded word, a subgroup figure before its base rate, undefined jargon). A **targeted
   re-check** confirms the fixes: a fresh blind agent checks only the changed sentences against the
   passages. The whole panel re-runs only if a fix changes a card's claim, its evidence label, or which
   side a point supports. Narrowing the words that say what a label part covers, so they match the source,
   without changing its grade ("Hearings held: Supported" to "Hearings scheduled: Supported" when the
   source is a notice) is an accuracy fix, not a label change. If a fix changes a values-guide line or an "In short" line, the targeted re-check
   also runs the editor's flip test on just those lines, since a source check can't judge balance. If a
   targeted re-check flags something, fix it and re-check only those units again.
   Stop when a re-check finds nothing that misstates a passage; its style notes become open items. (Trial 003
   took three passes, whose findings went 6, 3, 0.)
3. **Remaining medium and low panel suggestions become open items.** They are listed in `verification.md`
   and on the page's "How we checked" view, and they feed the next revision, which starts a new full
   round. Suggestions that contradict each other are logged side by side, not silently resolved.
4. The page is then labeled **"Passed our checks"**, never "verified" in general. The label links to the
   page's "How we checked" view, which gives the round number and the open items. (Lay readers couldn't
   place "Passed checks (round 6)", so the round moved off the label.)

**When only the editor blocks a round.** If both advocates already rate their side 4 or more and the round
fails only on the editor's high-severity issues, the next revision fixes only those issues and the accuracy
flags. Broad edits at that point tend to unsettle a balance both sides now accept. Trial 001 (round 3) and
Trial 002 (round 4) each passed the next round this way.

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
