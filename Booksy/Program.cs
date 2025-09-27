using Booksy.Extensions;
using Booksy.Services;
using Booksy.Utility.DBInitializer;
using Booksy.Utility.Settings;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddCustomSwagger();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// CORS
builder.Services.AddCustomCors();

// JWT
builder.Services.AddCustomJwtAuth(builder.Configuration);

// Application services (DbContext, Identity, Repos, Stripe, Email)
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS
app.UseCustomCors();

// Auth
app.UseAuthentication();
app.UseAuthorization();

// DB Initializer
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider.GetService<IDBInitializer>();
    service?.Initialize();
}

app.MapControllers();
app.Run();
