# Booksy - Book Management API

A modern, production-ready book management system built with .NET 8, CQRS pattern, and Entity Framework Core.

## 🚀 Quick Start

### Prerequisites
- .NET 8 SDK or later
- SQL Server 2019+
- Visual Studio 2022 or VS Code

### Setup
```bash
# Clone repository
git clone https://github.com/Mostafa-SAID7/Booksy.git
cd Booksy

# Restore dependencies
dotnet restore

# Apply migrations
dotnet ef database update

# Run application
dotnet run
```

### Default URLs
- **API**: https://localhost:5001/api
- **Swagger**: https://localhost:5001/swagger

---

## 📖 Documentation

- **[Architecture](./ARCHITECTURE.md)** - System design and patterns
- **[API Reference](./API.md)** - Endpoint documentation
- **[Setup Guide](./SETUP.md)** - Installation and configuration
- **[Database](./DATABASE.md)** - Schema and migrations

---

## 🏗️ Architecture Overview

**Stack**: .NET 8 + EF Core + CQRS + MediatR

**Layers**:
- Controllers → Commands/Queries → Handlers → Repository → Database
- Behaviors: Authorization, Caching, Logging, Performance, Validation, Exception Handling

**Key Features**:
- Role-based authorization (Admin, User)
- Pagination and search filtering
- Slug-based URL routing
- Automatic validation and exception mapping
- Request/response compression

---

## 🔐 Authentication

- **JWT Bearer tokens**
- **Email confirmation required**
- **Password reset support**

Default roles: `Admin`, `User`

---

## 📝 License

MIT License - see LICENSE file
