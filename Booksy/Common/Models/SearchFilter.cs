namespace Booksy.Common.Models;

/// <summary>
/// Centralized filter criteria for paginated queries
/// Supports pagination, search, sorting, and filtering
/// </summary>
public class SearchFilter
{
    /// <summary>
    /// Page number (1-indexed, default: 1)
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Items per page (default: 10, max: 100)
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Search term for full-text or property search
    /// </summary>
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Sorting specification: "fieldName" for ascending or "fieldName:desc" for descending
    /// Supports multiple sort fields: ["field1", "field2:desc"]
    /// </summary>
    public List<string>? SortBy { get; set; }

    /// <summary>
    /// Additional filter criteria as key-value pairs
    /// Example: { "status", "active" }, { "minPrice", 10.50 }
    /// </summary>
    public Dictionary<string, object>? FilterCriteria { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public SearchFilter()
    {
    }

    /// <summary>
    /// Constructor with pagination parameters
    /// </summary>
    public SearchFilter(int pageNumber, int pageSize)
    {
        PageNumber = Math.Max(1, pageNumber);
        PageSize = Math.Min(Math.Max(1, pageSize), 100); // Enforce min 1, max 100
    }

    /// <summary>
    /// Validate the search filter
    /// </summary>
    public bool IsValid(out List<string> errors)
    {
        errors = new List<string>();

        if (PageNumber < 1)
            errors.Add("PageNumber must be at least 1");

        if (PageSize < 1)
            errors.Add("PageSize must be at least 1");

        if (PageSize > 100)
            errors.Add("PageSize cannot exceed 100");

        return errors.Count == 0;
    }

    /// <summary>
    /// Calculate skip count for database queries
    /// </summary>
    public int GetSkipCount() => (PageNumber - 1) * PageSize;

    /// <summary>
    /// Get sort field and direction from sort specification
    /// </summary>
    public List<(string Field, bool IsDescending)> ParseSortBy()
    {
        var result = new List<(string Field, bool IsDescending)>();

        if (SortBy == null || SortBy.Count == 0)
            return result;

        foreach (var sort in SortBy)
        {
            if (string.IsNullOrWhiteSpace(sort))
                continue;

            if (sort.EndsWith(":desc", StringComparison.OrdinalIgnoreCase))
            {
                var fieldName = sort.Substring(0, sort.Length - 5);
                result.Add((fieldName, true));
            }
            else if (sort.EndsWith(":asc", StringComparison.OrdinalIgnoreCase))
            {
                var fieldName = sort.Substring(0, sort.Length - 4);
                result.Add((fieldName, false));
            }
            else
            {
                result.Add((sort, false)); // Default to ascending
            }
        }

        return result;
    }
}
