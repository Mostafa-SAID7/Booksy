using Booksy.Models.Entities.Books;
using Booksy.Models.Entities.Orders;
using Booksy.Models.Entities.Promotions;
using Booksy.Models.Entities.Users;
using Booksy.Repositories;
using Booksy.Repositories.IRepositories;
using Booksy.Services;
using Booksy.Utility.DBInitializer;
using Booksy.Utility.Settings;
using Booksy.Infrastructure.FileUpload;
using Booksy.Common.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;

namespace Booksy.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database Context — PostgreSQL connection string from:
            // 1. Replit PG* env vars
            // 2. User Secrets (ConnectionStrings:DefaultConnection)
            // 3. appsettings.json ConnectionStrings:DefaultConnection
            var pgHost = Environment.GetEnvironmentVariable("PGHOST");
            var pgPort = Environment.GetEnvironmentVariable("PGPORT") ?? "5432";
            var pgDb = Environment.GetEnvironmentVariable("PGDATABASE");
            var pgUser = Environment.GetEnvironmentVariable("PGUSER");
            var pgPass = Environment.GetEnvironmentVariable("PGPASSWORD");

            string connectionString;
            if (!string.IsNullOrEmpty(pgHost) && !string.IsNullOrEmpty(pgDb))
            {
                connectionString = $"Host={pgHost};Port={pgPort};Database={pgDb};Username={pgUser};Password={pgPass};SSL Mode=Disable;Trust Server Certificate=true;";
            }
            else
            {
                // Try to get from configuration (includes user secrets and appsettings)
                connectionString = configuration.GetConnectionString("DefaultConnection");
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("No database connection string found. Set 'ConnectionStrings:DefaultConnection' in appsettings.json or user secrets.");
                }
            }

            // PostgreSQL only
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString,
                    npgsqlOptions =>
                    {
                        npgsqlOptions.CommandTimeout(30);
                    }));

            // AutoMapper - Register all profiles from assembly
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(Program).Assembly);
            });

            // Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                // Password requirements - stronger policy
                option.Password.RequiredLength = 12;
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireUppercase = true;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequiredUniqueChars = 4;
                
                // Lockout policy - prevent brute force
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                option.Lockout.MaxFailedAccessAttempts = 5;
                option.Lockout.AllowedForNewUsers = true;
                
                // User requirements
                option.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Cookie Config
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Customer/Home/NotFoundPage";
            });


            // Unit of Work Pattern - Centralized repository management
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repositories - Only generic repositories (old specialized ones removed)
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IRepository<Author>, Repository<Author>>();
            services.AddScoped<IRepository<Book>, Repository<Book>>();
            services.AddScoped<IRepository<Tag>, Repository<Tag>>();
            services.AddScoped<IRepository<Cart>, Repository<Cart>>();
            services.AddScoped<IRepository<CartItem>, Repository<CartItem>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();
            services.AddScoped<IRepository<OrderItem>, Repository<OrderItem>>();
            services.AddScoped<IRepository<Promotion>, Repository<Promotion>>();
            services.AddScoped<IRepository<ApplicationUser>, Repository<ApplicationUser>>();
            services.AddScoped<IRepository<UserOTP>, Repository<UserOTP>>();
            services.AddScoped<IRepository<Booksy.Models.Entities.Books.Review>, Repository<Booksy.Models.Entities.Books.Review>>();

            // Services
            // Query Service - Centralized aggregations and filtering
            services.AddScoped<IQueryService, QueryService>();
            
            // Validation Service - Centralized business rule validation
            services.AddScoped<IValidationService, ValidationService>();
            
            // Slug Service - Centralized slug generation
            services.AddScoped<ISlugService, SlugService>();
            
            // Authorization Service - Security & access control
            services.AddScoped<Booksy.Security.IAuthorizationService, Booksy.Security.AuthorizationService>();
            
            // File Upload Service
            services.AddScoped<IFileUploadService, FileUploadService>();

            // DB Initializer
            services.AddScoped<IDBInitializer, DBInitializer>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            // Email Sender
            services.AddTransient<IEmailSender, EmailSender>();

            // Monitoring Service
            services.AddScoped<Booksy.Infrastructure.Monitoring.IMonitoringService, Booksy.Infrastructure.Monitoring.MonitoringService>();
            
            // Alerting Service
            services.AddScoped<Booksy.Infrastructure.Monitoring.IAlertingService, Booksy.Infrastructure.Monitoring.AlertingService>();

            // Stripe Config — prefer STRIPE_SECRET_KEY env var, fall back to appsettings
            var stripeSecretKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY")
                ?? configuration["Stripe:SecretKey"];
            services.Configure<StripeSettings>(configuration.GetSection("Stripe"));
            if (!string.IsNullOrWhiteSpace(stripeSecretKey))
            {
                StripeConfiguration.ApiKey = stripeSecretKey;
            }

            // Elasticsearch — full-text search
            services.AddElasticsearch(configuration);

            return services;
        }
    }
}
