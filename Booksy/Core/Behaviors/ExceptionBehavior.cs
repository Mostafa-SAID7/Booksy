using MediatR;
using Microsoft.Extensions.Logging;
using Booksy.Core.Exceptions;
using ApplicationException = Booksy.Core.Exceptions.ApplicationException;

namespace Booksy.Core.Behaviors;

/// <summary>
/// MediatR pipeline behavior for centralized exception handling
/// Runs AFTER logging, catches and transforms exceptions
/// Priority: 3 (Executes third)
/// 
/// Handles:
/// - ValidationException: Returns detailed validation errors
/// - NotFoundException: Returns 404 with entity info
/// - ConflictException: Returns 409 with conflict details
/// - BusinessException: Returns 400 with business rule violation
/// - Unhandled exceptions: Logs and re-throws
/// 
/// Responsibilities:
/// - Categorize exceptions
/// - Log with appropriate severity
/// - Update context with exception info
/// - Preserve stack traces for debugging
/// </summary>
public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ExceptionBehavior<TRequest, TResponse>> _logger;
    private readonly IBehaviorContext _context;

    public ExceptionBehavior(
        ILogger<ExceptionBehavior<TRequest, TResponse>> logger,
        IBehaviorContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        try
        {
            return await next();
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(
                "Validation Failed | Request: {RequestName} | ErrorCount: {ErrorCount} | Errors: {Errors}",
                requestName,
                ex.Errors.Count,
                string.Join("; ", ex.Errors.SelectMany(e => e.Value))
            );

            _context.Properties["ExceptionType"] = "ValidationException";
            _context.Properties["ErrorCount"] = ex.Errors.Count;
            throw;
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(
                "Resource Not Found | Request: {RequestName} | Details: {Message}",
                requestName,
                ex.Message
            );

            _context.Properties["ExceptionType"] = "NotFoundException";
            throw;
        }
        catch (ConflictException ex)
        {
            _logger.LogWarning(
                "Conflict Detected | Request: {RequestName} | Details: {Message}",
                requestName,
                ex.Message
            );

            _context.Properties["ExceptionType"] = "ConflictException";
            throw;
        }
        catch (BusinessException ex)
        {
            _logger.LogWarning(
                "Business Rule Violation | Request: {RequestName} | Details: {Message}",
                requestName,
                ex.Message
            );

            _context.Properties["ExceptionType"] = "BusinessException";
            throw;
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(
                ex,
                "Application Error | Request: {RequestName} | Type: {ExceptionType} | Details: {Message}",
                requestName,
                ex.GetType().Name,
                ex.Message
            );

            _context.Properties["ExceptionType"] = "ApplicationException";
            throw;
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogWarning(
                "Operation Cancelled | Request: {RequestName}",
                requestName
            );

            _context.Properties["ExceptionType"] = "OperationCanceledException";
            throw;
        }
        catch (Exception ex)
        {
            // Unhandled exception - log with full details
            _logger.LogError(
                ex,
                "Unhandled Exception | Request: {RequestName} | Type: {ExceptionType} | Message: {Message} | StackTrace: {StackTrace}",
                requestName,
                ex.GetType().Name,
                ex.Message,
                ex.StackTrace
            );

            _context.Properties["ExceptionType"] = ex.GetType().Name;
            _context.Properties["StackTrace"] = ex.StackTrace;
            throw;
        }
    }
}
