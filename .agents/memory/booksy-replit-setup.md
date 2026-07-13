---
name: Booksy Replit setup
description: Key decisions and quirks encountered setting up this .NET 9 project on Replit with PostgreSQL and Elasticsearch
---

# Booksy Replit setup

## Rules

- Use `PGHOST`/`PGPORT`/`PGUSER`/`PGPASSWORD`/`PGDATABASE` env vars (not `DATABASE_URL`) — Replit's `DATABASE_URL` is not parseable by Npgsql directly; the connection string must be built from the individual vars with `SSL Mode=Disable` (internal connection, no TLS needed).
- The EF local tool (`dotnet tool run dotnet-ef`) must be used for migrations — the global `dotnet-ef` tool path isn't on PATH in Replit shells. Tool manifest lives at `Booksy/.config/dotnet-tools.json`.
- Enum `HasDefaultValue` with a typed enum value fails at migration design time in Npgsql — use `HasConversion<int>()` instead and rely on C# model defaults.
- Remove `Microsoft.AspNetCore.RateLimiting` package — it's built into ASP.NET Core 9 and the NuGet package only goes up to 7.x.
- Remove `MediatR.Extensions.Microsoft.DependencyInjection` — merged into MediatR 12; the project uses `services.AddMediatR(cfg => {...})` which is native to MediatR 12.
- Use `dotnet-9.0` module in `.replit`, not `dotnet-7.0`. The project targets `net9.0`.

**Why:** These were all discovered during the first run attempt; recording them avoids repeating the same debugging cycle.

## Elasticsearch integration (Elastic.Clients.Elasticsearch 9.x)

- Package: `Elastic.Clients.Elasticsearch` 9.4.2 (installed in `Booksy/Booksy.csproj`).
- ES URL from `ELASTICSEARCH_URL` env var (falls back to `http://localhost:9200`). Optional basic auth via `ELASTICSEARCH_USERNAME` / `ELASTICSEARCH_PASSWORD`.
- Index name: `booksy-books`. Dynamic mapping (no explicit field mappings) — avoids `PropertiesDescriptor.Number()` which does not exist in 9.x.
- **9.x API gotchas:**
  - `PropertiesDescriptor<T>.Number()` does not exist — use dynamic mapping or correct typed methods.
  - `DeleteResult` enum does not exist — use `Elastic.Clients.Elasticsearch.Result.NotFound`.
  - `BulkAsync` with document lambda: use `.IndexMany(batch, (op, doc) => op.Id(doc.Id))` — the old NEST descriptor-per-item pattern is gone.
  - `PingAsync` takes a positional `CancellationToken`, not a named `ct` parameter.
- Graceful degradation: all search/index methods catch `Exception` and log — ES being down never crashes the API.
- `POST /api/search/reindex` (Admin) rebuilds the index from Postgres in batches of 500.

**Why:** 9.x is a full rewrite of NEST 7.x; many method signatures changed and must be verified against the installed version, not NEST docs.

## Swagger nav injection (Swashbuckle v9)

- Use `c.InjectStylesheet("/css/swagger-nav.css")` and `c.InjectJavascript("/js/swagger-nav.js")` in `UseSwaggerUI` block of `Program.cs`.
- The injected JS must use a `MutationObserver` to hide `.swagger-ui .topbar` because React renders it asynchronously after DOMContentLoaded.
- `swagger-nav.css` must NOT use `@import url(cdn...)` — this can delay CSS parsing in strict CSP contexts. Use inline SVG icons in the JS instead.
- The app has a strict CSP middleware (`script-src 'self'`). All injected scripts must be served from `'self'` (static files in wwwroot) — no CDN scripts.
- Screenshot tools may capture the Swagger page before JavaScript modifies the React-rendered DOM; the nav does appear correctly in real browsers.

**Why:** Swashbuckle v9 + Swagger UI v5 renders via React; timing-sensitive DOM injection requires MutationObserver. CDN @imports in CSS can block parsing under strict CSP.

## wwwroot static pages enhancement

- CSS animations: use `[data-reveal]` + `IntersectionObserver` in `app.js` for scroll-reveal. Classes `[data-reveal-delay="1-6"]` control stagger timing.
- Inline scripts blocked by `script-src 'self'` CSP — all page-specific JS must be external files (e.g. `particles-404.js`, `health-card.js`).
- 404 particle canvas must be in external `wwwroot/js/particles-404.js` (not inline `<script>`).
- Health card countdown (`health-card.js`) and dashboard stats (`dashboard.js`) are both served to `dashboard.html` as separate external scripts.
- Toast container is a `<div id="toasts" class="toast-container">` appended to body by `app.js`'s `toast()` function — no HTML markup needed.

**Why:** CSP blocks inline scripts on all pages; all dynamic behaviour must live in external JS files under wwwroot.

## Known pre-existing issues (not introduced by setup)

- `DBInitializer` has an inverted seed condition (`if (Roles.Any())` should be `if (!Roles.Any())`) — no roles or admin user are seeded on fresh DB. Follow-up task #2 covers this.
- `CartItem.CartId` is `int` but `Cart.Id` is `Guid` — EF creates a shadow FK `CartId1`. Follow-up task #3 covers this.
- JWT secret falls back to empty string if `JWT__SecretKey` env var is missing — app should fail fast at startup. Follow-up task #4 covers this.
