# Centralized Security Framework

## Overview

This document consolidates all security measures into a unified, non-redundant framework. Security is handled at multiple layers with clear responsibilities and no duplication.

---

## Security Architecture

```
┌─────────────────────────────────────────────────────┐
│  Client Request                                      │
└────────────────────┬────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────┐
│  CORS Validation (CorsExtensions)                   │
│  - Restrict origins, methods, headers               │
└────────────────────┬────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────┐
│  Security Headers (SecurityHeadersMiddleware)       │
│  - HSTS, CSP, X-Frame-Options, etc.                 │
└────────────────────┬────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────┐
│  Authentication (JWT Bearer)                        │
│  - Validate token, extract claims                   │
└────────────────────┬────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────┐
│  Authorization Filter ([Authorize] attribute)       │
│  - Verify user has access                           │
└────────────────────┬────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────┐
│  CQRS Pipeline Behaviors                            │
│  ├─ ValidationBehavior - Input validation           │
│  ├─ AuthorizationBehavior - Role checks             │
│  ├─ OwnershipValidationBehavior - Resource owner    │
│  ├─ ExceptionBehavior - Error mapping               │
│  └─ LoggingBehavior - Audit trail                   │
└────────────────────┬────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────┐
│  Handler Execution                                  │
│  - Business logic with validation                   │
└────────────────────┬────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────┐
│  Response                                            │
│  - Sanitized, masked sensitive data                 │
└─────────────────────────────────────────────────────┘
```

---

## Centralized Security Components

### 1. InputSanitizer (Prevents Injection & XSS)

**Location**: `Security/InputSanitizer.cs`

**Methods**:
- `SanitizeHtml()` - Remove HTML tags
- `SanitizeEmail()` - Validate and normalize email
- `SanitizeSlug()` - Sanitize URL slugs
- `SanitizeFilename()` - Prevent path traversal
- `SanitizeForLogging()` - Prevent log injection

**Usage**:
```csharp
// In any handler or service
var cleanedInput = InputSanitizer.SanitizeHtml(userInput);
var cleanEmail = InputSanitizer.SanitizeEmail(email);
```

**Single Source of Truth**: All input sanitization flows through this one service - no duplication.

---

### 2. ValidationContextExtensions (Centralized Validation)

**Location**: `Security/ValidationContextExtensions.cs`

**Methods**: (No duplication with handlers)
- `ValidateEntityExists()` - Check entity found
- `ValidateNotEmpty()` - Check string/Guid not empty
- `ValidateLength()` - Check string length
- `ValidatePositive()` - Check number > 0
- `ValidateRange()` - Check number in range
- `ValidateDateRange()` - Check start < end
- `ValidateOwnershipOrAdmin()` - Check resource ownership

**Usage in Handlers**:
```csharp
// Before: Duplicated in every handler
if (tag == null)
    throw new NotFoundException("Tag not found");

// After: Centralized single line
var tag = await _unitOfWork.Tags.GetByIdAsync(id)
    .ValidateEntityExists("Tag", id, _logger);

// String validation
request.Name
    .ValidateNotEmpty("Name", _logger)
    .ValidateLength("Name", 2, 50, _logger);

// Ownership check
resourceOwnerId.ValidateOwnershipOrAdmin(currentUserId, isAdmin, "Order", _logger);
```

**Eliminates**: ~50+ duplicate validation checks across all handlers.

---

### 3. OwnershipValidationBehavior (Automatic Ownership Checks)

**Location**: `Security/OwnershipValidationBehavior.cs`

**How it works**:
- Automatically checks if user owns the resource
- Works before handler executes
- Eliminates manual ownership checks in every handler

**Implementation**:
```csharp
// Command must implement interface
public class UpdateOrderCommand : IRequest<Unit>, IOwnershipValidatable
{
    public Guid OrderId { get; set; }
    public string UserId { get; set; }
    
    public string GetResourceOwnerId() => UserId;
}

// Behavior automatically validates:
// - User is either resource owner OR admin
// - Throws AuthorizationException if not
```

**Eliminates**: Manual ownership checks in every handler (50+ locations).

---

### 4. ErrorBoundaryMiddleware (Prevents Information Leakage)

**Location**: `Security/ErrorBoundaryMiddleware.cs`

**Features**:
- Catches ALL unhandled exceptions
- Prevents stack traces in production
- Returns secure error responses
- Logs full details internally
- Unique TraceId for debugging

**Error Responses**:
```json
{
  "success": false,
  "message": "An unexpected error occurred",
  "traceId": "unique-id-for-debugging",
  "timestamp": 1234567890
}
```

**Register in Program.cs**:
```csharp
app.UseMiddleware<ErrorBoundaryMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
```

---

### 5. SensitiveDataMaskingExtensions (PII Protection)

**Location**: `Security/SensitiveDataMaskingExtensions.cs`

**Masked Fields**:
- Passwords, tokens, secrets
- Email addresses, phone numbers
- Credit card numbers
- SSN, addresses, dates of birth
- API keys, authorization headers

**Usage in Logging**:
```csharp
// Don't do this:
_logger.LogInformation("User data: {Data}", userData);

// Do this:
var maskedData = userData.MaskSensitiveData();
_logger.LogInformation("User data: {Data}", maskedData);

// Or use helper:
_logger.LogInformation(SensitiveDataMaskingExtensions.CreateSafeLogMessage(
    "Processing user", userData));
```

**Example Output**:
```
Before: User email: john.doe@example.com, password: SecureP@ss123
After:  User email: j**e@example.com, password: ***REDACTED***
```

---

### 6. AuthorizationService (Role & Ownership Checks)

**Location**: `Security/AuthorizationService.cs`

