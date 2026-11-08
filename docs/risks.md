# Risks and open questions (as of 2026-11-08)

## Risk register

| # | Risk | Likelihood / impact | Mitigation |
|---|---|---|---|
| 1 | **AI checkers miss errors or share the drafter's blind spots** (no human in the loop, per [ADR 0014](adr/0014-ai-only-verification.md)) | Medium / High | Machine-checked evidence spans; independent fresh-context verifiers; planted-error calibration every trial (≥ 90% catch) with new error types each time; bias panel with opposing advocates; published catch and error rates; public challenges; fast corrections; revisit before launch with data |
| 1b | **One person runs out of capacity** | High / Medium | Cap at 6–8 workspaces; automate only proven steps; feature freeze |
| 2 | **Data sources break or lag** (undocumented legis.ga.gov; roster churn after a reported change in House control) | High / Medium | Three-source cross-check; freshness alarms; admin overrides; rosters verified by hand in January |
| 3 | **A wrong or misattributed claim is published** | Medium / High | Human support check on every citation; verbatim quotes; monthly audit; quarantine status; fast visible corrections |
| 4 | **Seen as partisan** in a high-attention session (new Governor; House control reportedly changed) | High / High | Published rubric and full coverage log including rejections; sponsor-mix stats; cross-spectrum pre-readers; no ratings |
| 5 | **Content is too hard to understand** | Medium / High | Style guide, length budgets, comprehension rounds that must pass before launch; the template changes when people fail |
| 6 | **Legal trouble** (IRS, defamation) | Medium / High | Fiscal sponsor review; media liability insurance before beta; counsel reviews the policies; verification documented |
| 7 | **Security incident** (defacement, account takeover, data poisoning) | Medium / High | See [security.md](architecture/security.md): Access + MFA, append-only revisions, untrusted-document handling, minimal dependencies |
| 8 | **AI behavior drifts** after model or prompt changes | Medium / Medium | Pinned model; prompts in git; evals from trial gold data re-run before any change; raw outputs stored |
| 9 | **Low adoption** | High / Medium | Search-indexable bill and hearing pages; ICS feeds; cross-spectrum partners; CC BY content others can reuse |
| 10 | **Redistricting before 2028** | Medium / Medium | Boundaries versioned by date and purpose; manual district picker as fallback |
| 11 | **Funding concentration or loss** | Medium / High | Cap any single funder at about 20%; diverse funders; low operating costs |

## Open questions (decisions still needed)

| Question | Current lean | Decide by |
|---|---|---|
| Public name | Not "Civic Lens". Run the naming process | Before public beta |
| Content license | CC BY 4.0 ([ADR 0010](adr/0010-licensing.md)) | Before public beta |
| Hosting | Render + Cloudflare + R2 ([ADR 0011](adr/0011-hosting.md)); confirm PostGIS and PITR on the plan | P1 |
| Fiscal sponsor | Shortlist (e.g. Code for Science & Society, Hacks/Hackers, a Georgia nonprofit) | Before public beta |
| Human review before public launch? | Owner: no human in the loop during the proof of concept. Revisit with 3+ trials of measured data and legal advice ([ADR 0014](adr/0014-ai-only-verification.md)) | Before public beta |
| Code of Conduct contact | Still points to the old Discord; replace with a project email | P1 |
| Repo visibility | **Decided: public** (owner, 2026-11-08) | Done |
| Network access for trials | **Resolved:** the environment now has unrestricted network access. Some sites still refuse scripted requests (WABE, LegiScan, Georgia Recorder, The Lancet); use a browser capture or another primary source | Done |
| Private vulnerability reporting | Owner approved turning it on. It is a repository setting, so the owner must enable it (Settings → Code security → Private vulnerability reporting) | Now |
| Comprehension testers | The owner recruits 3–5 people per round | P1 |
| Old `Development` branch | Suggest tagging it `legacy-dotnet-2021`; later make a new default branch from this one | Owner's choice |
