using Booksy.Core.Interfaces;

namespace Booksy.Features.Reports.Queries;

/// <summary>
/// Query to get top selling books
/// </summary>
public class GetTopBooksQuery : IQuery<List<TopBookDto>>
{
    public int Limit { get; set; } = 10;
}

/// <summary>
/// DTO for top book data
/// </summary>
public class TopBookDto
{
    public Guid BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int TotalSold { get; set; }
    public decimal TotalRevenue { get; set; }
}
