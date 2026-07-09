using MediatR;
using Microsoft.Extensions.Logging;

namespace Booksy.Core.Behaviors;

/// <summary>
/// MediatR pipeline behavior for logging requests and responses
/// Runs AFTER validation, provides timing and error tracking
/// Priority: 2 (Executes second)
/// 
/// Responsibilities:
/// - Log request initiation with context
/// - Track execution time
/// - Log response with status
/// - Log exceptions with full context
/// - Update metrics
/// </summary>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly IBehaviorContext _context;
    private readonly IBehaviorMetrics _metrics;

    public LoggingBehavior(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger,
        IBehaviorContext context,
        IBehaviorMetrics metrics)
    {
        _logger = logger;
        _context = context;
        _metrics = metrics;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var requestType = request.GetType();
        
        _context.RequestName = requestName;
        _context.RequestType = requestType.Name;
        _context.StartTime = DateTime.UtcNow;

        _metrics.RecordStart(requestName, requestType.Name);

        _logger.LogInformation(
            "CQRS Request Started | Name: {RequestName} | Type: {RequestType} | Namespace: {Namespace} | Time: {Time}",
            requestName,
            requestType.Name,
            requestType.Namespace,
            _context.StartTime.ToString("O")
        );

        var startTime = DateTime.UtcNow;
        
        try
        {
            var response = await next();
            var duration = DateTime.UtcNow - startTime;
            var durationMs = (long)duration.TotalMilliseconds;

            _metrics.RecordSuccess(requestName, durationMs);

            _logger.LogInformation(
                "CQRS Request Completed | Name: {RequestName} | Duration: {DurationMs}ms | Status: Success | Validated: {IsValidated}",
                requestName,
                durationMs,
                _context.IsValidationPassed
            );

            return response;
        }
        catch (Exception ex)
        {
            var duration = DateTime.UtcNow - startTime;
            var durationMs = (long)duration.TotalMilliseconds;

            _metrics.RecordFailure(requestName, durationMs, ex.GetType().Name);

            _logger.LogError(
                ex,
                "CQRS Request Failed | Name: {RequestName} | Duration: {DurationMs}ms | ExceptionType: {ExceptionType} | Message: {ErrorMessage} | Validated: {IsValidated}",
                requestName,
                durationMs,
                ex.GetType().Name,
                ex.Message,
                _context.IsValidationPassed
            );
            
            throw;
        }
    }
}
