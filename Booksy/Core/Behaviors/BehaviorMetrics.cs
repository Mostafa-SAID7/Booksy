namespace Booksy.Core.Behaviors;

/// <summary>
/// Implementation of behavior metrics tracking
/// Thread-safe metrics collection
/// </summary>
public class BehaviorMetrics : IBehaviorMetrics
{
    private readonly object _lockObject = new object();
    private BehaviorMetricsData _currentMetrics = new();

    public void RecordStart(string requestName, string requestType)
    {
        lock (_lockObject)
        {
            _currentMetrics = new BehaviorMetricsData
            {
                RequestName = requestName,
                RequestType = requestType,
                ExecutedAt = DateTime.UtcNow
            };
        }
    }

    public void RecordSuccess(string requestName, long durationMs)
    {
        lock (_lockObject)
        {
            _currentMetrics.RequestName = requestName;
            _currentMetrics.DurationMs = durationMs;
            _currentMetrics.IsSuccess = true;
            _currentMetrics.ErrorType = null;
        }
    }

    public void RecordFailure(string requestName, long durationMs, string errorType)
    {
        lock (_lockObject)
        {
            _currentMetrics.RequestName = requestName;
            _currentMetrics.DurationMs = durationMs;
            _currentMetrics.IsSuccess = false;
            _currentMetrics.ErrorType = errorType;
        }
    }

    public BehaviorMetricsData GetMetrics()
    {
        lock (_lockObject)
        {
            return _currentMetrics;
        }
    }
}
