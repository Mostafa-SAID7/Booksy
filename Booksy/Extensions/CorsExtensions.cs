using Microsoft.Extensions.DependencyInjection;

namespace Booksy.Extensions
{
    public static class CorsExtensions
    {
        private const string PolicyName = "_myAllowSpecificOrigins";

        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            var allowedOrigins = new[] { "http://localhost:5500" };
            services.AddCors(options =>
            {
                options.AddPolicy(name: PolicyName, policy =>
                {
                    policy.WithOrigins(allowedOrigins) 
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials(); // now allowed
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
        {
            app.UseCors(PolicyName);
            return app;
        }
    }
}
