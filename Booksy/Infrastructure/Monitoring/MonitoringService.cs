using Microsoft.Extensions.Logging;

namespace Booksy.Infrastructure.Monitoring
{
    /// <summary>
    /// Implementation of monitoring service
    /// Logs metrics and alerts for security and performance monitoring
    /// </summary>
    public class MonitoringService : IMonitoringService
    {
        private readonly ILogger<MonitoringService> _logger;

        public MonitoringService(ILogger<MonitoringService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Track authentication failures - alert if threshold exceeded
        /// </summary>
        public async Task TrackAuthenticationFailureAsync(string username, string ipAddress, string reason)
        {
            _logger.LogWarning(
                "SECURITY: Authentication failed - Username: {Username}, IP: {IpAddress}, Reason: {Reason}",
                username, ipAddress, reason);

            // Alert conditions
            if (reason.Contains("Invalid credentials"))
            {
                _logger.LogWarning("ALERT: Invalid credentials for user {Username} from {IpAddress}", username, ipAddress);
            }

            if (reason.Contains("Locked"))
            {
                _logger.LogError("ALERT: Account locked for user {Username} after multiple failed attempts from {IpAddress}", 
                    username, ipAddress);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Track authorization failures - 403 Forbidden responses
        /// </summary>
        public async Task TrackAuthorizationFailureAsync(string userId, string resource, string ipAddress)
        {
            _logger.LogWarning(
                "SECURITY: Authorization failed - UserId: {UserId}, Resource: {Resource}, IP: {IpAddress}",
                userId, resource, ipAddress);

            // Alert on repeated failures from same user/IP
            _logger.LogWarning("ALERT: Unauthorized access attempt to {Resource} by {UserId} from {IpAddress}",
                resource, userId, ipAddress);

            await Task.CompletedTask;
        }

        /// <summary>
        /// Track rate limiting events
        /// </summary>
        public async Task TrackRateLimitAsync(string identifier, int requestsInWindow)
        {
            _logger.LogInformation(
                "MONITORING: Rate limit hit - Identifier: {Identifier}, Requests: {RequestsInWindow}/minute",
                identifier, requestsInWindow);

            // Alert if severe
            if (requestsInWindow > 150) // More than 50% over limit
            {
                _logger.LogWarning(
                    "ALERT: Rate limit spike - {Identifier} with {RequestsInWindow} requests/minute",
                    identifier, requestsInWindow);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Track exceptions and errors
        /// </summary>
        public async Task TrackExceptionAsync(string exceptionType, string message, string stackTrace, string userId = null)
        {
            var severity = DetermineExceptionSeverity(exceptionType);

            _logger.Log(
                severity == "Critical" ? LogLevel.Critical : LogLevel.Error,
                "EXCEPTION [{Severity}]: Type: {ExceptionType}, Message: {Message}, UserId: {UserId}\nStackTrace: {StackTrace}",
                severity, exceptionType, message, userId ?? "N/A", stackTrace);

            if (severity == "Critical")
            {
                _logger.LogCritical("ALERT: Critical exception detected - {ExceptionType}: {Message}", exceptionType, message);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Track endpoint performance
        /// </summary>
        public async Task TrackEndpointPerformanceAsync(string endpoint, string method, long durationMs, int statusCode)
        {
            var logLevel = statusCode >= 500 ? LogLevel.Error : 
                          statusCode >= 400 ? LogLevel.Warning : 
                          LogLevel.Information;

            _logger.Log(
                logLevel,
                "PERFORMANCE: {Method} {Endpoint} - Status: {StatusCode}, Duration: {DurationMs}ms",
                method, endpoint, statusCode, durationMs);

            // Alert on slow endpoints
            if (durationMs > 5000) // 5 seconds
            {
                _logger.LogWarning(
                    "ALERT: Slow endpoint - {Method} {Endpoint} took {DurationMs}ms",
                    method, endpoint, durationMs);
            }

            // Alert on errors
            if (statusCode >= 500)
            {
                _logger.LogError(
                    "ALERT: Server error - {Method} {Endpoint} returned {StatusCode}",
                    method, endpoint, statusCode);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Track database query performance
        /// </summary>
        public async Task TrackDatabaseQueryAsync(string query, long durationMs, bool success)
        {
            var truncatedQuery = query.Length > 100 ? query.Substring(0, 100) + "..." : query;

            if (success)
            {
                _logger.LogInformation(
                    "DATABASE: Query completed in {DurationMs}ms - {Query}",
                    durationMs, truncatedQuery);
            }
            else
            {
                _logger.LogError(
                    "DATABASE: Query failed after {DurationMs}ms - {Query}",
                    durationMs, truncatedQuery);
            }

            // Alert on slow queries
            if (durationMs > 1000) // 1 second
            {
                _logger.LogWarning(
                    "ALERT: Slow database query ({DurationMs}ms) - {Query}",
                    durationMs, truncatedQuery);
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Track suspicious activity patterns
        /// </summary>
        public async Task TrackSuspiciousActivityAsync(string activityType, string description, string userId = null, string ipAddress = null)
        {
            var severity = DetermineSuspiciousActivitySeverity(activityType);

            var logLevel = severity switch
            {
                "Critical" => LogLevel.Critical,
                "High" => LogLevel.Error,
                "Medium" => LogLevel.Warning,
                _ => LogLevel.Warning
            };

            _logger.Log(
                logLevel,
                "SECURITY [{Severity}]: Suspicious Activity - Type: {ActivityType}, Description: {Description}, UserId: {UserId}, IP: {IpAddress}",
                severity, activityType, description, userId ?? "Unknown", ipAddress ?? "Unknown");

            // Specific alerts
            if (severity == "Critical")
            {
                _logger.LogCritical(
                    "ALERT: CRITICAL - {ActivityType}: {Description} by {UserId}",
                    activityType, description, userId ?? "Unknown");
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Get metrics summary (placeholder for now)
        /// </summary>
        public async Task<MonitoringMetrics> GetMetricsSummaryAsync(DateTime from, DateTime to)
        {
            // In production, query metrics database or Application Insights
            var metrics = new MonitoringMetrics
            {
                TotalRequests = 0,
                FailedAuthentications = 0,
                AuthorizationFailures = 0,
                RateLimitHits = 0,
                ExceptionCount = 0,
                AverageResponseTimeMs = 0,
                HttpErrors = 0
            };

            return await Task.FromResult(metrics);
        }

        /// <summary>
        /// Determine exception severity level
        /// </summary>
        private string DetermineExceptionSeverity(string exceptionType)
        {
            return exceptionType switch
            {
                "OutOfMemoryException" => "Critical",
                "StackOverflowException" => "Critical",
                "ExecutionEngineException" => "Critical",
                "IOException" when exceptionType.Contains("database") => "High",
                "TimeoutException" => "High",
                "AuthorizationException" => "Medium",
                "ValidationException" => "Low",
                _ => "Medium"
            };
        }

        /// <summary>
        /// Determine suspicious activity severity
        /// </summary>
        private string DetermineSuspiciousActivitySeverity(string activityType)
        {
            return activityType switch
            {
                "MultipleFailedLogins" => "High",
                "BruteForceAttempt" => "Critical",
                "PrivilegeEscalation" => "Critical",
                "UnauthorizedAccess" => "High",
                "DataExfiltration" => "Critical",
                "SQLInjectionAttempt" => "Critical",
                "XSSAttempt" => "High",
                "RateLimitExceeded" => "Medium",
                _ => "Medium"
            };
        }
    }
}
