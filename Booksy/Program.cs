using Booksy.Extensions;
using Booksy.Models.Entities.Users;
using Booksy.Utility.DBInitializer;
using Booksy.Utility.Settings;
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
// Add Controllers
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Add Custom CORS
builder.Services.AddCustomCors();

// Add Swagger
builder.Services.AddCustomSwagger();

// Add JWT Authentication
builder.Services.AddCustomJwtAuth(configuration);

// Add Application Services (EF, Identity, Repositories, Stripe, Email, etc.)
builder.Services.AddApplicationServices(configuration);

// ------------------------- App -------------------------
var app = builder.Build();

// ------------------------- Middleware -------------------------

// Enable CORS
app.UseCustomCors();
// Cookie settings

// Enable Swagger (for dev environment)
if (app.Environment.IsDevelopment())
{
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

// Use Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map Controllers
app.MapControllers();

// ------------------------- DB Initialization -------------------------
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
    dbInitializer.Initialize(); // <- synchronous
}
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


// ------------------------- Run -------------------------
app.Run();
