# How we use AI (draft)

**Short version:** AI drafts our pages from archived sources. Separate, independent AI checkers then
verify every sentence against those sources and review the page for bias. We measure how well the
checkers catch planted errors, and we publish those numbers. During the proof of concept no person sits
in this loop ([ADR 0014](../adr/0014-ai-only-verification.md)). Anyone can challenge anything we
publish.

## What AI may do

- **Draft** explanations and argument summaries, **only from our archived source passages**, with a
  citation on every factual sentence.
- **Classify** sentences (factual, predictive, values, computed, editorial) and pull out numbers and
  names for checking.
- **Flag** possible problems: missing context, loaded language, unbalanced structure, changed sources.
- **Help find** relevant passages in documents we've already archived.

## What AI may not do

- Publish anything that hasn't passed the full [verification protocol](../process/verification-protocol.md).
- Write **voting logistics** (registration, polling places, ballots, absentee rules). We link to official
  sources only.
- Attribute arguments to real people unless a verbatim, archived quote is cited.
- Generate images, audio or video of real people.
- Count as verification by agreeing with another model. **Several AI models agreeing is not
  verification.** Verification means:
  - an independent checker shows, with exact quoted words, that the archived source supports the
    sentence;
  - and that checker's error-catching rate has been measured.
- Follow instructions found inside documents it reads. Documents are treated as untrusted data.

## Process

1. **Prove by hand first.** Each step is done manually from a runbook before it's automated, and the
   manual results become the standard the automation must meet
   ([prove-then-automate.md](../process/prove-then-automate.md)).
2. **AI drafting runs in shadow mode** (compared with manual work, not published) until it meets that
   standard.
3. **Automated checks:**
   - every factual sentence is cited;
   - quotes are verbatim;
   - numbers and dates match the sources;
   - arithmetic is shown;
   - plain-language and loaded-word checks;
   - structural symmetry.
4. **Independent support check.** A separate verifier agent, which never sees the drafter's notes,
   confirms each citation actually **supports** its sentence and quotes the supporting words. Code checks
   the quotes.
5. **Bias panel.** A supporter's advocate, a critic's advocate and a standards editor review fairness
   and run a flip test.
6. **Calibration.** A verifier is also run on a copy with hidden planted errors. Its catch rate must be
   at least 90%.

## Disclosure

Each item says what happened. For example: *"Drafted with AI from the linked sources. Every sentence
links to its source or is labeled as our analysis. Checked by automated tests and independent AI
reviewers on [date]; no person reviewed it."* We keep a record of the model, the prompt version and the
inputs for every draft and check, and we publish catch rates and error rates.

## Models

We use Anthropic's Claude API with its document-citation feature, pinned to a specific model version.
Changing the model or prompt requires re-running our evaluation set from the manual trials.
