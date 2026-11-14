#!/usr/bin/env python3
"""Tests for desk.py on a small made-up page. Run: uv run --with pyyaml python3 trials/tools/test_desk.py

No network: the watch tests pass in a fake fetch function. Addresses and names here are invented.
"""
from __future__ import annotations

import json
import sys
import tempfile
import unittest
from pathlib import Path

sys.path.insert(0, str(Path(__file__).parent))
import desk  # noqa: E402

PAGE = """slug: demo
kind: budget
topic: "County budget"
stage: open
title: "A made-up county budget"
watch:
  sources: [S01, {id: S02, ignore: "^Last updated"}]
  events:
    - {date: 2026-11-20, what: "Vote scheduled. Add the outcome."}
    - {date: 2027-03-01, what: "Far off."}
    - {date: 2026-11-01, what: "Already handled.", done: true}
"""
SOURCES = """sources:
  - {id: S01, title: "Budget page", url: "https://example.org/budget", status: retrieved, retrieved_at: "2026-11-01T00:00:00Z"}
  - group:
      - {id: S02, title: "Calendar", url: "https://example.org/calendar", status: retrieved}
  - {id: S03, title: "A lead we never fetched", url: "https://example.org/x", status: not-retrieved}
"""
PASSAGES = """# Passages

### P-001 · S01 · page 1
> The county plans to spend $10 million in 2027.

### P-002 · S02 · calendar row
> The board meets on November 20, 2026.

### P-003 · S01 · page 2
> Property tax is the main income.
"""
WORKSPACE = """# A made-up county budget

Status: open · Trial 999, revision 1.2
How this page was made: by a test. <!-- nocheck -->

## 30-second version

- The county plans to spend $10 million in 2027. [P-001]
- The board meets on November 20, 2026. [P-002]
- Property tax is the main income. [P-003] This line is our own reading. {analysis}

## Layer 1: Participate

### Coming up

- **Nov 10, 2026:** A hearing was listed. [P-002]
- **Nov 20, 2026:** The board is scheduled to vote. [P-002]
"""
HOME = """# Home

## Coming up

- **Nov 10, 2026:** A hearing was listed. [P-002]
- **By Nov 20, 2026:** The board is scheduled to vote. [P-002]

## What happened

- **Oct 1, 2026:** The draft was posted. [P-001]
"""
HTML = "<html><head><script>var token='{tok}';</script></head><body><!-- built {tok} --><h1>Budget</h1><p>{body}</p></body></html>"
SAVED = HTML.format(tok="old", body="The county plans to spend $10 million in 2027.")   # our snapshot's exact bytes


