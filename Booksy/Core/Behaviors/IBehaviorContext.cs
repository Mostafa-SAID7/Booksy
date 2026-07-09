namespace Booksy.Core.Behaviors;

/// <summary>
/// Shared context for behaviors in the pipeline
/// Enables behaviors to share data and coordinate
/// </summary>
public interface IBehaviorContext
{
    /// <summary>
    /// Request name
    /// </summary>
    string RequestName { get; set; }

    /// <summary>
    /// Request type (Query, Command, etc.)
    /// </summary>
    string RequestType { get; set; }

    /// <summary>
    /// Start time of request
    /// </summary>
    DateTime StartTime { get; set; }

    /// <summary>
    /// Current user ID (if authenticated)
    /// </summary>
    string? UserId { get; set; }

    /// <summary>
    /// User roles (if authenticated)
    /// </summary>
    List<string> UserRoles { get; set; }

    /// <summary>
    /// Validation errors (if any)
    /// </summary>
    Dictionary<string, string[]>? ValidationErrors { get; set; }

    /// <summary>
    /// Whether request passed validation
    /// </summary>
    bool IsValidationPassed { get; set; }

    /// <summary>
    /// Whether request is authorized
    /// </summary>
    bool IsAuthorized { get; set; }

    /// <summary>
    /// Cache key (if applicable)
    /// </summary>
    string? CacheKey { get; set; }

    /// <summary>
    /// Whether response was cached
    /// </summary>
    bool WasCached { get; set; }

    /// <summary>
    /// Custom properties for behaviors
    /// </summary>
    Dictionary<string, object> Properties { get; set; }
}

/// <summary>
/// Default implementation of behavior context
/// </summary>
public class BehaviorContext : IBehaviorContext
{
    public string RequestName { get; set; } = string.Empty;
    public string RequestType { get; set; } = string.Empty;
    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public string? UserId { get; set; }
    public List<string> UserRoles { get; set; } = new();
    public Dictionary<string, string[]>? ValidationErrors { get; set; }
    public bool IsValidationPassed { get; set; } = true;
    public bool IsAuthorized { get; set; } = true;
    public string? CacheKey { get; set; }
    public bool WasCached { get; set; }
    public Dictionary<string, object> Properties { get; set; } = new();
}
