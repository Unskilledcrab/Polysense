# How we use AI (draft)

**Short version:** AI helps us draft and check. Before anything is published, people verify every
sentence against its source.

## What AI may do

- **Draft** explanations and argument summaries, **only from our archived source passages**, with a
  citation on every factual sentence.
- **Classify** sentences (factual, predictive, values, computed, editorial) and pull out numbers and
  names for checking.
- **Flag** possible problems: missing context, loaded language, unbalanced structure, changed sources.
- **Help find** relevant passages in documents we've already archived.

## What AI may not do

- Publish anything. **A person approves every publication.**
- Write **voting logistics** (registration, polling places, ballots, absentee rules). We link to official
  sources only.
- Attribute arguments to real people unless a verbatim, archived quote is cited.
- Generate images, audio or video of real people.
- Count as verification. **Several AI models agreeing is not verification.** Only a person checking the
  source is.
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
4. **Human support check.** A reviewer confirms each citation actually **supports** its sentence.
5. **Second reviewer** for named speakers, elections and corrections.

## Disclosure

Each item says what happened. For example: *"Drafted with AI assistance from the linked sources. Every
sentence checked against its source by [reviewer] on [date]."* We keep a record of the model, the prompt
version and the inputs for every AI-assisted draft, and we publish our error rates.

## Models

We use Anthropic's Claude API with its document-citation feature, pinned to a specific model version.
Changing the model or prompt requires re-running our evaluation set from the manual trials.
