# Changelog

All notable changes to Booksy API are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/),
and this project adheres to [Semantic Versioning](https://semver.org/).

# 1.0.0 (2026-09-07)


### Bug Fixes

* correct Dependabot dependency-type values (production vs invalid 'all') ([36e58d4](https://github.com/Mostafa-SAID7/Booksy-Api/commit/36e58d4ec33f192cde4b982acd01445ebc09919b))
* correct yamllint truthy rule configuration (check-keys instead of allowed) ([99e793f](https://github.com/Mostafa-SAID7/Booksy-Api/commit/99e793f6c333325e1179bccecb2f6954c4dee0c3))
* remove npm plugin from semantic-release config (.NET backend project) ([6b1167f](https://github.com/Mostafa-SAID7/Booksy-Api/commit/6b1167f4eed594fe104893be76b4deb106587e21))
* remove unused packages (Mapster, Scalar) and resolve all compilation errors ([ce514d2](https://github.com/Mostafa-SAID7/Booksy-Api/commit/ce514d24c57728676f8a1b178bf7544e33651f7b))
* Replace PostgreSQL with SQL Server support and fix header warnings ([721c3f5](https://github.com/Mostafa-SAID7/Booksy-Api/commit/721c3f58954e35beb89fb148401750e6c8a83c19))
* Update connection strings from PostgreSQL to SQL Server format ([eeefbce](https://github.com/Mostafa-SAID7/Booksy-Api/commit/eeefbce6eadcdeb5ccdf9640a2a50795eaa2ec3d))


### Features

* Add centralized JavaScript files and dashboard page ([20e5ae4](https://github.com/Mostafa-SAID7/Booksy-Api/commit/20e5ae490762fed37c8ce7f2028911c49654da2c))
* Add wwwroot static files and improve middleware pipeline ([6d80a45](https://github.com/Mostafa-SAID7/Booksy-Api/commit/6d80a4510ce40eb1777c592161e9a553ae9890eb))
* comprehensive security, monitoring, and documentation update ([d4e4729](https://github.com/Mostafa-SAID7/Booksy-Api/commit/d4e47292d08887d921f8dcc30f744e45394bc695))
* frontend comprehensive cleanup and standardization ([e0a81b6](https://github.com/Mostafa-SAID7/Booksy-Api/commit/e0a81b65c78c6dde43a781388d997ea75e96e1c8))

# Changelog

All notable changes to Booksy API are documented in this file.

## [1.0.0] - 2026-07-09

### Added - Security Infrastructure

#### JWT Authentication
- Environment-aware HTTPS enforcement (required in production)
- Token validation with symmetric key encryption
- Automatic token expiration handling
- User secrets integration for key management

#### CORS Hardening
- Removed wildcard policy - specific origins only
- Proper HTTP method restrictions
- Header whitelisting
- Production domain configuration

#### Password Security
- Increased minimum length: 6 → 12 characters
- Added complexity requirements: uppercase, lowercase, digits, special chars
- Unique character requirement (4 minimum)
- Account lockout: 5 minutes after 5 failed attempts

#### Security Headers
- X-Frame-Options: DENY (clickjacking prevention)
- X-Content-Type-Options: nosniff (MIME sniffing prevention)
- Content-Security-Policy (XSS prevention)
- Strict-Transport-Security (HTTPS enforcement)
- Referrer-Policy (referrer information control)
- Permissions-Policy (unnecessary features disabled)

#### Rate Limiting
- Global limit: 100 requests/minute per user/IP
- Authentication endpoint: 5 requests/minute per IP (strict)
- Auto-replenishing fixed windows
- 429 Too Many Requests responses

#### Request Size Limits
- 10 MB maximum payload
- IIS & Kestrel configuration
- Protection against large payload attacks

#### Authorization Service
- Centralized ownership validation
- User can only access own data
- Admin override capability
- Comprehensive logging

#### Secrets Management
- User Secrets for local development
- Azure Key Vault integration (production)
- No hardcoded secrets in code
- Environment-specific configuration

### Added - Ownership Validation

#### Orders
- CancelOrderCommand: Users can only cancel own orders
- UpdateOrderStatusCommand: Only own order status can be updated
- Authorization exception handling with 403 Forbidden response
- Audit logging for all order modifications

#### Reviews
- UpdateReviewCommand: Only own reviews can be updated
- DeleteReviewCommand: Only own reviews can be deleted
- Owner verification in handlers
- Security logging

#### Carts
- AddToCartCommandHandler: User ownership verification
- Cart management: Users manage own carts only
- Authorization checks on all cart operations

### Added - Monitoring & Alerting

#### MonitoringService
- Authentication failure tracking
- Authorization failure tracking (403 events)
- Rate limit spike detection
- Exception logging with severity levels
- Endpoint performance tracking
- Database query performance tracking
- Suspicious activity detection

#### PerformanceMonitoringMiddleware
- HTTP request/response time tracking
- Endpoint performance logging
- Slow endpoint alerts (>5 seconds)
- Status code categorization

#### MonitoringBehavior (CQRS)
- Command/query execution tracking
- Exception capture with stack traces
- Authorization failure monitoring
- Performance metrics per operation

#### AlertingService
- Multi-channel support:
  - Email (SMTP)
  - Slack (webhooks)
  - PagerDuty (incidents)
  - Application Insights (metrics)
  - Custom webhooks
- Severity-based routing
- Threshold-based triggering
- Alert deduplication

### Added - Audit Logging
- Authentication events (login, logout, password changes)
- Authorization events (access denied, 403 responses)
- Data modifications (create, update, delete)
- Admin actions
- Security alerts
- 90-day retention policy
- Database audit trail

### Added - Controllers
- OrdersController: Updated to pass UserId to commands
- ReviewsController: Updated to pass UserId to commands
- CartsController: Ownership verification added

### Added - Configuration
- appsettings.json: Base configuration with placeholders
- appsettings.Development.json: Development-specific settings
- appsettings.Production.json: Production settings template
- appsettings.Monitoring.json: Monitoring & alert configuration

### Added - Documentation
- README.md: Project overview with quick links
- docs/SECURITY.md: Security implementation guide
- docs/MONITORING.md: Monitoring & alerting guide
- CHANGELOG.md: This file

### Added - Infrastructure Services
- IMonitoringService interface
- MonitoringService implementation
- IAlertingService interface
- AlertingService implementation
- PerformanceMonitoringMiddleware
- MonitoringBehavior (CQRS)

### Changed

#### Extensions
- CqrsExtensions: Added MonitoringBehavior to pipeline
- MiddlewareExtensions: Added security headers middleware
- ServiceCollectionExtensions: 
  - Strengthened password policy
  - Added lockout configuration
  - Registered AuthorizationService
  - Registered MonitoringService
  - Registered AlertingService

#### Program.cs
- Added rate limiting configuration
- Added request size limits
- Added performance monitoring middleware
- Added proper error handling

#### JWT Configuration
- Changed from `RequireHttpsMetadata = false` to environment-aware
- Production: HTTPS required
- Development: HTTPS optional

#### CORS Configuration
- Changed from `AllowAnyMethod()` to explicit methods
- Changed from `AllowAnyHeader()` to specific headers
- Changed from wildcard origin to specific origins

### Removed
- Unnecessary documentation (12 files)
- Duplicate security guides
- Hardcoded secrets from configuration

### Fixed
- Security header middleware now properly integrated
- Rate limiting properly configured
- Authorization service properly registered
- Monitoring events properly tracked

### Security
- All endpoints protected with authentication where required
- Ownership validation on all user-scoped operations
- No secrets committed to repository
- SQL injection prevention verified
- XSS prevention via headers
- CSRF consideration noted (future implementation)

### Testing
- JWT authentication verified
- Rate limiting tested (101 requests)
- CORS policies validated
- Ownership validation tested
- Security headers verified
- Monitoring alerts tested

---

## [0.1.0] - Initial Release

### Added - Core Functionality
- CQRS pattern with MediatR
- FluentValidation integration
- Repository & Unit of Work pattern
- JWT authentication
- Error handling middleware
- Logging infrastructure
- Database migrations

### Added - Book Management
- Categories (CRUD)
- Books (CRUD)
- Authors (CRUD)
- Tags (CRUD)
- Reviews (CRUD)

### Added - E-Commerce
- Shopping cart
- Orders
- Order items
- Shipping

### Added - Reporting
- Sales reports
- Dashboard statistics

---

## Security Summary

| Feature | Status | Version |
|---------|--------|---------|
| JWT Authentication | ✅ | 1.0.0 |
| CORS Hardening | ✅ | 1.0.0 |
| Password Policy | ✅ | 1.0.0 |
| Rate Limiting | ✅ | 1.0.0 |
| Security Headers | ✅ | 1.0.0 |
| Ownership Validation | ✅ | 1.0.0 |
| Monitoring & Alerts | ✅ | 1.0.0 |
| Audit Logging | ✅ | 1.0.0 |

---

## Upgrade Guide

### From 0.1.0 to 1.0.0

1. **Update User Secrets**
   ```bash
   dotnet user-secrets init
   dotnet user-secrets set "JWT:SecretKey" "your-32-char-key"
   ```

2. **Update Configuration**
   - Copy new `appsettings.Monitoring.json`
   - Update CORS origins in `appsettings.json`
   - Review JWT configuration

3. **Database Migrations**
   - Create audit log table
   - Run pending migrations
   - Test on staging first

4. **Deployment**
   - Follow production checklist in [docs/SECURITY.md](docs/SECURITY.md)
   - Setup alert channels
   - Configure monitoring

---

## Known Issues

None at this time.

---

## Future Roadmap

### v1.1.0 (Q3 2026)
- [ ] CSRF token protection
- [ ] Two-factor authentication (2FA)
- [ ] API key management
- [ ] Advanced audit queries
- [ ] Custom dashboard builder

### v1.2.0 (Q4 2026)
- [ ] OAuth 2.0 social login
- [ ] Role-based access control (RBAC)
- [ ] Data encryption at rest
- [ ] Compliance reporting (GDPR, CCPA)
- [ ] Advanced threat detection

### v2.0.0 (2027)
- [ ] GraphQL API
- [ ] Real-time notifications
- [ ] Machine learning anomaly detection
- [ ] Multi-tenancy support
- [ ] Advanced analytics

---

## Contributing

See [docs/CONTRIBUTING.md](docs/CONTRIBUTING.md) for guidelines.

---

## Support

For issues, questions, or security concerns:
- 📧 Email: team@booksy.com
- 🐛 GitHub Issues: [GitHub Repo](https://github.com/booksy/booksy-api)
- 🔒 Security: security@booksy.com

---

**Current Version**: 1.0.0  
**Release Date**: July 9, 2026  
**Status**: Production Ready ✅

[Unreleased]: https://github.com/booksy/booksy-api/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/booksy/booksy-api/releases/tag/v1.0.0
[0.1.0]: https://github.com/booksy/booksy-api/releases/tag/v0.1.0
