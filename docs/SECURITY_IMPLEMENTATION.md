# Security Implementation Guide

## Quick Start - Apply Critical Fixes

### Step 1: Fix JWT Configuration (15 minutes)

**File**: `Extensions/JwtAuthExtension.cs`

Replace:
```csharp
o.RequireHttpsMetadata = false;
```

With:
```csharp
// Only allow HTTPS for token transmission (safe in development with self-signed certs)
o.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
```

### Step 2: Migrate to User Secrets (15 minutes)

```bash
# Initialize secrets storage
dotnet user-secrets init

# Add secrets
dotnet user-secrets set "JWT:SecretKey" "your-production-key-here-minimum-32-chars"
dotnet user-secrets set "Stripe:SecretKey" "sk_live_..."
dotnet user-secrets set "EmailSettings:Password" "your-smtp-password"
```

**Never** commit secrets to repository again.

### Step 3: Strengthen Password Policy (30 minutes)

**File**: `Extensions/ServiceCollectionExtensions.cs`

Replace:
```csharp
services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.Password.RequiredLength = 6;
    option.Password.RequireNonAlphanumeric = false;
    option.User.RequireUniqueEmail = true;
})
```

With:
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
    
    // Lockout policy - prevent brute force
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    option.Lockout.MaxFailedAccessAttempts = 5;
    option.Lockout.AllowedForNewUsers = true;
    
    // User requirements
    option.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
```

### Step 4: Fix CORS Configuration (1 hour)

**File**: `Extensions/CorsExtensions.cs`

Replace entire file with:
```csharp
namespace Booksy.Extensions
{
    public static class CorsExtensions
    {
        private const string PolicyName = "BooksyPolicy";

        public static IServiceCollection AddCustomCors(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            var allowedOrigins = configuration
                .GetSection("Cors:AllowedOrigins")
                .Get<string[]>() ?? new[] { "https://localhost:5001" };

            services.AddCors(options =>
            {
                options.AddPolicy(PolicyName, policy =>
                {
                    policy
                        .WithOrigins(allowedOrigins)
                        .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS")
                        .WithHeaders("Content-Type", "Authorization", "X-Requested-With")
                        .WithExposedHeaders("X-Total-Count", "X-Total-Pages")
                        .WithMaxAge(3600)
                        .AllowCredentials();  // Safe when using specific origins
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
        {
            app.UseCors(PolicyName);
            return app;
        }
    }
}
```

### Step 5: Add Security Headers (1 hour)

**File**: `Program.cs`

Add after middleware setup:
```csharp
// Add after services configuration
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();

// In middleware pipeline (after UseAuthentication)
app.UseSecurityHeaders();  // Add this line
app.UseAuthentication();
app.UseAuthorization();
```

### Step 6: Add Rate Limiting (2 hours)

**File**: `Program.cs`

Add to services:
```csharp
builder.Services.AddMemoryCache();
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.User.Identity?.Name 
                ?? context.Connection.RemoteIpAddress?.ToString() 
                ?? "anonymous",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            }));

    // Add endpoint-specific limits
    options.AddPolicy("auth-limit", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1)
            }));

    options.AddPolicy("api-limit", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.User.Identity?.Name ?? "anonymous",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 1000,
                Window = TimeSpan.FromMinutes(1)
            }));
});

// In app middleware
app.UseRateLimiter();
```

### Step 7: Add Ownership Validation (3 hours)

**Example**: Update `Features/Orders/OrdersController.cs`

```csharp
[HttpPost("{id:guid}/cancel")]
[Authorize]
public async Task<IActionResult> Cancel(Guid id)
{
    try
    {
        // Get the order
        var order = await _mediator.Send(new GetOrderByIdQuery { Id = id });
        
        // Verify ownership
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (order.UserId != userId && !User.IsInRole("Admin"))
        {
            return Forbid();  // 403 Forbidden
        }

        await _mediator.Send(new CancelOrderCommand { OrderId = id });
        return NoContent();
    }
    catch (Core.Exceptions.NotFoundException ex)
    {
        return NotFound(Result.Fail(ex.Message));
    }
    catch (Core.Exceptions.BusinessException ex)
    {
        return BadRequest(Result.Fail(ex.Message));
    }
}
```

### Step 8: Request Size Limits (30 minutes)

**File**: `Program.cs`

Add to service configuration:
```csharp
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 10 * 1024 * 1024;  // 10 MB
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 10 * 1024 * 1024;  // 10 MB
});
```

### Step 9: CSRF Protection (1 hour)

**File**: `Program.cs`

Add to services:
```csharp
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
    options.FormFieldName = "__RequestVerificationToken";
    options.SuppressXFrameOptionsHeader = false;
});
```

Apply to write endpoints:
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([FromBody] CreateBookCommand command)
{
    // ...
}
```

