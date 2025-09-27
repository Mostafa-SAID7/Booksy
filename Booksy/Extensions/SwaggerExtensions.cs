using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Booksy.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                // API Info
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Booksy API",
                    Description = "API documentation for Booksy project",
                    Contact = new OpenApiContact
                    {
                        Name = "Booksy Team",
                        Email = "support@booksy.com",
                        Url = new Uri("https://booksy.com")
                    }
                });

                // JWT Authentication in Swagger
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by space and JWT token"
                });

                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                // Enable Swagger Annotations
                o.EnableAnnotations();

                // Optional: CamelCase parameter names
                o.DescribeAllParametersInCamelCase();
            });

            return services;
        }
    }
}
