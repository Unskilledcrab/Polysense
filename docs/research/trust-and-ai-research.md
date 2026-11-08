# Trust, argument, clarity and AI research (as of 2026-11-08)

> Most sources were read through search summaries, because several hosts were blocked from the research
> environment. **[unverified]** marks items to confirm. Each section ends with what we do because of it.

## A. How to represent arguments

- **Toulmin model** (claim, grounds, warrant, backing, qualifier, rebuttal) maps almost one-to-one onto
  the argument explorer:
  - grounds/backing → *supports*;
  - warrant → the unstated assumption, under *what's missing*;
  - qualifier → *uncertain*;
  - rebuttal → *challenges*.
- **IBIS** (Issue → Positions → Arguments) was designed for public policy and matches Decision → Options
  → Arguments (https://en.wikipedia.org/wiki/Issue-based_information_system).
- **AIF (Argument Interchange Format)** makes every support and attack link a first-class object that can
  be cited and versioned (https://arg-tech.org/wp-content/uploads/2011/09/aif-spec.pdf).
- **Argdown** is text-based argument mapping that diffs well in git (https://argdown.org).
- **Do maps help laypeople?** The evidence comes from *training*:
  - courses produce about 0.7–0.8 SD gains (https://www.ncbi.nlm.nih.gov/pmc/articles/PMC6279835/);
  - there is no evidence that one-off viewing of a node-and-link map helps general readers.

→ **We store** IBIS at the top, Toulmin parts per argument, and AIF-style support/attack relations. **We
show** linear cards, not graphs.

## B. Fact-check and trust standards

- **ClaimReview:** Google phased out ClaimReview rich results in 2025, so it now matters mainly for
  machine-readable exchange (https://www.niemanlab.org/2025/06/google-kills-the-fact-checking-snippet/).
- **IFCN Code of Principles** has five commitments:
  1. nonpartisanship and fairness;
  2. transparency of sources;
  3. transparency of funding and organization;
  4. transparency of methodology;
  5. open and honest corrections.

  (https://ifcncodeofprinciples.poynter.org/the-commitments)
- **EFCSN:** stricter standards, for European organizations only, so a benchmark for us
  (https://efcsn.com/code-of-standards/).
- **The Trust Project:** 8 indicators: best practices, author expertise, type of work, citations,
  methods, local sourcing, diverse voices, actionable feedback (https://thetrustproject.org/).

→ We adopt the IFCN commitments as policy and use the Trust Project indicators as a page checklist. We
emit ClaimReview only for discrete fact-checks.

## C. What helps people evaluate political information

- **Backfire effect: mostly doesn't replicate.** Wood & Porter (2019) ran more than 10,000 participants
  on 52 issues and found none (https://pmc.ncbi.nlm.nih.gov/articles/PMC7462781). Correct freely.
- **Prebunking / inoculation** raises recognition of manipulation techniques by about 5% in field
  studies (https://pmc.ncbi.nlm.nih.gov/articles/PMC9401631). The effect fades without refreshers.
- **Lateral reading:** professional fact-checkers leave a site to check its source. Training improves
  students' credibility judgments
  (https://cor.inquirygroup.org/research/lateral-reading-and-the-nature-of-expertise).
- **Accuracy nudges** reduce the intent to share false headlines by about 10% in relative terms. A
  smaller effect has been reported in replications (https://dspace.mit.edu/handle/1721.1/144231).
- **Hostile media effect:** partisans on both sides see neutral coverage as biased against them.
- **Community Notes:** slow (about 24 hours), and most notes never show
  (https://arxiv.org/html/2510.09585v3).
- **Labels:**
  - **Implied truth effect:** when some items are labeled, unlabeled false items look more credible.
  - An "**AI-generated**" label lowered trust in *true* headlines too, because readers assumed no human
    oversight (https://pmc.ncbi.nlm.nih.gov/articles/PMC11443540).

→ We:
- correct without fear;
- add short "how to spot a misleading statistic" prebunks;
- put a "Who is this?" panel with outbound links on every speaker and source;
- **give every item a status**;
- **describe AI use as a process** with reviewer and date;
- expect both sides to see bias, and counter it with identical templates and published criteria;
- use bridging-style agreement only to rank challenges, never to verify.

## D. Communicating uncertainty

- **IPCC calibrated language:**
  - a likelihood scale (virtually certain 99–100% … unlikely 0–33%);
  - separate confidence levels, based on the strength of evidence and the agreement among sources.
- **Lay readers misread verbal terms** ("very likely" is read as about 62%). Pairing words with numbers
  reduces the error.
- **Numeric ranges** barely reduce trust; vague verbal hedging costs more (van der Bles et al. 2020,
  https://core.ac.uk/download/305110478.pdf).
- **Visuals:** quantile dotplots beat error bars for lay readers.

→ We use the fixed evidence vocabulary in [experience.md](../product/experience.md), pair it with
numbers when sources give them, always say *why* something is uncertain, and use dotplots for
projections.

## E. Plain language and accessibility

- **Federal Plain Language Guidelines** are now hosted at
  https://digital.gov/resources/federal-plain-language-guidelines.
- **Reading level for voter information:**
  - recommendations range from about 3rd grade (EAC) to 8th grade (UCS ballot-design report);
  - the Center for Civic Design stresses testing with real voters over formulas.
- **WCAG 2.2 AA** became ISO/IEC 40500:2025. New criteria include Focus Not Obscured, Target Size
  (24×24) and Accessible Authentication (https://www.w3.org/WAI/news/2025-10-21/wcag22-iso).
- **Language needs:**
  - Gwinnett is Georgia's only county covered under VRA Section 203, for Spanish (2021 determinations);
  - new determinations are expected around December 2026 **[unverified]**;
  - advocates also push for Korean, Vietnamese and Chinese
    (https://www.advancingjustice-atlanta.org/news/languageaccess203).
- **Mobile:** 15% of US adults are smartphone-only, concentrated among lower-income adults
  (https://www.pewresearch.org/internet/fact-sheet/mobile/).

→ We build mobile-first and low-bandwidth, meet WCAG 2.2 AA, target grade 6–8 **confirmed by testing**,
and add Spanish first after the pilot, with human-reviewed translation.

## F. Corrections and versioning practice

- **Wikipedia:** every revision is public, can be diffed and has a permanent link. The model to follow.
- **PolitiFact** distinguishes updates, corrections and new ratings, and keeps a corrections list.
- **Full Fact** pushes for corrections in the body of the piece.
- **Factcheck.lk** marks voided checks at the top but keeps the original text.
- **Tools:** NewsDiffs and diffengine track article changes. C2PA is for media, not text.

→ We keep append-only revisions with permanent links, SHA-256 hashes and public diffs, classified as typo,
update, correction or re-evaluation, plus a site-wide corrections log.

## G. Web archiving for evidence

- **Internet Archive Save Page Now 2:**
  - S3-style keys;
  - community tools cite about 12 concurrent captures and a 10-per-day limit per URL **[unverified]**;
  - captures can later be excluded at a site owner's request.
- **Perma.cc:** about $10–100 a month for individuals, or free through library or court registrars
  **[unverified]**.
- **WARC / WACZ** are the standard archive formats. For PDFs, store the original bytes, the hash, the
  URL, the time and the headers.
- **Memento (RFC 7089)** handles lookups of a page as it was at a given time.

→ Our own hashed copy is authoritative. Save Page Now serves as an independent witness, and Perma.cc is
used only for the highest-stakes items.

## H. Grounding AI in sources

- **Anthropic's citations feature** returns the cited span and its location (characters, page, or a
  content block). It shows *where* text came from, **not whether it supports the claim**.
- **Legal research tools built on retrieval** still hallucinated 17–33% of the time in Stanford
  RegLab/HAI tests, including "misgrounded" citations: real sources that don't support the claim
  (https://law.stanford.edu/publications/hallucination-free/).
- **Model graders are no substitute for experts.** In CaseSumm, LLM judges agreed with human experts no
  better than automatic metrics did (https://arxiv.org/pdf/2407.12848).
- **AI search tools** were wrong on more than 60% of citation queries in a March 2025 test
  (https://www.cjr.org/tow_center/we-compared-eight-ai-search-engines-theyre-all-bad-at-citing-news.php).
- **Election incidents:**
  - Proof News (Feb 2024): 51% of answers inaccurate;
  - Grok gave wrong ballot deadlines for nine states (Aug 2024);
  - 2026 midterm studies found 13.9–40% error rates on voting questions
    (https://www.kpbs.org/news/science-technology/2026/11/02/ai-chatbots-are-becoming-political-advisers-their-answers-can-mislead-voters).
- **Policies to model:**
  - AP: AI output is unvetted source material.
  - CalMatters: AI tip sheets go to reporters, not the public.

→ We:
- draft only from the archived, hashed passages;
- run a deterministic gate;
- have a human check each citation for support;
- use a second reviewer for named-speaker and election content;
- **never** produce AI-drafted voting logistics;
- publish our error rates.

## I. Laws on AI and election content

- **Georgia:** no statewide AI political-ad disclosure law was in force as of October 2026. SB 9 (2025)
  is reported as failed **[unverified]**.
- **Federal:**
  - The FEC says its fraudulent-misrepresentation rule covers AI (2024).
  - The FCC's broadcast disclosure proposal was apparently never finalized **[unverified]**.
  - A December 2025 executive order targets state AI laws.
- None of these target AI-assisted summaries on a nonpartisan information site.

→ We disclose AI use voluntarily, **never generate synthetic audio, images or video of real people**,
watch the 2027 session, and have counsel review before any paid promotion.
