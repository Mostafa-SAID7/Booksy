using Booksy.Infrastructure.Monitoring;
using System.Diagnostics;

namespace Booksy.Middleware
{
    /// <summary>
    /// Middleware to track API endpoint performance
    /// Logs all request/response times and status codes
    /// </summary>
    public class PerformanceMonitoringMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PerformanceMonitoringMiddleware> _logger;

        public PerformanceMonitoringMiddleware(RequestDelegate next, ILogger<PerformanceMonitoringMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IMonitoringService monitoringService)
        {
            var stopwatch = Stopwatch.StartNew();
            var originalBodyStream = context.Response.Body;

            try
            {
                // Call next middleware
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();

                // Track performance
                var endpoint = context.Request.Path.Value;
                var method = context.Request.Method;
                var statusCode = context.Response.StatusCode;
                var durationMs = stopwatch.ElapsedMilliseconds;

                await monitoringService.TrackEndpointPerformanceAsync(endpoint, method, durationMs, statusCode);
            }
        }
    }

    /// <summary>
    /// Extension method to register performance monitoring middleware
    /// </summary>
    public static class PerformanceMonitoringExtensions
    {
        public static IApplicationBuilder UsePerformanceMonitoring(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PerformanceMonitoringMiddleware>();
        }
    }
}
