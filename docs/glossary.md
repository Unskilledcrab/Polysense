# Glossary

## Product terms

| Term | Meaning |
|---|---|
| **Act now / Coming up / What happened** | The three homepage sections, in that order: opportunities open now, upcoming ones, and recent outcomes |
| **Opportunity** | One thing a person can do: vote, testify, send written comment, contact an official, attend or watch. It has a time window and an audience |
| **Participation power** | How much influence an opportunity carries. *Decide*: you vote directly. *Input*: formal testimony, comment or contact. *Observe*: watch or attend |
| **Decision workspace** | The in-depth page for one decision (for example, a bill). It is organized in 5 layers |
| **Layers 1–5** | 1. What can I participate in? 2. What decision is being made? 3. What are the options and tradeoffs? 4. What arguments will I hear? 5. What does the evidence show? |
| **30-second version** | The three lines at the top of every workspace: what it does, who it affects, and what happens next |
| **Argument explorer** | Layer 4. Each argument has a claim, what supports it, what's missing, what challenges it, what's uncertain, and the evidence |
| **Documented argument** | An argument actually made by an identifiable speaker, with a date, venue and verbatim source passage |
| **Anticipated argument** | An argument people may encounter. It is clearly labeled and never attributed to anyone |
| **Claim type** | *factual* (checkable), *predictive* (about the future), *values* (about priorities), *computed* (arithmetic from cited numbers), *editorial* (our framing, such as "what's missing") |
| **Status** | Every published item shows one: Verified, Under review, Disputed, Source changed, or Outdated |

## Evidence and trust terms

| Term | Meaning |
|---|---|
| **Source** | A document or page, such as a bill version, fiscal note, testimony or report |
| **Snapshot** | An immutable, timestamped copy of a source with a SHA-256 hash |
| **Passage** | A numbered, citable piece of a snapshot (`P-012`) with its page or paragraph location |
| **Citation** | A link from a sentence to the passage that supports it |
| **Support check** | A human confirms that the cited passage actually supports the sentence, not just that it exists |
| **Revision** | An append-only, hashed version of a published item. Every change creates a new one |
| **Change class** | Why a revision exists: *typo*, *update* (new facts), *correction* (we were wrong) or *re-evaluation* (new judgment) |
| **Challenge** | A public report that something is wrong, missing, unsupported, unfair or outdated |
| **Coverage log** | The public record of which topics we considered, the scores they got, and why we covered them or not |

## Process terms

| Term | Meaning |
|---|---|
| **Trial** | One manual, end-to-end run of a process, recorded in `trials/NNN-slug/` |
| **Runbook** | Written step-by-step instructions for a manual process |
| **Automation gate** | The five conditions a step must meet before it may be automated |
| **Shadow mode** | Automation runs beside the manual process and is compared against it, but isn't published |
| **Gold data** | The verified outputs of manual trials, used as the standard automation must meet |
| **Verification protocol** | The AI-only way we check facts and fairness during the proof of concept (no human in the loop) |
| **Independent verifier** | A fresh AI agent that sees only the page and its passages, never the drafter's notes, and judges each sentence |
| **Evidence span** | The exact words a verifier quotes from a passage to show it supports a sentence; code confirms they exist |
| **Bias panel** | A supporter's advocate, a critic's advocate and a standards editor who each review a page for fairness |
| **Flip test** | Asking whether a sentence about one side would be written the same way about the other side |
| **Seeded-error calibration** | Planting known errors in a test copy and measuring how many a verifier catches (its **catch rate**) |

## Georgia legislative terms

| Term | Meaning |
|---|---|
| **General Assembly** | Georgia's legislature: the House (180 members) and the Senate (56) |
| **Biennium** | The two-year term (2027–2028). Bills from 2027 that did not pass can still move in 2028 |
| **Legislative day** | A day the legislature meets. The session runs 40 legislative days, which is not the same as calendar days |
| **Crossover Day** | Legislative day 28. The last day a bill can pass its first chamber and stay alive that year |
| **Sine Die** | Legislative day 40, the last day of the session |
| **Veto window** | The 40 days after Sine Die when the Governor signs or vetoes bills |
| **Substitute** | A rewritten version of a bill, usually adopted in committee. It is labeled with an LC number |
| **Fiscal note** | An official cost estimate for a bill, prepared on request by the Department of Audits and Accounts and the Office of Planning and Budget |
| **Local act** | A bill affecting only one county or city. It is excluded from pilot coverage |
| **Rules Committee** | The committee that decides which bills reach a floor vote |

## Data and technology terms

| Term | Meaning |
|---|---|
| **OCD division ID** | A standard ID for a political area, such as `ocd-division/country:us/state:ga/sldl:54` |
| **Division vs. boundary** | A division is the identity of an area. Its boundary is a dated shape that can change with redistricting |
| **Point-in-polygon** | Finding which district shapes contain a location |
| **Open States / Plural** | An open legislative data project. Its legislator data is public domain (CC0) |
| **LegiScan** | A legislative data service with a free API. Its data is licensed CC BY 4.0 |
| **My Voter Page (MVP)** | The Georgia Secretary of State's official lookup for registration, polling place and sample ballot. We link to it and never copy it |
| **SPLOST** | Special Purpose Local Option Sales Tax, a local sales tax voters approve by referendum |