### Step 10: Audit Logging (4 hours)

Create `Security/AuditLog.cs`:
```csharp
public class AuditLog
{
    public Guid Id { get; set; }
    public string Action { get; set; }  // Create, Update, Delete
    public string EntityType { get; set; }  // Book, Order, etc.
    public Guid EntityId { get; set; }
    public string UserId { get; set; }
    public string? Changes { get; set; }  // JSON diff
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? IpAddress { get; set; }
}
```

Create `Security/AuditService.cs`:
```csharp
public class AuditService : IAuditService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AuditService> _logger;

    public async Task LogAsync(AuditLog auditLog)
    {
        _context.AuditLogs.Add(auditLog);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Audit: {Action} on {EntityType} {EntityId} by {UserId}", 
            auditLog.Action, auditLog.EntityType, auditLog.EntityId, auditLog.UserId);
    }
}
```

---

## Security Checklist

- [ ] JWT `RequireHttpsMetadata` set correctly
- [ ] Secrets moved to User Secrets (dev) or Key Vault (prod)
- [ ] Password policy strengthened (12+ chars, mixed case, special chars)
- [ ] CORS configured with specific origins only
- [ ] Security headers middleware added
- [ ] Rate limiting implemented
- [ ] Ownership validation added to all user-scoped endpoints
- [ ] Request size limits enforced
- [ ] CSRF protection enabled
- [ ] Audit logging configured
- [ ] SQL injection prevention verified (no raw SQL)
- [ ] Sensitive data not logged (passwords, tokens, PII)
- [ ] HTTPS enforced in production
- [ ] Admin endpoints IP-restricted (optional)

---

## Environment Variables (Production)

Set these in your deployment environment:

```bash
# Required
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=https://+:443;http://+:80

# Database
ConnectionStrings__DefaultConnection=Server=prod-server;Database=Booksy;...

# JWT
JWT__SecretKey=your-production-secret-key-here

# Stripe
Stripe__SecretKey=sk_live_...

# Email
EmailSettings__Password=your-smtp-password

# CORS
Cors__AllowedOrigins__0=https://yourdomain.com
Cors__AllowedOrigins__1=https://www.yourdomain.com
```

---

## Testing Security

### Test CORS
```bash
curl -H "Origin: http://evil.com" http://localhost:5001/api/books
# Should fail with 403 or no CORS headers
```

### Test Rate Limiting
```bash
for i in {1..150}; do 
  curl http://localhost:5001/api/books
done
# 100 requests should succeed, 51+ should fail with 429
```

### Test Authentication
```bash
# Without token - should fail
curl http://localhost:5001/api/admin/books

# With expired token - should fail
curl -H "Authorization: Bearer expired_token" http://localhost:5001/api/admin/books

# With valid token - should succeed
curl -H "Authorization: Bearer valid_token" http://localhost:5001/api/admin/books
```

---

## Deployment Checklist

Before deploying to production:

1. ✅ Enable HTTPS only (no HTTP)
2. ✅ Set strong JWT secret (min 32 chars, random)
3. ✅ Configure production CORS origins only
4. ✅ Enable database encryption
5. ✅ Set password policy to strict
6. ✅ Configure secure SMTP for emails
7. ✅ Enable security headers
8. ✅ Set rate limiting appropriately
9. ✅ Enable audit logging
10. ✅ Test all authentication/authorization flows
11. ✅ Test error handling (no stack traces to users)
12. ✅ Configure monitoring and alerting
13. ✅ Perform security scan
14. ✅ Review sensitive data handling
