# Security Audit & Framework

## Executive Summary

The Booksy application has a **solid foundation** with:
- ✅ JWT-based authentication
- ✅ Role-based authorization (Admin/User)
- ✅ Centralized exception handling
- ✅ Input validation via FluentValidation
- ✅ SQL injection protection (EF Core parameterized queries)
- ✅ HTTPS enforcement

However, **critical vulnerabilities and gaps** require immediate attention.

---

## 🔴 Critical Issues

### 1. Insecure JWT Configuration
**Severity**: CRITICAL  
**File**: `JwtAuthExtension.cs`  
**Issue**: 
```csharp
o.RequireHttpsMetadata = false;  // ❌ Allows HTTP in production!
```

**Impact**: Tokens can be transmitted over unencrypted HTTP, exposing them to man-in-the-middle attacks.

**Fix**:
```csharp
o.RequireHttpsMetadata = true;  // Force HTTPS
o.Audience = jwtSettings.Audience;
o.SaveToken = true;
```

---

### 2. Exposed Secrets in Configuration
**Severity**: CRITICAL  
**File**: `appsettings.json`  
**Issue**:
```json
{
  "JWT": {
    "SecretKey": "SuperSecretKey_ChangeThisToAtLeast32Chars!"  // ❌ Hardcoded!
  },
  "EmailSettings": {
    "Password": "smtp-password"  // ❌ Exposed in file!
  },
  "Stripe": {
    "SecretKey": "#"  // ❌ Placeholder but still in repo
  }
}
```

**Impact**: Secrets are committed to repository, visible in version history, and hardcoded for everyone.

**Fix**: Use secrets management:
```csharp
// In Program.cs
if (builder.Environment.IsProduction())
{
    // Use Azure Key Vault, AWS Secrets Manager, or HashiCorp Vault
    builder.Configuration.AddAzureKeyVault(
        new Uri("https://your-vault.vault.azure.net/"),
        new DefaultAzureCredential()
    );
}
else
{
    // Development: Use User Secrets
    builder.Configuration.AddUserSecrets<Program>();
}
```

Immediately:
```bash
dotnet user-secrets init
dotnet user-secrets set "JWT:SecretKey" "your-min-32-char-secret"
dotnet user-secrets set "Stripe:SecretKey" "sk_..."
```

---

### 3. Weak Password Policy
**Severity**: HIGH  
**File**: `ServiceCollectionExtensions.cs`  
**Issue**:
```csharp
option.Password.RequiredLength = 6;  // ❌ Too weak
option.Password.RequireNonAlphanumeric = false;  // ❌ No special chars required
```

**Impact**: Users can set weak passwords like "123456" or "password".

**Fix**:
```csharp
services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    // Password requirements
    option.Password.RequiredLength = 12;
    option.Password.RequireDigit = true;
    option.Password.RequireLowercase = true;
    option.Password.RequireUppercase = true;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequiredUniqueChars = 4;
    
    // Lockout policy
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    option.Lockout.MaxFailedAccessAttempts = 5;
    option.Lockout.AllowedForNewUsers = true;
    
    // User requirements
    option.User.RequireUniqueEmail = true;
})
```

---

### 4. Insufficient Authorization Checks
**Severity**: HIGH  
**Issue**: Missing user ownership validation in Update/Delete operations.

**Example - Orders**:
```csharp
// ❌ Dangerous: Admin can see/modify ANY order
[HttpPut("{id:guid}/status")]
[Authorize(Roles = "Admin")]
public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateOrderStatusCommand command)
{
    // No check that order belongs to current user!
}
```

**Fix**: Add ownership validation:
```csharp
[HttpPost("{id:guid}/cancel")]
[Authorize]
public async Task<IActionResult> Cancel(Guid id)
{
    var order = await _mediator.Send(new GetOrderByIdQuery { Id = id });
    
    // Validate ownership
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (order.UserId != userId && !User.IsInRole("Admin"))
    {
        return Forbid();  // 403 Forbidden
    }
    
    await _mediator.Send(new CancelOrderCommand { OrderId = id });
    return NoContent();
}
```

