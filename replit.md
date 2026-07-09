# Booksy API

A modern, scalable book management REST API built with .NET 9 and CQRS.

## How to run

The **"Start application"** workflow starts the server. Swagger UI is served at the root (`/`).

```
cd Booksy && dotnet run --urls http://0.0.0.0:5000
```

## Tech stack

| Layer | Technology |
|-------|-----------|
| Framework | .NET 9 / ASP.NET Core 9 |
| ORM | Entity Framework Core 9 + Npgsql (PostgreSQL) |
| Database | Replit built-in PostgreSQL |
| Auth | JWT Bearer + ASP.NET Identity |
| Patterns | CQRS (MediatR 12), FluentValidation |
| Docs | Swagger / OpenAPI at `/` |
| Payments | Stripe |

## Environment

- `ASPNETCORE_ENVIRONMENT=Development` — enables Swagger and dev error pages
- `ASPNETCORE_URLS=http://0.0.0.0:5000` — port binding for Replit
- `PGHOST`, `PGPORT`, `PGUSER`, `PGPASSWORD`, `PGDATABASE` — injected automatically by Replit

## Database

Uses Replit's built-in PostgreSQL. Migrations are applied automatically on startup via `DBInitializer`.

To add a migration after model changes:
```bash
cd Booksy && dotnet tool run dotnet-ef migrations add <MigrationName>
```

## Notes

- Swagger is enabled in all environments (not just Development) to make it accessible on Replit
- HTTPS redirect is disabled; Replit handles SSL termination at the proxy layer
- The seed data condition in `DBInitializer.Initialize()` is inverted — roles/admin are seeded when roles already exist (pre-existing quirk, not a blocker)
- Stripe and Email settings use placeholder values; configure real keys in Replit Secrets if needed

## User preferences

_(none yet)_
