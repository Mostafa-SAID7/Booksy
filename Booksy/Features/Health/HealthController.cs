using Booksy.Common.Results;
using Booksy.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace Booksy.Features.Health;

/// <summary>
/// Health check endpoint — reports uptime, database connectivity, and version info.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Tags("Health")]
public class HealthController : ControllerBase
{
    private static readonly DateTime StartedAt = DateTime.UtcNow;

    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _env;

    public HealthController(ApplicationDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    /// <summary>
    /// Returns server uptime, database connection status, and version info.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(Result<HealthDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<HealthDto>), StatusCodes.Status503ServiceUnavailable)]
    public async Task<ActionResult<Result<HealthDto>>> Get()
    {
        var now = DateTime.UtcNow;
        var uptimeSeconds = (long)(now - StartedAt).TotalSeconds;
        var uptime = FormatUptime(now - StartedAt);

        var version = Assembly.GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion?.Split('+')[0] ?? "1.0.0";

        var dbHealth = await CheckDatabaseAsync();

        var dto = new HealthDto
        {
            Status = dbHealth.Status == "connected" ? "healthy" : "degraded",
            Version = version,
            Environment = _env.EnvironmentName,
            Uptime = uptime,
            UptimeSeconds = uptimeSeconds,
            StartedAt = StartedAt,
            CheckedAt = now,
            Database = dbHealth
        };

        var result = Result<HealthDto>.Ok(dto, dto.Status == "healthy"
            ? "All systems operational"
            : "Service degraded — database unreachable");

        return dto.Status == "healthy"
            ? Ok(result)
            : StatusCode(StatusCodes.Status503ServiceUnavailable, result);
    }

    private async Task<DatabaseHealthDto> CheckDatabaseAsync()
    {
        var sw = Stopwatch.StartNew();
        try
        {
            var canConnect = await _db.Database.CanConnectAsync();
            sw.Stop();
            return new DatabaseHealthDto
            {
                Status = canConnect ? "connected" : "unreachable",
                Provider = "PostgreSQL",
                ResponseMs = sw.ElapsedMilliseconds
            };
        }
        catch
        {
            sw.Stop();
            return new DatabaseHealthDto
            {
                Status = "error",
                Provider = "PostgreSQL",
                ResponseMs = sw.ElapsedMilliseconds
            };
        }
    }

    private static string FormatUptime(TimeSpan ts)
    {
        if (ts.TotalDays >= 1)
            return $"{(int)ts.TotalDays}d {ts.Hours}h {ts.Minutes}m";
        if (ts.TotalHours >= 1)
            return $"{ts.Hours}h {ts.Minutes}m {ts.Seconds}s";
        if (ts.TotalMinutes >= 1)
            return $"{ts.Minutes}m {ts.Seconds}s";
        return $"{ts.Seconds}s";
    }
}
