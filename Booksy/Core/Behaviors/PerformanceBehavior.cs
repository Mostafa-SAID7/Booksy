using MediatR;
using Microsoft.Extensions.Logging;

namespace Booksy.Core.Behaviors;

/// <summary>
/// MediatR pipeline behavior for performance monitoring
/// Detects slow queries and logs performance metrics
/// Priority: 4 (Executes fourth)
/// 
/// Performance Thresholds:
/// - Slow: > 1000ms - Warning log (potential optimization needed)
/// - Very Slow: > 5000ms - Error log (requires attention)
/// - Critical: > 10000ms - Critical log (immediate action needed)
/// 
/// Responsibilities:
/// - Monitor execution time
/// - Detect performance anomalies
/// - Log with appropriate severity
/// - Update context with performance data
/// </summary>
public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
    private readonly IBehaviorContext _context;
    
    private const int SlowThresholdMs = 1000;
    private const int VerySlowThresholdMs = 5000;
    private const int CriticalThresholdMs = 10000;

    public PerformanceBehavior(
        ILogger<PerformanceBehavior<TRequest, TResponse>> logger,
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
        var startTime = DateTime.UtcNow;

        var response = await next();

        var duration = DateTime.UtcNow - startTime;
        var durationMs = (long)duration.TotalMilliseconds;

        // Store performance data in context
        _context.Properties["DurationMs"] = durationMs;
        _context.Properties["PerformanceLevel"] = "Normal";

        if (durationMs > CriticalThresholdMs)
        {
            _context.Properties["PerformanceLevel"] = "Critical";
            
            _logger.LogCritical(
                "CRITICAL PERFORMANCE | Request: {RequestName} | Duration: {DurationMs}ms (> {ThresholdMs}ms threshold) | Requires immediate optimization",
                requestName,
                durationMs,
                CriticalThresholdMs
            );
        }
        else if (durationMs > VerySlowThresholdMs)
        {
            _context.Properties["PerformanceLevel"] = "VerySlow";
            
            _logger.LogError(
                "VERY SLOW | Request: {RequestName} | Duration: {DurationMs}ms (> {ThresholdMs}ms threshold) | Requires attention",
                requestName,
                durationMs,
                VerySlowThresholdMs
            );
        }
        else if (durationMs > SlowThresholdMs)
        {
            _context.Properties["PerformanceLevel"] = "Slow";
            
            _logger.LogWarning(
                "SLOW | Request: {RequestName} | Duration: {DurationMs}ms (> {ThresholdMs}ms threshold) | Consider optimization",
                requestName,
                durationMs,
                SlowThresholdMs
            );
        }
        else
        {
            _logger.LogDebug(
                "Performance OK | Request: {RequestName} | Duration: {DurationMs}ms",
                requestName,
                durationMs
            );
        }

        return response;
    }
}
