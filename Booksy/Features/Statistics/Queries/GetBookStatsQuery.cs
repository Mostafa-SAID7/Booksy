using Booksy.Core.Interfaces;

namespace Booksy.Features.Statistics.Queries;

/// <summary>
/// Query to get book statistics
/// </summary>
public class GetBookStatsQuery : IQuery<BookStatsDto> { }

/// <summary>
/// DTO for book statistics
/// </summary>
public class BookStatsDto
{
    public int TotalBooks { get; set; }
    public int TotalAuthors { get; set; }
    public int TotalCategories { get; set; }
    public int OutOfStockBooks { get; set; }
    public int LowStockBooks { get; set; }
    public decimal AverageBookPrice { get; set; }
    public int AverageRating { get; set; }
}
