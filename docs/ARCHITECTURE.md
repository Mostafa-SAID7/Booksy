# Architecture

## System Design

### Layered Architecture
```
API Layer (Controllers)
    ↓
CQRS Layer (Commands & Queries)
    ↓
Handler Layer (Business Logic)
    ↓
Repository Layer (Data Access)
    ↓
Database Layer (SQL Server)
```

## CQRS Pattern

Commands handle state-changing operations (Create, Update, Delete).
Queries handle read operations (Get, Filter, Search).

Each command/query has:
- **Validator** - Input validation
- **Handler** - Business logic implementation
- **Mapping** - DTO conversions

## Request Pipeline

1. **Controller** receives request
2. **MediatR** routes to handler
3. **Behaviors execute in order**:
   - Validation
   - Authorization
   - Logging
   - Performance monitoring
   - Caching (read-only)
   - Exception handling
   - Transaction management
4. **Handler executes** business logic
5. **Response** returned through behaviors

## Behaviors

| Behavior | Purpose |
|----------|---------|
| ValidationBehavior | Validates command/query input |
| AuthorizationBehavior | Checks role-based access |
| LoggingBehavior | Logs requests/responses |
| PerformanceBehavior | Tracks execution time |
| CachingBehavior | Caches query results |
| ExceptionBehavior | Maps exceptions to HTTP responses |
| TransactionBehavior | Manages database transactions |

## Services

### IQueryService
Aggregation methods replacing GetAllAsync():
- `GetAsync(predicate)` - Filter with conditions
- `GetOneAsync(predicate)` - Single entity lookup
- `GetByIdAsync(id)` - By primary key
- `CountAsync(predicate)` - Record count
- `AnyAsync(predicate)` - Existence check

### IValidationService
Reusable validation methods:
- `ValidateNotEmpty(value, fieldName)`
- `ValidateRange(value, min, max)`
- `ValidateLength(value, min, max)`
- `ValidateEmail(email)`
- `ValidateUrl(url)`

### ISlugService
Slug generation and management:
- `GenerateUniqueSlugAsync(unitOfWork, input, entityType, excludeId)`
- Entity-aware uniqueness checking
- SEO-friendly URL support

## Authorization

All write endpoints require authentication:
- **Admin endpoints**: `[Authorize(Roles = "Admin")]`
- **User endpoints**: `[Authorize]`
- **Public endpoints**: No attribute

Response codes:
- `401` - Unauthorized (missing/invalid token)
- `403` - Forbidden (insufficient permissions)

## Error Handling

Centralized exception mapping in `ExceptionMappingExtensions`:
- `NotFoundException` → 404
- `ValidationException` → 400 (with error details)
- `ConflictException` → 409
- `BusinessException` → 400

All exceptions logged with context.

## Database

**ORM**: Entity Framework Core  
**Database**: SQL Server  
**Migrations**: Code-first approach

**Key Entities**:
- Book, Author, Category, Tag, Review
- Cart, Order, OrderItem
- Promotion, Inventory

Configured relationships and constraints in `DataAccess/Configurations`.
