namespace Booksy.Common.Models;

/// <summary>
/// Generic paginated response wrapper for list endpoints
/// Provides consistent API response structure with pagination metadata
/// </summary>
/// <typeparam name="T">Type of items in the paginated list</typeparam>
public class PaginatedResponse<T>
{
    /// <summary>
    /// The items for the current page
    /// </summary>
    public List<T> Items { get; set; } = new List<T>();

    /// <summary>
    /// Current page number (1-indexed)
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total count of all items (across all pages)
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    /// <summary>
    /// Whether there is a previous page
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// Whether there is a next page
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// Current page start index (0-indexed)
    /// </summary>
    public int StartIndex => (PageNumber - 1) * PageSize + 1;

    /// <summary>
    /// Current page end index (1-indexed)
    /// </summary>
    public int EndIndex => Math.Min(PageNumber * PageSize, TotalCount);

    /// <summary>
    /// Create a paginated response with the given parameters
    /// </summary>
    public PaginatedResponse(List<T> items, int pageNumber, int pageSize, int totalCount)
    {
        Items = items ?? new List<T>();
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    /// <summary>
    /// Parameterless constructor for serialization
    /// </summary>
    public PaginatedResponse()
    {
    }
}
