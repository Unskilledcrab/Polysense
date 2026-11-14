# 0016: Point to sources where they are hosted; don't host copies

- **Status:** Accepted
- **Date:** 2026-11-14
- **Amends:** [0007](0007-evidence-snapshots.md) (which planned an evidence bucket holding every fetched file)

## Context

Four pages had saved about 1.3 GB of source files, 1 GB of it video exhibits from one docket. The files lived only in
the session that fetched them; nothing kept them. The owner's direction: link to where a document is hosted, with
pointers, instead of hosting it ourselves.

Most of the pointer was already in git. Each source had its link, the date we fetched it and a fingerprint of what we
fetched, and each page had the exact passages it quotes. What assumed a kept copy was the tooling: the hash check
failed without the files, passages could not be rebuilt, and the fetcher downloaded everything it was offered.

## Decision

**We don't host source documents.** For each source we keep, in git:

| What | Where |
|---|---|
| The link where the document is hosted | `sources.yaml` `url` |
| The date and time we read it | `retrieved_at` |
| A fingerprint (SHA-256) of the bytes we read | `sha256` |
| The place in the document, and only the short passages we quote | `passages.md` |
| A fingerprint of its visible text, for pages whose bytes shift on every load | `watch.json` `links` |
| An Internet Archive copy's link, if one exists or could be made | `watch.json` `links` → `archive` |

Rules that follow:

- **A fetched file is a cache.** `_raw/` can be deleted at any time. `desk.py restore` fetches a link again and keeps
  the file only if its fingerprint matches, so a restored file is provably the document we read.
- **The gate doesn't need the cache.** `verify_hashes.py` reports a missing file as "not cached", and still fails a
  cached file whose fingerprint is wrong. `build_passages.py` carries a passage forward unchanged when its source
  isn't cached; adding or changing a passage needs that one source restored.
- **Large media is never downloaded.** Video, audio and files over 50 MB are recorded by link, type and size.
- **We ask the Internet Archive to keep a copy** (`desk.py archive`), and record its link, or record that the request
  was blocked or that no copy exists. We never claim a copy we can't link to.
  - First run, 2026-11-14: 48 of 93 cited sources already had a copy there. The other 45 had none, and the request to
    make one could not be sent from the session's network (the lookup address answers; the save address does not).
    They are recorded as having no archived copy. Anyone with a browser can make those copies; a later session with
    that address reachable can too.
- **A stale pointer is shown, not hidden.** `desk.py links --fetch` compares each cited link with its fingerprint.
  A link that is gone or now serves a different document goes on the queue, and the page says so beside the source.
- **Listing pages are different.** Calendars and docket lists change by nature; `desk.py watch` follows them for new
  events and is not a broken-link check.

## The trade-off

If an agency edits or removes a document and no archive copy exists, we still have the passage we quoted, the date
and the fingerprint, but nobody can re-read the original. A reader can then see what we quoted and when, and that the
link no longer matches; they cannot check the quote against its surroundings. We accept that for public records in
exchange for storing nothing, and reduce it with archive copies and the link check.

## Consequences

- Nothing to store or back up beyond the repository. A new session needs no evidence files to run the gate, render
  the app or check a changed sentence.
- This matches the copyright rule we already had for copyrighted analyses (a link, a hash and short excerpts), and
  extends it to public records.
- Readers see "read on [date]", the fingerprint, the link to the original and, where there is one, the archived copy.
  The app no longer says "saved copies".
- The future application's `Snapshot` record becomes a pointer with an optional cache
  ([data model](../architecture/data-model.md)).
- Requests to outside sites use a generic client name. An earlier client string named this repository; it was
  replaced on 2026-11-14.
