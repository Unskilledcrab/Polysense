# Lay-reader check, baseline (prototype version 5, before the reader-first revision), 2026-11-08

Experimental AI stand-in for a reader with no background (verification protocol, "Reader-first pages"). Each persona read
only what a phone shows at 390px on the home page and one decision's Summary, Options and Arguments tabs, before tapping
anything. Answers were graded against keys written before the run (`answer-keys.md`). It never blocks a pass.

| Question | PSC 57171 (renter, Georgia Power bill) | Fulton 2027 budget (homeowner) | HB 1009 (parent of a 10th grader) |
|---|---|---|---|
| 1. What is decided, by whom | Yes | Yes (the Board's full name unclear) | Yes |
| 2. Raise or lower my costs, how much, when, or why not known | Yes, but unsure: "standard rates" undefined; only a per-$100 million example, with the fuel cut smaller than the possible base rise | **No**: no tax effect and no reason given | Partly: found no cost, couldn't tell why |
| 3. Where the money goes | Yes | Partly: where it comes from isn't stated | Yes (none) |
| 4. What I can do, by when | Yes | Yes | Yes |
| 5. What the evidence labels mean | **No**: not explained on screen | **No** | **No** |
| 6. One argument each way | Yes | Yes | Yes |
| Terms or references it couldn't place | 52 | 33 | 31 |

Main confusions it shared with the human usability feedback: what the PSC is and what "standard rates" means; whether the
bill goes up or down; what "Supported / Mixed / Uncertain" mean; vague references; the Options tab's "What lawmakers could
have done" on pages that aren't laws; "trial" and "sha256" in the page chrome.
