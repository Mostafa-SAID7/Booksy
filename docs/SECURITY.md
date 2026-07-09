# Security

Comprehensive 10-layer security implementation with JWT authentication, rate limiting, ownership validation, and audit logging.

---

## Components

### 1. JWT Authentication
- **Location**: `Extensions/JwtAuthExtension.cs`
- **Features**:
  - Environment-aware HTTPS (required in production, optional in dev)
  - Symmetric key encryption (HS256)
  - Token-based stateless authentication
- **Setup**:
  ```bash
  dotnet user-secrets set "JWT:SecretKey" "your-32-char-minimum-key"
  ```
- **Configuration**: `appsettings.json` JWT section

### 2. CORS Hardening
- **Location**: `Extensions/CorsExtensions.cs`
- **Policy**: Specific origins only (no wildcards)
- **Configuration**: `appsettings.json` Cors section
- **Per Environment**: Update AllowedOrigins for each deployment
  - Development: localhost:3000, localhost:5500
  - Production: yourdomain.com, www.yourdomain.com

### 3. Password Policy
- **Location**: `Extensions/ServiceCollectionExtensions.cs`
- **Requirements**:
  - Minimum 12 characters (vs standard 8)
  - Uppercase, lowercase, digits, special chars
  - 4 unique characters
  - 5 failed attempts → 5 minute lockout
- **Applied To**: All user registrations and password changes

### 4. Security Headers
- **Location**: `Middleware/SecurityHeadersMiddleware.cs`
- **Headers Added**:
  - `X-Frame-Options: DENY` - Prevent clickjacking
  - `X-Content-Type-Options: nosniff` - Prevent MIME sniffing
  - `Content-Security-Policy` - Prevent XSS attacks
  - `Strict-Transport-Security` - Force HTTPS
  - `Referrer-Policy` - Control referrer info
  - `Permissions-Policy` - Disable unnecessary features

### 5. Rate Limiting
- **Location**: `Program.cs`
- **Configuration**:
  - Global: 100 requests/minute per user/IP
  - Authentication: 5 requests/minute per IP
  - Response: 429 Too Many Requests
- **Thresholds**: Configurable in `appsettings.Monitoring.json`

### 6. Request Size Limits
- **Location**: `Program.cs`
- **Limit**: 10 MB maximum request body
- **Protection**: Against large payload attacks

### 7. Authorization & Ownership Validation
- **Location**: `Common/Services/AuthorizationService.cs`
- **Pattern**: 
  1. Extract UserId from JWT claims
  2. Pass to command
  3. Validate ownership in handler
  4. Return 403 Forbidden if unauthorized
- **Implemented For**: Orders, Reviews, Carts

### 8. Secrets Management

**Development** (Local):
```bash
dotnet user-secrets init
dotnet user-secrets set "JWT:SecretKey" "your-key"
dotnet user-secrets set "Stripe:SecretKey" "sk_test_..."
dotnet user-secrets set "EmailSettings:Password" "..."
```

**Production** (Azure Key Vault):
```bash
az keyvault secret set --vault-name my-vault --name "JWT-SecretKey" --value "key"
az keyvault secret set --vault-name my-vault --name "Stripe-SecretKey" --value "key"
az keyvault secret set --vault-name my-vault --name "EmailSettings-Password" --value "pass"
```

### 9. Environment Configuration
- **appsettings.json** - Base configuration
- **appsettings.Development.json** - Dev overrides
- **appsettings.Production.json** - Production overrides
- **appsettings.Monitoring.json** - Monitoring settings

### 10. Audit Logging
- **What's Tracked**:
  - Authentication events (login, logout, password changes)
  - Authorization events (403 Forbidden)
  - Data modifications (create, update, delete)
  - Admin actions
  - Security alerts
- **Retention**: 90 days minimum
- **Location**: Database audit log table

---

## Security Testing

### Test JWT Authentication
```bash
# Without token (should fail with 401)
curl https://localhost:5001/api/admin/books

# With token (should succeed with 200)
curl -H "Authorization: Bearer YOUR_JWT_TOKEN" \
     https://localhost:5001/api/admin/books
```

### Test Rate Limiting
```bash
# Make 101 requests - 100 should succeed, 1 should fail with 429
for i in {1..101}; do 
  curl https://localhost:5001/api/books
done
```

### Test CORS
```bash
# From unauthorized origin (should fail)
curl -H "Origin: http://evil.com" \
     -H "Access-Control-Request-Method: POST" \
     https://localhost:5001/api/books

# Should NOT include Access-Control-Allow-Origin header
```

