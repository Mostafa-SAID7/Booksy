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
            // Database Context
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(
                     configuration.GetConnectionString("DefaultConnection"),
                     sqlOptions =>
                     {
                         sqlOptions.EnableRetryOnFailure(
                             maxRetryCount: 5,
                             maxRetryDelay: TimeSpan.FromSeconds(10),
                             errorNumbersToAdd: null
                         );
                     }
                 )
             );

            // AutoMapper - Register all profiles from assembly
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(Program).Assembly);
            });

            // Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 6;
                option.Password.RequireNonAlphanumeric = false;
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
            
            // File Upload Service
            services.AddScoped<IFileUploadService, FileUploadService>();

            // DB Initializer
            services.AddScoped<IDBInitializer, DBInitializer>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            // Email Sender
            services.AddTransient<IEmailSender, EmailSender>();

            // Stripe Config
            services.Configure<StripeSettings>(configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];

            return services;
        }
    }
}
