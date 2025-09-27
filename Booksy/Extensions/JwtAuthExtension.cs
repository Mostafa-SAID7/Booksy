using Booksy.Utility.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

            // Resolve settings via Options pattern
            var serviceProvider = services.BuildServiceProvider();
            var jwtSettings = serviceProvider.GetRequiredService<IOptions<JwtSettings>>().Value;

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });

            return services;
        }
    }
}
