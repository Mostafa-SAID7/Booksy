using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;

namespace Booksy.Features.Inventory.Queries;

/// <summary>
/// Handler for getting item inventory details
/// </summary>
public class GetItemInventoryQueryHandler : IQueryHandler<GetItemInventoryQuery, ItemInventoryDto>
{
    private readonly IRepository<Book> _bookRepository;

    public GetItemInventoryQueryHandler(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<ItemInventoryDto> Handle(GetItemInventoryQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId);
        if (book == null)
        {
            throw new NotFoundException($"Book with ID {request.BookId} not found");
        }

        return new ItemInventoryDto
        {
            BookId = book.Id,
            Title = book.Title,
            CurrentStock = book.Stock,
            Price = book.Price,
            InventoryValue = (decimal)book.Stock * book.Price
        };
    }
}
