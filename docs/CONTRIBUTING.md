# Contributing

## Code Standards

### Naming Conventions
- **Classes/Methods**: PascalCase
- **Variables/Parameters**: camelCase
- **Constants**: UPPER_CASE
- **Interfaces**: IPrefixPascalCase

### CQRS Structure
- Commands in `Features/YourFeature/Commands/`
- Queries in `Features/YourFeature/Queries/`
- DTOs in `Features/YourFeature/DTOs/`
- Validators in `Features/YourFeature/Validators/`

### Example: Create Book Feature
```
Features/Books/
├── Commands/
│   ├── CreateBookCommand.cs
│   └── CreateBookCommandHandler.cs
├── Queries/
│   ├── GetAllBooksQuery.cs
│   └── GetAllBooksQueryHandler.cs
├── DTOs/
│   ├── BookResponse.cs
│   └── BookCreateRequest.cs
├── Validators/
│   └── CreateBookValidator.cs
└── BooksController.cs
```

## Guidelines

### Command/Query
- Implement `IRequest<T>` from MediatR
- Add validation attributes
- Keep separate request/response models

### Handler
- Implement `IRequestHandler<TRequest, TResponse>`
- Use `ILogger<T>` for logging
- Throw specific exceptions:
  - `NotFoundException` - Resource not found
  - `ValidationException` - Input invalid
  - `BusinessException` - Business rule violated
  - `ConflictException` - Duplicate/conflict detected

### Controller
- Thin layer - delegate to MediatR
- Add `[Authorize]` attributes for write endpoints
- Include response type attributes
- Handle exceptions and return proper status codes

### Services
- Inject via constructor
- Use dependency injection
- Implement interfaces
- Keep methods focused and testable

## Testing

Run tests:
```bash
dotnet test
```

Test categories:
- **Unit Tests**: Services, handlers (isolated)
- **Integration Tests**: Controllers, handlers with database
- **API Tests**: Full request/response flow

## Pull Request Process

1. Create feature branch: `git checkout -b feature/description`
2. Follow code standards
3. Test locally: `dotnet run`
4. Commit with clear messages
5. Push and create PR with description
6. Ensure CI passes
7. Request review

## Commit Messages

Format:
```
[Feature/Fix/Docs] Brief description (50 chars max)

Longer explanation if needed (72 chars per line)
- Bullet points for changes
```

Examples:
```
[Feature] Add promotion discount support
[Fix] Resolve N+1 query in book loading
[Docs] Update API documentation
```