**Methods**:
- `IsOwnerOrAdmin()` - Verify ownership
- `GetCurrentUserId()` - Extract from claims
- `IsAdmin()` - Check admin role
- `HasRole()` - Check specific role

**Usage in Controllers**:
```csharp
[HttpPost("{id:guid}/cancel")]
[Authorize]
public async Task<IActionResult> Cancel(Guid id)
{
    var order = await _mediator.Send(new GetOrderByIdQuery { Id = id });
    var userId = _authService.GetCurrentUserId(User);
    
    // Centralized check
    if (!_authService.IsOwnerOrAdmin(order.UserId, userId, User))
    {
        return Forbid();
    }
    
    await _mediator.Send(new CancelOrderCommand { OrderId = id });
    return NoContent();
}
```

---

### 7. SecurityHeadersMiddleware (OWASP Headers)

**Location**: `Security/SecurityHeaders.cs`

**Headers Added**:
- `X-Frame-Options: DENY` - Prevent clickjacking
- `X-Content-Type-Options: nosniff` - Prevent MIME sniffing
- `Content-Security-Policy` - Restrict resource loading
- `Strict-Transport-Security` - Force HTTPS
- `Referrer-Policy` - Control referrer info
- `Permissions-Policy` - Disable unnecessary features

**Register in Program.cs**:
```csharp
app.UseSecurityHeaders();
```

---

### 8. IpRestrictionAttribute (Admin IP Whitelist)

**Location**: `Security/IpRestrictionAttribute.cs`

**Usage**:
```csharp
[Authorize(Roles = "Admin")]
[IpRestriction("192.168.1.0/24", "10.0.0.5")]
[HttpPost("/admin/promotions")]
public async Task<IActionResult> CreatePromotion([FromBody] CreatePromotionCommand command)
{
    // Only accessible from whitelisted IPs
}
```

---

## Security Layers (Unified Flow)

### Layer 1: Request Validation
```csharp
// appsettings.json
{
  "Security": {
    "MaxRequestBodySize": 10485760,  // 10 MB
    "RateLimiting": {
      "PermitLimit": 100,
      "WindowMinutes": 1
    }
  }
}
```

### Layer 2: Authentication
- JWT token validation
- Claim extraction
- Token expiration check

### Layer 3: Authorization
- [Authorize] attribute
- Role checking
- Resource ownership validation

### Layer 4: Input Validation
- FluentValidation in validators
- Sanitization via InputSanitizer
- Type validation

### Layer 5: Execution
- Business logic validation
- Error handling
- Audit logging

### Layer 6: Error Response
- Exception mapping
- Sensitive data masking
- TraceId for debugging

---

## No-Duplication Checklist

✅ **Input Validation**
- ALL validation through `ValidationContextExtensions`
- InputSanitizer for all user input
- No repeated `if (string.IsNullOrEmpty)` checks

✅ **Authorization**
- AuthorizationBehavior for role checks
- OwnershipValidationBehavior for ownership
- IAuthorizationService for utilities
- [Authorize] attributes in controllers

✅ **Error Handling**
- ErrorBoundaryMiddleware for all exceptions
- ExceptionHandlingMiddleware for app-specific
- No try-catch duplication in handlers

✅ **Logging**
- SensitiveDataMaskingExtensions for PII
- LoggingBehavior for request/response
- Single audit approach

✅ **Security Headers**
- SecurityHeadersMiddleware once in pipeline
- CORS configured once
- No duplicate security checks

---

## DI Registration

**File**: `Extensions/ServiceCollectionExtensions.cs`

```csharp
// Add all security services
services.AddScoped<IAuthorizationService, AuthorizationService>();
services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

// Register behaviors
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
    cfg.AddOpenBehavior(typeof(OwnershipValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(ExceptionBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
```

**File**: `Program.cs`

```csharp
// Add middleware in correct order
app.UseSecurityHeaders();
app.UseMiddleware<ErrorBoundaryMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
```

---

## Security Testing

### Test Ownership Validation
```bash
# Without token - should fail with 401
curl http://localhost:5001/api/orders/123/cancel

# As different user - should fail with 403
curl -H "Authorization: Bearer user2_token" \
  http://localhost:5001/api/orders/user1_order/cancel

# As correct user - should succeed
curl -H "Authorization: Bearer user1_token" \
  http://localhost:5001/api/orders/user1_order/cancel
```

### Test Input Sanitization
```bash
# XSS attempt - should be sanitized
curl -X POST http://localhost:5001/api/books \
  -d '{"title":"<script>alert(1)</script>","price":19.99}'
# Response should have sanitized title

# SQL injection - prevented by EF Core parameterization
curl http://localhost:5001/api/books?search="'; DROP TABLE Books;--"
# Query is parameterized, injection prevented
```

### Test Error Responses (Production Safety)
```bash
# Endpoint that throws exception
curl http://localhost:5001/api/something/broken

# Production response (no stack trace)
{
  "success": false,
  "message": "An unexpected error occurred",
  "traceId": "0HMGQ..."
}

# Development response (detailed)
{
  "success": false,
  "message": "An error occurred",
  "traceId": "0HMGQ...",
  "details": [
    {"field": "Exception", "message": "NullReferenceException"},
    {"field": "Message", "message": "Object reference not set"},
    {"field": "StackTrace", "message": "at..."}
  ]
}
```

---

## Summary

This centralized security framework provides:

1. **No Duplication** - Single implementation for each concern
2. **Consistent Application** - All checks enforced uniformly
3. **Easy Maintenance** - Changes in one place affect all usages
4. **Clear Layers** - Well-defined security boundaries
5. **Production Ready** - Information leakage prevention
6. **Audit Trail** - Complete logging with PII masking
7. **Easy Testing** - Testable, isolated components

**Result**: A secure, maintainable, non-redundant security infrastructure.
