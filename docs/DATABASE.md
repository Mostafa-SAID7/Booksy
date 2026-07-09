# Database

**Engine**: SQL Server 2019+  
**ORM**: Entity Framework Core 9  
**Approach**: Code-first migrations

---

## Core Entities & Relationships

```
Author (1) ──── (M) Book
Category (1) ──── (M) Book
Book (1) ──── (M) Review
Book (1) ──── (M) BookTag
Tag (1) ──── (M) BookTag
Order (1) ──── (M) OrderItem
ApplicationUser (1) ──── (M) Cart/Order/Review
```

---

## Key Tables

### Author
- `Id` (PK), `Name` (UNIQUE), `Slug` (UNIQUE), `Bio`, `CreatedAt`, `UpdatedAt`

### Book
- `Id` (PK), `Title`, `Slug` (UNIQUE), `Description`, `Price`, `Stock`
- `AuthorId` (FK), `CategoryId` (FK), `Rating`, `CreatedAt`, `UpdatedAt`

### Category
- `Id` (PK), `Name` (UNIQUE), `Slug` (UNIQUE), `Description`, `CreatedAt`, `UpdatedAt`

### Order
- `Id` (PK), `UserId` (FK), `Status`, `TotalAmount`, `ShippingAddress`, `CreatedAt`, `UpdatedAt`

### Review
- `Id` (PK), `BookId` (FK), `UserId` (FK), `Rating` (1-5), `Comment`, `CreatedAt`, `UpdatedAt`

### Cart & CartItem
- Cart: `Id` (PK), `UserId` (UNIQUE FK), `CreatedAt`, `UpdatedAt`
- CartItem: `Id` (PK), `CartId` (FK), `BookId` (FK), `Quantity`, `CreatedAt`

---

## Indexes

Performance-critical:
```sql
CREATE UNIQUE INDEX IX_Books_Slug ON Books(Slug);
CREATE UNIQUE INDEX IX_Authors_Slug ON Authors(Slug);
CREATE INDEX IX_Books_AuthorId ON Books(AuthorId);
CREATE INDEX IX_Orders_UserId ON Orders(UserId);
CREATE INDEX IX_Reviews_BookId ON Reviews(BookId);
```

---

## Migrations

```bash
dotnet ef migrations list              # View migrations
dotnet ef migrations add MigrationName # Create new
dotnet ef database update              # Apply all pending
dotnet ef database update MigrationName # Apply specific
dotnet ef migrations script --output migration.sql  # SQL script
```

---

## Backup & Restore

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

---

## Seeding

Default data seeded on first run:
- Admin user: `admin@booksy.local` / `Admin@123456`
- 5 authors, 5 categories, 20 books, 50 reviews

Disable in `Program.cs` if needed.

---

## Connection Strings

**Development**
```
Server=(local)\SQLEXPRESS;Database=Booksy;Trusted_Connection=true;Encrypt=true;TrustServerCertificate=true
```

**Production**
```
Server=prod-server;Database=Booksy;User ID=sa;Password=***;Encrypt=true;TrustServerCertificate=false
```

---

## Common Queries

### Get Book with Relations
```csharp
var book = await _context.Books
    .Include(b => b.Author)
    .Include(b => b.Category)
    .Include(b => b.Reviews)
    .FirstOrDefaultAsync(b => b.Id == id);
```

### Get User Orders
```csharp
var orders = await _context.Orders
    .Where(o => o.UserId == userId)
    .Include(o => o.Items)
    .ThenInclude(oi => oi.Book)
    .OrderByDescending(o => o.CreatedAt)
    .ToListAsync();
```

### Low Stock Items
```csharp
var lowStockCount = await _context.Books
    .CountAsync(b => b.Stock < 10);
```

---

## Performance Tuning

- Use `.Include()` to prevent N+1 queries
- Filter at database level, not in-memory
- Paginate large result sets
- Create indexes on frequently searched columns
- Configure command timeout: `sqlOptions.CommandTimeout(30)`
