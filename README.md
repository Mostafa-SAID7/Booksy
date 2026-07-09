# Booksy API

> A modern, scalable book management API built with .NET 9 and CQRS with comprehensive security & monitoring.

[![Build](https://img.shields.io/badge/build-passing-brightgreen)]()
[![Security](https://img.shields.io/badge/security-hardened-green)]()
[![License](https://img.shields.io/badge/license-MIT-blue)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-9.0-blue)](https://dotnet.microsoft.com/download/dotnet/9.0)

---

## 🚀 Quick Start (5 minutes)

```bash
cd Booksy
dotnet user-secrets init
dotnet user-secrets set "JWT:SecretKey" "your-32-char-secret-key-minimum!"
dotnet user-secrets set "Stripe:SecretKey" "sk_test_your_key"
dotnet user-secrets set "EmailSettings:Password" "your-password"
dotnet run
```

**API**: https://localhost:5001/api  
**Swagger**: https://localhost:5001/swagger

---

## 📊 Tech Stack

| Layer | Technology | Version |
|-------|-----------|---------|
| **Framework** | .NET | 9.0 |
| **API** | ASP.NET Core | 9.0.9 |
| **ORM** | Entity Framework Core | 9.0.9 |
| **Database** | SQL Server | 2019+ |
| **Patterns** | CQRS (MediatR) | 11.1.2 |
| **Validation** | FluentValidation | 11.9.0 |
| **Auth** | JWT Bearer | 9.0.9 |
| **Identity** | ASP.NET Identity | 9.0.9 |
| **Documentation** | Swagger/OpenAPI | 9.0.4 |
| **Mapping** | AutoMapper | 12.0.0 |
| **Payment** | Stripe | 48.5.0 |

---

## ✨ Features

### Security (10 layers) ✅
- 🔐 JWT Authentication (HTTPS enforced)
- 🛡️ CORS Hardening (specific origins only)
- 🔒 Strong Passwords (12+ chars, complexity)
- ⏱️ Rate Limiting (100 req/min global, 5 req/min auth)
- 📋 Security Headers (7 types: CSP, HSTS, etc.)
- 🔑 Ownership Validation (users access own data only)
- 🚫 Account Lockout (5 min after 5 failed attempts)
- 📏 Request Size Limits (10 MB max)
- 🔐 Secrets Management (User Secrets / Key Vault)
- 📡 Audit Logging (comprehensive event trail)

### Core Features ✅
- 📚 Book Management (CRUD)
- 👥 User Accounts & Authentication
- 🛒 Shopping Cart
- ⭐ Reviews & Ratings
- 🏷️ Categories & Tags
- 🎯 Search & Pagination
- 📊 Reports & Statistics
- 💰 Payment Integration (Stripe)

### Monitoring & Alerting ✅
- 📊 Performance Tracking (HTTP & CQRS layers)
- 🚨 Multi-Channel Alerts (Email, Slack, PagerDuty, Webhooks)
- ⚡ Real-time Event Detection (auth failures, rate limits, exceptions)
- 📈 Slow Endpoint Alerts (>5 sec response time)
- 🔍 Suspicious Activity Detection

---

## 📚 Documentation

| Document | Purpose |
|----------|---------|
| [docs/SECURITY.md](docs/SECURITY.md) | Security configuration & testing |
| [docs/MONITORING.md](docs/MONITORING.md) | Monitoring & alert setup |
| [docs/ARCHITECTURE.md](docs/ARCHITECTURE.md) | CQRS patterns & design |
| [docs/API.md](docs/API.md) | Endpoint reference |
| [docs/DATABASE.md](docs/DATABASE.md) | Schema & migrations |
| [docs/CONTRIBUTING.md](docs/CONTRIBUTING.md) | Development guidelines |
| [CHANGELOG.md](CHANGELOG.md) | Version history |

---

## 🧪 Quick Tests

```bash
# Public endpoint
curl https://localhost:5001/api/books

# Protected endpoint  
curl -H "Authorization: Bearer TOKEN" https://localhost:5001/api/admin/books

# Rate limit test (101 requests, 100 pass, 1 fails with 429)
for i in {1..101}; do curl https://localhost:5001/api/books; done
```

See [docs/SECURITY.md](docs/SECURITY.md) for complete test suite.

---

## 🚢 Production Checklist

1. Update secrets in Azure Key Vault
2. Configure alert channels in `appsettings.Monitoring.json`
3. Set `ASPNETCORE_ENVIRONMENT=Production`
4. Review [docs/SECURITY.md](docs/SECURITY.md#production-deployment)
5. Run security tests before deploying

---

## 📋 Project Status

| Component | Status |
|-----------|--------|
| Security | ✅ Production Ready |
| API | ✅ Production Ready |
| Monitoring | ✅ Production Ready |
| Documentation | ✅ Complete |

---

## 📞 Support

- 📖 Full documentation in [docs/](docs/) directory
- 🐛 Report issues on GitHub
- 📧 Email: team@booksy.com

---

**Version**: 1.0.0 | **Status**: ✅ Production Ready | **License**: MIT
