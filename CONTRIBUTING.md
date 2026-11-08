# Contributing

Thanks for your interest. The project is in its **planning and manual-trial** phase, and there is no
application code yet. The most useful contributions right now are:

- **Reviewing the plan.** Open an issue if something in `docs/` is wrong, unclear, unfair or missing.
- **Checking trial outputs.** Read a `trials/NNN-*/workspace.md` against its sources and report
  problems.
- **Pointing to better sources** for Georgia legislative, budget or participation data.

## Ground rules

- Read [docs/README.md](docs/README.md) first, especially the
  [editorial standards](docs/policies/editorial-standards.md) and the
  [prove-then-automate process](docs/process/prove-then-automate.md).
- Nonpartisan means **the same standards for everyone**. Contributions that push a side will be declined,
  whoever they favor.
- Cite sources for every factual claim, and mark anything you couldn't verify.
- Never include personal addresses or other personal data in issues, test data or commits.
- Security problems go to [SECURITY.md](SECURITY.md), not public issues.

## Workflow (once code exists)

- Branch from the default branch: `feature/<issue>-<short-name>` or `fix/<issue>-<short-name>`.
- Keep pull requests small. Each one needs passing CI: lint, types, tests, security scans and the
  accessibility smoke test.
- Record significant decisions as an ADR in `docs/adr/`.
- New dependencies need an entry in [docs/architecture/dependencies.md](docs/architecture/dependencies.md).

## Code of Conduct

Everyone participating agrees to the [Code of Conduct](CODE_OF_CONDUCT.md).
