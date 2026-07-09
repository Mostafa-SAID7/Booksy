# Booksy

> A modern, scalable book management API built with .NET 8 and CQRS

[![Build](https://img.shields.io/badge/build-passing-brightgreen)]()
[![License](https://img.shields.io/badge/license-MIT-blue)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/download/dotnet/8.0)

## Quick Start

```bash
# Clone & setup
git clone https://github.com/Mostafa-SAID7/Booksy.git
cd Booksy/Booksy

# Restore & migrate
dotnet restore
dotnet ef database update

# Run
dotnet run
```

**API**: https://localhost:5001/api  
**Swagger**: https://localhost:5001/swagger

---

## Features

- 📚 **Book Management** - Create, read, update, delete books
- 👥 **User Accounts** - Registration, authentication, profiles
- 🛒 **Shopping Cart** - Add/remove items, checkout process
- ⭐ **Reviews & Ratings** - User reviews with ratings
- 🏷️ **Categories & Tags** - Organize books efficiently
- 🔐 **Authorization** - Role-based access control (Admin/User)
- 🎯 **Search & Filter** - Advanced search with pagination
- 📊 **Reports & Statistics** - Sales and user metrics
- 💰 **Promotions** - Discount management system
- 🛍️ **Inventory** - Stock tracking and management

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| **Framework** | .NET 8 |
| **Database** | SQL Server + EF Core 8 |
| **API Pattern** | CQRS with MediatR |
| **Authentication** | JWT Bearer Tokens |
| **Validation** | FluentValidation |
| **Mapping** | AutoMapper |

---

## Project Structure

```
Booksy/
├── Features/           # Feature modules
│   ├── Books/
│   ├── Authors/
│   ├── Categories/
│   ├── Orders/
│   ├── Reviews/
│   └── ... (15+ features)
├── Common/             # Shared services & utilities
│   ├── Services/
│   ├── Extensions/
│   └── Models/
├── Core/               # CQRS & behaviors
├── DataAccess/         # EF Core configuration
├── Infrastructure/     # External integrations
└── Repositories/       # Data access layer
```

---

## Documentation

- **[Architecture](./docs/ARCHITECTURE.md)** - System design & patterns
- **[API Reference](./docs/API.md)** - Complete endpoint documentation  
- **[Setup Guide](./docs/SETUP.md)** - Installation & configuration
- **[Database](./docs/DATABASE.md)** - Schema & migrations
- **[Troubleshooting](./docs/TROUBLESHOOTING.md)** - Common issues & solutions
- **[Contributing](./docs/CONTRIBUTING.md)** - Development guidelines

---

## API Examples

### Get Books
```bash
curl https://localhost:5001/api/books?pageNumber=1&pageSize=10
```

### Create Book (Admin)
```bash
curl -X POST https://localhost:5001/api/books \
  -H "Authorization: Bearer <token>" \
  -H "Content-Type: application/json" \
  -d '{"title":"Book Name","price":19.99,"authorId":"...","categoryId":"..."}'
```

### Get User Orders
```bash
curl https://localhost:5001/api/orders/user/{userId} \
  -H "Authorization: Bearer <token>"
```

[Full API Docs →](./docs/API.md)

---

## Default Credentials

| Role | Email | Password |
|------|-------|----------|
| Admin | admin@booksy.local | Admin@123456 |

> ⚠️ Change credentials in production

---

## Requirements

- .NET 8 SDK or later
- SQL Server 2019+ (or SQL Server Express)
- Git

---

## Configuration

Edit `appsettings.json` before running:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=Booksy;..."
  },
  "Jwt": {
    "SecretKey": "your-secret-key-minimum-32-characters-long",
    "ExpiryMinutes": 60
  }
}
```

---

## Development

### Build
```bash
dotnet build
```

### Run Tests
```bash
dotnet test
```

### Create Migration
```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

---

## Deployment

```bash
# Production build
dotnet publish -c Release

# Run with environment
ASPNETCORE_ENVIRONMENT=Production dotnet Booksy.dll
```

Set environment variables for production:
- `ConnectionStrings__DefaultConnection`
- `Jwt__SecretKey`
- `ASPNETCORE_ENVIRONMENT=Production`

---

## License

MIT License - see [LICENSE](LICENSE) file

---

## Support

- 📖 [Documentation](./docs)
- 🐛 [Report Issues](https://github.com/Mostafa-SAID7/Booksy/issues)
- 💬 [Discussions](https://github.com/Mostafa-SAID7/Booksy/discussions)

---

**Made with ❤️ by [toAminStore](https://github.com/Mostafa-SAID7)**
