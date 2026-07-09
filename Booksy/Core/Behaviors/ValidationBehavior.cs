using FluentValidation;
using MediatR;
using ValidationException = Booksy.Core.Exceptions.ValidationException;

namespace Booksy.Core.Behaviors;

/// <summary>
/// MediatR pipeline behavior for automatic request validation
/// Runs FIRST in pipeline to ensure all input is valid before processing
/// Priority: 1 (Executes first)
/// 
/// Responsibilities:
/// - Validate request using FluentValidation validators
/// - Collect and organize validation errors
/// - Throw ValidationException if validation fails
/// - Update behavior context with validation status
/// </summary>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly IBehaviorContext _context;

    public ValidationBehavior(
        IEnumerable<IValidator<TRequest>> validators,
        IBehaviorContext context)
    {
        _validators = validators ?? Enumerable.Empty<IValidator<TRequest>>();
        _context = context;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Skip if no validators registered
        if (!_validators.Any())
        {
            _context.IsValidationPassed = true;
            return await next();
        }

        // Run all validators in parallel for performance
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken))
        );

        // Collect all failures
        var failures = validationResults
            .Where(r => !r.IsValid)
            .SelectMany(r => r.Errors)
            .ToList();

        // Throw if any validation failed
        if (failures.Any())
        {
            _context.IsValidationPassed = false;
            _context.ValidationErrors = failures
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray()
                );

            throw new ValidationException(failures);
        }

        _context.IsValidationPassed = true;
        return await next();
    }
}
