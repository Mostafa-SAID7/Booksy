namespace Booksy.Infrastructure.Monitoring
{
    /// <summary>
    /// Service for application monitoring and metrics tracking
    /// </summary>
    public interface IMonitoringService
    {
        /// <summary>
        /// Track an authentication failure
        /// </summary>
        Task TrackAuthenticationFailureAsync(string username, string ipAddress, string reason);

        /// <summary>
        /// Track authorization/access denied event
        /// </summary>
        Task TrackAuthorizationFailureAsync(string userId, string resource, string ipAddress);

        /// <summary>
        /// Track rate limiting event
        /// </summary>
        Task TrackRateLimitAsync(string identifier, int requestsInWindow);

        /// <summary>
        /// Track exception/error
        /// </summary>
        Task TrackExceptionAsync(string exceptionType, string message, string stackTrace, string userId = null);

        /// <summary>
        /// Track API endpoint performance
        /// </summary>
        Task TrackEndpointPerformanceAsync(string endpoint, string method, long durationMs, int statusCode);

        /// <summary>
        /// Track database query performance
        /// </summary>
        Task TrackDatabaseQueryAsync(string query, long durationMs, bool success);

        /// <summary>
        /// Track suspicious activity pattern
        /// </summary>
        Task TrackSuspiciousActivityAsync(string activityType, string description, string userId = null, string ipAddress = null);

        /// <summary>
        /// Get metrics summary for dashboard
        /// </summary>
        Task<MonitoringMetrics> GetMetricsSummaryAsync(DateTime from, DateTime to);
    }

    /// <summary>
    /// Monitoring metrics summary
    /// </summary>
    public class MonitoringMetrics
    {
        public int TotalRequests { get; set; }
        public int FailedAuthentications { get; set; }
        public int AuthorizationFailures { get; set; }
        public int RateLimitHits { get; set; }
        public int ExceptionCount { get; set; }
        public double AverageResponseTimeMs { get; set; }
        public int HttpErrors { get; set; }
        public List<SuspiciousActivity> SuspiciousActivities { get; set; } = new();
    }

    /// <summary>
    /// Suspicious activity record
    /// </summary>
    public class SuspiciousActivity
    {
        public DateTime Timestamp { get; set; }
        public string ActivityType { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string IpAddress { get; set; }
        public string Severity { get; set; } // Low, Medium, High, Critical
    }
}