---

### 5. Inadequate CORS Configuration
**Severity**: HIGH  
**File**: `CorsExtensions.cs`  
**Issue**:
```csharp
policy.WithOrigins("http://localhost:5500")
      .AllowAnyMethod()        // ❌ Allows DELETE, PATCH, etc.
      .AllowAnyHeader()        // ❌ No header validation
      .AllowCredentials();      // ⚠️ With AllowAny = XSS risk
```

**Impact**: Any origin can make any request, CSRF attacks possible.

**Fix**:
```csharp
public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration config)
{
    var allowedOrigins = config.GetSection("Cors:AllowedOrigins")
        .Get<string[]>() ?? new[] { };

    services.AddCors(options =>
    {
        options.AddPolicy(PolicyName, policy =>
        {
            policy
                .WithOrigins(allowedOrigins)
                .WithMethods("GET", "POST", "PUT", "DELETE")  // Explicit methods
                .WithHeaders("Content-Type", "Authorization")  // Explicit headers
                .WithExposedHeaders("X-Total-Count")           // Only needed headers
                .WithMaxAge(3600);                              // Cache preflight
        });
    });
    
    return services;
}
```

**appsettings.json**:
```json
{
  "Cors": {
    "AllowedOrigins": ["https://yourdomain.com", "https://www.yourdomain.com"]
  }
}
```

---

### 6. Missing Rate Limiting
**Severity**: HIGH  
**Issue**: No protection against brute force, DoS attacks.

**Fix**: Add rate limiting:
```csharp
// Program.cs
builder.Services.AddMemoryCache();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.User.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            }));
});

app.UseRateLimiter();
```

---

### 7. No HTTPS Enforcement for JWT
**Severity**: HIGH  
**Issue**: `RequireHttpsMetadata = false` allows HTTP token transmission.

**Fix**:
```csharp
// Only in Program.cs
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();  // HTTP Strict Transport Security
    app.UseHttpsRedirection();  // Force HTTPS
}
```

---

## 🟡 High Priority Issues

### 8. Insufficient Audit Logging
**Issue**: No tracking of who modified what and when.

**Fix**: Add audit logging to base handler:
```csharp
public abstract class BaseAuditHandler<TRequest, TResponse> 
    : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected readonly IAuditService _auditService;
    
    protected async Task LogAuditAsync(
        string action, 
        string entityType, 
        Guid entityId, 
        string userId,
        object? changes = null)
    {
        await _auditService.LogAsync(new AuditLog
        {
            Action = action,
            EntityType = entityType,
            EntityId = entityId,
            UserId = userId,
            Changes = JsonSerializer.Serialize(changes),
            Timestamp = DateTime.UtcNow,
            IpAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString()
        });
    }
}
```

---

### 9. Missing CSRF Protection
**Issue**: No CSRF tokens for form submissions.

**Fix**:
```csharp
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
    options.FormFieldName = "__RequestVerificationToken";
});

// In controller
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([FromBody] CreateBookCommand command)
{
    // Handle request
}
```

---

### 10. No Request Validation Limits
**Issue**: Unbounded request sizes can cause memory exhaustion.

**Fix**:
```csharp
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 10 * 1024 * 1024;  // 10 MB
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 10 * 1024 * 1024;
});
```

---

### 11. Inadequate Input Validation
**Issue**: Some endpoints lack comprehensive validation.

**Fix**: Ensure all DTOs have validators:
```csharp
public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title required")
            .Length(1, 300).WithMessage("Title must be 1-300 chars")
            .Must(x => !ContainsInvalidChars(x))
            .WithMessage("Title contains invalid characters");
            
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be > 0")
            .LessThanOrEqualTo(9999.99).WithMessage("Price too high");
    }
    
    private bool ContainsInvalidChars(string value)
        => value.Any(c => !char.IsLetterOrDigit(c) && c != ' ' && c != '-');
}
```

