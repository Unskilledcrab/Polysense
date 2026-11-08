# Landscape, name check, funding and legal (research as of 2026-11-08)

> **How this was researched:** web search plus reading sources where the network allowed. Several sites
> could not be fetched directly, so some facts come from search summaries. Items marked **[unverified]**
> need a check against the primary source before we rely on them.

## Comparable products

| Name | What it does | Status in 2026 | Business model | Lesson for us |
|---|---|---|---|---|
| Ballotpedia (incl. Sample Ballot Lookup) | Wiki-style election encyclopedia; address lookup of what's on your ballot | Active; named VoteMate its official AI partner in May 2026 | 501(c)(3), individual and foundation donors; critics cite conservative-linked funders | Data completeness is its moat; who funds you gets used against you |
| Vote411 (League of Women Voters) | Prints candidates' questionnaire answers, unedited | Active, with 2026 Georgia guides | League education fund | A trusted model, but it depends on candidates choosing to respond |
| BallotReady / CivicEngine | Ballot guide; licenses its data to organizations | Acquired by Civitech, 2025-05-22 | B2B licensing | Ownership and customers shape how neutral you look |
| Vote Smart | Bios, voting records, "Political Courage Test" | Active; 2025 revenue $941k against $1.48M expenses | Donations | Running deficits; questionnaires are fragile |
| GovTrack | Tracks Congress | Active | LLC, ads and Patreon | Lean, data-first tools can last |
| Countable | Bill summaries plus "tell your rep" | Bought Brigade/Causes (2019), then merged into Causes (2020) | For-profit engagement app | Engagement apps without revenue fold or get absorbed |
| Resistbot | Text a bot and it sends letters to your officials | Active | Small donors | Low-friction action works; the name signals a side |
| Democracy Works / TurboVote | Election reminders; its Elections API feeds AI companies (Anthropic, OpenAI) | Active; 14.5M notifications in 2025 | Foundations plus partner fees | Being infrastructure scales reach, but it still gets attacked |
| **CalMatters Digital Democracy** | California hearing transcripts, votes, donations; AI "tip sheets" | Active; expanded to Hawaiʻi; won $9M (Apr 2026) | Philanthropy | **Closest analogue.** AI output goes only to reporters, as leads, not published conclusions. Not in Georgia |
| Google Civic Information API | Address → representatives | **Representatives API shut down Apr 2025**; divisions, elections and voterInfo remain | Free, run by Google | Don't build on one free upstream |
| AllSides / Ground News | Left/center/right outlet labels; story coverage groupings | Active | Ground News: subscriptions | Outlet-level labels draw false-balance criticism |
| PolitiFact / FactCheck.org | Verdicts on individual claims | Active; **Meta ended US fact-checking partnerships 2025-01-07** | Poynter, platform contracts; Annenberg | Platform money vanishes overnight; verdict meters polarize |
| Kialo | Pro/con argument trees | Active; Kialo Edu free for teachers | Company | Works in classrooms; open debates sprawl and lack sources |
| Pol.is / vTaiwan | Group opinion mapping that surfaces consensus | Active | Open source (AGPL) | Consensus-finding is valuable, but impact needs a government willing to listen |
| X Community Notes | Bridging algorithm across raters who usually disagree | **Over 90% of notes never published**; publish rate falling | Platform | Agreement across perspectives builds trust; speed and volume fail |
| Wikipedia NPOV | Talk pages, noticeboards, strict rules on living people | Active; faced 2025 bias accusations | Donations | Public history plus a dispute process is the gold standard, and still gets attacked |
| Democracy Club (UK) | Volunteers collect candidate data; open API | Active | Grants | Open data plus volunteers became national infrastructure |
| mySociety (UK) | TheyWorkForYou, WriteToThem | Active, with fragile funding | Grants, donations | Even iconic tools struggle to fund themselves |
| Full Fact (UK) | Fact-checking plus licensed AI tools | **Google ended funding Oct 2025** (over a third of income); about a quarter of staff cut | Grants, tool licensing | Depending on one funder is the biggest risk |
| **AJC Legislative Navigator** | Georgia bills by topic, votes, committees, donors | Active | Newspaper | Georgia bill tracking is covered. Partner and link, don't duplicate |
| Georgia Recorder / GPB Lawmakers | Statehouse news; nightly TV wrap-up | Active | Nonprofit / public media | Sources, and possible partners |
| GBPI / Georgia Public Policy Foundation | Budget and tax analysis (generally seen as progressive) and free-market analysis | Active | Think tanks | Ready-made documented arguments; label where each stands |
| AI voter-guide startups (VoteMate, Ballot Brief…) | Chatbot ballot guides | **WaPo, Oct 2026: across more than 3,000 chatbot answers, wrong or missing congressional district 22–48% of the time**; ZIP→district errors propagated; Amazon paused Alexa voter guides | Mixed | Exact districts and grounded data are non-negotiable |