class DeskTest(unittest.TestCase):
    def setUp(self):
        self.tmp = tempfile.TemporaryDirectory()
        self.root = Path(self.tmp.name)
        self.trial = self.root / "999-demo"
        (self.trial / "_raw").mkdir(parents=True)
        for name, text in (("page.yaml", PAGE), ("sources.yaml", SOURCES), ("passages.md", PASSAGES), ("workspace.md", WORKSPACE), ("home.md", HOME)):
            (self.trial / name).write_text(text, encoding="utf-8")
        (self.trial / "_raw" / "S01.html").write_text(SAVED, encoding="utf-8")
        (self.trial / "_raw" / "S01.txt").write_text("a derived text file, not the snapshot", encoding="utf-8")

    def tearDown(self):
        self.tmp.cleanup()

    def fetcher(self, pages):
        return lambda url: (200, "text/html", pages[url].encode("utf-8"))

    def test_sources_are_found_when_nested(self):
        self.assertEqual(sorted(desk.sources_of(self.trial)), ["S01", "S02", "S03"])

    def test_passed_date_is_queued_and_future_date_is_not(self):
        items = desk.queue(self.root, "2026-11-14")
        passed = [i for i in items if i["rule"] == "date-passed"]
        self.assertEqual(sorted(i["where"] for i in passed), ["home.md:5", "workspace.md:16"])
        self.assertTrue(all("Nov 10, 2026" in i["what"] for i in passed))
        self.assertTrue(json.loads((self.root / "queue.json").read_text())["items"])

    def test_events_due_soon_are_queued_done_and_distant_ones_are_not(self):
        events = [i for i in desk.queue(self.root, "2026-11-14") if i["rule"] == "event"]
        self.assertEqual([i["due"] for i in events], ["2026-11-20"])

    def test_unchecked_sentences_are_queued(self):
        check = [i for i in desk.queue(self.root, "2026-11-14") if i["rule"] == "check"]
        self.assertEqual(len(check), 1)
        self.assertIn("sentences waiting", check[0]["what"])

    def test_impact_lists_exactly_the_sentences_citing_a_source(self):
        res = desk.impact(self.trial, "S01")
        self.assertEqual(res["passages"], ["P-001", "P-003"])
        self.assertEqual(sorted((s["file"], s["line"]) for s in res["sentences"]), [("home.md", 10), ("workspace.md", 8), ("workspace.md", 10)])
        one = desk.impact(self.trial, "P-002")
        self.assertEqual(len(one["sentences"]), 5)   # one in the summary, two under Coming up on the page, two in home.md

    def test_a_token_or_comment_change_is_not_a_change(self):
        body = "The county plans to spend $10 million in 2027."
        pages = {"https://example.org/budget": HTML.format(tok="aaa", body=body), "https://example.org/calendar": "<p>Meetings</p><p>Last updated 9:00</p>"}
        try:
            import bs4  # noqa: F401
        except ImportError:
            self.skipTest("beautifulsoup4 not installed")
        desk.watch_check(self.trial, "2026-11-14T00:00:00Z", self.fetcher(pages))
        pages["https://example.org/budget"] = HTML.format(tok="bbb", body=body)
        pages["https://example.org/calendar"] = "<p>Meetings</p><p>Last updated 10:30</p>"   # ignored by the page's `ignore` rule
        state = desk.watch_check(self.trial, "2026-11-15T00:00:00Z", self.fetcher(pages))
        self.assertEqual({k: v["state"] for k, v in state["sources"].items()}, {"S01": "unchanged", "S02": "unchanged"})
        self.assertFalse([i for i in desk.queue(self.root, "2026-11-15") if i["rule"] == "source-changed"])

    def test_a_text_change_is_flagged_queued_and_can_be_accepted(self):
        try:
            import bs4  # noqa: F401
        except ImportError:
            self.skipTest("beautifulsoup4 not installed")
        pages = {"https://example.org/budget": HTML.format(tok="aaa", body="The county now plans to spend $12 million in 2027."),
                 "https://example.org/calendar": "<p>Meetings</p>"}
        state = desk.watch_check(self.trial, "2026-11-14T00:00:00Z", self.fetcher(pages))
        self.assertEqual(state["sources"]["S01"]["state"], "changed")      # against our saved copy, on the first check
        self.assertEqual(state["sources"]["S01"]["diff"]["added"], 1)
        queued = [i for i in desk.queue(self.root, "2026-11-14") if i["rule"] == "source-changed"]
        self.assertEqual(len(queued), 1)
        self.assertIn("3 sentences cite it", queued[0]["size"])
        desk.watch_accept(self.trial, "S01", "2026-11-14T01:00:00Z")
        state = desk.watch_check(self.trial, "2026-11-14T02:00:00Z", self.fetcher(pages))
        self.assertEqual(state["sources"]["S01"]["state"], "unchanged")

    def test_only_keeps_our_rows_on_a_page_whose_sidebar_rolls(self):
        old = "Calendar\nNov 12 Hearing, Docket 12345\nNov 17 Some other meeting\nDec 9 Hearing, Docket 12345"
        new = "Calendar\nNov 24 A different meeting\nDec 9 Hearing, Docket 12345\nNov 12 Hearing, Docket 12345"
        self.assertNotEqual(desk.digest(desk.norm_text(old)), desk.digest(desk.norm_text(new)))
        self.assertEqual(sorted(desk.norm_text(old, only="12345").splitlines()), sorted(desk.norm_text(new, only="12345").splitlines()))

    def test_a_json_list_served_as_html_gains_one_line_per_new_item(self):
        old = b'{"count": 1, "items": [{"id": 7, "description": "Direct testimony", "filedDate": "2026-09-23"}]}'
        new = b'{"count": 2, "items": [{"id": 9, "description": "Hearing transcript", "filedDate": "2026-11-20"}, {"id": 7, "description": "Direct testimony", "filedDate": "2026-09-23"}]}'
        counts, detail = desk.compare(desk.text_of(old), desk.text_of(new))
        self.assertEqual(counts, {"added": 2, "removed": 1})   # the new filing and the new count; the old count
        self.assertIn('+ items.[] filedDate="2026-11-20" | description="Hearing transcript"', detail)

    def test_changed_lines_stay_out_of_the_committed_record(self):
        pages = {"https://example.org/budget": HTML.format(tok="x", body="Jane Roe of 12 Example Street spoke."), "https://example.org/calendar": "<p>Meetings</p>"}
        try:
            import bs4  # noqa: F401
        except ImportError:
            self.skipTest("beautifulsoup4 not installed")
        desk.watch_check(self.trial, "2026-11-14T00:00:00Z", self.fetcher(pages))
        self.assertNotIn("Example Street", (self.trial / "watch.json").read_text())
        self.assertIn("Example Street", (self.trial / "_raw" / "watch" / "S01.diff.txt").read_text())

    def test_a_failed_fetch_is_recorded_as_blocked(self):
        def boom(url):
            raise OSError("no route")
        state = desk.watch_check(self.trial, "2026-11-14T00:00:00Z", boom)
        self.assertEqual({v["state"] for v in state["sources"].values()}, {"blocked"})

    def test_index_row_matches_the_page(self):
        rows = desk.index(self.root, "2026-11-14", refresh=False)
        r = rows[0]
        self.assertEqual((r["slug"], r["stage"], r["revision"], r["next_date"], r["sources"], r["passages"]), ("demo", "open", "1.2", "2026-11-20", 2, 3))
        self.assertFalse(r["passed_checks"])   # nothing has been checked yet
        self.assertEqual(json.loads((self.root / "registry.json").read_text())["pages"][0]["slug"], "demo")

    def test_brief_maps_sections_to_lines(self):
        text = desk.brief(self.root, self.trial, "2026-11-14")
        self.assertIn("L6-11  30-second version", text)
        self.assertIn("[date-passed] 2026-11-10", text)


if __name__ == "__main__":
    unittest.main()
