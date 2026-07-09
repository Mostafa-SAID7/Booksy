# Contributing

---

## Code Standards

| Item | Convention |
|------|-----------|
| Classes/Methods | PascalCase |
| Variables/Parameters | camelCase |
| Constants | UPPER_CASE |
| Interfaces | IPrefixPascalCase |

---

## CQRS Structure

```
Features/YourFeature/
├── Commands/
│   ├── CreateCommand.cs
│   └── CreateCommandHandler.cs
├── Queries/
│   ├── GetAllQuery.cs
│   └── GetAllQueryHandler.cs
├── DTOs/
│   ├── Response.cs
│   └── CreateRequest.cs
├── Validators/
│   └── CreateValidator.cs
└── Controller.cs
```

---

## Implementation Guidelines

### Command/Query
- Implement `IRequest<T>` from MediatR
- Add validation attributes
- Separate request/response models

### Handler
- Implement `IRequestHandler<TRequest, TResponse>`
- Use `ILogger<T>` for logging
- Throw specific exceptions:
  - `NotFoundException` - Resource not found
  - `ValidationException` - Input invalid
  - `BusinessException` - Business rule violated
  - `ConflictException` - Duplicate/conflict

### Controller
- Thin layer - delegate to MediatR
- Add `[Authorize]` for write endpoints
- Include response type attributes
- Handle exceptions properly

### Services
- Inject via constructor
- Implement interfaces
- Keep methods focused and testable

---

## Testing

```bash
dotnet test
```

Test categories:
- **Unit Tests**: Services, handlers (isolated)
- **Integration Tests**: Controllers with database
- **API Tests**: Full request/response

---

## Pull Request Process

1. Create branch: `git checkout -b feature/description`
2. Follow code standards
3. Test locally: `dotnet run`
4. Commit with clear messages
5. Push and create PR with description
6. Ensure CI passes
7. Request review

---

## Commit Message Format

```
[Feature/Fix/Docs] Brief description (50 chars max)

Longer explanation if needed (72 chars per line)
- Bullet points for changes
```

**Examples**:
```
[Feature] Add promotion discount support
[Fix] Resolve N+1 query in book loading
[Docs] Update API documentation
```
