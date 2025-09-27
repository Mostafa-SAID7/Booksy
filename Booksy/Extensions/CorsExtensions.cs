using Microsoft.Extensions.DependencyInjection;

namespace Booksy.Extensions
{
    public static class CorsExtensions
    {
        private const string PolicyName = "_myAllowSpecificOrigins";

        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: PolicyName, policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
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
