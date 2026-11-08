# civic — Georgia civic decisions, explained with evidence

> Working codename: **`civic`**. The public name is not chosen yet (see
> [docs/product/naming.md](docs/product/naming.md)); "Civic Lens" was the original working name and is
> **not** usable as-is.

**Know what's coming. Understand what's at stake. Know how you can participate.**

An independent, nonpartisan platform that shows people in Georgia what government decisions they can
influence, explains those decisions in plain language, and helps them weigh the arguments, with every
factual statement traceable to its original evidence.

## Status: planning (no application code yet)

This branch is a **fresh start**. It was created as an orphan branch, so it shares no history with the
earlier Polysense .NET project. That project (2020–2021: scrapers, an EF Core API, and WPF, Xamarin,
Blazor, Angular and React clients) still lives on the `Development` branch.

| Item | Decision |
|---|---|
| First pilot | The **2027 Georgia General Assembly session** (convenes Jan 11, 2027) |
| Builder | One developer working with AI assistance |
| Approach | **Prove each step by hand, then automate it** ([process](docs/process/prove-then-automate.md)) |
| Stack | Django (GeoDjango) + PostgreSQL/PostGIS, server-rendered pages, Claude API for drafting *after* the manual process is proven ([architecture](docs/architecture/overview.md)) |

## Principles

1. **Clarity first.** Answer first, plain language, the same template everywhere, tested with real people.
2. **Every claim traceable.** Each factual sentence cites an archived source passage, or is labeled as analysis.
3. **Trust you can inspect.** Public revision history, public corrections, published methods and funding.
4. **Nonpartisan by structure.** The same template and standards for every side, with no ratings or endorsements, and no false balance.
5. **Privacy by default.** No accounts and no stored addresses; personalization uses districts only.
6. **Secure and reliable.** Mature, well-maintained, permissively licensed software, with defense in depth.

## Read the docs

Start at **[docs/README.md](docs/README.md)**. It lists every document and the order to read them in.

Manual trials (each process done by hand before anything is automated) live in **[trials/](trials/)**.

## License

Code is under the [Apache License 2.0](LICENSE). Published content is proposed to be under CC BY 4.0, and
data keeps each source's license ([ADR 0010](docs/adr/0010-licensing.md)). Contributors follow the
[Code of Conduct](CODE_OF_CONDUCT.md).
