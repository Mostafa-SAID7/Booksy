using Booksy.Utility.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Booksy.Extensions
{
    public static class JwtAuthExtension
    {
        public static IServiceCollection AddCustomJwtAuth(this IServiceCollection services, ConfigurationManager configuration)
        {
            // Bind JwtSettings
            services.Configure<JwtSettings>(configuration.GetSection("JWT"));

            // Read settings directly from configuration — avoids BuildServiceProvider() anti-pattern
            var jwtSettings = configuration.GetSection("JWT").Get<JwtSettings>()
                ?? throw new InvalidOperationException("JWT settings are missing from configuration.");

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                // Only require HTTPS in production (safe in dev with self-signed certs)
                o.RequireHttpsMetadata = !Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                    ?.Equals("Development", StringComparison.OrdinalIgnoreCase) ?? true;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero   // no grace period beyond ExpiryMinutes
                };
            });

            return services;
        }
    }
}
