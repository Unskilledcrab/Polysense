# Lay-reader check, after the reader-first revision (2026-11-08)

Same method and answer keys as the baseline (`../baseline/grades.md`, `../answer-keys.md`): one AI stand-in reader with
no background per decision, reading only what a phone shows at 390px on the home page and one decision's Summary,
Options and Arguments tabs, before tapping anything. Experimental; it never blocks a pass.

Pages read: Trial 001 revision 6, Trial 002 revision 7, Trial 003 revision 4 (all before their verification rounds),
with the template as of commit 78b4771 (002, 003) or 5a8d7aa (001).

| Question | PSC 57171 (renter) | Fulton 2027 budget (homeowner) | HB 1009 (parent of a 10th grader) |
|---|---|---|---|
| 1. What is decided, by whom | Yes | Yes | Yes |
| 2. Raise or lower my costs, how much, when, or why not known | **Yes** (was: yes, but unsure): fuel charge likely down, base rates could rise later, net not estimated, and why | **Yes** (was: no): not known yet, because the proposal isn't public | **Yes** (was: partly): the law sets no money aside and charges families nothing |
| 3. Where the money goes | Yes, with effort: pieced together from three tabs | Partly (same as before): no dollar figures were visible | Yes (none), but who pays for schools' new work isn't said |
| 4. What I can do, by when | Partly: found the 9:30 a.m. Dec 14 cutoff, but the page doesn't say how to send a comment | Yes | Yes, but no contact routes for districts or lawmakers |
| 5. What the evidence labels mean | **Yes** (was: no) | **Partly** (was: no): meanings found, but it couldn't tell which label went with which part | **Yes** (was: no); same part-mapping problem on two cards |
| 6. One argument each way | Yes | Yes | Yes |
| Terms or references it couldn't place | 39 (was 52) | 35 (was 33) | 41 (was 31) |
| Words read | 4,503 (was 3,172) | 4,226 (was 2,950) | 3,422 (was 1,850) |
| Unclear terms per 1,000 words | 8.7 (was 16.4) | 8.3 (was 11.2) | 12.0 (was 16.8) |

**Better:** question 2 (the tester's main complaint) now gets a right answer on all three pages, with the reason when the
size isn't known. Question 5 went from three misses to two passes and one partial. Unclear terms per 1,000 words fell on
all three pages; the raw counts rose on two because the pages are longer and the HB 1009 reader also listed 16 terms
from the other decisions' home-page cards.

**Still wrong, and what was done about it:**

- *Which label goes with which part* (Fulton; two HB 1009 cards; "which claim is the supported one" on the PSC page): the
  template now shows one line per grade with the parts it covers ("● Supported: Rules / ○ Uncertain: Results; …"), and
  the open card lists every part with its reason.
- *Folds read as empty headings* ("Where the money comes from", "Not known yet", "Decided now, and decided later"): the
  money steps and "Not known yet" now open by default. Part of this is the instrument: the extracted text shows a closed
  fold as a bare heading, while the phone shows a "+".
- *Option letters missing on cards* (Fulton): fixed; cards keep "Option C: …". The PSC page already used "Plan A: …".
- *INPUT / OBSERVE unexplained:* the chips now read "Have your say" and "Watch or read".
- *How to send a PSC comment:* the PSC's Public Comments page doesn't say; the page now has to say where to ask, in the
  comment item itself (Trial 002 fix list).
- *Jargon by page:* PSC: fuel charge, base rates, "the credit", RTP, FCR, "Option 1/2" inside Plan B, EMC "territory and
  financing only", docket. Fulton: 5% of what, "filed", jail agreement, General Fund, "the county's savings", Res.
  HB 1009: Act 395, "health or disability plan", "similar devices", what changes on Dec 31. These go to each trial's
  fix list.
- *Chrome:* "Passed checks (round 6)", "Reader-first update; checks pending" and the fingerprint in the header meant
  nothing to readers.

**Perceived tilt (recorded, not graded):** all three said "mostly even". PSC: six of nine cards argue for a change, and
every card leads with the fuel-charge effect, so the change side's benefit reads first. Fulton: the only values-guide line
is the critics' (the supporter advocate made the same point in round 4). HB 1009: the opponents' values-guide quote (a
teacher on confrontations) is weaker than the emergency-contact worry parents cite most.
