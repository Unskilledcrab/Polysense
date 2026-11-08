# Editorial standards (draft)

We base these on the IFCN Code of Principles and use the Trust Project indicators as a checklist. They
apply to everything we publish, whoever it helps or hurts.

## Our commitments

1. **Nonpartisanship and fairness.** The same methods and template for every party, side and position.
   See [nonpartisanship.md](nonpartisanship.md).
2. **Transparent sources.** Every factual sentence links to an archived passage of its source, so
   readers can check it themselves.
3. **Transparent funding and organization.** See [funding.md](funding.md).
4. **Transparent methods,** including how AI is used ([ai-use.md](ai-use.md)) and how topics are chosen
   ([topic-selection.md](topic-selection.md)).
5. **Open and honest corrections.** Public revision history and a corrections log
   ([corrections-and-challenges.md](corrections-and-challenges.md)).

## Sources

- **Prefer primary sources:** bill text, fiscal notes, official records, hearing video.
- **Secondary research** is used with its method and author noted.
- **Advocacy analyses** are *documented arguments*. We cite them as what that organization says, label
  where it stands, and never present them as neutral fact.
- **News** provides context and a timeline. It is never the only source for a contested fact.
- **Every source is archived** with a capture date and a SHA-256 hash. If a source changes, dependent
  sentences are flagged for re-review.

## Claims

- **Each claim is typed:** factual, predictive, values, computed or editorial. Only factual and predictive
  claims get an evidence label ([experience.md](../product/experience.md#evidence-vocabulary-used-only-for-factual-and-predictive-claims)).
- **Disagreement about values is not misinformation.** We say what the disagreement is about. We don't
  rule on it.
- **No false balance.** Every option gets its strongest case. When the evidence is unequal, we say so.

## Documented vs. anticipated arguments

- **Documented** arguments need an identifiable speaker, their role, the venue, the date, and a
  **verbatim** quote from an archived source.
- **Anticipated** arguments are labeled "Some people may argue…", and are **never attributed** to a
  person or organization.
- Documented arguments need **two independent verification passes** before publication
  ([verification protocol](../process/verification-protocol.md)).
- **Minors are never named.** Describe them by role ("a 17-year-old student who testified"), even if
  news reports name them.
- **Keep political parties out of it.** Describe people by their role and arguments by the issue,
  never as left vs. right. Mention a party only when a fact can't be understood without it (a party-line
  vote, for example), and then the same way for every side.

## Things we never do

- Endorse, rate or score candidates or bills.
- Write voting logistics from memory or with AI. We link to official sources.
- Generate synthetic audio, images or video of real people.
- Edit published content without a new public revision.
- Use loaded language outside verbatim quotes.

## Review

- Every published item shows when and how it was verified, with links to the verification records.
- Named-speaker, election-related and correction content needs two independent verification passes.
- Verifiers apply the [review desk handbook](../ops/review-desk-handbook.md) rubric. During the proof of
  concept the verifiers are independent AI agents, with no human in the loop
  ([ADR 0014](../adr/0014-ai-only-verification.md)).
