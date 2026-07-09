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
                context.Response.Headers.Add("X-Frame-Options", "DENY");

                // ==================== MIME TYPE SNIFFING PROTECTION ====================
                // Prevent browsers from MIME-sniffing a response away from the declared Content-Type
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

                // ==================== XSS PROTECTION (Legacy) ====================
                // Enable XSS protection in older browsers that support this header
                context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");

                // ==================== CONTENT SECURITY POLICY ====================
                // Comprehensive CSP to prevent XSS, data injection, and other attacks
                context.Response.Headers.Add("Content-Security-Policy",
                    // Default: restrict to same-origin unless explicitly allowed
                    "default-src 'self'; " +
                    
                    // Scripts: only from same-origin (no inline scripts)
                    "script-src 'self'; " +
                    
                    // Styles: allow from same-origin and unsafe-inline (needed for dynamic styling)
                    // Plus trusted CDNs for bootstrap, material-ui, etc.
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
                    
                    // API connections: only to same-origin (all fetch, XHR, websockets)
                    "connect-src 'self'; " +
                    
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
                    "media-src 'self'; " +
                    
                    // Upgrade insecure requests automatically
                    "upgrade-insecure-requests; " +
                    
                    // Block all mixed content
                    "block-all-mixed-content");

                // ==================== STRICT TRANSPORT SECURITY ====================
                // Force HTTPS for all future connections
                // max-age: 1 year, includeSubDomains: apply to all subdomains, preload: allow in browser preload lists
                if (context.Request.IsHttps || context.Request.Host.Host == "localhost")
                {
                    context.Response.Headers.Add("Strict-Transport-Security",
                        "max-age=31536000; includeSubDomains; preload");
                }

                // ==================== REFERRER POLICY ====================
                // Control how much referrer information is shared with external sites
                // strict-origin-when-cross-origin: send origin only for cross-origin requests
                context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");

                // ==================== PERMISSIONS POLICY ====================
                // Disable dangerous APIs and features not needed by the application
                context.Response.Headers.Add("Permissions-Policy",
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
                // COEP: Cross-Origin-Embedder-Policy (isolate resources)
                context.Response.Headers.Add("Cross-Origin-Embedder-Policy", "require-corp");
                
                // COOP: Cross-Origin-Opener-Policy (isolate from opener)
                context.Response.Headers.Add("Cross-Origin-Opener-Policy", "same-origin");

                // ==================== X-PERMITTED-CROSS-DOMAIN-POLICIES ====================
                // Restrict Flash/PDF cross-domain policies
                context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");

                await next();
            });

            return app;
        }
    }
}
