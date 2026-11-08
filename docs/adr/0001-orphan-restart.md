# 0001: Restart on an orphan branch

- **Status:** Accepted
- **Date:** 2026-11-08

## Context

The repository held Polysense, a .NET solution from 2020–2021: C# news scrapers, an EF Core API, and
WPF, Xamarin, Blazor, Angular and React clients. The project's direction has changed completely (see
[vision.md](../product/vision.md)). None of the code fits the new architecture, and the owner asked for a
completely fresh start.

## Decision

Start the new project on an **orphan branch** with no shared history. Carry over only `LICENSE`
(Apache-2.0) and `CODE_OF_CONDUCT.md`. The old work remains on `Development` and the other existing
branches.

## Consequences

- The new history is clean and starts with the planning docs.
- GitHub can't open a pull request from this branch into `Development`, because they share no history.
  When ready, make a new default branch from it.
- Suggested: tag the old work (for example `legacy-dotnet-2021`) so it stays easy to find.
