# AI pipeline (design; built **last**)

**Status:** not built. It will be built only after bill-workspace trials A#1–#3 pass review. It then runs
in **shadow mode** against the manual process until it meets the bar below. Policy:
[ai-use.md](../policies/ai-use.md).

## Flow

```
archived passages (hashed) ──► 1. Draft with citations ──► 2. Classify sentences ──► 3. Deterministic gate
                                                                                         │ fail → back to 1 or human
                                                                                         ▼ pass
                                       5. Publish revision ◄── 4. Human Review Desk (support check per citation,
                                          (human approves)        second reviewer for named speakers/elections)
```

### 1. Draft with citations

- Each source's passages are sent as a **custom-content document**: one content block per passage, with
  citations enabled. Claude returns cited text blocks whose `content_block_location` identifies the exact
  passage block, so **each citation maps one-to-one to a Passage ID**.
- **Model:** `claude-opus-5-5`, pinned. Thinking is adaptive (it can't be turned off on this model).
  Effort is set explicitly to **high** for drafting.
- The prompt is the frozen methodology plus the workspace template, from `prompts/` and versioned by git
  SHA. **Prompt caching** keeps this stable prefix cheap.
- **No tools and no web access.** Source text is untrusted data, so instructions inside documents are
  ignored by design.

### 2. Classify sentences (a separate call)

- Citations can't be combined with structured outputs in a single call, so a second call uses
  **structured output** (`output_config.format`) at effort **low**.
- It labels each already-cited sentence with a type (factual, predictive, values, computed, editorial),
  pulls out numbers and dates, and names any actors.
- Forced `tool_choice` isn't available on this model; structured output is the supported route.

### 3. Deterministic gate (code, not models)

- Every factual sentence has at least one citation.
- `cited_text` appears verbatim in the hashed passage.
- Every number and date appears in a cited passage. Otherwise the sentence must be `computed`, with its
  arithmetic shown and re-checked.
- `editorial` sentences ("what's missing", "what's uncertain") are flagged for explicit reviewer
  sign-off.
- Reading grade, jargon list and loaded-word list (outside quotes).
- **Structural** symmetry: every option has the same fields, and its strongest case for and against.
  Never equalized ratings.
- Length budgets.

### 4. Human review

Reviewers follow the [review desk handbook](../ops/review-desk-handbook.md). Every citation gets a
verdict (supports, partial, no, wrong span). A second reviewer is required for named speakers, election
content and corrections.

### 5. Publish

A person approves. A Revision is appended with AIRun provenance.

## Provenance and reproducibility

The model doesn't take sampling settings, so outputs aren't exactly reproducible. **Provenance is a
replayable record:**
- the requested model and the model that **actually served** the request (`response.model`);
- `prompt@git-sha`, effort, input passage IDs;
- usage;
- the raw request and response JSON, stored in the evidence bucket by hash.

## Handling failures

- Check `stop_reason` before reading content. Handle `refusal` (log it and route to a person) and
  `max_tokens` (retry with a larger limit or split the task).
- Use streaming for long requests, and retries with backoff on 429 and 5xx errors.
- If server-side model fallbacks are enabled, record the served model and treat a fallback draft as
  needing full review.

## Cost

- At current list prices ($4 / $20 per million input / output tokens for Opus 5.5), a workspace costs a
  few dollars.
- Use the **Batch API** (about 50% cheaper) for backfills, and cache the methodology prefix.
- A cheaper model is used only if evals show quality holds. That's the owner's call.
- **Reviewer hours are the real cost.** Measure them.

## Promotion bar (measured against trial gold data)

AI drafting may replace manual drafting for a workspace type only when, across at least 3 shadow runs
compared with the manual baseline:
- the gate pass rate is at least the manual baseline;
- the share of citations judged *does not support* or *wrong span* is ≤ the manual baseline, and below 3%;
- reviewer time per workspace is no worse;
- the comprehension and length budgets are met.

Re-run the full eval before **any** model or prompt change.
