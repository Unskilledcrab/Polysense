# 0008: AI drafting with citations, a deterministic gate and human support checks

- **Status:** Accepted (implementation deferred until manual trials prove the process; see [0012](0012-prove-then-automate.md))
- **Date:** 2026-11-08

## Context

LLM citations show where text came from, not whether it supports the claim. Retrieval-based legal tools
still hallucinate 17–33% of the time. AI election tools have repeatedly given wrong logistics. Model
agreement isn't verification.

## Decision

The pipeline ([ai-pipeline.md](../architecture/ai-pipeline.md)):

1. **Draft** with the Claude API (`claude-opus-5-5`, pinned). Passages are sent as custom-content
   documents with citations enabled, so citations map one-to-one to passage IDs.
2. **Classify** sentences in a separate structured-output call. Citations and structured output can't
   share a call.
3. **Deterministic gate** in code.
4. **Human support check** on every citation, with a second reviewer for named speakers and elections.
5. **A person approves** publication.

Other rules:
- Prompts are versioned in `prompts/`.
- Raw requests and responses are stored by hash.
- Evals built from trial gold data must be re-run before any model or prompt change.
- No tools or web access in drafting calls.
- Voting logistics are never AI-drafted.

## Consequences

AI saves drafting time. Reviewer time stays the bottleneck and is measured. The pipeline must beat the
manual baseline in shadow mode before it's used.
