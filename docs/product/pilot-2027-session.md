# Pilot: the 2027 Georgia General Assembly session

**Session:** convenes **2027-01-11**. It runs 40 legislative days. Crossover Day is about early March,
Sine Die about late March or early April, and then the Governor's 40-day signing window follows (see
[georgia-calendar.md](../research/georgia-calendar.md)).

**Why this pilot:**
- It has real deadlines and rich public data, and it exercises every layer.
- It lets us test participation outside elections, such as hearings, testimony and contacting
  legislators.
- It starts soon enough to learn before the 2028 election cycle.

**Built by:** one developer with AI assistance. Verification is AI-only: a measured, independent
[verification protocol](../process/verification-protocol.md), with no human in the loop
([ADR 0014](../adr/0014-ai-only-verification.md)).

## User journeys

1. **"What can I do this week?"**
   - Enter an address. We show your two legislators and the committees they sit on.
   - We also show upcoming hearings and floor votes on the bills we cover. Items where your legislator is
     on the committee are highlighted.
   - You can add the calendar to your phone with an ICS feed.
2. **"What's at stake in HB X?"** Arrive from a shared link. The 30-second version comes first, then
   Layers 2–5 as you want more.
3. **"How do I weigh in?"** A hearing page shows:
   - the time, livestream and agenda;
   - how to request to testify or send written testimony;
   - which of your legislators are on the committee.
4. **"That's wrong."** Submit a challenge. It gets a public status, and any fix appears in the revision
   diff and the corrections log.

## Scope

| Ship in the pilot | Cut until after Sine Die |
|---|---|
| Address → districts → my legislators and their committees | Accounts and alerts (use ICS/RSS feeds instead) |
| Feed of all hearings and floor calendars, built from structured data only (no AI) | Agency rulemaking, Public Service Commission, local government |
| Data-only bill cards (status, next step, fiscal-note link) for bills with hearings or floor action, labeled "not yet explained" | AI summaries for every bill |
| **6–8 deep decision workspaces**, about one a week | Argument graph view |
| Public revisions with diffs, challenges, corrections log, published policies | Spanish (pages built ready for translation), donor data, transcription of hearing video |
| Governor veto-window tracker for every passed bill | Persisted topic follows (topic filters are just URL parameters) |
| Special elections, when called, listed as "vote" items with official links only | Bulk data downloads and a publicly announced API |

### How many workspaces

The first plan assumed human review at about 12–16 hours per workspace. Without a human in the loop,
the limits are:
- the agent's time to retrieve and draft;
- the verification protocol's run time and fix cycles;
- the owner's time to review the user experience.

Trial 001 is the first measurement ([log](../../trials/001-hb1009-high-school-cellphones/log.md)). Start
at 6–8 for the session and raise the number only if verification stays clean at scale.

## "Act now" items during a session

| Opportunity | Participation power |
|---|---|
| Watch a committee hearing or floor session (livestream) | Observe |
| Request to testify, or send written testimony to the committee | Input |
| Contact your legislator, especially when they sit on the hearing committee or the bill is on the Rules calendar | Input |
| Contact the Governor's office during the 40-day signing window | Input |
| Vote in a special election to fill a vacant seat | Decide |

## Picking the workspaces

We use the published [topic-selection rubric](../policies/topic-selection.md). Each candidate is scored
0–3 on everyday impact, whether input is still possible, available evidence, public confusion or
attention, and likelihood to move. Proposed constitutional amendments get a bonus, because they become
items on the November 2028 ballot. All scores and decisions, including rejections, go in the public
coverage log along with the sponsor-party mix.

## Success metrics

| Area | Target |
|---|---|
| Understanding | Each comprehension round passes with at least 4 of 5 participants ([usability-testing.md](usability-testing.md)) |
| Accuracy | Fewer than 2 substantive corrections per 100 claims; verifier catch rate at least 90% on planted errors every trial; zero ungrounded verifier evidence; zero errors in voting logistics |
| Gate coverage | 100% of published sentences cited or typed |
| Responsiveness | First response to a challenge within 2 business days; resolved within 7 |
| Timeliness | Covered hearings listed at least 24 hours ahead; median under 3 hours from a legislature posting to our listing |
| Effort | Reviewer hours and dollars per workspace, tracked every time |
| Use | Weekly visitors; share who set districts; clicks on actions and feeds; how often people open evidence and history |
| Qualitative | 10–15 user interviews across backgrounds and politics |

## Gates before going public (around Feb 1, 2027, or later)

Public beta needs all of the following:
- a cleared public name;
- published policies;
- the verification protocol passing its thresholds on at least 3 trials;
- media liability insurance;
- a fiscal sponsor;
- 3 verified workspaces;
- the owner's sign-off on the user experience;
- one passed comprehension round.
