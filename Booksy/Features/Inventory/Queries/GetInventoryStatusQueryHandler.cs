using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;

namespace Booksy.Features.Inventory.Queries;

/// <summary>
/// Handler for getting inventory status
/// </summary>
public class GetInventoryStatusQueryHandler : IQueryHandler<GetInventoryStatusQuery, InventoryStatusDto>
{
    private readonly IRepository<Book> _bookRepository;

    public GetInventoryStatusQueryHandler(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<InventoryStatusDto> Handle(GetInventoryStatusQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllAsync();

        return new InventoryStatusDto
        {
            TotalBooks = books.Count(),
            LowStockCount = books.Count(b => b.Stock > 0 && b.Stock <= 10),
            OutOfStockCount = books.Count(b => b.Stock <= 0),
            TotalInventoryValue = books.Sum(b => (decimal)b.Stock * b.Price)
        };
    }
}
