using Booksy.Extensions;
using Booksy.Models.Entities.Users;
using Booksy.Utility.DBInitializer;
using Booksy.Utility.Settings;
using Booksy.Common.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// ------------------------- Configuration -------------------------
var configuration = builder.Configuration;
var services = builder.Services;

// ------------------------- Services -------------------------
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

const string defaultCulture = "en";

var supportedCultures = new[]
{
    new CultureInfo(defaultCulture),
    new CultureInfo("ar")
};

builder.Services.Configure<RequestLocalizationOptions>(options => {
    options.DefaultRequestCulture = new RequestCulture(defaultCulture);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
// Add Controllers with filters
builder.Services.AddControllers(options =>
{
    // Register global filters
    options.Filters.Add<Booksy.Filters.ValidateModelFilter>();
})
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Request size limits
builder.Services.Configure<Microsoft.AspNetCore.Server.IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 10 * 1024 * 1024;  // 10 MB
});

builder.Services.Configure<Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 10 * 1024 * 1024;  // 10 MB
});

// Memory cache for rate limiting
builder.Services.AddMemoryCache();

// Rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = Microsoft.AspNetCore.RateLimiting.PartitionedRateLimiter.Create<HttpContext, string>(context =>
        Microsoft.AspNetCore.RateLimiting.RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.User.Identity?.Name 
                ?? context.Connection.RemoteIpAddress?.ToString() 
                ?? "anonymous",
            factory: _ => new Microsoft.AspNetCore.RateLimiting.FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            }));

    // Strict limits for authentication endpoints (prevent brute force)
    options.AddPolicy("auth-limit", context =>
        Microsoft.AspNetCore.RateLimiting.RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
            factory: _ => new Microsoft.AspNetCore.RateLimiting.FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1)
            }));

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// Add Custom CORS
builder.Services.AddCustomCors(configuration);

// Add Swagger
builder.Services.AddCustomSwagger();

// Add JWT Authentication
builder.Services.AddCustomJwtAuth(configuration);

// Add CQRS Services (MediatR, FluentValidation, Validators, Behaviors)
builder.Services.AddCqrsServices();

// Add Application Services (EF, Identity, Repositories, Stripe, Email, etc.)
builder.Services.AddApplicationServices(configuration);

// ------------------------- App -------------------------
var app = builder.Build();

// ------------------------- Middleware -------------------------
// Custom middleware pipeline (exception handling, logging, performance)
app.UseCustomMiddleware();

// Performance monitoring middleware
app.UsePerformanceMonitoring();

// Serve static files
app.UseStaticFiles();

// Enable CORS
app.UseCustomCors();

// Enable Swagger (for dev environment)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booksy API V1");
        c.RoutePrefix = string.Empty; // Swagger at root
        c.DocumentTitle = "Booksy API Documentation";
        c.DisplayRequestDuration(); // Shows request duration
        c.DefaultModelsExpandDepth(-1); // Collapse schemas by default
    });
}

// Use HTTPS Redirection
app.UseHttpsRedirection();

// Rate limiting middleware
app.UseRateLimiter();

// Use Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Request localization
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

// Map Controllers
app.MapControllers();

// ------------------------- DB Initialization -------------------------
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
    dbInitializer.Initialize();
}

// ------------------------- Run -------------------------
app.Run();
