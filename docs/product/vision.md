# Vision

## The problem

Politics is harder to follow than it needs to be. Information is fragmented, selectively presented and
framed to persuade. That makes it hard for an ordinary person to know:

- what is actually happening in government, and which decisions affect them;
- when and how they can influence those decisions;
- whether an argument is accurate, misleading or missing context;
- which explanations they can trust.

## The vision

> **An independent, nonpartisan civic platform that shows people what they can influence in government,
> helps them understand the decisions ahead, and equips them to evaluate political arguments using
> transparent, verifiable evidence.**

**Know what's coming. Understand what's at stake. Know how you can participate.**

We don't ask people to trust us, a politician, a newsroom or a fact-checker. We give them what they need
to check things for themselves: the original evidence, how we reasoned, and every change we have made.

### Tenets

Set by the project owner on 2026-11-08, after reviewing the first prototype. Every feature, layer and
process change should hold to all three:

1. **Correct and nonbiased.** Every claim matches its source. Each side gets its strongest case, under
   the same rules.
2. **Concise and digestible.** Answer first, plain words, no more than a reader needs. Designed for a phone
   first.
3. **Informative and actionable.** Readers learn what matters and what they can do about it.

We are still experimenting. Methods and rules can change as we learn; these tenets don't.

### Three commitments

1. **Make participation accessible.** People should know what they can influence and when.
2. **Make understanding achievable.** Complex decisions should be easy to approach without losing nuance.
   Clarity is the product.
3. **Make trust verifiable.** Anyone can inspect the evidence and reconstruct the reasoning.

## How the idea developed

| Stage | Idea | What we learned |
|---|---|---|
| 1 | Simplify legislation | People need understandable explanations of laws and their consequences |
| 2 | Examine political arguments | Summaries are not enough; people need to see competing claims and missing context |
| 3 | Build trust through transparency | Every important claim needs traceable evidence and a public correction history |
| 4 | Organize around everyday concerns | People shouldn't need to know bill numbers or government jargon |
| 5 | Focus on civic opportunities | The homepage should show what people can actually influence now and soon |

## What makes this different

Existing tools cover pieces of this (see [research/landscape.md](../research/landscape.md)):

- **Deadlines and ballots:** TurboVote, Vote411, BallotReady, Ballotpedia and VoteMate already cover election deadlines and ballots.
- **Georgia bill tracking:** the AJC Legislative Navigator, Plural and LegiScan already track Georgia bills.
- **Name conflict:** an app called "CivicLens Legislation Tracker" already offers a personalized bill feed.

So **the dashboard alone is not our reason to exist.** These are:

1. **Layers 3–5, done rigorously.** Options and tradeoffs, an argument explorer that separates facts,
   predictions and values, and the original evidence behind every claim.
2. **Participation beyond elections.** Committee hearings, written testimony, contacting the legislator
   who actually sits on the committee, and the Governor's veto window. Later: agency comment periods,
   Public Service Commission hearings, local meetings and referenda.
3. **Trust you can inspect.** Archived, hashed sources; a public revision history with diffs; a public
   challenge process with response targets; and published methods, AI use, topic selection and funding.

The closest model is CalMatters' Digital Democracy in California, which treats AI output as leads for
humans, never as published conclusions. Nobody does this for Georgia.

## Who it's for

Everyday Georgians and voters, especially people who care about an issue ("rent", "schools", "power
bills") but don't follow the legislature. Design for a phone over a slow connection, and for a reader who
gives us 30 seconds.

## Non-goals

- **No endorsements, ratings, scorecards or "truth meters"** for candidates or bills.
- **No engagement-driven feed.** The order of items follows published, deterministic rules, not clicks.
- **No inferred political profiles.** Personalization uses only the districts and topics a person chooses.
- **No authoritative voting logistics.** For registration, polling places and ballots we link to the
  Secretary of State's My Voter Page and county sources.
- **No full coverage at the cost of accuracy.** We cover fewer things well, and publish what we chose and
  why.
- **No dependence on government cooperation or reform.** We build only with what is public today.

## Where we start

Georgia, with the **2027 General Assembly session** as the first pilot
([pilot-2027-session.md](pilot-2027-session.md)). Then local government and the 2027 local referenda,
then the 2028 election cycle ([roadmap.md](roadmap.md)).
