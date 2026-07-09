# Database

## Overview

- **Engine**: SQL Server 2019+
- **ORM**: Entity Framework Core 8
- **Approach**: Code-First Migrations

## Entity Relationships

```
Author (1) ──── (M) Book
Category (1) ──── (M) Book
Book (1) ──── (M) Review
Book (1) ──── (M) BookTag
Tag (1) ──── (M) BookTag
Book (1) ──── (M) CartItem
Book (1) ──── (M) OrderItem
Order (1) ──── (M) OrderItem
ApplicationUser (1) ──── (M) Cart
ApplicationUser (1) ──── (M) Order
ApplicationUser (1) ──── (M) Review
Promotion (1) ──── (M) Order
```

## Core Tables

### Author
```sql
CREATE TABLE Authors (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  Name NVARCHAR(200) NOT NULL UNIQUE,
  Slug NVARCHAR(250) NOT NULL UNIQUE,
  Bio NVARCHAR(MAX),
  CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
  UpdatedAt DATETIME2
);
```

### Book
```sql
CREATE TABLE Books (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  Title NVARCHAR(300) NOT NULL,
  Slug NVARCHAR(350) NOT NULL UNIQUE,
  Description NVARCHAR(MAX),
  Price DECIMAL(10,2) NOT NULL,
  Stock INT NOT NULL DEFAULT 0,
  AuthorId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY,
  CategoryId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY,
  Rating DECIMAL(3,2),
  CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
  UpdatedAt DATETIME2
);
```

### Category
```sql
CREATE TABLE Categories (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  Name NVARCHAR(200) NOT NULL UNIQUE,
  Slug NVARCHAR(250) NOT NULL UNIQUE,
  Description NVARCHAR(MAX),
  CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
  UpdatedAt DATETIME2
);
```

### Order
```sql
CREATE TABLE Orders (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  UserId NVARCHAR(450) NOT NULL FOREIGN KEY,
  Status NVARCHAR(50) DEFAULT 'Pending',
  TotalAmount DECIMAL(10,2) NOT NULL,
  ShippingAddress NVARCHAR(MAX),
  CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
  UpdatedAt DATETIME2
);
```

### Review
```sql
CREATE TABLE Reviews (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  BookId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY,
  UserId NVARCHAR(450) NOT NULL FOREIGN KEY,
  Rating INT NOT NULL CHECK (Rating >= 1 AND Rating <= 5),
  Comment NVARCHAR(MAX),
  CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
  UpdatedAt DATETIME2
);
```

### Cart
```sql
CREATE TABLE Carts (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  UserId NVARCHAR(450) NOT NULL UNIQUE FOREIGN KEY,
  CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
  UpdatedAt DATETIME2
);

CREATE TABLE CartItems (
  Id UNIQUEIDENTIFIER PRIMARY KEY,
  CartId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY,
  BookId UNIQUEIDENTIFIER NOT NULL FOREIGN KEY,
  Quantity INT NOT NULL DEFAULT 1,
  CreatedAt DATETIME2 DEFAULT GETUTCDATE()
);
```

## Indexes

Performance-critical indexes:
```sql
-- Slug lookups
CREATE UNIQUE INDEX IX_Books_Slug ON Books(Slug);
CREATE UNIQUE INDEX IX_Authors_Slug ON Authors(Slug);
CREATE UNIQUE INDEX IX_Categories_Slug ON Categories(Slug);

-- Foreign keys
CREATE INDEX IX_Books_AuthorId ON Books(AuthorId);
CREATE INDEX IX_Books_CategoryId ON Books(CategoryId);
CREATE INDEX IX_Orders_UserId ON Orders(UserId);
CREATE INDEX IX_Reviews_BookId ON Reviews(BookId);
CREATE INDEX IX_Reviews_UserId ON Reviews(UserId);
CREATE INDEX IX_CartItems_CartId ON CartItems(CartId);

-- Search
CREATE INDEX IX_Books_Title ON Books(Title);
CREATE INDEX IX_Authors_Name ON Authors(Name);
```

## Migrations

### View Migrations
```bash
dotnet ef migrations list
```

### Add New Migration
```bash
dotnet ef migrations add MigrationName
```

### Apply Migrations
```bash
# Apply all pending
dotnet ef database update

# Apply to specific migration
dotnet ef database update MigrationName

# Revert last migration
dotnet ef database update LastGoodMigration
```

### Generate SQL Script
```bash
dotnet ef migrations script --output migration.sql
```

## Seeding

Default data seeded on first run in `DataAccess/Seeds/`:

- **Admin User**: admin@booksy.local / Admin@123456
- **Sample Authors**: 5 authors
- **Sample Categories**: 5 categories
- **Sample Books**: 20 books with relations
- **Sample Reviews**: 50 reviews

Disable seeding in `Program.cs` if needed.

## Backup & Restore

### SQL Server Backup
```sql
-- Backup
BACKUP DATABASE Booksy 
TO DISK = 'C:\Backups\Booksy.bak'
WITH INIT, COMPRESSION;

-- Restore
RESTORE DATABASE Booksy 
FROM DISK = 'C:\Backups\Booksy.bak'
WITH REPLACE;
```

## Connection Strings

### Local Development
```
Server=(local)\SQLEXPRESS;Database=Booksy;Trusted_Connection=true;Encrypt=true;TrustServerCertificate=true
```

### Production
```
Server=prod-server;Database=Booksy;User ID=sa;Password=***;Encrypt=true;TrustServerCertificate=false
```

## Performance Tuning

### Query Optimization
- Use `.Include()` to prevent N+1 queries
- Filter at database level, not in-memory
- Paginate large result sets
- Create indexes on frequently searched columns

### Configuration
```csharp
// in Program.cs
options.UseSqlServer(connectionString, sqlOptions =>
{
    sqlOptions.CommandTimeout(30);
    sqlOptions.UseRelationalNulls(true);
});
```

## Common Queries

### Get Book with Related Data
```csharp
var book = await _context.Books
    .Include(b => b.Author)
    .Include(b => b.Category)
    .Include(b => b.Reviews)
    .Include(b => b.BookTags)
    .FirstOrDefaultAsync(b => b.Id == id);
```

### Get User's Orders
```csharp
var orders = await _context.Orders
    .Where(o => o.UserId == userId)
    .Include(o => o.Items)
    .ThenInclude(oi => oi.Book)
    .OrderByDescending(o => o.CreatedAt)
    .ToListAsync();
```

### Count Low Stock Items
```csharp
var lowStockCount = await _context.Books
    .CountAsync(b => b.Stock < 10);
```
