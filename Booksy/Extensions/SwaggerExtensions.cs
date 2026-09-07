using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Booksy.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(o =>
            {
                // ── API Info ──────────────────────────────────────────────
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version     = "v1",
                    Title       = "Booksy API",
                    Description = """
                        ## Booksy REST API

                        Full-featured bookstore API supporting:
                        - 📚 Books, Authors, Categories & Tags
                        - 🛒 Cart & Orders with Stripe payments
                        - 👤 User authentication & roles (JWT)
                        - ⭐ Reviews & Promotions
                        - 📊 Dashboard statistics

                        ### Authentication
                        Click **Authorize** and enter: `Bearer <your_jwt_token>`
                        """,
                    Contact = new OpenApiContact
                    {
                        Name  = "Booksy Team",
                        Email = "support@booksy.com",
                        Url   = new Uri("https://booksy.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url  = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                // ── JWT Bearer Authentication ─────────────────────────────
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name        = "Authorization",
                    Type        = SecuritySchemeType.Http,
                    Scheme      = "bearer",
                    BearerFormat = "JWT",
                    In          = ParameterLocation.Header,
                    Description = "Enter your JWT token. Example: `eyJhbGci...`"
                });

                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id   = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                // ── XML Documentation ────────────────────────────────────
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    o.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                }

                // ── Swagger UI tweaks ────────────────────────────────────
                o.EnableAnnotations();
                o.DescribeAllParametersInCamelCase();

                // Sort endpoints alphabetically by tag then path
                o.OrderActionsBy(api => $"{api.GroupName}_{api.RelativePath}");
            });

            return services;
        }
    }
}
