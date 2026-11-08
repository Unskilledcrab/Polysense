# 0002: One Django application, server-rendered with htmx

- **Status:** Accepted
- **Date:** 2026-11-08

## Context

One developer will build and run the product. The owner asked for the best tool for each part, and for
secure, reliable software. The product is mostly text: layered explanations and linear argument cards
(research shows maps help only trained readers). It must be fast on cheap phones and accessible. We also
need editor tools, data ingestion and, later, AI drafting.

Options considered:
- **(a)** A Django back office plus a separate TypeScript/Astro public site using a generated API client.
- **(b)** A single Django app serving server-rendered templates with htmx.
- **(c)** A full-stack JavaScript framework.

## Decision

**(b)**, one Django application (GeoDjango) for the editor tools, ingestion, the future AI pipeline and
the public site. Pages are server-rendered with htmx and a little vendored JavaScript (pdf.js, a diff
view).

## Why

- **Fewest moving parts:** one language, one test suite, one image, one deploy. No contract between
  languages to keep in sync.
- **Security:** Django's secure defaults (CSRF, auto-escaping, ORM parameterization, built-in CSP in
  Django 6), its dedicated security team, and LTS releases.
- **Readers:** server-rendered HTML is the fastest and most robust option on low-end phones, and it works
  without JS.
- **Editors:** Django admin covers the CRUD screens for dozens of civic entity types, so custom work goes
  only into the Review Desk.

## Triggers to add a separate front end later

Revisit this decision if **any** of these become true:
- a feature needs complex client-side state that htmx can't express cleanly;
- a native app is planned;
- a front-end team joins.

A read-only API (Django REST Framework) would let a new front end be added without changing the core.

## Consequences

Interactive pieces stay small and progressive. Personal pages use district-keyed URLs, so they can be
cached ([0009](0009-no-accounts-district-urls.md)).
