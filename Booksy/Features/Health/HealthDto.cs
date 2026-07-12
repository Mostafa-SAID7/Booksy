namespace Booksy.Features.Health;

public class HealthDto
{
    public string Status { get; set; } = "healthy";
    public string Version { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
    public string Uptime { get; set; } = string.Empty;
    public long UptimeSeconds { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime CheckedAt { get; set; }
    public DatabaseHealthDto Database { get; set; } = new();
}

public class DatabaseHealthDto
{
    public string Status { get; set; } = string.Empty;
    public string Provider { get; set; } = "PostgreSQL";
    public long ResponseMs { get; set; }
}
