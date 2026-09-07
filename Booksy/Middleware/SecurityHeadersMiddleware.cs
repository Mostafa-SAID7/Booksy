using Microsoft.AspNetCore.Builder;

namespace Booksy.Security
{
    /// <summary>
    /// Middleware to add security headers to all HTTP responses
    /// Production-hardened security configuration
    /// </summary>
    public static class SecurityHeadersMiddleware
    {
        /// <summary>
        /// Add security headers middleware to application
        /// Implements defense-in-depth with multiple security headers
        /// </summary>
        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                // ==================== CLICKJACKING PROTECTION ====================
                // Prevent the page from being embedded in iframes
                context.Response.Headers.Append("X-Frame-Options", "DENY");

                // ==================== MIME TYPE SNIFFING PROTECTION ====================
                // Prevent browsers from MIME-sniffing a response away from the declared Content-Type
                context.Response.Headers.Append("X-Content-Type-Options", "nosniff");

                // ==================== XSS PROTECTION (Legacy) ====================
                // Enable XSS protection in older browsers that support this header
                context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");

                // ==================== CONTENT SECURITY POLICY ====================
                // Comprehensive CSP to prevent XSS, data injection, and other attacks
                context.Response.Headers.Append("Content-Security-Policy",
                    // Default: restrict to same-origin unless explicitly allowed
                    "default-src 'self'; " +
                    
                    // Scripts: allow from same-origin + inline scripts for frontend pages
                    "script-src 'self' 'unsafe-inline' https://cdn.jsdelivr.net; " +
                    
                    // Styles: allow from same-origin, unsafe-inline, and trusted CDNs
                    "style-src 'self' 'unsafe-inline' " +
                        "https://cdn.jsdelivr.net " +
                        "https://cdnjs.cloudflare.com " +
                        "https://fonts.googleapis.com; " +
                    
                    // Images: allow from same-origin, data URIs, and HTTPS sources
                    "img-src 'self' data: https:; " +
                    
                    // Fonts: allow from same-origin and Google Fonts
                    "font-src 'self' " +
                        "https://fonts.gstatic.com " +
                        "https://cdn.jsdelivr.net " +
                        "https://cdnjs.cloudflare.com; " +
                    
                    // API connections: same-origin + Supabase for frontend calls
                    "connect-src 'self' https://*.supabase.co; " +
                    
                    // Prevent framing (no iframes allowed from external sources)
                    "frame-ancestors 'none'; " +
                    
                    // Only allow redirects to same-origin
                    "base-uri 'self'; " +
                    
                    // Form submissions only to same-origin
                    "form-action 'self'; " +
                    
                    // Frame embedding only from same-origin
                    "frame-src 'self'; " +
                    
                    // Manifest and worker scripts from same-origin only
                    "manifest-src 'self'; " +
                    "worker-src 'self'; " +
                    
                    // No plugins allowed (Flash, Java, etc.)
                    "object-src 'none'; " +
                    
                    // Media (audio/video) from same-origin only
                    "media-src 'self'");

                // ==================== STRICT TRANSPORT SECURITY ====================
                // Force HTTPS for all future connections
                // max-age: 1 year, includeSubDomains: apply to all subdomains, preload: allow in browser preload lists
                if (context.Request.IsHttps || context.Request.Host.Host == "localhost")
                {
                    context.Response.Headers.Append("Strict-Transport-Security",
                        "max-age=31536000; includeSubDomains; preload");
                }

                // ==================== REFERRER POLICY ====================
                // Control how much referrer information is shared with external sites
                // strict-origin-when-cross-origin: send origin only for cross-origin requests
                context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");

                // ==================== PERMISSIONS POLICY ====================
                // Disable dangerous APIs and features not needed by the application
                context.Response.Headers.Append("Permissions-Policy",
                    "geolocation=(), " +           // No location tracking
                    "microphone=(), " +             // No microphone access
                    "camera=(), " +                 // No camera access
                    "payment=(), " +                // No payment API
                    "usb=(), " +                    // No USB access
                    "magnetometer=(), " +           // No magnetometer sensor
                    "gyroscope=(), " +              // No gyroscope sensor
                    "accelerometer=(), " +          // No accelerometer sensor
                    "ambient-light-sensor=(), " +   // No light sensor
                    "autoplay=(), " +               // No autoplay
                    "encrypted-media=(), " +        // No encrypted media
                    "fullscreen=(), " +             // No fullscreen API
                    "picture-in-picture=(), " +     // No picture-in-picture
                    "sync-xhr=()");                 // No sync XHR

                // ==================== CROSS-ORIGIN POLICIES ====================
                // COEP: 'unsafe-none' — 'require-corp' blocks CDNs (Google Fonts, Bootstrap Icons)
                // that don't serve Cross-Origin-Resource-Policy headers
                context.Response.Headers.Append("Cross-Origin-Embedder-Policy", "unsafe-none");
                
                // COOP: isolate from opener (safe to keep)
                context.Response.Headers.Append("Cross-Origin-Opener-Policy", "same-origin-allow-popups");

                // ==================== X-PERMITTED-CROSS-DOMAIN-POLICIES ====================
                // Restrict Flash/PDF cross-domain policies
                context.Response.Headers.Append("X-Permitted-Cross-Domain-Policies", "none");

                await next();
            });

            return app;
        }
    }
}
