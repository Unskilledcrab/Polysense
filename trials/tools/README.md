# Trial tools

Small helpers for the manual trials. They are the seed of the future deterministic gate
([ai-pipeline.md](../../docs/architecture/ai-pipeline.md#3-deterministic-gate-code-not-models)).

| Script | Checks | Dependencies |
|---|---|---|
| `check_citations.py WORKSPACE PASSAGES` | Every sentence has a passage citation or a tag; passage IDs exist; quotes are verbatim; numbers appear in cited passages; loaded words outside quotes | stdlib |
| `verify_hashes.py SOURCES.yaml [--print]` | Every retrieved file in `_raw/` matches its recorded SHA-256 | PyYAML (`uv run --with pyyaml …`) |
| `readability.py WORKSPACE` | Flesch–Kincaid grade per section; flags the 30-second version and Layers 1–3 above 8.5 | stdlib |

Tested on 2026-11-08 against a throwaway fixture. Results:
- `check_citations.py` caught a non-verbatim quote, an uncited sentence, a misplaced marker, an unknown
  passage ID, a number missing from its source, and a loaded word.
- `verify_hashes.py` caught a hash mismatch.
- `readability.py` scored plain text at grade 3.9 and dense legalese at 32.4.

These are **screening aids**. A human support check and comprehension testing remain the real tests.