Sources: https://calmatters.org/inside-the-newsroom/2026/04/trust-challenge-win/ ·
https://dicktofel.substack.com/p/bringing-digital-democracy-to-california ·
https://groups.google.com/g/google-civicinfo-api/c/9fwFn-dhktA ·
https://www.npr.org/2025/01/07/nx-s1-5251151/meta-fact-checking-mark-zuckerberg-trump ·
https://fullfact.org/technology/google-cuts-funding-to-full-fact/ ·
https://fortune.com/2025/07/10/more-than-90-x-community-notes-are-never-published-stuck-limbo-study/ ·
https://democracyclub.org.uk/about/funding/ ·
https://www.democracy.works/news/we-powered-democracy-in-2025 ·
https://en.wikipedia.org/wiki/Vote_Smart · https://en.wikipedia.org/wiki/Countable_(company) ·
https://ajc.com/politics/georgia-state-legislature/georgia-legislative-navigator-how-to-learn-about-laws-and-lawmakers-from-the-ajc/I6XVHKFK2VEC5F4X2KNLE2ZHKI ·
https://letsdatascience.com/news/washington-post-tests-six-chatbots-on-voter-guides-finds-err-64c77967
(secondary coverage of the WaPo test) ·
https://www.globenewswire.com/news-release/2026/05/19/3297708/0/en/Ballotpedia-Names-VoteMate-Official-AI-Partner.html

## Gaps: where we add value, and where we overlap

