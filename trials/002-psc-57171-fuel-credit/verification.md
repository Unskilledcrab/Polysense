# Verification: Trial 002 (PSC Docket 57171)

**Scope of this file:** runbook step 5 only (the deterministic checklist), run by the drafter on revision 1,
2026-11-08. Step 6, the AI-only [verification protocol](../../docs/process/verification-protocol.md) (blind source
check, bias panel, planted-error calibration), has **not** been run: the brief assigns it to independent checkers.
Nothing on the page may be called "verified" until that protocol's results are recorded here.

## Step 5 checklist (revision 1)

- [x] **Every sentence is cited or tagged.** `python3 trials/tools/check_citations.py trials/002-psc-57171-fuel-credit/workspace.md trials/002-psc-57171-fuel-credit/passages.md`:
      PASS, 269 units (226 cited, 16 analysis, 23 editorial, 4 computed), 0 errors, 0 warnings. `home.md`: PASS, 4 units.
- [x] **Every quoted span appears verbatim in its cited passage** (same run; quotes are checked word for word).
- [x] **Every number and date appears in a cited passage, or is `{computed}` with the arithmetic shown** (same run, 0 number warnings).
      The four `{computed}` items: the comment deadline (P-024, P-030), the $1.65 base-side bill effect (P-143),
      Staff's Option 2 projections rounded to $126 million and $399 million (P-079), and the $1.65 versus $1.48 comparison (P-142, P-143).
- [x] **Every cited passage ID exists**; all 160 passages are cited at least once.
- [x] **Every source has a sha256 and the file in `_raw/` matches it.** `uv run --with pyyaml python3 trials/tools/verify_hashes.py trials/002-psc-57171-fuel-credit/sources.yaml`:
      PASS, 65 files across 35 retrieved sources, 0 problems.
- [x] **Reading grade 6–8 for the 30-second version and Layers 1–3.** `readability.py`: 6.2, 5.6, 6.2, 8.0 (target ≤ 8.5).
      Not targeted: Layer 4 10.0, Layer 5 9.3. Outliers: none in the target sections.
- [x] **Jargon defined or removed.** Defined in place: real-time pricing, fuel charge and fuel credit, base rates, base rate case,
      marginal cost, take-or-pay contracts, pipeline fees, briefs, return on equity, kWh and MWh, public witness, and the
      abbreviations inside quotes (RTP, FCR, FT, PPA, GPC, incremental load). The bill watch-list terms do not occur.
- [x] **Loaded-word scan clean outside quotes**, and no party or left-right label anywhere on the page (checker: 0 warnings).
      Party labels printed on the commissioners' PSC pages were kept out of the passages by span choice.
- [x] **Structural symmetry.** Four options, each with the same five fields (What it does · Who supports it · Who benefits ·
      Who pays · What's uncertain). Six argument cards, each with the same seven fields, a Side, a claim type and an evidence
      label. Each side's strongest documented case appears, and evidence is not equalized: the page says outright that
      only Option 2 has public dollar estimates and that only one party backs it.
- [x] **Length budgets.** Layer 2 "What's being decided": 74 words (≤ 80). "What would change": 5 bullets. Card "In short"
      lines: 37–46 words (≤ 60). Home items: 4, each ≤ 25 words plus a date. A few sentences run to 26 words.
- [x] **"Where I'm least sure"**: below.

## Where I'm least sure

1. **Staff's corrected numbers.** Staff's 2026-10-07 errata is a scanned image. I read it by eye (README D5) but could not turn it
   into passages, so the page avoids the corrected totals and says the figures were corrected. If my reading is wrong, the
   "$322 million" and "$1.35 a month" (which the errata does not list as changed) could also be affected.
2. **"Including data centers."** The link between real-time pricing and data centers rests on GIPL's statement (P-097) and Staff's
   note that new large-load customers pay real-time rates (P-071). No archived source names a data-center customer.
3. **Public witnesses at the 2026-11-12 hearing.** The PSC's public-witness page is general guidance (P-025, P-159). No filing
   in this docket confirms a public-witness period at that hearing; the page tells readers to call and confirm.
4. **Bill effects per $100 million.** Georgia Power's workpaper (P-141 to P-143) is built from its storm-cost and fuel-rate
   changes. Using it to describe a shift of real-time revenue between base and fuel is an approximation, and the base side
   only changes in a future base rate case.
5. **"No party asks to keep today's method."** Based on the four archived testimonies. GAM comes closest (it accepts either of
   Georgia Power's options only if the PSC finds a change is needed). RSM's scanned testimony is not in the passages.
6. **Option 1's size.** Every estimate of what Option 1 would add is blacked out in the public copy (P-139). Other data-request
   responses in the docket (not retrieved) might hold a public figure.
7. **Motion to strike.** "No ruling found" rests on the filing list; a ruling made aloud at the 2026-10-15 hearing would only
   be in its transcript, which has no file.
8. **Plan summaries.** Options B–D compress multi-part recommendations. Georgia Power says GIPL changed its pipeline-cost
   position at a hearing (P-154, P-155); that could not be checked.
9. **Who votes.** "The same five are set to vote" relies on the PSC's bio pages as of 2026-11-08; a resignation or new
   appointment before 2026-12-15 would change it.
10. **GAM's class table** comes from a 2023 cost study whose docket number GAM gives as "44820" (probably 44280); we quote it as given.

## Step 6: independent verification

Not run (by instruction). Suggested planted-error types for calibration on this page, beyond Trial 001's set: an errata-superseded
number, a redacted value presented as zero, an option attributed to the wrong party, a deadline shifted by the 24-hour rule,
a commissioner's term date, and "Option 1" and "Option 2" swapped in one sentence.
