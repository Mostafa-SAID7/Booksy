# Setup Guide

## Prerequisites

- **Runtime**: .NET 8 SDK
- **Database**: SQL Server 2019+ (or SQL Server Express)
- **IDE**: Visual Studio 2022 / VS Code
- **Tools**: Git, NuGet

## Installation

### 1. Clone Repository
```bash
git clone https://github.com/Mostafa-SAID7/Booksy.git
cd Booksy/Booksy
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Configure Database

Edit `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=Booksy;Trusted_Connection=true;Encrypt=true;TrustServerCertificate=true"
  }
}
```

For SQL Server Express:
```
Server=(local)\SQLEXPRESS;Database=Booksy;...
```

### 4. Apply Migrations
```bash
dotnet ef database update
```

Creates database schema and applies pending migrations.

### 5. Run Application
```bash
dotnet run
```

Access at: `https://localhost:5001`

---

## Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "..."
  },
  "Jwt": {
    "SecretKey": "your-secret-key-min-32-chars",
    "Issuer": "Booksy",
    "Audience": "BooksyAPI",
    "ExpiryMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning"
    }
  }
}
```

### User Seeding

Default admin user created on first run:
- **Email**: admin@booksy.local
- **Password**: Admin@123456

Change in `DataAccess/Seeds/ApplicationUserSeed.cs`

---

## Troubleshooting

### Database Connection Failed
- Verify SQL Server is running
- Check connection string syntax
- Ensure database user has CREATE permissions

### Migration Errors
```bash
# Remove last migration
dotnet ef migrations remove

# Create fresh migration
dotnet ef migrations add MigrationName

# Apply again
dotnet ef database update
```

### Port Already in Use
```bash
# Use different port
dotnet run --urls="https://localhost:5002"
```

### Build Errors
```bash
# Clean solution
dotnet clean

# Full rebuild
dotnet build
```

---

## Development

### Project Structure
```
Booksy/
├── Features/          # Feature modules (Books, Authors, etc.)
├── Common/            # Shared services, models, utilities
├── Core/              # CQRS interfaces, behaviors
├── DataAccess/        # EF Core configuration
├── Extensions/        # DI and middleware setup
├── Infrastructure/    # External services
├── Models/            # Entity definitions
└── Repositories/      # Data access layer
```

### Adding New Feature

1. Create folder under `Features/YourFeature`
2. Add Controllers, Commands, Queries, DTOs
3. Create validators in `Validators/`
4. Add mapping profile in `Mappings/`
5. Register in DI container

### Running Tests
```bash
dotnet test
```

Requires xUnit/NUnit setup (future enhancement).

---

## Deployment

### Production Build
```bash
dotnet publish -c Release
```

### Environment Variables
```bash
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=production-string
Jwt__SecretKey=production-secret
```

### Database
- Always backup before migration
- Test migrations on staging first
- Keep connection string secure