**Heavy overlap (don't compete):**
- Layer 1 election dashboards: TurboVote, Vote411, BallotReady, Ballotpedia, VoteMate.
- Layer 2 Georgia bill tracking: AJC Navigator, Plural, LegiScan.
- The "CivicLens Legislation Tracker" app.

**Underserved:**
- **Participation outside elections, in one place.** Hearings and testimony, the veto window, agency
  comment periods, PSC hearings, local boards and referenda. We found no Georgia aggregator.
- **Layers 3–5 for state and local decisions.** Claim → support → what's missing → what's uncertain →
  primary source. Fact-checkers give one verdict per claim; Kialo is unsourced.
- **Public version history plus a structured challenge process** for civic explanations.

**Risk inside the concept:** "anticipated" arguments are new ground. Keep them impersonal and clearly
labeled, and be extra careful near elections.

## Name check

| Found | What it is | Overlap |
|---|---|---|
| USPTO application "CIVICLENS", serial 99772668 | CivicLens LLC, Class 42, filed 2026-04-18, pending **[unverified: check USPTO TSDR]** | Trademark |
| "CivicLens Legislation Tracker" (iOS) | Personalized federal and state bill feed; contact your representatives | **Direct** |
| CivicLens (Esri partner) | Citizen engagement and service requests | Civic, different function |
| UChicago CAPP 30320 "CivicLens" | Simplifies commenting on federal regulations | Comment periods |
| Others | Hackathon "CivicLens AI" apps, CIVICUS Lens, a Czech project | Name clutter |
| "Polysense" | IoT sensors (Santa Clara); Belgian food-tech AI ($10.7M seed, Jul 2026) | Not civic, but crowded |

**Verdict:** pick a new name and get attorney clearance. See [naming.md](../product/naming.md).

Sources: https://markinton.com/trademark/civiclens-99772668 ·
https://mwm.ai/apps/civiclens-legislation-tracker/6776493131 ·
https://www.github.com/uchicago-capp-30320/CivicLens

## Funding and sustainability

**Funders in this space:**
- Knight, Democracy Fund, Hewlett and Rita Allen (the Knight News Challenge on Elections).
- Arnold Ventures and Lever for Change (Digital Democracy).
- Craig Newmark Philanthropies (automated fact-checking).
- UK: Luminate and JRCT.

**The trend is worrying.** Open-government funding is reported at about a tenth of earlier levels, and
OpenSecrets laid off a third of its staff in 2024 (https://www.thebulwark.com/p/open-government-nonprofits-are-dying-off-just-when-needed-most).

**Earned revenue that works elsewhere:**
- data and API licensing (BallotReady, Democracy Works, Ballotpedia);
- tool licensing (Full Fact);
- ads plus Patreon (GovTrack);
- subscriptions (Ground News);
- small-dollar membership (TheyWorkForYou);
- government engagement grants (mySociety).

**What hurt perceived independence:** concentrated platform money (Meta, Google), donor-network
associations (Ballotpedia), ownership (BallotReady), and "Soros" attacks (Democracy Works).

**What died:** Sunlight Foundation (2020), Brigade (2019), Countable (absorbed), Google's Representatives
API. Vote Smart is running deficits.

→ Policy in [funding.md](../policies/funding.md): publish every funder, cap any single funder at about
20%, take no money from parties, campaigns or PACs, and start under a fiscal sponsor.

## Legal and structure (not legal advice; confirm with counsel)

**501(c)(3) rules for voter education.** No intervention for or against candidates. Voter education is
allowed if it is nonpartisan, judged on all the facts and circumstances.
- **Rev. Rul. 78-248:** questionnaires go to all candidates, cover a broad range of issues, and use
  neutral wording with no editorial comment or ratings.
- **Rev. Rul. 80-282:** voting records need broad coverage, no editorial comment, and no election
  targeting.
- **Rev. Rul. 2007-41:** risk rises when content identifies candidates, approves or disapproves of
  positions, is timed to an election, or isn't part of an ongoing series.
- Neutral "how to participate" content is not lobbying. Urging a position on a bill would be.
- Sources: https://www.eitc.irs.gov/pub/irs-tege/rr78-248.pdf · https://www.eitc.irs.gov/pub/irs-tege/rr80-282.pdf ·
  https://nonprofitlawblog.com/501c3-electioneering-rules-voter-guides-candidate-questionnaires/

**Fiscal sponsorship.** Under Model A the sponsor owns the project, for typical fees of 5–10% (Code for
Science & Society charges 15%). Expect the sponsor to review election-season content.

**Georgia-specific:**
- **SB 202:** no prefilled absentee applications, and fines for sending applications to voters who
  already requested one. Voter-started tools are outside this.
- **Voter file:** commercial use is prohibited (O.C.G.A. § 21-2-225(c)).
- **AI deepfake bills:** SB 9 (2025) is reported as failed. Georgia had no statewide AI political-ad law
  as of October 2026 **[unverified]**.
- Sources: https://campaignlegal.org/cases-actions/voteamerica-v-raffensperger ·
  https://law.justia.com/codes/georgia/title-21/chapter-2/article-6/section-21-2-225

**Section 230 and defamation:**
- Section 230 protects us for **user challenges** we host. It does **not** protect **our own analysis**.
- Public officials must show actual malice. *Walters v. OpenAI* (Georgia, 2025) went for the defendant,
  partly because of its documented efforts to reduce errors.
- Georgia's anti-SLAPP statute is O.C.G.A. § 9-11-11.1.
- Implications: documented verification, fast corrections, and **media liability insurance**.
- Source: https://schjodt.com/news/artificial-intelligence-and-defamation-the-walters-v-openai-decision

## Top lessons

1. **Don't compete on the dashboard.** Link to or license election data, and put our effort into
   Layers 3–5 and participation outside elections.
2. **Copy CalMatters' safeguard.** AI output is a lead for humans. Publish only after human verification,
   and log every correction publicly.
3. **District mapping must be exact.** Address → official boundaries, never ZIP.
4. **Build IRS neutrality into the product.** Symmetric structure, no ratings, broad issue coverage, and
   care with timing near elections.
5. **Diversify funding from day one.** Cap single funders and publish all of them.
6. **Rename now.** Start under a fiscal sponsor, with counsel and insurance in place before public beta.
