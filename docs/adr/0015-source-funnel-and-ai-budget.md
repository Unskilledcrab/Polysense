# 0015: A funnel for source documents, and AI usage as a budget

- **Status:** Accepted for trial (the funnel's stages are unproven until Trial 004 runs)
- **Date:** 2026-11-13
- **Amends:** [0008](0008-ai-drafting-and-verification.md) (which model does what) and
  [0014](0014-ai-only-verification.md) (how much verification runs, and when)

## Context

The reader-first cycle used about 13.5 million tokens for three pages and exhausted the owner's weekly allowance. The
owner's direction: do more with less. There are far too many source documents to push through an expensive model, and
the process had grown its own paperwork that agents kept re-reading.

Two different costs were mixed together:

1. **Checking pages we already have.** Solved separately by the claims ledger and pair checks; see the
   [verification protocol](../process/verification-protocol.md), "The lean flow".
2. **Deciding what to cover, and reading the sources.** The Georgia legislature, the Public Service Commission and one
   county produce thousands of documents a year. Three trials read about 70 sources between them, chosen by hand.

This record covers the second, and sets the rule for both.

## Decision

**Most documents never reach a model. Each stage is the cheapest thing that can do its job, and passes on less than it
received.**

| Stage | Who does it | Input | Output | AI |
|---|---|---|---|---|
| 0. Watch | Code | Official listings: bill status, PSC dockets, county agendas | One row per item: title, status, dates, links | None |
| 1. Score | Code | The rows | A ranked shortlist, from rubric fields the listing already has: a decision date soon, a way for the public to take part, money, how many people it touches | None |
| 2. Skim | Cheapest model | Title and first page only, for the top 20 | Three lines each: what's decided, who's affected, what people can do | Small, batched |
| 3. Select | The owner, or a rule | The skims | At most 6 to 8 active pages ([risks](../risks.md), 1b) | None |
| 4. Extract | Code | The chosen item's documents | Snapshots, text, a section index, and candidate passages found by keyword search for each question the page must answer | None |
| 5. Draft | Strongest model, once | Candidate passages only, never whole filings | The page | The main spend |
| 6. Check | Code, then the qualified cheap tier | Changed sentences and their cited passages | Verdicts in the ledger | Small |

Rules that follow:

- **A model reads a whole document only when code can't narrow it first.** Filings run to hundreds of pages; a page
  usually needs a dozen passages.
- **A stage is automated only after it is done by hand and passes the automation gate**
  ([prove-then-automate](../process/prove-then-automate.md)). Stages 0, 1, 2 and 4 are new and unproven.
- **Every AI step has a tier, a budget and a measured cost**, recorded where the step is defined. A stronger tier
  needs a test showing the cheaper one fails.
- **One interface, two back ends.** AI steps are scripts that write small batches and read back answers. Today a
  subagent answers each batch. With an API key, the same batches go through the Message Batches API with prompt
  caching, which removes the fixed overhead that dominated the tests (about 95k tokens per subagent for a 7k-token
  batch). The API back end is not built until the owner provides a key, and it needs an entry in
  [dependencies](../architecture/dependencies.md).

## What is proven, and what isn't

- **Proven on existing pages:** the ledger and the pair check. Three pages' follow-up checks cost about 110k tokens on
  Sonnet, against about 750k on the top tier for the same job last cycle.
- **Measured limit:** no cheap tier reliably finds subtle overstatement from a sentence and its cited passages alone.
  A new page still gets one whole-page read.
- **Tried once by hand (2026-11-14, [Trial 004](../../trials/004-psc-57243-plant-power-sale/README.md)):** stage 4.
  Code fetched 27 filings, extracted about 8 million characters, and keyword search narrowed them to 200,000 for the
  drafter, who needed 8 lookups outside that pack. Stage 5 cost 332k tokens against an estimate of 250k. The whole
  first publication cost 1.53M delegated tokens (about 520k on the top tier) against a first cap of 1.0M.
  Fixed overhead per subagent (110k to 145k) is most of that, which is the case for the API back end.
- **Not yet tried:** stages 0 to 3. Trial 004's subject came from an earlier scouting list, not from a watched listing
  and a rubric, so watching, scoring and skimming are still untested.

## Consequences

- Coverage is chosen by a published rubric applied to every listed item, which is also fairer than hand-picking.
- Drafting stays the main cost, on purpose: it is where judgment matters most.
- A cheap skim can miss an important item with a dull title. The shortlist and the rejected items are both kept, so a
  miss can be found and the rubric changed.
- The process has fewer documents: status is generated (`trials/tools/status.py`), and agents read a one-page
  [drafter card](../process/drafter-card.md) instead of the history.
