# 0006: Append-only revisions of publishable bundles

- **Status:** Accepted
- **Date:** 2026-11-08

## Context

Trust requires a public version history with diffs and a corrections log. No CMS offers *public*
revision history. Per-row history tools are too fine-grained for readable public diffs.

## Decision

- One generic **Revision** table. Each publish serializes a whole *bundle* (a decision workspace, an
  opportunity, or a policy page) to canonical JSON and appends a revision.
- Each revision records its SHA-256, `parent_sha`, a change class (typo, update, correction,
  re-evaluation), the reason, the editor, its reviews and its AI provenance.
- Public pages render **only** from revisions. Every revision has a permanent link and a diff against its
  parent.
- **Enforcement:** a trigger rejects UPDATE and DELETE, and the runtime database role lacks those
  privileges.
- The corrections log is a view over revisions with `change_class = correction`.

## Consequences

History can't be quietly rewritten. Drafts stay mutable until published. Storage grows with each
revision, which is fine at this scale.