### Test Ownership Validation
```bash
# Create order as User1
USER1_ID="user-1"
USER1_TOKEN="eyJ..."
curl -H "Authorization: Bearer $USER1_TOKEN" \
     -X POST https://localhost:5001/api/orders \
     -H "Content-Type: application/json" \
     -d '{"items": [...]}'

# Try to access as User2 (should fail with 403)
USER2_TOKEN="eyJ..."
curl -H "Authorization: Bearer $USER2_TOKEN" \
     https://localhost:5001/api/orders/{order-id}
# Response: 403 Forbidden
```

### Test Security Headers
```bash
curl -I https://localhost:5001/api/books

# Should include:
# X-Frame-Options: DENY
# X-Content-Type-Options: nosniff
# Strict-Transport-Security: max-age=31536000
# Content-Security-Policy: default-src 'self'
```

---

## Production Deployment Checklist

### Pre-Deployment
- [ ] Set `ASPNETCORE_ENVIRONMENT=Production`
- [ ] Generate strong JWT key (32+ random characters)
- [ ] Create Azure Key Vault
- [ ] Store all secrets in Key Vault
- [ ] Update CORS origins for production domain
- [ ] Configure production SMTP credentials
- [ ] Enable HTTPS only (redirect HTTP to HTTPS)
- [ ] Setup database encryption
- [ ] Configure firewall rules

### Security Verification
- [ ] Build succeeds (`dotnet build`)
- [ ] JWT enforcement working (test with/without token)
- [ ] Rate limiting functional (101 requests = 1 fail)
- [ ] CORS blocks unauthorized origins
- [ ] Ownership validation prevents cross-user access
- [ ] Security headers present in responses
- [ ] No hardcoded secrets in code
- [ ] All secrets in Key Vault

### Monitoring Setup
- [ ] Email alerts enabled and tested
- [ ] Slack webhook configured (if using)
- [ ] PagerDuty integration (for critical alerts)
- [ ] Application Insights dashboard created
- [ ] Alert thresholds appropriate for production
- [ ] Audit log retention configured (90+ days)

### Performance Testing
- [ ] Load test (1000 concurrent users)
- [ ] Slow endpoint detection working
- [ ] Database connection pool sizing
- [ ] Cache configuration optimized
- [ ] Response time thresholds appropriate

### Operational Setup
- [ ] Backup strategy configured
- [ ] Audit logs backed up daily
- [ ] Recovery procedure documented and tested
- [ ] On-call rotation established
- [ ] Incident response plan documented
- [ ] Monitoring dashboards accessible

---

## Configuration Examples

### Development (appsettings.Development.json)
```json
{
  "ASPNETCORE_ENVIRONMENT": "Development",
  "Cors": {
    "AllowedOrigins": ["http://localhost:3000", "http://localhost:5500"]
  },
  "JWT": {
    "SecretKey": "dev-secret-min-32-chars-long-required-for-local-dev"
  },
  "Monitoring": {
    "Alerts": {
      "Channels": {
        "Email": { "Enabled": false }
      }
    }
  }
}
```

### Production (appsettings.Production.json)
```json
{
  "ASPNETCORE_ENVIRONMENT": "Production",
  "Cors": {
    "AllowedOrigins": ["https://booksy.com", "https://www.booksy.com"]
  },
  "Monitoring": {
    "Alerts": {
      "Channels": {
        "Email": { "Enabled": true, "Recipients": ["admin@booksy.com"] },
        "PagerDuty": { "Enabled": true }
      }
    }
  }
}
```

---

## Common Issues & Solutions

### 401 Unauthorized on Protected Endpoint
**Solution**: Verify JWT token included (`Authorization: Bearer TOKEN`) and hasn't expired.

### 403 Forbidden on Own Data
**Solution**: Verify UserId from JWT matches owner. Review `AuthorizationService` implementation.

### Rate Limit False Positives
**Solution**: Adjust thresholds in `appsettings.Monitoring.json` or extend `TimeWindowMinutes`.

### Alerts Not Sending
**Solution**: Verify alert channel enabled, credentials valid, and channel connectivity working.

---

## Key Metrics to Monitor

| Metric | Target | Alert |
|--------|--------|-------|
| Auth Success Rate | >99% | <95% |
| Rate Limit Hits | <5/day | >50/day |
| 403 Forbidden Rate | <1% | >5% |
| Exception Rate | <0.1% | >1% |
| Avg Response Time | <500ms | >5000ms |

---

## Deployment Frequency

- **Security Updates**: Immediate
- **Bug Fixes**: Within 48 hours
- **New Features**: Weekly/bi-weekly
- **Configuration Changes**: As needed (no restart required for most)

---

**Last Updated**: July 9, 2026  
**Status**: Production Ready ✅
