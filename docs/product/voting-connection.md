# Connecting decisions to voting (exploratory)

**Status:** ideas to test, not a plan. Written 2026-11-08 after the owner asked how this work could help
people make informed decisions when voting. Nothing here is decided. Each idea would start as a manual
trial, like everything else ([prove-then-automate](../process/prove-then-automate.md)).

## The question

A bill workspace explains a decision that lawmakers made. Voters make a different decision: who
represents them, and how to vote on ballot questions. How can the same evidence help with that, while
staying correct and nonbiased, concise and digestible, and informative and actionable?

## Ideas, roughly from closest to what we have to furthest

| Idea | What a reader gets | Why it might fit | What makes it hard |
|---|---|---|---|
| **1. How my legislators voted** | On a bill we've explained, how each of *your* legislators voted, next to the workspace | Roll calls are public records, and district lookup (Trial C) connects them to a reader. Facts only, no scores | Procedural votes and amendments can mislead without context. Some roll calls are unpublished ([data sources](../research/georgia-data-sources.md)). Voting-record rules apply ([nonpartisanship](../policies/nonpartisanship.md)) |
| **2. Ballot questions as workspaces** | Constitutional amendments and referendum questions explained with the same five layers, where "yes" and "no" are the options | Voters decide these directly, and the workspace format fits them almost unchanged | Ballot wording is fixed and often confusing. Timing is tight before an election. The roadmap already plans amendment workspaces for 2028 |
| **3. Issue history** | For one topic (say, phones in schools), every bill, vote and documented statement over time | Organized around everyday concerns, not bill numbers | Needs several workspaces on one topic first |
| **4. Candidates' documented positions** | What each candidate said on the record, with the same questions for every candidate | Closest to the voting decision itself | Highest risk to fairness and completeness, and bound by IRS rules (Rev. Rul. 78-248). The roadmap allows it only with an editorial team and counsel review |

All of these keep the existing guardrails ([CLAUDE.md](../../CLAUDE.md), [nonpartisanship](../policies/nonpartisanship.md)):

- Registration, polling places, ballots and deadlines come only from official links (the Secretary of
  State's My Voter Page and county sites), never from our own writing or model output.
- No endorsements, ratings or scores of candidates or bills. One template for every candidate and side.
- Documented statements carry speaker, date, venue and the verbatim passage. Nothing is attributed to a
  candidate that they didn't say.
- Extra care near elections. Legislators are also future candidates.

## Open questions for the owner

1. Is idea 1 (how my legislators voted on bills we've explained) the right first step? It reuses the
   bill workspace and the planned district lookup.
2. Should the 2027 pilot include any election-related content at all, or wait for the 2028 cycle?
3. How should a page handle a vote whose meaning depends on context (an amendment, a procedural motion)?

## A possible first experiment (only if the owner wants it)

A manual trial that shows how the House members for a few public landmark addresses voted on HB 1009,
with links to the official roll call and the workspace. It would test whether vote records can be shown
without editorializing, and how much context a reader needs. It would also reuse Trial C's district
lookup.
