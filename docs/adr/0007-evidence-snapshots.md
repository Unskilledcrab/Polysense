# 0007: Immutable snapshots, versioned passages, Internet Archive as witness

- **Status:** Accepted
- **Date:** 2026-11-08

## Context

Every factual sentence must trace to evidence that can't silently change. Government pages move, and
PDFs get replaced.

## Decision

- **Every fetched source becomes an immutable Snapshot.** We store the bytes in the evidence bucket under
  their SHA-256, along with the URL, `fetched_at`, HTTP status and headers.
- **Passages** are numbered, citable units of a snapshot, tagged with `extractor_version`. They are never
  re-split in place: a new extractor creates new passages.
- **External witness:** for key documents, request an Internet Archive Save Page Now capture and record
  its URL. Use Perma.cc only for the highest-stakes items.
- **Change detection (SourceWatch):** a new hash marks dependent content "source changed: re-review".
- **Copyright:** government records may be stored in full. For copyrighted analyses we keep the hash, the
  URL and short cited excerpts only.

## Consequences

Readers see "captured on [date] · hash" next to the evidence, and anyone can check our copy. Storage
costs are small.
