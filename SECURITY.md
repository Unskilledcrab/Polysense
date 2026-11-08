# Security policy

## Reporting a vulnerability

Please report security problems **privately**. Use GitHub's private vulnerability reporting: open the
repository's **Security** tab and choose **Report a vulnerability**. Do not open a public issue for a
security problem.

> Maintainer to-do: enable "Private vulnerability reporting" in the repository settings, and publish a
> `/.well-known/security.txt` when the site launches.

Please include what you found, how to reproduce it, and the impact you expect.

## What to expect

| Step | Target |
|---|---|
| Acknowledge your report | within 3 business days |
| Initial assessment | within 7 business days |
| Fix or mitigation for high-severity issues | as fast as practical; we will keep you updated |

We will credit you when the issue is disclosed, unless you prefer not to be named.

## Good-faith research

We will not pursue action against research done in good faith that:
- avoids privacy violations, data destruction and service disruption;
- does not access other people's data;
- gives us reasonable time to fix the issue before you disclose it.

## Not a security issue?

Errors in published content, such as a wrong fact, a missing source or an unfair description, go through
the public challenge process instead. See
[docs/policies/corrections-and-challenges.md](docs/policies/corrections-and-challenges.md).

## How we build securely

See [docs/architecture/security.md](docs/architecture/security.md) for the threat model and controls,
and [ADR 0013](docs/adr/0013-security-baseline.md) for the baseline.
