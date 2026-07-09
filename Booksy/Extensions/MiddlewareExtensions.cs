using Booksy.Middleware;
using Booksy.Security;

namespace Booksy.Extensions
{
    /// <summary>
    /// Extension methods for registering custom middleware in the HTTP pipeline
    /// 
    /// After Duplication Analysis (DUPLICATION_ANALYSIS_AND_RESOLUTION.md):
    /// - Removed: PerformanceMiddleware (Superseded by PerformanceBehavior)
    /// - Removed: RequestLoggingMiddleware (Superseded by LoggingBehavior)
    /// - Kept: ExceptionHandlingMiddleware (Global safety net)
    /// 
    /// Performance & request logging now handled by CQRS behavior pipeline
    /// which provides more granular control and richer context.
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Add global exception handling middleware
        /// Must be registered first in the pipeline
        /// Catches all unhandled exceptions and returns standardized Result wrapper
        /// </summary>
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }

        /// <summary>
        /// Register all custom middleware in recommended order
        /// 
        /// Note: Performance monitoring and request logging moved to CQRS behavior pipeline
        /// for better separation of concerns and elimination of duplicate logging
        /// </summary>
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
        {
            // Exception handling first (outermost) - global safety net
            app.UseExceptionHandling();
            
            // Security headers must be early in the pipeline
            app.UseSecurityHeaders();

            return app;
        }
    }
}
