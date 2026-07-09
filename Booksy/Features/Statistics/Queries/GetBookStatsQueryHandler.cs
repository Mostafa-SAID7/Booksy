using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using Microsoft.Extensions.Logging;

namespace Booksy.Features.Statistics.Queries;

/// <summary>
/// Handler for getting book statistics
/// </summary>
public class GetBookStatsQueryHandler : IQueryHandler<GetBookStatsQuery, BookStatsDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetBookStatsQueryHandler> _logger;

    public GetBookStatsQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetBookStatsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<BookStatsDto> Handle(GetBookStatsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching book statistics");

        // Use async enumeration to avoid loading all data at once
        var books = await _unitOfWork.Books.GetAsync();
        var authors = await _unitOfWork.Authors.GetAsync();
        var categories = await _unitOfWork.Categories.GetAsync();
        var reviews = await _unitOfWork.Reviews.GetAsync();

        var bookList = books.ToList();
        var reviewList = reviews.ToList();

        _logger.LogInformation(
            "Book statistics calculated: TotalBooks={TotalBooks}, OutOfStock={OutOfStock}, LowStock={LowStock}",
            bookList.Count,
            bookList.Count(b => b.Stock <= 0),
            bookList.Count(b => b.Stock > 0 && b.Stock <= 10));

        return new BookStatsDto
        {
            TotalBooks = bookList.Count,
            TotalAuthors = authors.Count(),
            TotalCategories = categories.Count(),
            OutOfStockBooks = bookList.Count(b => b.Stock <= 0),
            LowStockBooks = bookList.Count(b => b.Stock > 0 && b.Stock <= 10),
            AverageBookPrice = bookList.Any() ? bookList.Average(b => b.Price) : 0,
            AverageRating = reviewList.Any() ? (int)reviewList.Average(r => r.Rating) : 0
        };
    }
}
