# Log: Trial 003 (Fulton County 2027 budget)

Times are UTC on 2026-11-08. They measure **agent** wall-clock time, which isn't comparable to the time a person
would need. Steps overlapped: retrieval and discovery ran together, and the gate ran after every drafting pass.

| Step | Start → end | What happened | Friction / failures |
|---|---|---|---|
| Setup | 06:57 → 06:59 | Read the brief, CLAUDE.md, runbook v0.3, style guide v0.4, Trial 001's files and the tools README. Checked the proxy (open) | — |
| 0. Choose and justify | 06:59 → 07:00 | Re-scored the scout's pick with the rubric: 14/15, same as the scout (README). Categories: money and taxes; safety and courts | The rubric's "likely to move" is automatic for a budget the code says must be adopted by a deadline |
| 1. Find sources | 06:59 → 07:16 | Walked the Legistar Web API: Board events July 2026 to Feb 2027 → agenda items for 2026-08-05, 08-19, 10-21, 11-04 → roll calls → attachments. Found the **2027 meeting calendar item (26-0686)** the scout missed, and two budget-process discussion items. Found 43 department "2027" budget-hearing files (decks and updated versions) on the open data portal through the Socrata Discovery API. 11 web searches (leads only). Checked leads: the AJC (paywalled), Capital B (about the 2025 budget), the Sheriff's Office press-release page (nothing after July 2026), the Clerk's pages (public comment rules) and the FGTV and "Watch Past Meetings" pages (no usable static text). **Voices counted before drafting:** for the process, one commissioner (Ellis) plus the Board's own resolution; against it, three justice leaders (Gammage, Johnson, Labat) and one commissioner (Barrett) plus her resolution. Searched again for the vice chair, other commissioners and the leaders' letter; found nothing more. Discrepancies D1-D11 written (README) | The runbook's source list is for bills (versions on legis.ga.gov, fiscal note, O.C.G.A. sections). A county budget needed a different list (see "Where the runbook didn't fit", 1). The scout's notes were good leads but two were wrong or stale: the 2027 calendar was posted (D2), and the "4-1" vote count is not what the roll call shows (D1) |
| 2. Snapshot | 07:00 → 07:15 | 36 snapshot runs with `trials/tools/snapshot.py`: 33 sources retrieved, 2 blocked (Census QuickFacts and FindLaw: Cloudflare challenges), 1 transient 404 (S01, kept as evidence and retried once). Derived text: `pdf_markup.py` for the two resolutions; pdfplumber (`scripts/pdf_text.py`) for agendas, schedules and the budget book; a stdlib extractor (`scripts/pptx_text.py`) for four PowerPoint decks. `verify_hashes.py`: 33/33 OK | **S01 returned HTTP 404** from one server node (gasmp-legweb4) although the scout got it at 06:46; a retry 33 s later reached gasmp-legweb5 and matched the scout's hash. **Census QuickFacts and FindLaw** served Cloudflare challenges; recorded as blocked, not reloaded in a browser. The **Census Data API** now needs a key; not requested. The **Internet Archive capture** request (runbook 2.5) was denied by this session's permission system; not retried. The open data portal's own catalog endpoint returned nothing; the shared `api.us.socrata.com` endpoint worked |
| 3. Passages | 07:16 → 07:29 | 84 passages in `passages.spec.yaml`, built by `build_passages.py` with 0 errors. They hold the **full text** of County Code Sec. 2-261 (as amended) and Sec. 2-263, the full text of Resolution 26-0729, every roll call cited, and the whole Legistar history of 26-0729, so absence claims can be checked. Visual checks: S01 pp. 2-3 (strike/underline), S09b p.4 (signed scan, OCR text), p.77 (stacked table headings), p.78 (fund-balance rows); S01b (signed copy) pp. 2, 3 and 7 against S01 | `pdf_markup.py` caught the strikes but its line grouping misplaced bold words ("WHEREAS," moved to the end of a line), so unmarked passages come from a prefixed plain extraction and only the struck section from `--clean`. The builder hard-codes "Trial 001 (HB 1009)" as the title of passages.md; replaced by `scripts/retitle_passages.py` (header only). Trimmed two copyrighted news excerpts to the sentences cited |
| 4. Draft | 07:29 → 07:40 | `workspace.md` (30-second version + Layers 1-5, 3 options, 6 cards, "Questions to weigh"), `home.md` (4 items), `page.yaml` (slug `fulton-budget-2027`). A test render of the prototype with Trials 001 and 003 together worked (scratch only; phone screenshot checked) | Several template fields assume a bill (see below). One pronoun ("he" for the chairman) had no source; replaced by the role |
| 5. Deterministic gate | 07:32 → 07:44 | First run: PASS with 19 warnings; after fixes, **0 errors, 0 warnings** (234 units). Reading grade 6.6 / 7.8 / 7.4 / 6.3 for the 30-second version and Layers 1-3. Hashes 33/33. Step 5 checklist in `verification.md` | One quote failed: the source breaks "DECISION-MAKING" across a line. The checker's "several sentences share one marker" warning fires on "U.S.", "a.m.", middle initials and "Jr."; reworded rather than ignored. The number check needs the year or the outlet name ("FOX 5") inside a cited passage, so several sentences gained a second citation; sentences that carry our capture date ("On Nov 8, ...") are tagged `{analysis}` because no passage holds that date |
| Records | 07:36 → 07:47 | `sources.yaml`, README (status table, D-list, hosts), `verification.md`, `review.md`, this log; derivation scripts copied to `scripts/` and re-run to confirm the derived files reproduce exactly | — |
| *Round 1 (independent checks)* | by 08:14 | Run by the coordinating session, not the drafter: a blind source check (128 supports, 23 partial, 0 unsupported, 0 wrong span; 10 questionable tags; 19 framing issues; 234/234 spans grounded), a calibration checker (12/12 planted errors caught, plus 3 real issues in the true text) and the bias panel. **The panel failed:** supporter's advocate 3/5, critic's advocate 3/5, editor neutrality 3/5 and clarity 3/5 (3 high, 14 medium, 19 low; 20 flip-test asymmetries). Reports copied unchanged to `verification/round1/` | No factual errors were found. The failures were about balance and precision: labels not set by one visible rule, card 6 missing the county's own jail evidence, "no reasons" claims that didn't disclose the unreviewed meeting video, and "failed" used for a resolution whose motion failed |
| Revision 2 | 08:15 → 08:40 | Read all four reports and the three calibration flags; saved revision 1 as `verification/round1/workspace.r1.md`. Rewrote the page against a 77-row disposition table (`verification.md`): one printed evidence-label rule applied to all six cards; card 6 given the county's consent-decree evidence; one note on the unreviewed video; precise procedural terms; both sides' missing documented points added (the 1953-2018 history, non-member votes, Ellis's full committee sentence, the hearing slots and written requests, the panel objection, Resolution 26-0729's policy case); cards paired by topic; questions reworded. Added P-086 and P-087, widened P-046, rebuilt with the updated tool (86 passages). Gate: 311 units, **0 errors, 0 warnings**; grades 7.4 / 8.0 / 7.9 / 7.4; Layer 4 10.6 → 8.6; hashes 33/33 | The advocates' suggestions conflicted in two places (the "narrative form" point in card 4; whether to keep the timing question); both views and the choice are in `verification.md` (rows 29 and 45). The first rewrite raised Layers 1 and 2 above the grade limit (8.7, 8.6) and Layer 4 to 10.6; splitting sentences and trimming quotes to exact key phrases fixed it. The coordinator had already added `title` and `header` keys to the passage spec and changed one page.yaml chip; both kept, and `scripts/retitle_passages.py` was deleted as no longer needed |
| *Round 2 (independent checks)* | by 09:16 | Run by the coordinating session on revision 2: the blind source check (189 supports, 13 partial, 0 unsupported, 0 wrong span; 9 questionable tags; 15 framing issues; 202/202 spans grounded) and the bias panel. **The panel failed again:** supporter's advocate 3/5 (no high items), critic's advocate 3/5, editor neutrality 3/5 and clarity 3/5 (1 high, 9 medium, 24 low; 16 flip-test items). No calibration run this round. Reports copied unchanged to `verification/round2/` | Again no factual errors. The editor and critic found that the framing around the cards leaned mildly toward the current process: unexplained votes used as rebuttals against critics only, a page-supplied "tight finances" motive, "neither allows nor forbids", and card 4 dropping P-029's "14 of them" clause. The supporter found card 1 weak: no sponsors' reason, and a net 0.4% that hid larger changes |
| Revision 3 | 09:16 → 09:50 | Read all four reports in full; saved revision 2 as `verification/round2/workspace.r2.md` and `home.r2.md`. **Minutes search, 09:19 to 09:22 UTC:** Legistar's meeting pages showed "Published minutes: Not available" for Oct 21 and Nov 4, and the API listed no minutes file (archived as S05d). Each page linked the Clerk's "post agenda minutes"; both archived in full with `snapshot.py` (S05b, S05c). They record motions, seconds and votes but **no reasons**, and list the budget updates only as "DISCUSSED". The Oct 21 minutes were ratified Nov 4; the Nov 4 minutes are tentative until Nov 18. Per-item minutes notes were empty. No video transcribed. Then rewrote against a 73-row disposition table (`verification.md`): votes taken out of the cards; the motive, the legal conclusion, the rent example and "no exceptions" removed; "as presented" stated once with its 2027 time frame; the Oct 7 entry, 30-second version, card 1, card 4's labels, Q1 and the timing question redone; per-part labels on every card; cards 3 and 4 swapped; "justice officials" for the coalition's self-name; Layer 2 opening cut to about 90 words. Added P-088 to P-096 (95 passages). Gate: 329 units, **0 errors, 0 warnings**; grades 7.9 / 8.2 / 8.3 / 7.2; Layer 4 8.9; hashes 36/36 | Three new number warnings (minutes entries carry no dates) were fixed by adding each minutes document's heading as a passage (P-090 widened, P-096). The renderer splits card meta lines on "·", so my first per-part labels would have shown only their first part; a test render caught it, and the parts are now separated by periods. While fixing card 1, the budget book's memos showed the Jan 7, 2026 changes were the CFO's recommendations, approved Jan 21, so the page no longer calls them the Board's changes |
| *Round 3 (independent checks)* | by 10:56 | Run by the coordinating session on revision 3, with the gate tools updated after revision 3: blind source check of workspace.md (205 supports, 6 partial, 0 unsupported, 0 wrong span; 3 questionable tags; 12 framing issues; 329/329 spans machine-checked as grounded) and of home.md (3 supports, 1 partial; 6/6 grounded), and the bias panel. **Passed:** supporter's advocate 4/5, critic's advocate 4/5, editor neutrality 4/5 and clarity 4/5 (0 high, 7 medium, 14 low; 14 flip-test items). The new whole-number check gave 4 warnings. Reports copied unchanged to `verification/round3/` | No unsupported claim. The flags were precision ones: reporters' paraphrases inside quote marks, a net figure without its gross figures, "the sponsors say" for a Board finding, and claims a little broader than their passages |
| Revision 3.1 (accuracy fixes only) | 10:58 → 11:15 | Saved revision 3 and the round-3 reports to `verification/round3/`. Made only the 14 fixes the coordinator listed under the protocol's "When a page is done" rule (line by line in `verification.md`, Round 3): no card's claim, label or side changed. Logged every other suggestion as an open item, and ten pairs of contradicting suggestions side by side without resolving them. Gate: 332 units, **0 errors, 0 warnings** with the new whole-number check; home.md 7 units, 0 and 0; grades 7.9 / 8.0 / 7.9 / 7.3; hashes 36/36 | Swapping home.md's citation from P-042 to P-036 removed the only cited passage carrying "2027", so the new check warned; "the 2027 budget" became "the county budget" rather than citing an unrelated passage. Fix 2 had to keep the 0.4% point a challenge: dropping the net figure would have left only gross changes, which support card 1. So the gross and net figures both stay, and what they show is tagged `{analysis}` |

