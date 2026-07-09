namespace Booksy.Core.Behaviors;

/// <summary>
/// Tracks metrics for behavior execution
/// Enables performance monitoring and diagnostics
/// </summary>
public interface IBehaviorMetrics
{
    /// <summary>
    /// Record request start
    /// </summary>
    void RecordStart(string requestName, string requestType);

    /// <summary>
    /// Record successful completion
    /// </summary>
    void RecordSuccess(string requestName, long durationMs);

    /// <summary>
    /// Record failure
    /// </summary>
    void RecordFailure(string requestName, long durationMs, string errorType);

    /// <summary>
    /// Get current metrics
    /// </summary>
    BehaviorMetricsData GetMetrics();
}

/// <summary>
/// Behavior metrics data
/// </summary>
public class BehaviorMetricsData
{
    public string RequestName { get; set; } = string.Empty;
    public string RequestType { get; set; } = string.Empty;
    public long DurationMs { get; set; }
    public bool IsSuccess { get; set; }
    public string? ErrorType { get; set; }
    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;
}
