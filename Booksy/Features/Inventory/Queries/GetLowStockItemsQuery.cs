using Booksy.Core.Interfaces;
using Booksy.Features.Books.DTOs;

namespace Booksy.Features.Inventory.Queries;

/// <summary>
/// Query to get low stock items
/// </summary>
public class GetLowStockItemsQuery : IQuery<List<BookResponse>>
{
    public int Threshold { get; set; } = 10;
}
