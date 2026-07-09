namespace Booksy.Core.Exceptions;

/// <summary>
/// Base application exception
/// </summary>
public class ApplicationException : Exception
{
    public ApplicationException(string message) : base(message) { }
    
    public ApplicationException(string message, Exception innerException) 
        : base(message, innerException) { }
}

/// <summary>
/// Exception thrown when a requested resource is not found
/// </summary>
public class NotFoundException : ApplicationException
{
    public NotFoundException(string message) : base(message) { }
    
    public NotFoundException(string resource, object key) 
        : base($"{resource} with ID {key} was not found") { }
}

/// <summary>
/// Exception thrown when business logic validation fails
/// </summary>
public class BusinessException : ApplicationException
{
    public BusinessException(string message) : base(message) { }
}

/// <summary>
/// Exception thrown when input validation fails
/// </summary>
public class ValidationException : ApplicationException
{
    public Dictionary<string, string[]> Errors { get; set; }

    public ValidationException() : base("Validation failed")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(Dictionary<string, string[]> errors)
        : base("Validation failed")
    {
        Errors = errors;
    }

    public ValidationException(IEnumerable<FluentValidation.Results.ValidationFailure> failures)
        : base("Validation failed")
    {
        Errors = failures
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).ToArray()
            );
    }
}

/// <summary>
/// Exception thrown when operation conflicts with existing data
/// </summary>
public class ConflictException : ApplicationException
{
    public ConflictException(string message) : base(message) { }
}

/// <summary>
/// Exception thrown for unauthorized access
/// </summary>
public class UnauthorizedException : ApplicationException
{
    public UnauthorizedException(string message = "Unauthorized access") : base(message) { }
}

/// <summary>
/// Exception thrown when user lacks permission for operation
/// </summary>
public class AuthorizationException : ApplicationException
{
    public AuthorizationException(string message) : base(message) { }
}

/// <summary>
/// Exception thrown for forbidden operations
/// </summary>
public class ForbiddenException : ApplicationException
{
    public ForbiddenException(string message = "Access forbidden") : base(message) { }
}
