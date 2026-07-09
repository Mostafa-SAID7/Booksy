using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booksy.Extensions
{
    public static class CorsExtensions
    {
        private const string PolicyName = "BooksyPolicy";

        public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration = null)
        {
            // Use configuration if provided, otherwise fall back to defaults based on environment
            var allowedOrigins = configuration?.GetSection("Cors:AllowedOrigins").Get<string[]>() 
                ?? GetDefaultAllowedOrigins();

            services.AddCors(options =>
            {
                options.AddPolicy(name: PolicyName, policy =>
                {
                    policy.WithOrigins(allowedOrigins)
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
