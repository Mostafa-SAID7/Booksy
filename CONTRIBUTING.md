# Contributing to Booksy API

Thank you for contributing to Booksy API! This guide explains our development workflow, branching strategy, and commit conventions.

## Table of Contents

- [Getting Started](#getting-started)
- [Development Workflow](#development-workflow)
- [Conventional Commits](#conventional-commits)
- [Git Workflow](#git-workflow)
- [Pull Request Process](#pull-request-process)
- [Code Quality](#code-quality)
- [Testing](#testing)
- [Secrets & Security](#secrets--security)
- [Questions?](#questions)

---

## Getting Started

### Prerequisites

- .NET 9.0 SDK or later
- Git
- Visual Studio 2022 or VS Code
- Supabase account (for database access)

### Setup

```bash
# Clone the repository
git clone https://github.com/YOUR_ORG/booksy-api.git
cd booksy-api

# Initialize user secrets
cd Booksy
dotnet user-secrets init

# Set required secrets
dotnet user-secrets set "JWT:SecretKey" "your-32-char-key-minimum!"
dotnet user-secrets set "Stripe:SecretKey" "sk_test_your_key"
dotnet user-secrets set "DatabasePassword" "your-supabase-password"
dotnet user-secrets set "EmailSettings:Password" "your-email-password"

# Restore and build
dotnet restore
dotnet build

# Run the application
dotnet run
```

**API Base URL**: https://localhost:5001/api  
**Swagger Documentation**: https://localhost:5001/swagger

---

## Development Workflow

### 1. Create a Feature Branch

Always create a feature branch from `master`:

```bash
git fetch origin
git checkout -b feature/my-feature origin/master
```

### 2. Make Changes

- Write clean, readable code
- Follow C# naming conventions (PascalCase for classes/methods)
- Add XML documentation comments for public APIs
- Update relevant configuration files

### 3. Commit Changes

Follow [Conventional Commits](#conventional-commits) specification.

### 4. Push and Create PR

```bash
git push origin feature/my-feature
```

Then create a Pull Request on GitHub targeting `master`.

### 5. Automated Checks

GitHub Actions will automatically run:

- ✅ **CI**: Build, restore, and test
- ✅ **Security**: CodeQL, secret scanning, dependency review
- ✅ **Docker**: Dockerfile validation and image build
- ✅ **YAML**: Workflow validation

All checks must pass before merge.

### 6. Code Review

Request reviews from team members. Address feedback and push new commits (don't force-push).

### 7. Merge

Once approved and all checks pass, merge to `master`:

```bash
# GitHub will handle the merge
# Or locally:
git checkout master
git pull origin master
git merge feature/my-feature
git push origin master
```

---

## Conventional Commits

All commits must follow [Conventional Commits](https://www.conventionalcommits.org/) specification.

### Format

```
<type>[optional scope]: <description>

[optional body]

[optional footer(s)]
```

### Types

| Type | Meaning | Release Impact |
|------|---------|-----------------|
| `feat` | New feature | MINOR version bump |
| `fix` | Bug fix | PATCH version bump |
| `perf` | Performance improvement | PATCH version bump |
| `refactor` | Code refactoring | No release |
| `test` | Test additions/changes | No release |
| `docs` | Documentation | No release |
| `style` | Formatting, style | No release |
| `build` | Build system changes | No release |
| `ci` | CI/CD changes | No release |
| `chore` | Maintenance tasks | No release |
| `revert` | Revert previous commit | PATCH version bump |

### Examples

#### Feature
```
feat(cart): add persistent cart items

- Store cart in Redis for session persistence
- Add cart expiration after 24 hours
- Include cart metadata in checkout

Closes #42
```

#### Bug Fix
```
fix(auth): prevent expired refresh token reuse

The refresh token validation was not checking token expiration.
This allows attackers to reuse expired tokens.

Closes #127
```

#### Breaking Change
```
feat(api)!: redesign authentication contract

BREAKING CHANGE: The /auth/login endpoint now requires
two-factor authentication by default. Clients must update
to support the new challenge flow.

See docs/auth-migration.md for upgrade guide.
```

#### Performance
```
perf(db): optimize product search query

Reduce query execution from 2.5s to 150ms by adding
composite index on (category_id, name).
```

---

## Git Workflow

### Branch Naming

```
feature/short-description     # New features
fix/short-description         # Bug fixes
docs/short-description        # Documentation
refactor/short-description    # Refactoring
perf/short-description        # Performance
test/short-description        # Tests
ci/short-description          # CI/CD
```

### Commit History

Keep history clean:

```bash
# ✅ Good: Logical, focused commits
feat(cart): add items
fix(cart): prevent duplicates
test(cart): add unit tests

# ❌ Avoid: Too many or unclear commits
WIP
update
fix typo
fix typo again
almost there
done
```

### Before Pushing

```bash
# Check what you're pushing
git log origin/master..HEAD

# Verify only your changes
git diff origin/master

# Push
git push origin feature/my-feature
```

---

## Pull Request Process

### PR Title

```
feat(scope): concise description
fix(scope): concise description
```

### PR Description

```markdown
## 📝 Description
Brief explanation of what this PR does.

## 🎯 Related Issue
Closes #42

## 🔍 Changes
- Change 1
- Change 2
- Change 3

## ✅ Testing
How did you test this? Include steps to reproduce.

## 📷 Screenshots (if applicable)
Add screenshots for UI changes.

## 🚨 Breaking Changes
Does this break existing functionality?

## ⚠️ Notes
Any special considerations for reviewers.
```

### Review Checklist

Before marking PR as ready:

- [ ] All tests pass locally
- [ ] No secrets committed
- [ ] Code follows style guide
- [ ] Documentation updated if needed
- [ ] Commits follow conventional commits
- [ ] No unnecessary dependencies added
- [ ] Performance implications considered

---

## Code Quality

### C# Style

Follow Microsoft [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions):

```csharp
// ✅ Good
public class BookService
{
    private readonly IRepository<Book> _repository;
    
    public async Task<Book> GetBookAsync(Guid id)
    {
        var book = await _repository.GetAsync(id);
        return book ?? throw new NotFoundException("Book not found");
    }
}

// ❌ Avoid
public class BookService {
    private IRepository<Book> repository;
    public Book GetBook(Guid id) { var b = repository.Get(id); return b; }
}
```

### Documentation

Add XML documentation to public methods:

```csharp
/// <summary>
/// Retrieves a book by its ID.
/// </summary>
/// <param name="id">The unique identifier of the book.</param>
/// <returns>The book if found; otherwise null.</returns>
/// <exception cref="NotFoundException">Thrown when book is not found.</exception>
public async Task<Book> GetBookAsync(Guid id)
{
    // Implementation
}
```

### Static Analysis

Visual Studio will flag issues. Address all warnings before committing.

---

## Testing

### Unit Tests

Run tests locally:

```bash
cd Booksy
dotnet test --configuration Release
```

### Test Naming

```csharp
// Format: MethodName_Scenario_ExpectedResult
[Fact]
public async Task CreateBook_WithValidData_ReturnsSuccess()
{
    // Arrange
    var command = new CreateBookCommand { /* ... */ };
    
    // Act
    var result = await _handler.Handle(command, default);
    
    // Assert
    Assert.True(result.IsSuccess);
}
```

### Coverage

Aim for >80% code coverage on new code.

---

## Secrets & Security

### DO NOT commit secrets

- Database passwords
- JWT signing keys
- Stripe API keys
- Email service credentials
- Any API tokens

### Use User Secrets (Development)

```bash
dotnet user-secrets set "KeyName" "value"
```

### Use GitHub Secrets (CI/CD)

Repository → Settings → Secrets and Variables → Actions

---

## Git Safety Rules

- **Never force push** to `master`
- **Never commit secrets** to any branch
- **Always create PR** for code review before merging
- **Never merge PR** with failing checks
- **Never delete** production branches or tags

---

## Troubleshooting

### Build Fails

```bash
# Clean and rebuild
dotnet clean
dotnet build
```

### Tests Fail

```bash
# Ensure database connection string is set
dotnet user-secrets list

# Run tests with verbose output
dotnet test --verbosity detailed
```

### PR Checks Fail

Check the GitHub Actions tab in your PR. Common issues:

1. **Build failure**: Fix compilation errors
2. **Test failure**: Fix failing tests or add new tests
3. **CodeQL issue**: Address security warnings
4. **Secret detected**: Remove the secret, do NOT retry CI
5. **Docker build failure**: Check Dockerfile syntax

---

## Questions?

- 📖 Check [README.md](README.md) for project overview
- 📚 See [RELEASES.md](RELEASES.md) for release process
- 🔒 Review [docs/SECURITY.md](docs/SECURITY.md) for security guidelines
- 📧 Email: team@booksy.com

---

**Last Updated**: September 7, 2026  
**Version**: 1.0.0