## Where the runbook didn't fit (v0.3 was written for bills)

The three biggest gaps come first.

1. **The decision's key document doesn't exist yet, and the runbook has no "not yet published" path.** Steps 3 to
   5 assume a text to extract and explain. Here the proposal is due 2026-11-15 (a Sunday), the hearing isn't posted,
   and the 2027 schedule is only proposed. I adapted by: (a) a README status table of what is not published, where
   we looked and when; (b) writing "not published" as dated, positive statements about what each page showed
   ("the newest notice was for October's hearings") rather than bare absence claims, since the style guide allows
   absence claims only with the whole text archived; (c) an append-only "How to update" plan in the README. Gaps:
   the capture date lives in `sources.yaml`, not in any passage, so dated sentences can't be cited and fail the
   number check unless tagged `{analysis}`; and the runbook needs a change-tracking step (re-snapshot and diff the
   budget page, notices page and Legistar events on a schedule).
2. **The source list, types and ingestion steps are bill-specific.** Step 1's priority list (bill versions,
   legis.ga.gov status, fiscal note, O.C.G.A. sections) and step 2's legis.ga.gov browser method don't apply. The
   county analogues were: the code section and its amending resolution (Legistar attachments), Legistar matter
   histories and roll calls (Web API: events → items → votes; **no tallies are given**, so every count is
   `{computed}`), last year's budget book, public notices, meeting schedules, and the offices' own requests (open
   data portal decks, PowerPoint files the tools couldn't read). The type vocabulary has no "primary, local
   government" value, so county records are typed `primary-government` and the `local-government` type went
   unused. A county-budget runbook (as the scout suggested) should list these sources, the API chains and the
   formats.
3. **Documented arguments in a local dispute come mostly from news, and one side's reasons are mostly votes.** The
   justice leaders' letter is not published, so their positions come only from two news reports, against the
   rule that news is "secondary support only, never the only source for a fact". The Board majority's reasons for
   keeping the 5% limit appear nowhere in writing; they may be in meeting video (S16), which the runbook has no
   method for (transcription, timestamps as locators). The page says plainly that the process side is thinly
   documented and why. The runbook's "count voices per side" rule worked, but it should add "votes are the base
   rate; reasons may only be on video".

Other places it didn't fit or needed a choice:

4. **Option template.** For a budget, the status quo is not "no change": the code makes the proposal the operating
   budget until adoption, so "adopt as presented" is the default path (Option A). The field "What happened" became
   "Where it stands" for all options, because two of three are still ahead. "Reject" is not a real option on the
   record and is explained, not listed.
5. **Card sides.** "Supports / opposes the law" didn't fit a dispute with three positions (the process, its critics,
   and the justice offices' funding asks). Sides used: "supports the current process", "critical of the current
   process", "county justice leaders".
6. **30-second version and Layer 2 labels.** "What it does" was kept (the budget "will set how much the county can
   spend"), but "What would change" became "What's different this year", plus "Not published yet" and "The 2026
   budget, for scale" sections, because the 2027 contents are unknown.
7. **Layer 1 order.** Kept Trial 001's order (What happened, Coming up, What you can do) for template consistency.
   For an upcoming decision, "Coming up" first may read better; worth a usability test.
8. **Bot-check fallback vs. the brief.** Runbook step 2.3 says to reload bot-checked pages in a headless browser;
   the brief says never to work around a block. I treated Cloudflare challenges as blocks. The runbook should say
   which is which (a site that needs JavaScript to render vs. an anti-bot challenge).
9. **Internet Archive captures (step 2.5)** were denied by the session's permission system. The runbook should say
   who requests captures (the agent, or the coordinator after review).
10. **Transient errors.** "One request per source" met a load-balanced server that returned 404 from one node. I kept
    the failed response, retried once after a pause, and recorded both. The runbook should allow one retry for 404
    or 5xx responses, never for 403 or challenges.
11. **Scanned PDFs.** The signed resolution has no text layer and there is no OCR tool here; I compared three pages
    visually and cite the draft text. The runbook needs a rule (OCR as a dependency, or visual comparison of named
    pages, recorded).
12. **Per-household numbers.** The style guide asks for human-terms conversions, but no population or household
    source was reachable (Census blocked; its API needs a key). I used "cents of every General Fund dollar"
    instead. The project needs one archived population/household table per county.
13. **Election-adjacent timing.** The leaders' "wait until January" ask is about who votes on the budget (incoming
    board members). The page reports their reason and describes no election, but there is no rule for this case.
14. **Multi-part evidence labels.** The card template has one evidence field, but the printed rule (round 1) gives
    each part of a claim its own label. Revision 3 writes one short "Part: Label (basis)." sentence per part. The
    runbook should give a format, because the prototype splits the meta line on "·" (see tool changes).
15. **Correction to item 4 (revision 3).** "The code makes the proposal the operating budget until adoption"
    overstated P-005. The proposal governs spending "for the following year", so it can serve as the operating
    budget only in 2027 before adoption. Option A is still the path if the Board makes no changes.

## Shared tool changes needed (described, not made)

- `build_passages.py`: ~~take the title and intro from the spec instead of hard-coding Trial 001's~~ (done by the coordinating session before revision 2; `scripts/retitle_passages.py` deleted); allow a
  per-file option for the line-number prefix (it can't be used on agendas, where lines start with real numbers).
  A JSON mode (passages from fields, not raw text) would make roll-call passages readable.
- `snapshot.py`: add PowerPoint/Word types to the extension map (the decks were saved as `.bin`), and write text for
  PDFs and decks (this trial's `scripts/pdf_text.py` and `scripts/pptx_text.py` could move to `trials/tools/`).
- `check_citations.py`: add single capital initials, "Jr.", "U.S." and "a.m./p.m." to the abbreviation list; consider
  a `{captured: date}` tag for dated "as of" sentences.
- `prototypes/hb1009/template.html`: the header says "TRIAL 001" for every trial.
- `render_prototype.py`: `parse_card_meta` splits the whole meta line on "·" and keeps only the part that starts
  with "Evidence:", so an evidence label that uses "·" between its parts loses every part after the first, without
  warning. Either parse "Evidence:" to the end of the line, or document the separator in the runbook.
- `readability.py`: card meta lines (`<!-- nocheck -->`) are scored as prose, so a long per-part evidence label raises
  Layer 4's grade. Consider scoring them separately.
- `check_citations.py`: the number check is a substring match, so a short date number such as "4" in "Nov 4" can
  pass on an unrelated digit in a cited passage. A date-aware check would be stricter. *(Done after revision 3: the whole-number check now also checks identifiers such as 26-0729.)*
- `render_prototype.py`: it reads the page's revision as "3" from "revision 3.1". Minor; a revision label with a
  decimal point needs the whole number read.
- Dependencies: no new project dependency. pdfplumber (listed) and the standard library did all extraction. The
  system tools `pdftoppm` (page images for visual checks), `pdftotext` (exploration only) and headless Chrome (a
  screenshot of the local test render) were used outside the project's code, and fetched no source.

## Rules learned

1. **Primary vote records beat summaries, even reputable ones.** A civic group's "4-1" was really 4 yes, 1 abstain
   and 1 "did not vote" (D1). Legistar roll calls give names and values but no totals; count them and show the work.
2. **Check what a scout says is missing.** The 2027 calendar "not posted" was attached to a 2026-10-21 consent item
   (D2). Search the meeting records, not only the website.
3. **A failed request isn't always a block.** Keep the failed response, retry once for 404/5xx, and never for 403 or
   challenges.
4. **Write "not yet published" as what each page showed on a date.** It is checkable; "no X exists" is not.
5. **For a fast-changing local record, archive whole histories** (Legistar histories, roll calls), so "no later
   action" can be checked.
6. **County resolutions carry bill-style strike/underline markup.** `pdf_markup.py` works on them, but check bold
   words' order and compare against the signed copy.

### Rules learned from round 1

7. **Print one evidence rule and set every label by it.** "Supported" needs a record that settles the point: a rule's
   own text for what the rule says; for money, needs or effects, a county record not written by the people making
   the claim. Labels then differ only where the evidence does (both advocates and the editor converged on this).
8. **Use each passage for everything it says.** Card 6 quoted P-049's "no additional resources" but not its next
   sentence, "additional resources will be needed for consent decree compliance". Both sides' strongest points often
   sit in the same county passage.
9. **Say what you didn't review before saying no reasons exist.** The Legistar history links meeting video; "no
   reasons in the records" must name the unreviewed video, once.
10. **Procedural precision matters.** A motion to approve failed; the resolution was filed later. Name every member's
    vote on every roll call, or none.
11. **A vote is not an argument.** A procedural vote (filing) doesn't belong under "Who disagrees", and one person's
    argument (a resolution's sponsor) shouldn't be credited to "critics".
12. **Cite the document you archived.** "Last year's proposal" was really the adopted budget book; widening P-046 to
    the book's own statement that it reprints the proposal's budget message fixed the provenance.
13. **Short sentences beat dropped facts.** The first rewrite added both sides' missing points and pushed card text to
    grade 10.6; splitting sentences and quoting only exact key phrases brought it to 8.6 without losing any.

### Rules learned from round 2

14. **A vote is not evidence for either side.** Unexplained votes, used only against one side, read as rebuttals. List
    votes once under What happened, keep them out of the cards, and say why.
15. **Never supply a motive.** When no one states a reason, say so and stop. Facts placed next to the gap ("county
    records show tight finances") work as an unlabeled anticipated argument.
16. **Check the other half of a cited sentence.** Card 4 quoted "27 of 41" and dropped "14 of them received budget
    increases that exceeded 5%", the critics' strongest figure, from the same clause.
17. **Say what the text shows, not what it means legally.** "Mentions no cap" is checkable; "neither allows nor
    forbids" decides the question the two sides dispute.
18. **Read a rule's time frame.** "Serves as the operating budget until adoption" applies to "the following year"; a
    2027 proposal cannot operate in 2026.
19. **Check who made a change before naming them.** The budget book's change columns don't say who proposed a
    change; its memos do (the Jan 7 changes were the CFO's recommendations).
20. **Open every document link on a meeting page.** Legistar said "Published minutes: Not available", yet the Clerk's
    "post agenda minutes", which the Board later ratifies, were posted under "Post Agenda".
21. **Use each record in both directions.** The hearing notice, the net and gross budget changes, and the news-report
    caveat had each been used for one side only.

### Rules learned from round 3

22. **Quote marks belong only around words the passage gives the speaker.** A reporter's paraphrase of what someone
    said is written "[Outlet] reported that ...", without quote marks.
23. **An adopted resolution's findings are the Board's.** "The sponsors say" was wrong for a WHEREAS clause the Board
    adopted; name the sponsors separately.
24. **A changed citation can drop a number's support.** Re-run the gate after every citation swap, not only after
    new text.

## Automation notes (not yet eligible: 0 of 3 runs)

- **Legistar Web API pulls** (events → agenda items → roll calls → attachments) were deterministic and fast; a good
  first candidate once three manual runs agree. **Change watchers** for the budget page, the notices page, Legistar
  events and item 26-0686's status would catch the proposal, the hearing notice and the final schedule.
- **Open-data discovery** (Socrata catalog for "Budget Presentation_2027") is easy to automate; reading the decks
  needs the PowerPoint extractor.
- **Minutes checks** are scriptable: the Legistar API's `EventMinutesStatusName` and `EventMinutesFile`, the meeting
  page's "Post Agenda" link, and the next meeting's "Ratification of Minutes" item, which shows when tentative minutes
  become final.
- **Manual for now:** deciding which records matter, settling discrepancies, choosing passages, and writing the cards.
