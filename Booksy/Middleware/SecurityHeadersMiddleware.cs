using Microsoft.AspNetCore.Builder;

namespace Booksy.Security
{
    /// <summary>
    /// Middleware to add security headers to all HTTP responses
    /// </summary>
    public static class SecurityHeadersMiddleware
    {
        /// <summary>
        /// Add security headers middleware to application
        /// </summary>
        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                // Prevent clickjacking attacks
                context.Response.Headers.Add("X-Frame-Options", "DENY");

                // Prevent MIME type sniffing
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

                // Enable XSS protection in older browsers
                context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");

                // Content Security Policy - Restrict resource loading
                context.Response.Headers.Add("Content-Security-Policy",
                    "default-src 'self'; " +
                    "script-src 'self'; " +
                    "style-src 'self' 'unsafe-inline'; " +
                    "img-src 'self' data: https:; " +
                    "font-src 'self'; " +
                    "connect-src 'self'; " +
                    "frame-ancestors 'none'; " +
                    "base-uri 'self'; " +
                    "form-action 'self'");

                // Referrer Policy - Control referrer information
                context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");

                // Permissions Policy - Disable unnecessary features
                context.Response.Headers.Add("Permissions-Policy",
                    "geolocation=(), " +
                    "microphone=(), " +
                    "camera=(), " +
                    "payment=(), " +
                    "usb=(), " +
                    "magnetometer=(), " +
                    "gyroscope=(), " +
                    "accelerometer=()");

                // Strict Transport Security - Force HTTPS
                if (context.Request.IsHttps || context.Request.Host.Host == "localhost")
                {
                    context.Response.Headers.Add("Strict-Transport-Security",
                        "max-age=31536000; includeSubDomains; preload");
                }

                await next();
            });

            return app;
        }
    }
}
