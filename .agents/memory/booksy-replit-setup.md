---
name: Booksy Replit setup
description: Key decisions and quirks encountered setting up this .NET 9 project on Replit with PostgreSQL
---

# Booksy Replit setup

## Rules

- Use `PGHOST`/`PGPORT`/`PGUSER`/`PGPASSWORD`/`PGDATABASE` env vars (not `DATABASE_URL`) — Replit's `DATABASE_URL` is not parseable by Npgsql directly; the connection string must be built from the individual vars with `SSL Mode=Disable` (internal connection, no TLS needed).
- The EF local tool (`dotnet tool run dotnet-ef`) must be used for migrations — the global `dotnet-ef` tool path isn't on PATH in Replit shells. Tool manifest lives at `Booksy/.config/dotnet-tools.json`.
- Enum `HasDefaultValue` with a typed enum value fails at migration design time in Npgsql — use `HasConversion<int>()` instead and rely on C# model defaults.
- Remove `Microsoft.AspNetCore.RateLimiting` package — it's built into ASP.NET Core 9 and the NuGet package only goes up to 7.x.
- Remove `MediatR.Extensions.Microsoft.DependencyInjection` — merged into MediatR 12; the project uses `services.AddMediatR(cfg => {...})` which is native to MediatR 12.

**Why:** These were all discovered during the first run attempt; recording them avoids repeating the same debugging cycle.

## Known pre-existing issues (not introduced by setup)

- `DBInitializer` has an inverted seed condition (`if (Roles.Any())` should be `if (!Roles.Any())`) — no roles or admin user are seeded on fresh DB. Follow-up task #4 covers this.
- `CartItem.CartId` is `int` but `Cart.Id` is `Guid` — EF creates a shadow FK `CartId1`. Follow-up task #2 covers this.
- JWT secret falls back to a plaintext placeholder in `appsettings.Development.json`. Follow-up task #3 covers this.
