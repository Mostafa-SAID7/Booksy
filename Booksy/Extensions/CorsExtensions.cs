using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booksy.Extensions
{
    public static class CorsExtensions
    {
        private const string PolicyName = "BooksyPolicy";

        public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: PolicyName, policy =>
                {
                    // Allow any Replit proxy domain plus explicit configured origins
                    var configuredOrigins = configuration?.GetSection("Cors:AllowedOrigins").Get<string[]>()
                        ?? GetDefaultAllowedOrigins();

                    policy.SetIsOriginAllowed(origin =>
                    {
                        if (string.IsNullOrEmpty(origin)) return false;
                        // Allow Replit preview/proxy domains
                        if (origin.EndsWith(".replit.dev", StringComparison.OrdinalIgnoreCase)) return true;
                        if (origin.EndsWith(".replit.app", StringComparison.OrdinalIgnoreCase)) return true;
                        if (origin.EndsWith(".repl.co", StringComparison.OrdinalIgnoreCase)) return true;
                        // Allow explicitly configured origins
                        return configuredOrigins.Contains(origin, StringComparer.OrdinalIgnoreCase);
                    })
                    .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS")
                    .WithHeaders("Content-Type", "Authorization", "X-Requested-With")
                    .WithExposedHeaders("X-Total-Count", "X-Total-Pages")
                    .AllowCredentials();
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
        {
            app.UseCors(PolicyName);
            return app;
        }

        private static string[] GetDefaultAllowedOrigins()
        {
            // In production, configure via appsettings.Production.json
            // For now, restrict to development localhost only
            return new[] { "https://localhost:7001", "https://localhost:5001" };
        }
    }
}
