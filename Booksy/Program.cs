using Booksy.Extensions;
using Booksy.Models.Entities.Users;
using Booksy.Utility.DBInitializer;
using Booksy.Utility.Settings;
using Booksy.Common.Extensions;
using Booksy.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.RateLimiting;
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
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 10 * 1024 * 1024;  // 10 MB
});

// Memory cache for rate limiting
builder.Services.AddMemoryCache();

// Rate limiting
builder.Services.AddRateLimiter(options =>
{
    // Global limiter - 100 requests per minute per user/IP
    options.OnRejected = async (context, _) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        await context.HttpContext.Response.WriteAsync("Rate limit exceeded");
    };

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    
    // Add default fixed window limiter
    options.AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 100;
        options.Window = TimeSpan.FromMinutes(1);
        options.AutoReplenishment = true;
    });

    // Strict policy for auth endpoints
    options.AddFixedWindowLimiter(policyName: "auth-limit", options =>
    {
        options.PermitLimit = 5;
        options.Window = TimeSpan.FromMinutes(1);
        options.AutoReplenishment = true;
    });
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

// Map / → index.html (must come before UseStaticFiles)
app.UseDefaultFiles();

// Serve static files
app.UseStaticFiles();

// Enable CORS
app.UseCustomCors();

// Developer exception page — only in Development to avoid leaking stack traces
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Swagger - at /swagger route
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booksy API V1");
    c.RoutePrefix = "swagger"; // Swagger at /swagger
    c.DocumentTitle = "Booksy API Documentation";
    c.DisplayRequestDuration(); // Shows request duration
    c.DefaultModelsExpandDepth(-1); // Collapse schemas by default
    c.InjectStylesheet("/css/swagger-nav.css");
    c.InjectJavascript("/js/swagger-nav.js");
});

// Rate limiting middleware
app.UseRateLimiter();

// Use Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Request localization
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

// Map Controllers
app.MapControllers();

// Serve index.html for root path (UseDefaultFiles can't run before implicit routing)
app.MapGet("/", async (IWebHostEnvironment env) =>
{
    var filePath = Path.Combine(env.WebRootPath, "index.html");
    return Results.File(filePath, "text/html");
});

// 404 fallback for unmatched routes
app.MapFallback(async context =>
{
    var path = context.Request.Path.Value ?? "";

    // API and Swagger routes return JSON 404
    if (path.StartsWith("/api", StringComparison.OrdinalIgnoreCase) ||
        path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase))
    {
        context.Response.StatusCode = 404;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"error\":\"Not found\"}");
        return;
    }

    // HTML 404 for other routes
    context.Response.StatusCode = 404;
    context.Response.ContentType = "text/html; charset=utf-8";
    var env = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
    var filePath = Path.Combine(env.WebRootPath, "404.html");
    if (File.Exists(filePath))
    {
        await context.Response.SendFileAsync(filePath);
    }
});

// ------------------------- DB Initialization -------------------------
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
    dbInitializer.Initialize();
}

// ------------------------- Run -------------------------
app.Run();
