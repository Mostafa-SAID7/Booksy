using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using Booksy.Models.Entities.Orders;
using Microsoft.Extensions.Logging;

namespace Booksy.Features.Reports.Queries;

/// <summary>
/// Handler for getting top selling books
/// </summary>
public class GetTopBooksQueryHandler : IQueryHandler<GetTopBooksQuery, List<TopBookDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetTopBooksQueryHandler> _logger;

    public GetTopBooksQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetTopBooksQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<TopBookDto>> Handle(GetTopBooksQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching top {Limit} selling books", request.Limit);

        // Use async enumeration - load order items and books
        var orderItems = await _unitOfWork.OrderItems.GetAsync();
        var books = await _unitOfWork.Books.GetAsync();

        var bookList = books.ToList();

        var topBooks = orderItems
            .GroupBy(oi => oi.BookId)
            .Select(g => new TopBookDto
            {
                BookId = g.Key,
                Title = bookList.FirstOrDefault(b => b.Id == g.Key)?.Title ?? "Unknown",
                TotalSold = g.Sum(oi => oi.Quantity),
                TotalRevenue = g.Sum(oi => (decimal)oi.Quantity * oi.Price)
            })
            .OrderByDescending(x => x.TotalSold)
            .Take(request.Limit)
            .ToList();

        _logger.LogInformation("Top books retrieved: {Count} books", topBooks.Count);

        return topBooks;
    }
}