---

### 12. Missing Security Headers
**Issue**: No security headers like CSP, X-Frame-Options, etc.

**Fix**:
```csharp
app.Use(async (context, next) =>
{
    // Prevent clickjacking
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    
    // Prevent MIME type sniffing
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    
    // Enable XSS protection
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    
    // Content Security Policy
    context.Response.Headers.Add("Content-Security-Policy", 
        "default-src 'self'; script-src 'self'; style-src 'self' 'unsafe-inline'");
    
    // Referrer Policy
    context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
    
    // Permissions Policy
    context.Response.Headers.Add("Permissions-Policy", "geolocation=(), microphone=(), camera=()");
    
    await next();
});
```

---

### 13. No SQL Injection Prevention
**Current Status**: ✅ Good - EF Core handles parameterization.

However, ensure **never** using:
- ❌ `FromSqlRaw("SELECT * FROM Books WHERE Id = " + id)`
- ✅ `FromSqlInterpolated($"SELECT * FROM Books WHERE Id = {id}")`

---

### 14. Sensitive Data in Logs
**Issue**: Passwords, tokens, PII may be logged.

**Fix**:
```csharp
// appsettings.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Warning"
    }
  }
}

// In handler
_logger.LogInformation("User login attempt for email: ****{EmailSuffix}",
    email.Substring(Math.Max(0, email.Length - 4)));
```

---

### 15. No IP Whitelisting for Admin
**Issue**: Admin endpoints accessible from anywhere.

**Fix**:
```csharp
[Authorize(Roles = "Admin")]
[IpRestriction("192.168.1.0/24", "10.0.0.0/8")]
[HttpPost("/admin/promotions")]
public async Task<IActionResult> CreatePromotion([FromBody] CreatePromotionCommand command)
{
    // Only accessible from whitelisted IPs
}
```

---

## ✅ Implemented Well

1. **Exception Mapping** - Custom exceptions with proper HTTP status codes
2. **Request Validation** - FluentValidation on all commands/queries
3. **EF Core Parameterization** - SQL injection protection
4. **Token Validation** - JWT validation on protected routes
5. **Role-Based Access** - [Authorize] attributes on endpoints
6. **Centralized Logging** - ILogger<T> in handlers

---

## 📋 Implementation Priority

| Priority | Issue | Effort | Impact |
|----------|-------|--------|--------|
| 🔴 P1 | Secrets management | 2 hrs | Critical |
| 🔴 P1 | JWT RequireHttpsMetadata | 15 min | Critical |
| 🟡 P2 | Password policy | 30 min | High |
| 🟡 P2 | CORS configuration | 1 hr | High |
| 🟡 P2 | Authorization checks | 3 hrs | High |
| 🟡 P2 | Rate limiting | 2 hrs | High |
| 🟡 P2 | Security headers | 1 hr | High |
| 🟡 P3 | Audit logging | 4 hrs | Medium |
| 🟡 P3 | Request size limits | 30 min | Medium |

---

## 🚀 Next Steps

1. **Immediate** (This week):
   - Move secrets to Azure Key Vault / AWS Secrets Manager
   - Enable `RequireHttpsMetadata = true`
   - Strengthen password policy
   - Fix CORS configuration

2. **Short-term** (Next sprint):
   - Add rate limiting
   - Implement security headers middleware
   - Add ownership validation to all endpoints
   - Implement request size limits

3. **Medium-term** (Sprint after):
   - Add audit logging
   - Implement IP whitelisting for admin
   - Set up CSRF protection
   - Add comprehensive input validation

---

## 📚 References

- [OWASP Top 10](https://owasp.org/www-project-top-ten/)
- [Microsoft Security Best Practices](https://docs.microsoft.com/en-us/dotnet/standard/security/)
- [JWT Best Practices](https://tools.ietf.org/html/rfc8725)
- [CORS Security](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS)
