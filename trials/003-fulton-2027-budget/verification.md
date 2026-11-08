# Verification: Trial 003 (Fulton County 2027 budget)

**Scope: runbook step 5 only (the deterministic checklist), run by the drafting agent on 2026-11-08.**
The [verification protocol](../../docs/process/verification-protocol.md) (step 6: blind source checks, the
planted-error calibration copy and the bias panel) has **not** run. Independent checkers will run it. Until then,
nothing on this page may be called "verified".

## Step 5 checklist (revision 1)

| Check | Result | How |
|---|---|---|
| Every sentence is cited or tagged | **Pass**: 234 units (151 cited, 41 analysis, 27 editorial, 15 computed); 0 errors, 0 warnings | `python3 trials/tools/check_citations.py trials/003-fulton-2027-budget/workspace.md trials/003-fulton-2027-budget/passages.md`. `home.md`: 6 units, 0 errors, 0 warnings |
| Every quoted span appears verbatim in its cited passage | **Pass** (checked by the same tool). One quote first failed: the source breaks "DECISION-MAKING" across a line ("DECISION- MAKING"), so the page now quotes only "to ensure transparency" | `check_citations.py` |
| Every number and date appears in a cited passage, or is `{computed}` with the arithmetic shown | **Pass**: 0 number warnings. The tool's check is a substring match, so the 15 `{computed}` units were also re-done by hand (vote counts from the four roll calls; $4,720,447; $1,419,778; $2,782,235; 0.48; the Sunday) | `check_citations.py`; manual arithmetic |
| Every cited passage id exists | **Pass** (84 passages; every cited id resolves) | `check_citations.py` |
| Every source in `sources.yaml` has a sha256, and the file in `_raw/` matches it | **Pass**: 33 retrieved, 0 problems | `uv run --with pyyaml python3 trials/tools/verify_hashes.py trials/003-fulton-2027-budget/sources.yaml` |
| Reading grade, 30-second version and Layers 1-3, is 6-8 | **Pass**: 6.6, 7.8, 7.4, 6.3. Outliers (not targeted): Layer 4 is 9.5 and Layer 5 is 9.9, driven by long verbatim quotes and source titles | `python3 trials/tools/readability.py trials/003-fulton-2027-budget/workspace.md` |
| Jargon watch-list terms are defined or removed | **Pass**: no watch-list term appears outside quotes and passages. Other terms are defined where first used: consent agenda (P-085), consent decree, fund balance, General Fund, at large, abstain, filing | Script scan of the page text outside markers and quotes |
| Loaded-word scan is clean outside quotes | **Pass** (0 warnings). "hijack" and "shenanigans" appear only inside verbatim quotes | `check_citations.py` loaded-word list |
| Party labels | **Pass**: none on the page (0 warnings from the tool's party check). No person is described by party; the page's only source text with party labels (an unrelated news sidebar) is not cited | `check_citations.py` party check |
| Pronouns need a source | **Pass**: no gendered pronoun for a named person outside quotes | Script scan |
| Structural symmetry | **Pass**: all three options have the same five fields (What it does, Where it stands, Who benefits, Who pays, What's uncertain). All six cards have the same seven fields in the same order, and every card's "In short" ends with an evidence status | Script check of headings and field labels |
| Length budgets | **Pass**: "What's being decided" 50 words (budget 80) + 5 items (budget 5); each option field has 1-3 sentences (budget 3); card summaries 31-44 words (budget 60); home cards 11-24 words (budget 25) | Script word counts |
| "Where I'm least sure" lists the claims most likely to be wrong | Done (below) | |

## Where I'm least sure

1. **The 2027-01-20 deadline.** It comes from a schedule labeled "Proposed" whose record says it was approved
   with the consent agenda but still shows "Agenda Ready" (D2). If the final schedule differs, the date changes.
2. **What the 5% limit is.** Its terms come from Commissioner Barrett's failed resolution and a FOX 5 report, which
   word it differently ("5% of their 'base budget'" vs. "5% above current allocations"). We found no written
   instruction (D4). Cards 2 and 4 and Option A depend on it.
3. **"No one ... defended the 5% limit directly"** is true of our archived sources only. The Board's discussions
   on 2026-10-21 and 2026-11-04 exist as video (S16), which we did not review; reasons may be stated there.
4. **"Filing ends work on an item without approving it"** is our plain-language reading of Legistar's "Filed"
   status and of a history with no later step; it is tagged `{analysis}`.
5. **Consent decree = the 2025 Justice Department agreement.** The budget book describes the agreement and, in the
   next sentence, "consent decree compliance"; it never says "this agreement is the consent decree" in one
   sentence. Tagged `{analysis}`.
6. **Last year's hearing date (2025-12-03)** comes from the text layer of a signed scan (S09b p.4). It was checked
   against the page image, and one missing space was fixed (P-047 note).
7. **$507.5 million for justice and safety** comes from a table whose stacked column headings extract out of
   order; the column was confirmed against the rendered page (P-053 note).
8. **Card 4's comparison** of the public defender's $4,720,447 in listed requests with 5% of its 2026 budget
   assumes the 2026 adopted budget is a fair yardstick; the office's 2027 base budget is not published.
9. **Vote counts** are tallied by us from Legistar roll calls, which carry no official tally (`{computed}`). D1
   shows that a secondary source counted the 2026-08-05 vote differently.
10. **News paraphrases.** The justice leaders' ask to wait until January, and Commissioner Ellis's "no talk of an
    early vote", are reporters' paraphrases, not quotes; the leaders' letter itself was not found (D6).

## Step 6 and later

Not run, by instruction: the bias panel and blind source checks are for independent checkers. When they run,
record their reports, the planted-error copy and its answer key (kept outside the repository during the run), and
every disposition here, as Trial 001 did.
