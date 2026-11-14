# Trial 004: PSC Docket 57243, a power plant for one data center (the scale test)

> **Current state:** run `trials/tools/desk.py brief psc-57243`. This file records what the trial measured; it is not
> a status page.

**What this trial was for.** The owner's concern on 2026-11-14 was how much there is to keep track of as content
grows. This page was added through the new machinery, to measure it: the source funnel's stage 4
([ADR 0015](../../docs/adr/0015-source-funnel-and-ai-budget.md)), the desk (`trials/tools/desk.py`), and the lean
checking flow, against a cap of 1.0M delegated tokens (300k on the top tier).

**The subject.** A Newton County nonprofit and nearby residents asked the Georgia Public Service Commission to decide
whether VoltaGrid's plant, which would supply one data center, breaks two state power laws. VoltaGrid says the PSC has
no authority over it. Georgia EMC, Georgia Power and the PSC's staff also filed. A hearing is set for 2026-12-09.

## Result: published 2026-11-14, passing; about 1.53M delegated tokens

The page passes: 249 of 249 sentences checked, every gate clean, the whole-page read on record, and no open
high-severity fairness issue. It reached its first cap (1.0M) before its last checks; the owner approved finishing
it with a second cap of 600k.

| Step | Who | Tokens | Result |
|---|---|---|---|
| Fetch 27 filings and 6 PSC pages; extract text | Code | 0 | 33 sources saved and hashed; about 8 million characters of text |
| Candidate passages (funnel stage 4) | Code | 0 | 200,000 characters for the drafter: 14 short documents whole, 21 long ones searched for 12 questions |
| Draft | One fresh drafter, top tier | 332,523 | 87 passages, 229 sentences, 3 outcomes, 6 argument cards; every gate clean |
| Pair check, with a blind planted-error quiz | Two Sonnet agents | 255,515 | Quiz: 11 of 11 planted errors caught, 0 of 20 false alarms. Page: 208 pass, 21 slightly overstated, 0 unsupported |
| Fairness panel (first publication) | Three Sonnet agents | 406,200 | 3 of 5 from each seat; one high-severity issue from each |
| *First cap reached* | | *994,238* | *Top tier: 332,523 against a cap of 300,000* |
| Fairness re-review of the patched page | One Sonnet agent | 129,300 | 4 of 5; the three high issues resolved; no new high issue; 10 medium notes and 2 label rulings, all patched |
| Whole-page source read | One top-tier agent | 190,319 | 237 units, every evidence span verified by code; 0 unsupported, 8 partial, 6 questionable notes; all patched |
| Pair check of 23 changed sentences | One Sonnet agent | 81,314 | 21 pass, 2 slightly overstated |
| Two confirming checks (2 sentences, then 1) | Two Sonnet agents | 137,274 | All pass |
| Patches throughout | Coordinator, by hand | not counted | |
| **Total delegated** | | **1,532,445** | Second cap: 538,207 of 600,000; top tier 190,319 of 300,000 |

(The first pair-check figure includes two sentences from Trial 002.)

**What a first publication costs through subagents: about 1.5M tokens**, of which about 520k is on the top tier.
The last 137k went on re-checking three sentences, because each checker costs about 68k however little it reads.

## What the trial showed

1. **Keyword search was enough to draft from.** The drafter needed 8 lookups outside the candidate pack and found what
   it wanted in 7. It never needed a whole long filing. The pack was 2.5% of the saved text.
2. **Fixed overhead is the cost, again.** A fairness reviewer reading a page of about 9,000 tokens cost about 135,000.
   A pair checker cost 112,000 to 143,000 for 10,000 to 25,000 tokens of batches. The same work through an API with
   batches and caching would cost a small fraction; that back end is still waiting on a key.
3. **The drafter went over budget** (332k against an estimate of 250k), mostly on reading the pack and on 29 tool
   calls. A smaller pack, or a draft in two passes (passages, then page), is the next thing to try.
4. **The checker couldn't see who was speaking.** Ten of the 21 flags were "the passage doesn't say who said this".
   `judge.py` now sends each passage's document, filer and place with it (prompt `pair-3`).
5. **Fairness was the hard part, as in every trial.** The gates and the pair check found no false statement. The
   panel found that card titles stated one side's conclusion in our voice, that a quote was cut where the rest of it
   helped the other side, and that "no effect on your bill is stated" left out two filed statements.
6. **The privacy gate held.** The filings name private residents and their roads. No name or street reached a tracked
   file; the count of residents was dropped from the page because name-free passages can't show it.
7. **The desk did its job on the older pages.** While this page was being drafted, `desk.py` flagged a passed hearing
   date on the PSC fuel-credit page and a newly posted agenda on the Fulton page; both were revised and re-checked for
   73,518 tokens in all.

## Sources and privacy

- `sources.yaml`: 33 sources. Filing titles are by role. Four video exhibits (about 1 GB) are saved and hashed but not
  read. Two orders are images without text (S02c, S03c); one piece of testimony could not be downloaded (S15).
- `_raw/` is git-ignored and exists only in the session that fetched it.
- `candidates.yaml` records exactly what the drafter was given.
- `verification/fairness-review.json` holds every panel finding, including the open ones.
