# Architecture

---

## System Design

```
Controllers (API Layer)
  ↓
MediatR (CQRS Routing)
  ↓
Behaviors (Validation → Authorization → Logging → Monitoring)
  ↓
Handlers (Business Logic)
  ↓
Repository (Data Access)
  ↓
Database (SQL Server)
```

---

## CQRS Pattern

- **Commands**: State-changing operations (Create, Update, Delete)
- **Queries**: Read operations (Get, Filter, Search)

Each has: Validator → Handler → DTO Mapping

---

## Behaviors Pipeline

| Behavior | Purpose |
|----------|---------|
| ValidationBehavior | Input validation |
| AuthorizationBehavior | Role-based access |
| LoggingBehavior | Request/response logging |
| PerformanceBehavior | Execution time tracking |
| CachingBehavior | Query result caching |
| ExceptionBehavior | Exception-to-HTTP mapping |
| TransactionBehavior | Database transactions |
| MonitoringBehavior | Security & performance events |

---

## Services

### IQueryService
```csharp
GetAsync(predicate)          // Filter with conditions
GetOneAsync(predicate)       // Single entity lookup
GetByIdAsync(id)             // By primary key
CountAsync(predicate)        // Record count
AnyAsync(predicate)          // Existence check
```

### IValidationService
```csharp
ValidateNotEmpty(value, fieldName)
ValidateRange(value, min, max)
ValidateLength(value, min, max)
ValidateEmail(email)
ValidateUrl(url)
```

### ISlugService
```csharp
GenerateUniqueSlugAsync(unitOfWork, input, entityType, excludeId)
// Entity-aware uniqueness checking for SEO-friendly URLs
```

---

## Authorization

| Requirement | Pattern |
|-------------|---------|
| Admin endpoints | `[Authorize(Roles = "Admin")]` |
| Authenticated users | `[Authorize]` |
| Public endpoints | No attribute |
| Ownership validation | Checked in handler, returns 403 |

---

## Error Handling

Centralized exception mapping:
- `NotFoundException` → 404
- `ValidationException` → 400 (with details)
- `ConflictException` → 409
- `BusinessException` → 400
- All logged with context

---

## Database (EF Core)

**Engine**: SQL Server (2019+)  
**Approach**: Code-first migrations  
**Relationships**: Author↔Book, Category↔Book, User↔Order/Review/Cart

See [docs/DATABASE.md](DATABASE.md) for schema details.
