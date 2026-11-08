# Usability and comprehension testing

Readability formulas help, but **only people can tell us whether something is understood.** Every round
tests real comprehension and navigation.

## Round format

- **Participants:** 5 per round, recruited across ages, education levels, devices and political
  leanings. Include at least one person who rarely follows politics and one smartphone-only user.
- **Session:** 20–30 minutes, on the participant's own phone where possible. They think aloud. We record
  notes only, never video, unless they consent.
- **Material:** one workspace and the homepage. Early on this means trial outputs rendered as plain HTML
  or Markdown.
- **Consent and privacy:** verbal consent, no names in notes, and no addresses. Participants use a public
  landmark address for any district lookup.

## Tasks and pass criteria

| # | Task | Pass |
|---|---|---|
| 1 | "In your own words, what does this bill do?" (after up to 60 seconds of reading) | Matches the 30-second version on the essentials |
| 2 | "Who would this affect?" | Names at least one affected group correctly |
| 3 | "What could you do about it, and by when?" | Finds the action and the date within 2 taps |
| 4 | "Name one argument for and one against." | Both are correct; the participant can tell which are documented and which anticipated |
| 5 | "Where did this number come from?" | Reaches the source passage |
| 6 | "Did this seem to favor one side? Which parts?" | Recorded (not pass/fail); feeds the fairness review |
| 7 | "What was confusing?" | Recorded; every confusion becomes an issue |

**A round passes when at least 4 of 5 participants pass tasks 1–5.** A failed task changes the template or
the style guide, not just that one page.

## Logging

Each round gets `trials/usability/RNN-YYYY-MM-DD.md`. It records the material version (revision hash),
anonymized participant profiles, results per task, quotes, and the changes made in response.

## Cadence

- **Round 1:** in P1 (Nov–Dec 2026) on Trial #1–#3 outputs. The user recruits participants.
- **Round 2:** before private alpha (early Jan 2027).
- **Then** before public beta, and once a month during the session.
