# Log: Trial 002 (PSC Docket 57171)

Times are UTC on 2026-11-08 and measure **agent** wall-clock time, which isn't comparable to a person's time.
The brief came from a coordinating session with a scout's leads (docs/research/upcoming-decisions-2026-11.md);
every lead was re-checked on a primary page and re-snapshotted here.

| Step | Start → end | What happened | Friction / failures |
|---|---|---|---|
| Read | 06:57 → 06:59 | Brief, CLAUDE.md, runbook v0.3, style guide v0.4, Trial 001's files, the tools and their README | — |
| 0. Choose | (given) | The brief chose Docket 57171. Scored it 12/15 (scout: 13): evidence lowered to 2 for redactions and missing transcripts (README) | — |
| 1 + 2. Find and snapshot | 07:00 → 07:16 | psc.ga.gov and services.psc.ga.gov reachable. Snapshotted the docket page, the filing list (32 items), the scheduling order (.docx and signed scan), the 2026-05-28 fuel-case short order and 2026-08-04 order, the legal notice, all five parties' testimony, Staff's errata, Georgia Power's hearing-request responses, the motion to strike and response, the calendar and three event pages, the public-witness, public-comment and FAQ pages, and all five commissioners' PSC pages. Discovery search, then 4 news leads (3 retrieved, 1 blocked) | **The docket page fills its filing table with JavaScript**; snapshot of the page alone has no filings. Fetched the JSON the page requests. The first try without `pageNumber=1` returned **HTTP 200 with an empty body** and `snapshot.py` reported no error (discarded and redone). Filings are `.docx`, `.zip` and `.xlsx`, which `snapshot.py` saves as `.bin` with no text; **three key files are scans with no text layer** (the signed order, Staff's errata, RSM's testimony) |
| Reading | 07:03 → 07:18 | Read every filing used. Wrote stdlib `.docx` and `.xlsx` text extractors, a pdfplumber extractor that keeps page and margin line numbers in the `pN: LINE` form `build_passages.py` already strips, and a safe unzip (flat names, new directory). Checked the signed order scan against the `.docx` by eye (pp. 1, 3, 4, 7: match) | **Found that the scout's "$329 million" quote was corrected** by Staff's errata of 2026-10-07 ("$328 million"), and that **every Option 1 and Option 2 estimate is blacked out** in the public spreadsheets. The errata could only be read by eye. RSM's testimony (which the scout had not read) argues for wider access to real-time pricing and takes no position on the options |
| 1 (again). Count voices | 07:09 → 07:13 | Per question, not per side: take-or-pay contracts: Option 1 = Georgia Power, GIPL; Option 2 = PIA Staff; either = GAM. Pipeline fees: Georgia Power (+GAM) vs. Staff vs. GIPL's variant. Other costs: Staff and GIPL vs. Georgia Power. Base trade-off: GAM (+Georgia Power) vs. Staff, GIPL. Option 2 is thin (one party). Searched for more: news quotes one commissioner, a company spokesperson and a non-party advocacy group, but no one else argues for Option 2. Said so on the page | A news story (S24) describes a vote by party label; not used. Commissioners are deciders, not sides, so their reported remarks were left out |
| 3. Passages | 07:19 → 07:30 | 160 passages in `passages.spec.yaml`, built with `build_passages.py`, 0 errors. Fixes only for extraction glitches: page footers inside spans, footnote numbers fused to words, line-end hyphens; each noted | **Two footer fixes failed** (the footer also carries "Docket No. 57171"). **One span ended early** (P-058 stopped at the first "revenues to the fuel clause."); caught by a quote error, then audited every spec entry for repeated end strings (scratch `span_audit.py`). Business addresses were cut from witness passages; party labels and family details on commissioners' pages kept out by span choice |
| 4. Draft | 07:29 → 07:40 | `workspace.md`: 30-second version, Layer 1 with a new "Who decides", four options (A today's method, B Georgia Power, C PIA Staff, D GIPL), six cards, "Questions to weigh", Layer 5. First gate: 2 errors, 6 warnings; then 0 and 0. Self-review against passages tightened paraphrases, split definitions into tagged sentences, gave every card the same "In short" form (claim, then evidence status), reordered cards by question and filing order, and defined the abbreviations that appear inside quotes | Checker false positives avoided by wording: a middle initial ("Alicia M. Johnson") and "a.m." mid-sentence count as sentence ends. Card 3's first evidence label said no party disputes that pipeline fees are fixed; GIPL never conceded it, so the label now names who calls them fixed |
| Sources | 07:38 → 07:42 | `sources.yaml`: 35 retrieved sources, 65 hashed files (zip members hashed too), plus not-retrieved and rejected leads. `verify_hashes.py` PASS | **New type `party-filing`** (see below) |
| Archive | 07:42 | Tried to request an Internet Archive capture of the scheduling order page (runbook step 2.5) | **Denied by the session's permission policy** (public data-sharing). Not retried and not worked around. No archive URLs recorded; the availability API shows the PSC calendar has an earlier Wayback capture (2026-09-24) |
| Home, page, render | 07:42 → 07:45 | `home.md` (4 items), `page.yaml` (slug `psc-57171`). Test-rendered both trials into scratch: 6 layers, 4 options, 6 cards parse | The prototype shows the raw type `party-filing` (no label) |
| Write-up and final pass | 07:44 → 07:48 | README (rubric, D1–D14, dates seen, how to resume), verification.md (step 5 only), review.md, this log. Copied the four extractors into `extract/` and confirmed they regenerate the derived text byte for byte. Final read of the page: reworded plan summaries so they don't imply real-time customers' prices change (only how their payments are counted), narrowed two absence claims to "the public copies we found". Gate re-run: PASS | — |
| Round 1 (independent checkers) | (coordinator) | Blind source check, calibration on a planted-error copy, and a three-member bias panel ran on revision 1. Results and every disposition are in verification.md | Bias panel **failed** (advocates 3/5 and 3/5; editor 3/5 and 3/5 with 4 high issues). Source check: 197 supports, 28 partial, 1 does not support (a party named without a passage). Calibration caught 12/12 |
| Read round 1 | 08:30 → 08:40 | Read all four reports in full and the calibration flags; copied the inputs and reports into `verification/round1/` (originals untouched) | Three misstated positions (GIPL in card 1, GAM in card 3, the unnamed filer); labels not on the printed rule; both advocates listed passage-backed arguments the page left out, usually where it quoted the other side's rebuttal |
| Revision 2: passages | 08:40 → 08:42 | Spec gained `title` and `header` keys (the builder now reads them, so no hand-edited header). P-065 widened to all of Staff's Table 1, header row included; merged cells checked in the `.docx` XML and noted. New P-162 (the filing page that names Resource Supply Management), P-163 (PSC contact numbers), P-164 (RTP-DA and RTP-HA spelled out), P-165 to P-167 (Georgia Power witnesses' jobs), P-168 (GAM on pipeline fees and very large customers). Built: 167 passages, 0 errors | — |
| Revision 2: draft | 08:42 → 08:46 | Rewrote `workspace.md` from the dispositions: Plans A–D, "Where it stands", one label rule, seven cards (new card 5 for GIPL's pipeline plan), a roles list for every party, four neutral questions | — |
| Revision 2: accuracy pass | 08:46 → 08:58 | Re-read every passage the draft cited, sentence by sentence (my own pass, not a source check) | Caught in my own first draft: GAM's P-113 and P-110 placed so GAM looked like it backed or opposed Georgia Power's plan outright; P-071 ("pay RTP rates", no baseline) conflicts with GAM's P-168 ("all or a portion"), so both are attributed; "written" comment (P-030 says only "public comment"); the legal notice's help line was for the Sep 1 hearing; "possibly as a step" for GIPL's "until the next rate case"; a Southern Company Services witness called a Georgia Power manager; "Five parties" claimed a complete count; "RSM" equated with Resource Supply Management without a passage; P-145 called a "reply" though it came before the rebuttal |
| Revision 2: gate | 08:58 → 09:05 | `check_citations.py`: one number warning ("Option 1" cited only to P-100; added P-128), then PASS 357 units. `readability.py`: Layer 3 9.0 → 7.9 after splitting long sentences; Layer 4 13.1 → 11.9 after shortening labels and splitting each speaker sentence from its quote. Dropped four passages no longer cited (P-002, P-057, P-069, P-122): 163 passages, all cited | **`readability.py` doesn't count a sentence that ends inside a closing quote** (`."`), so quote-heavy sections score high (Layer 4: 11.9 by the tool, about 10.2 counting those) |
| Revision 2: sources, home, page | 09:00 → 09:08 | S10 now cites its document page (listed first, `S10.txt` derived) and its notes say the scan is still uncited. `home.md` and `page.yaml` follow the new title, plan names and public-witness wording. Removed this trial's `extract/` folder after confirming its four scripts are byte-identical to `trials/tools/extract/` (adopted in commit bbf81fc); references now point there. `verify_hashes.py` PASS, 66 files (revision 1 reported 65, a miscount) | — |
| Revision 2: write-up | 09:10 → 09:16 | verification.md (round 1 results, 58 dispositions, 10 contradictions, revision 2 checklist, "where I'm least sure"), this log, README status | — |
| Round 2 (independent checkers) | (coordinator) | Fresh blind source check and bias panel on revision 2; calibration carried over | Source check: 289 supports, 7 partial, 0 unsupported, 357/357 spans grounded: the accuracy part of the pass rule is met. Bias panel **failed** again (advocates 3/5 and 3/5, the utility side "close to 4"; editor 3/5 and 3/5, 1 high). Each side named different gaps of its own; the editor saw a mild lean toward PIA Staff's framing |
| Read round 2 | 09:55 → 10:03 | Saved revision 2's page, home and passages and the four reports in `verification/round2/` (originals untouched); read all four reports in full. Added `end_occurrence: 1` to P-068 after the builder's new end-repeat check (span unchanged) | The highest-weight items were shared: GAM's card was graded on a narrower premise than its own summary; nothing said both take-or-pay options raise the credit; the cost-model exchange ran out of date order; Plan C's "What's uncertain" held an unanswered attack |
| Revision 3: draft | 10:08 → 10:10 | Rewrote `workspace.md` from the dispositions: two-part labels that grade each card's stated main claim; the take-or-pay mismatch stated in Layer 2, Plan A and card 1; a new card 7 for Georgia Power's case on other fuel costs (GAM's card is now 8); "large-load customers" for the filings' term; reasons added inside cards; repeats cut (the profit estimate to one place, Georgia Power's account of GIPL's switch to card 5, one errata note); five neutral questions | — |
| Revision 3: gate and accuracy pass | 10:10 → 10:15 | First gate run: PASS, 0 warnings, but Layer 2 read 8.5 and Layer 3 8.4 after the new glossary entries; split long sentences until 7.6 and 7.7. Shortened labels from up to 81 words to 68. Re-read each changed sentence against its passages. Re-cited P-047 and P-072, which the draft had left uncited | Caught in my own pass: card 3's label didn't credit GIPL's peak tie; card 7's "all costs" for Georgia Power's "all embedded costs"; a Markdown link that the prototype would show raw (the template sets text, not HTML), so the docket URL stays in the source drawer |
| Revision 3: write-up | 10:15 → 10:18 | verification.md (round 2 results, 39 dispositions, 10 contradictions, revision 3 checklist), this log, README status. `verify_hashes.py` PASS; test render: revision 3, 8 cards and 4 plans parse | — |
| Round 3 (independent checkers) | (coordinator) | Fresh blind source check and bias panel on revision 3; calibration carried over | Source check: 321 supports, 5 partial, 0 unsupported, 402/402 spans grounded: the accuracy part is met again. Home: 1 questionable tag (the Dec 14 deadline without its time), fixed by the coordinator and re-checked. Bias panel **failed** again, but closer: the more-credit advocate 4/5, the editor 4/5 on neutrality (clarity 3/5), the utility-side advocate 3/5 with 1 high item. Two checkers saw the remaining lean as structural, toward PIA Staff |
| Read round 3 | 11:31 → 11:40 | Saved revision 3's page, home and passages and the round 3 reports in `verification/round3/` (originals untouched); read all four reports in full | The shared items: only the fuel side of the dollar-for-dollar shift had numbers; the label rule could only return "Mixed"; replies were given on Staff's cards only; GAM's pipeline position had no card, though the printed ordering rule promised one |
| Revision 4: draft | 11:40 → 11:43 | Rewrote from the dispositions: the base-side half in Layer 2, the 30-second version and beside every Staff figure; a label rule that grades the undisputed premise apart from the dispute; one reply rule with "had said"; a new card 6 for GAM's pipeline position (later cards renumbered); parallel plan fields; claims paired with their counters in Layer 2; new glossary entries; questions rewritten | — |
| Revision 4: gate and accuracy pass | 11:43 → 11:47 | First gate: number warnings for "cards 8 and 9" inside a cited sentence (moved to its own `{editorial}` sentence) and for a "Table 5" whose passage never prints the 5 (reworded). The 30-second version read 8.6, so long sentences were split (6.8); labels cut from 88 words to 71. Re-read each changed sentence against its passages | Caught in my own pass: "answered" for a rebuttal that only "says"; an unattributed "real-time customers' prices would not change" (now the case's scope, cited to P-133); a Markdown link the prototype would show as raw text |
| Revision 4: final pass, home and write-up | 11:47 → 12:03 | Cut repeats (Plan A's uncertainty, a commissioner's background line), cut the two longest labels to 67 words, led card 7's "What's missing" with what is missing, quoted Georgia Power's motion at full strength, and added Georgia Power's footnote to Plan D. `home.md`: "Observe" for Nov 12, the Oct 27 item in plain words, Dec 10 under "Coming up". verification.md (round 3 results, 30 dispositions, 11 contradictions, revision 4 checklist), this log and README. Gate re-run: clean | The page grew to 415 units (402 before); the new card adds 12 |
| Round 4 (independent checkers) | (coordinator) | Fresh blind source check and bias panel on revision 4; calibration carried over | Source check: 310 supports, 11 partial, 0 unsupported, 0 wrong span, 421/421 spans grounded; no wrong figure, date, quote or party name. Bias panel **failed** on one item: both advocates 4/5 (the utility side up from 3), editor 4/5 on neutrality with 1 high issue (the 30-second "Who it affects" line, flagged by all three), clarity 3/5 |
| Read round 4 | 13:00 → 13:05 | Saved revision 4's page, home and passages and the five round 4 reports in `verification/round4/` (originals untouched); read all five in full. The coordinator asked for a narrow revision: line 9, the accuracy flags, six agreed wording fixes, and nothing that changes a card's claim, a grade, the card order or the plan structure | — |
| Revision 5: narrow edits | 13:05 → 13:10 | Line 9 rewritten as facts (both halves of the shift, every plan raises the credit, the per-$100 million examples, the base-rate dispute left to the next base rate case); "fuel credit" glossed in line 8; 13 source-check flags fixed; six agreed wording fixes; home items moved or extended (Nov 12 to "Coming up" with the PSC's number, Dec 14 with how to ask, Oct 27 with the base side) | The first rewrite of the 30-second version ran 156 words; cut to 149 by folding the blacked-out note into the plans sentence and glossing "fuel credit" in parentheses |
| Revision 5: gate and write-up | 13:10 → 13:14 | Gate clean: 420 units, 0 errors, 0 warnings; reading grades 6.6 to 8.2 in the targeted sections; hashes and passages unchanged; test render parses. verification.md (round 4 results, 9 changes, 20 open items, 9 contradictions, revision 5 checklist), this log and README | — |
| Round 5 (independent checkers) | (coordinator) | Fresh blind source check and bias panel on revision 5; calibration carried over | **Passed.** Gate clean; source check 315 supports, 10 partial, 0 unsupported, 0 wrong span, 13 questionable tags, 429/429 spans grounded; both advocates 4/5; editor 4/5 on neutrality with no high issue, 3/5 on clarity. The coordinator found that P-107's gap is a redaction (rendered image of PDF p.19) and updated `check_citations.py` to warn on unscoped absence claims |
| Read round 5 | 13:54 → 13:58 | Saved revision 5's page, home and passages and the five round 5 reports in `verification/round5/`; read them in full, and the protocol's "When a page is done" rule | The brief allows accuracy fixes only, plus one label change (card 5) |
| Revision 6: accuracy fixes | 13:58 → 14:02 | The 14 source-check items and 3 editor items in the brief: scoped every "no party disputes" to the parties whose testimony we could read; card 5's first grade from "Supported" to "Mixed" (P-102); P-107's redaction marked in the spec with a note, and `passages.md` rebuilt; GAM's ranking caveat made exact in two places; hedges restored (P-097, P-079); direction words for the 30-second bill figures; home line 15 in plain words | The new gate check flagged the 11 unscoped absence claims until item 9 and item 12 were done; then 0 warnings. Two flagged lines (SC L254 and L266) have a second point that would change another card's label; logged as O1 for the coordinator |
| Revision 6: gate and write-up | 14:02 → 14:04 | Gate clean: 426 units, 0 errors, 0 warnings; reading grades 6.8 to 8.3 in the targeted sections; hashes PASS; test render parses (card 5 folds to "Mixed"). verification.md (round 5 results, 17 changes, 17 open items, 7 contradictions, revision 6 checklist), this log and README | — |
| Revision 6: cards 7 and 8 | 14:05 → 14:06 | The coordinator decided O1: fix both now, since round 6 re-runs the whole panel. Card 8's premise narrowed to the price's design, with GIPL's peak-price point (P-094) in the support sentence and the label's "Mixed" part; card 7's premise narrowed to "most" left-out costs, naming the renewable-program exception (P-065). verification.md: fixes A15 and A16. Gate: 427 units, 0 errors, 0 warnings | Card 8's label grew to 83 words |
| Round 6 (independent checkers) | (coordinator) | Full panel again, because revision 6 changed card 5's evidence label; fresh blind source check | **Passed.** Gate clean; source check 322 supports, 7 partial, 0 unsupported, 0 wrong span, 1 questionable tag, 437/437 spans grounded; both advocates 4/5; editor 4/5 on neutrality with no high issue, 3/5 on clarity |
| Revision 6.1: accuracy fixes | 14:35 → 14:38 | Saved revision 6's page, home and passages and the five round 6 reports in `verification/round6/`; read them. Made the coordinator's nine accuracy fixes (lines 9, 24, 76, 110, 138, 174, 195 and 254, and home line 15) with no label, card claim or structure change, checked line by line against revision 6. Gate clean: 430 units, 0 errors, 0 warnings. verification.md: round 6 results, 9 fixes, 14 open items, 9 contradictions; this log; README | Line 9's narrower wording added 4 words to the 30-second version (160) |
| *Targeted re-checks 1 to 3* | (coordinator) | Fresh blind agents on the changed units only. Re-check 1 (13 units): 0 unsupported, 1 questionable tag (line 174 implied Staff revised its profit-rate estimates) plus five precision points; fixed by the coordinator as revision 6.2. Re-check 2 (7 units): nothing misstates a passage; one note on the 30-second version, read alone, fixed as 6.3 ("take-or-pay estimates"). Re-check 3 (1 unit): supports. Gate after each revision: 0 errors, 0 warnings | The 30-second version is read alone, so a line accurate in context can still mislead there; treat that as an accuracy flag |
| **Status** | — | **Passed checks (round 6)**, revision 6.3 | — |
| Revision 7: reader-first update | 19:43 → 20:07 | Read the reader-first brief, style guide v0.6 (section 9), runbook v0.6 Step 4, the tools notes and the lay reader's answers. Saved revision 6.3's page, home and display settings in `verification/round7/`. Added P-169 to P-171 from the archived PSC FAQ (S19). Rewrote the 30-second version (89 words), added "What it means for you" (4 "If you" lines, money steps, decided now and later, not known yet), reordered Layer 1 with "Who decides, and how to reach them", a 60-word Layer 3 intro and a values guide from verbatim quotes, and rewrote every "In short". No evidence label, card claim, card order or plan structure changed. Gate clean: 512 units, 0 errors, 0 warnings; targeted sections 7.5 to 8.4 | First pass: Layers 2 and 3 read 8.8, two "If you" lines and three "In short" lines ran over budget, and two bare "neither"s; fixed by splitting sentences and naming things. Bare "It says" sentences across the cards now name the speaker. No passage says why Georgia Power's estimates are blacked out, so the page says the public copy doesn't say why |
| Revision 7.1: fixes after passing round 7 | 21:33 → 21:46 | Read the coordinator's brief, CLAUDE.md, style guide v0.6 (sections 8 and 9), the protocol's "When a page is done", the six round 7 reports and the lay reader's unclear terms (not the calibration folder). Made the 24 listed fixes (14 accuracy, 10 written-rule) with no card claim, evidence label or side changed: GAM's position in P-111's and P-115's words, the line 149 condition stated in reader text, every "In short" with both the fuel and the base side, one all-party note on blacked-out material, a values guide with one line per side in one form, "If you" lines in direction-size-when order, first-mention definitions on each tab, "take-or-pay choice 1/2" in reader text, a new title, and the comment item led by its deadline. Gate clean: 541 units, 0 errors, 0 warnings; reading grades 7.2 to 8.4 in the targeted sections; hashes PASS; test render clean. verification.md: round 7 results, 24 changes, 12 open items, 9 contradictions; the changed-units list is in the coordinator's scratch folder | The 45-word "If you" budget can't hold direction, size and when plus GAM's group comparison, so the comparison left the business line (C6). The new definitions pushed the 30-second version to 92 words and grade 10.2 until it was rewritten in shorter sentences (90 words, 7.3). Writing "choice 1" instead of "Option 1" loses the checker's label exemption, so each such sentence must cite a passage that prints the number. The brief called P-111's concerns GAM's own; the passage says PIA Staff's and intervenors' |
| Revision 7.2: fixes from the first targeted re-check | 22:24 → 22:32 | Read the 7.2 brief and both re-check reports (source re-check: 9 partial, 3 questionable, 0 unsupported; flip test: neutrality 3/5, 1 high). Archived revision 7.1 and the unit list the checkers used in `verification/round7-targeted/`. Made the 12 listed fixes with no card claim, label or side changed: GIPL's values label in its own terms and one all-party note in place of Georgia Power's extra sentence; GAM's view as the closest to Plan A; cards 5, 8 and 9 give the whole effect in our voice and card 6 attributes GAM's; P-016 for every "later"; the real-time line and the comment deadline scoped; the Plan C chip "PSC advocacy staff's plan". Gate clean: 545 units, 0 errors, 0 warnings; reading grades 7.3 to 8.5; hashes PASS; test render clean. verification.md: re-check 1 results, 12 changes, 9 open items, 2 contradictions | Adding "your" and a definition of "net" pushed the home and business lines past 45 words until "or the fuel-charge date" came out. The new Layer 3 notes pushed the section to grade 8.54; splitting the GAM note and paraphrasing P-129 brought it to 8.46. A label in a party's own words still has to avoid a value other parties also claim, so the shared value now sits in one note for every party |
| Revision 7.3: fixes from the second targeted re-check | 22:56 → 23:02 | Read the 7.3 brief and both reports (source re-check: 3 partial, 0 questionable, 0 unsupported; flip test: neutrality 4/5, no high). Archived revision 7.2 beside them in `verification/round7-targeted-2/`. Made the listed fixes with no card claim, label or side changed: P-111 in the page's standard shape at line 168; card 5's split limited to new fees until the next rate case; card 8's double negative attributed; the shared cost-shift concern named once for PIA Staff and GIPL, with each line given a value only that party states; card 6's base-side effect in our voice; the fuel-charge effect in the options intro; "PIA Staff's plan" chip; eight low notes, including the home item naming the case. Gate clean: 549 units, 0 errors, 0 warnings; reading grades 7.3 to 8.5; hashes PASS; test render clean | Giving each values line a value only that party states meant moving PIA Staff and GIPL off their shared core concern, which now sits in the note for both. GIPL's new quote pushed Layer 3 to grade 8.54 until it was shortened and the GAM note split. Every budget (40, 35, 60 and 25 words) was at or near its limit, so each fix had to trade words elsewhere |

## Where the runbook did not fit (and what I did)

1. **Step 1's source list is bill-shaped.** It asks for bill versions, legis.ga.gov status, a fiscal note and O.C.G.A. text.
   A PSC docket's record is different: the order that opened the case (here, a fuel-case short order), the procedural and
   scheduling order, a dated filing list, pre-filed sworn testimony from each party, errata, data-request responses, motions,
   transcripts, briefs, calendar entries, public-participation pages and the commissioners' own pages. I used that list.
   → A regulatory-docket runbook (as the scout recommended), or a "record types by decision kind" table in this one.
2. **Step 2 assumes HTML and bill PDFs.** Docket files came as `.docx`, multi-file `.zip` archives, `.xlsx` workpapers and
   scanned PDFs. `snapshot.py` saved them as `.bin` with no text, so I wrote four extractors (now `trials/tools/extract/`). **Scans with no text
   layer cannot become passages**, and there is no approved OCR tool, so Staff's corrected figures and RSM's testimony could
   only be read by eye and are not on the page as facts. This is the biggest content gap in the trial.
3. **No source type fits party filings.** The vocabulary (primary-government, secondary-research, advocacy, news,
   local-government) would label PIA Staff "government" and the other parties "advocacy", which signals unequal authority
   when Staff is a party like the rest (P-006). I added `party-filing` for every party's filing and log it here.
4. **"Sides" are per question, not per law.** Trial 001's cards say "supports/opposes the law". Here there are five parties
   and four questions, and alliances cross (GIPL sides with Georgia Power on Option 1 and with Staff on counting more costs).
   Cards are labeled by what they favor ("Side: favors Option 1", "favors PIA Staff's pipeline plan", "accepts either of
   Georgia Power's options (GAM)"), ordered by question and then by filing order. The "count voices on each side" rule was
   applied per question.
5. **Layer 3's fields assumed a passed law.** "What happened" makes no sense for pending options, so every option has
   "Who supports it" instead (What it does · Who supports it · Who benefits · Who pays · What's uncertain). Options are whole
   packages (A–D) to avoid colliding with the parties' own "Option 1" and "Option 2".
   *Revision 2:* runbook v0.4 now says "Where it stands" for options still ahead, so the field was renamed; the packages
   became "Plan A" to "Plan D", because "Option B uses Option 1" confused readers (editor, round 1).
6. **Layer 1 had no place for the decider.** Added "Who decides" with the **Decide** power level, the five commissioners, the
   mid-year appointment and each term's end date (all from PSC pages), and a line saying which dates were seen when.
7. **The evidence vocabulary assumes studies.** Here the evidence is the parties' own estimates, often blacked out. The
   labeling rule on the page defines "Supported" as backed by the case record or a party's own figures, and adds a rule
   that a blacked-out number is hidden, not zero.
   *Revision 2:* round 1 showed that rule let a party's own estimate earn "Supported" and let opposing cards be graded on
   different side facts. The new rule grades each card's main factual premise, named in the label; "Supported" needs the
   record or parties other than the claimant; a second part gives the size's status, with "Uncertain" when it is blacked out.
8. **The 30-second version's "What it does" label** presumes an enacted law; used "What's being decided".
9. **No step for a moving record.** Staff's errata (2026-10-07) silently superseded a figure the scout quoted; briefs arrive
   2026-12-02 and the vote 2026-12-15. → The runbook needs (a) a check for errata and corrections after each testimony is
   found, and (b) a re-check schedule tied to the decision's dates (README "How to resume").
10. **No "fiscal note" equivalent.** The closest are Staff's bill estimate and Georgia Power's per-$100 million workpaper;
    both are party numbers, so both are attributed and the comparison is `{computed}`.
11. **Official pages can carry party labels and family details.** The commissioners' PSC pages print a party next to each
    name and one names a commissioner's children. Passages must be chosen to exclude them. → Add to step 3.
12. **Party-hosted copies of official records.** The only copy of the 2026-09-01 transcript I found is hosted by counsel for
    one party (S31). I did not use it. → The runbook should say how to treat such copies.
13. **Archive step** could not run in this session (permission denied). Trial 001 recorded no archive URLs either.

## Tool change requests (shared tools were not edited)

- `build_passages.py`: the generated header is hard-coded to "Trial 001 (HB 1009)" and its bill-text note. I replaced only
  the header lines of `passages.md` after the final build (no passage text touched). → Take the title and intro from the spec. **Adopted** before revision 2 (spec `title` and `header` keys).
- `build_passages.py`: the end string is matched at its first occurrence after the start, which silently cut P-058 short.
  → Warn when the end string occurs again within the next few hundred characters, or allow an occurrence index.
  **Adopted** before revision 3 (a CHECK warning and an `end_occurrence` key; P-068 now carries `end_occurrence: 1`).
- `snapshot.py`: (a) map `.docx`, `.xlsx` and `.zip` content types to real extensions; (b) fail on an empty body; (c) print
  the content-disposition filename; (d) optionally write text for `.docx` and PDF. The four extractors (`docx2txt.py`,
  `pdf2txt.py`, `xlsx2txt.py`, `safe_unzip.py`) were **adopted** into `trials/tools/extract/`; (a)–(c) were **adopted**
  before revision 3 (real extensions, exit 1 on an empty body, the Content-Disposition file name).
- `check_citations.py`: treat single capital initials ("M.") and "a.m."/"p.m." as abbreviations. **Adopted.**
- `render_prototype.py`: add a `party-filing` label ("Party filing") to `TYPE_LABEL`; the Layer 5 parser only finds IDs
  written out in full (no ranges such as "S17b–S17e"). The label was **adopted**.
- `readability.py` (new, revision 2; **adopted** before revision 3, which also leaves card labels out of the grade): the sentence counter `[.!?]+(?:\s|$)` misses sentences that end inside a closing quote
  or bracket (`."`, `.)`), so pages that quote testimony score 1–2.5 grades too high. → Allow closing quotes and brackets
  before the whitespace. Revision 2's target sections were measured both ways and pass either way.
- `check_citations.py`: the label skip matches the singular "card N" only, so "cards 8 and 9" inside a cited sentence
  warns. → Also skip plurals and "N and M" lists after "cards", "layers", "options" and "questions".
- `check_citations.py`: warn on unscoped absence claims ("no party disputes …"). **Adopted** before revision 6.
- `build_passages.py`: its doc comment limits `fix` to extraction glitches; revision 6 also uses one to mark a redaction the
  text layer drops (P-107, checked on a rendered page image). → Document that use, or add a `redaction` key.
- `readability.py`: CHECK lines catch bare "both", "neither" and "either" but not sentence-initial "It" or "This"; revision 7 found about 25 "It says" sentences by hand. → Add those to the CHECK prompts.
- An **approved OCR tool** (with a dependencies.md entry) is needed for scanned filings.

## Rules learned (proposed; not yet applied to the runbook or style guide)

1. **Check for errata before quoting a number from testimony.** A quoted figure can be superseded weeks later.
2. **A redaction is not an absence.** Say a figure is blacked out; never treat it as zero or as "not estimated".
3. **When only one option has public numbers, say so on the page** instead of comparing a number with nothing.
4. **Label every party's filing the same way**, including the regulator's own advocacy staff when it acts as a party.
5. **For multi-party cases, count voices per question** and name the crossing alliances.

From round 1 (revision 2):

6. **Give each party its own reason in its own sentence.** Two parties that back the same option may do so for different,
   even conflicting, reasons (Georgia Power and GIPL on Option 1).
7. **Never list a party as a speaker or supporter of a plan it doesn't back.** Quote its whole sentence ("No specific
   portion", not "no part") and both halves, and show a mixed stance as mixed.
8. **Name a party only from a passage that names it.** A filing list's description ("Jim Clarkson Direct Testimony") is not
   enough; the filing's own page carries the company field.
9. **One label rule, applied to the card's main premise and named in the label.** Parallel cards get the same claim type.
   A party's own estimate never makes its claim "Supported".
10. **Same verbs, same caveats, same direction.** Every party "says" or "estimates"; a caveat says which way a known gap cuts;
    say what is public as well as what is blacked out.
11. **Credit partial agreement both ways, and never repeat that a party stands alone.**
12. **When quoting a rebuttal, give the argument it answers**, and call something a reply only if it came later.
13. **Keep the page's own voice out of predictions.** Report what each party says and what the order leaves to a later case.
14. **Put a caveat where the figure is used**, not only in a general note (Staff's page-7 figures).
15. **Speaker, filing and date for every documented card's lead voice.**

From round 2 (revision 3):

16. **Grade the claim the card's summary makes, and grade every card that way.** A narrower, undisputed premise in one
    card's label against contested ones elsewhere gives that side the only "Supported" on the page.
17. **State the agreed facts plainly and early**, in the page's own Layer 2 (both take-or-pay options raise the credit).
    Leaving them to one plan's fields made a shared finding look like one party's claim.
18. **Run each exchange in date order.** A claim filed before its challenge must not read as the reply to it.
19. **A blacked-out number is not evidence against another party's public figure.** Label the public estimate as public.
20. **Use each party's own term**, and say when the passages give no threshold, instead of merging terms.
21. **One description template for every party**, with no mission statements, and witness titles in the same form.
22. **Count repeats**: a point stated four times (a profit estimate, an unverified account) weighs more than one stated once.

From round 3 (revision 4):

23. **Grade the premise no party disputes apart from the dispute.** A label that grades the whole summary, its "who
    disagrees" clause included, can only say "Mixed", and then it can't tell agreed facts from real conflicts.
24. **One reply rule for every card**, with "had said" for an answer filed before the challenge. Say which party filed
    last, so a missing written reply doesn't read as a concession.
25. **Give both halves of a dollar-for-dollar shift.** Wherever one side's figure appears, give the matching amount on the
    other side and say where it goes.
26. **Pair each claim with its documented counter, both ways, where the claim appears**, and say when a passage reports
    concerns rather than a finding.
27. **Follow the page's own printed rules.** When the ordering rule promises a card, write the card or change the rule.
28. **Borrowed support carries its source's own qualifier** (a showing made "simply for illustration", a party that
    rejects the plan), or it doesn't go in the support field.

From round 4 (revision 5):

29. **Keep a party's hedge.** Quote a double negative or a condition as filed ("does not mean that … do not pay";
    "depending on how …"); a flat paraphrase makes the claim stronger than it was filed.
30. **A summary states facts, not one party's argument and another's rebuttal.** Same verb, same kind of label for every
    party, and no last word.
31. **When the page computes, give every bound.** A net figure for one scenario only argues for that scenario.
32. **When both advocates accept the balance, fix only the agreed accuracy items** and log the rest as open items, with
    contradicting suggestions side by side (Trial 001's lesson, applied here).

From round 5 (revision 6):

33. **Scope every absence claim to what was read.** "No party disputes" covered a party whose testimony was an unreadable
    scan and a hearing with no transcript; "no party whose testimony we could read disputes" is what the record shows.
    The gate now checks this.
34. **Check a gap in a passage on a rendered page image before calling it an extraction glitch.** P-107's missing words
    were a redaction; mark it in the passage and say on the page that part of the recommendation is blacked out.
35. **Before grading a premise "Supported", read the supporting party's own words for the other side's view.** P-102's
    "I would agree with Georgia Power" showed Georgia Power held the opposite view.

From round 6 (revision 6.1):

36. **A summary sentence can't claim more than its citations.** "Every proposed change" was true of the take-or-pay options
    the citations covered, not of every fallback in the plans.
37. **Mark a look-back as a look-back.** A "had been in place" estimate is not a saving from adopting a plan now.

From the reader-first update (revision 7):

38. **Write each "If you" line's first sentence to stand alone**, with the condition the source gives, and say why the
    size isn't known instead of offering an example as if it were a plan's effect.
39. **A values line must quote the party, not our summary of it.** When no party backs an option in full, say so in the
    guide rather than stretching a quote.
40. **Name the speaker in every sentence** ("Georgia Power said", not "It said"); readers land mid-card on a phone.

## Automation notes (not eligible: 0 of 3 runs for this kind of decision)

- The PSC filing list is structured JSON (date, filer, description, attachment flag): a good candidate for a later
  docket watcher that flags new filings (briefs, errata, orders) for a manual re-check. Not built.
- File extraction is mechanical and was reproducible byte for byte; it moved to `trials/tools/extract/` (commit bbf81fc).
