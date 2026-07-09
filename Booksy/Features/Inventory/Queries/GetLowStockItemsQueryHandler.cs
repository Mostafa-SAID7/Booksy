using AutoMapper;
using Booksy.Core.Interfaces;
using Booksy.Features.Books.DTOs;
using Booksy.Models.Entities.Books;

namespace Booksy.Features.Inventory.Queries;

/// <summary>
/// Handler for getting low stock items
/// </summary>
public class GetLowStockItemsQueryHandler : IQueryHandler<GetLowStockItemsQuery, List<BookResponse>>
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IMapper _mapper;

    public GetLowStockItemsQueryHandler(IRepository<Book> bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<List<BookResponse>> Handle(GetLowStockItemsQuery request, CancellationToken cancellationToken)
    {
        // Get low stock books
        var books = await _bookRepository.GetAllAsync();
        var lowStockBooks = books.Where(b => b.Stock <= request.Threshold).ToList();

        return _mapper.Map<List<BookResponse>>(lowStockBooks);
    }
}
