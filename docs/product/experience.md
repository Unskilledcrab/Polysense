# Experience design

The product is understanding. Every screen should answer the reader's question in seconds and let them
go deeper only if they want to. How we write is in [content-style-guide.md](content-style-guide.md).

## Homepage: "What can I influence?"

It is a personal civic dashboard, not a news feed. It has three sections, always in this order:

| Section | Contains | Example |
|---|---|---|
| **Act now** | Opportunities open now | "Senate Education Committee hears SB 12 Tuesday 2 pm. Watch live, or send written testimony." |
| **Coming up** | Things you can prepare for | "HB 45 is on the House Rules calendar. Your representative sits on Rules." |
| **What happened** | Recent outcomes and what comes next | "HB 30 passed the House 120–54. Next stop: Senate committee." |

- Each item shows its **participation power** (Decide, Input or Observe), its **date or window**, **one
  action**, and its **status**.
- Items are labeled **"Applies to you"** only when the district match is certain. Otherwise they say
  "May apply: confirm on My Voter Page".
- A **statewide view** ("everything consequential") is always one tap away, so people don't end up in a
  bubble.
- Ordering is deterministic and published: deadline proximity × jurisdiction match × participation power.

## Decision workspaces: five layers

```
30-second version   what it does · who it affects · what happens next / what you can do
Layer 1  Participate  dates, how to watch, testify, comment, contact; your members' roles
Layer 2  Decision     what is being decided, in ≤ 80 words + ≤ 5 "what would change" bullets
Layer 3  Options      each option (incl. status quo): who benefits · who pays · what's uncertain
Layer 4  Arguments    argument cards (see below), documented first, anticipated clearly labeled
Layer 5  Evidence     every source with type badge, capture date, hash, and "why this source"
```

- Layer tabs (**Overview · Options · Arguments · Evidence**) with jump links. Readers can start at any
  layer.
- Every workspace uses **the same template**: the same headings, order and labels, whatever the topic or
  side.
- Every factual sentence can be tapped to show its source passage, highlighted in the archived document.

## Argument explorer (Layer 4)

Arguments appear as **linear cards**, not node-link graphs. Research shows maps help only people trained
to read them.

```
┌ Argument ─────────────────────────────── [Documented · Speaker, venue, date] ┐
│ Claim (one sentence)                                    [factual|predictive|values] │
│ ▸ What supports it      cited evidence                                        │
│ ▸ What's missing        omitted context, unstated assumptions   {editorial}   │
│ ▸ What challenges it    counter-evidence, rebuttals                           │
│ ▸ What's uncertain      what can't be established yet, and why                │
│ ▸ Evidence              links into Layer 5 passages                           │
└───────────────────────────────────────────────────────────────────────────────┘
```

- **Documented** arguments carry speaker, venue, date and a verbatim quote. **Anticipated** arguments
  are labeled as such and never attributed to anyone.
- **Values** disagreements are presented as legitimate differences in priorities. They are never labeled
  misinformation.
- Every option gets **its strongest case for and against**. Symmetry is structural only. We never
  equalize evidence that isn't equal.

## Evidence vocabulary (used only for factual and predictive claims)

We use a fixed scale modeled on the IPCC's: **strength of evidence** × **agreement among sources**.
Wording is paired with numbers whenever the sources give them.

| Label | Meaning |
|---|---|
| **Well established** | Strong evidence, high agreement |
| **Supported** | Good evidence, some disagreement or limits |
| **Mixed** | Credible evidence points in different directions |
| **Uncertain** | Little evidence yet; we say *why* (too new, not measured, depends on choices not yet made) |
| **Contradicted** | Credible evidence shows it is inaccurate |
| **Not a factual question** | A values or priorities claim, so evidence can't settle it |

## Item statuses

Every item shows a status, because unlabeled items look endorsed (the implied-truth effect).

**Verified** (with reviewer and date) · **Under review** · **Disputed** (an open challenge) · **Source
changed: re-review** · **Outdated**

**AI disclosure** describes the process, not a bare label. For example: "Drafted with AI assistance from
the linked sources; every sentence checked against its source by [reviewer] on [date]."

## Navigation rules

- No more than **2 taps** from the homepage to an action.
- A "What can I do?" link on every page.
- Search by everyday words ("rent", "schools", "power bill"), which map to topics and bills.
- Breadcrumbs and layer tabs. The back button always works, because these are real URLs.
- Personal pages use district-keyed URLs (`/my/h054-s039/`) that can be shared and bookmarked. No login.

## Phone first

Design for a phone first, then widen. The owner reviewed the first prototype on a phone and found it
informative but harder to read and navigate than it should be. The second version of the prototype tries
these patterns. They are experiments: keep the ones readers find easier, and change the rest.

- The site menu sits in a bottom bar on phones, within thumb reach. Wide screens keep it at the top.
- A bill page has one row of plainly named tabs (Summary, What it does, Options, Arguments, Sources),
  pinned at the top while you scroll. Tab content starts near the top of the screen; the 30-second
  version appears only on Summary.
- Summary leads with the 30-second version, then "What you can do".
- Labeled fields show the label on its own line and one sentence per line, with a short source chip at
  the end of each line.
- Options and argument cards start folded, so readers see every option and both sides' claims before
  opening any of them.
- Each tab ends with links to the next and previous tabs.
- Tables become stacked cards on narrow screens.

## To try: questions to weigh

The owner wants pages that make readers think about the issue rather than pick a team. To try in the next
trials: end each decision with two or three neutral questions that both sides would accept as fair, such
as "How much should parents be able to reach their child during the school day?" The bias panel checks
them like any other text. Keep the idea only if readers find it useful.

## Accessibility and performance

- Meet **WCAG 2.2 AA**.
- Pages work without JavaScript.
- Body text is at least 16px. Tap targets are at least 24px, aiming for 44px.
- Use more than color to show meaning.
- Keep JavaScript under **100 KB**. Pages should be fast on a low-end Android phone over a slow network.
  (15% of US adults are smartphone-only, mostly lower-income.)
- Borrow accessible patterns from the USWDS and GOV.UK design systems, but keep a **distinct identity**
  and an **"Independent — not a government website"** notice.
- Pages are built ready for translation. Spanish comes after the pilot, with human-reviewed translation.
