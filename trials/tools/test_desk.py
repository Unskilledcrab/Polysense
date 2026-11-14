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

    # ----- pointers, not copies (ADR 0016) -----

    def hashed(self):
        """Give the fixture's S01 a real fingerprint, as a snapshot would."""
        sha = desk.sha_bytes(SAVED.encode("utf-8"))
        (self.trial / "sources.yaml").write_text(SOURCES.replace('status: retrieved, retrieved_at: "2026-11-01T00:00:00Z"}',
            f'status: retrieved, retrieved_at: "2026-11-01T00:00:00Z", local_file: S01.html, sha256: {sha}}}'), encoding="utf-8")
        return sha

    def test_a_missing_cache_is_not_a_failure_but_a_wrong_hash_is(self):
        import subprocess
        self.hashed()
        tool = str(Path(__file__).parent / "verify_hashes.py")
        run = lambda: subprocess.run([sys.executable, tool, str(self.trial / "sources.yaml")], capture_output=True, text=True)   # noqa: E731
        self.assertIn("PASS", run().stdout.splitlines()[-1])
        (self.trial / "_raw" / "S01.html").rename(self.trial / "_raw" / "moved-aside.html")
        r = run()
        self.assertEqual(r.returncode, 0)
        self.assertIn("1 not cached", r.stdout.splitlines()[-1])
        (self.trial / "_raw" / "S01.html").write_text("a different document", encoding="utf-8")
        self.assertEqual(run().returncode, 1)

    def test_restore_keeps_a_file_only_if_it_is_the_document_we_read(self):
        try:
            import bs4  # noqa: F401
        except ImportError:
            self.skipTest("beautifulsoup4 not installed")
        self.hashed()
        cached = self.trial / "_raw" / "S01.html"
        desk.links(self.trial, "2026-11-14T00:00:00Z")            # records the text fingerprint while the file is cached
        cached.unlink()
        rows = desk.restore(self.trial, "2026-11-14T00:00:00Z", self.fetcher({"https://example.org/budget": SAVED}))
        self.assertEqual([(r["source"], r["state"]) for r in rows], [("S01", "restored")])
        self.assertTrue(cached.exists() and (self.trial / "_raw" / "S01.txt").exists())
        cached.unlink()
        changed = HTML.format(tok="z", body="The county now plans to spend $12 million in 2027.")
        rows = desk.restore(self.trial, "2026-11-14T00:00:00Z", self.fetcher({"https://example.org/budget": changed}))
        self.assertEqual(rows[0]["state"], "changed")
        self.assertFalse(cached.exists())                         # a changed document never replaces the original
        self.assertTrue((self.trial / "_raw" / "changed" / "S01.html").exists())

    def test_links_tells_same_from_changed_from_gone_and_queues_the_stale_ones(self):
        try:
            import bs4  # noqa: F401
        except ImportError:
            self.skipTest("beautifulsoup4 not installed")
        self.hashed()
        (self.trial / "page.yaml").write_text(PAGE.replace("sources: [S01, {id: S02", "sources: [{id: S02"), encoding="utf-8")   # S01 is cited, not watched
        url = "https://example.org/budget"
        same_text = HTML.format(tok="new-token", body="The county plans to spend $10 million in 2027.")   # new bytes, same words
        book = desk.links(self.trial, "2026-11-14T00:00:00Z", self.fetcher({url: same_text}))
        self.assertEqual(book["S01"]["state"], "same")
        # The page gains a sidebar, but the passage we can test (the one that is in our saved copy) is still there.
        grown = HTML.format(tok="t", body="The county plans to spend $10 million in 2027.</p><p>Upcoming: a meeting on Nov 24.")
        book = desk.links(self.trial, "2026-11-14T12:00:00Z", self.fetcher({url: grown}))
        self.assertEqual(book["S01"]["state"], "same")
        self.assertEqual(book["S01"]["files"]["S01.html"]["testable"], ["P-001"])   # P-003 isn't in the saved copy, so it can't be tested
        self.assertIn("still there", book["S01"]["files"]["S01.html"]["note"])
        book = desk.links(self.trial, "2026-11-15T00:00:00Z", self.fetcher({url: HTML.format(tok="t", body="Something else entirely.")}))
        self.assertEqual(book["S01"]["state"], "changed")
        self.assertIn("1 of 1 passages", book["S01"]["files"]["S01.html"]["note"])
        stale = [i for i in desk.queue(self.root, "2026-11-15") if "link we cite" in i["what"]]
        self.assertEqual(len(stale), 1)
        self.assertIn("3 sentences cite it", stale[0]["size"])
        book = desk.links(self.trial, "2026-11-16T00:00:00Z", lambda u: (404, "text/html", b"not found"))
        self.assertEqual(book["S01"]["state"], "gone")
        self.assertNotIn("S02", book)                             # a watched listing page is left to `watch`

    def test_passages_carry_forward_when_the_source_is_not_cached(self):
        import subprocess
        (self.trial / "_raw" / "S01.html.txt").write_text("Budget\nThe county plans to spend $10 million in 2027.\nProperty tax is the main income.", encoding="utf-8")
        (self.trial / "passages.spec.yaml").write_text(
            'passages:\n'
            '  - {id: P-001, source: S01, file: S01.html.txt, locator: "page 1", start: "The county plans", end: "in 2027."}\n'
            '  - {id: P-003, source: S01, file: S01.html.txt, locator: "page 2", start: "Property tax", end: "main income."}\n', encoding="utf-8")
        tool = str(Path(__file__).parent / "build_passages.py")
        run = lambda: subprocess.run([sys.executable, tool, str(self.trial / "passages.spec.yaml")], capture_output=True, text=True)   # noqa: E731
        self.assertEqual(run().returncode, 0)
        built = (self.trial / "passages.md").read_text()
        (self.trial / "_raw" / "S01.html.txt").unlink()
        r = run()
        self.assertEqual(r.returncode, 0)
        self.assertIn("2 carried forward", r.stdout)
        self.assertEqual((self.trial / "passages.md").read_text(), built)       # byte for byte
        spec = self.trial / "passages.spec.yaml"
        spec.write_text(spec.read_text().replace('start: "Property tax"', 'start: "A span we never built"'), encoding="utf-8")
        self.assertEqual(run().returncode, 1)                                   # a changed spec needs the source restored

    def test_archive_records_what_it_found_and_never_invents_a_copy(self):
        def found(url):
            return 200, "application/json", b'{"archived_snapshots": {"closest": {"available": true, "url": "http://web.archive.org/web/20261109/https://example.org/budget", "timestamp": "20261109000000"}}}'
        self.assertEqual(desk.archive_lookup("https://example.org/budget", found)["url"], "https://web.archive.org/web/20261109/https://example.org/budget")
        def nothing(url):
            return (200, "application/json", b'{"archived_snapshots": {}}') if "available" in url else (523, "text/html", b"")
        res = desk.archive_lookup("https://example.org/budget", nothing)
        self.assertEqual(res["state"], "none")
        self.assertNotIn("url", res)
        def boom(url):
            raise OSError("no route")
        self.assertEqual(desk.archive_lookup("https://example.org/budget", boom)["state"], "blocked")

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
