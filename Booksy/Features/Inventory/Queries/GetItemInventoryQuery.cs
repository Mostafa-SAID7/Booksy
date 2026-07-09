using Booksy.Core.Interfaces;
using Booksy.Features.Books.DTOs;

namespace Booksy.Features.Inventory.Queries;

/// <summary>
/// Query to get inventory for a specific item
/// </summary>
public class GetItemInventoryQuery : IQuery<ItemInventoryDto>
{
    public Guid BookId { get; set; }
}

/// <summary>
/// DTO for item inventory details
/// </summary>
public class ItemInventoryDto
{
    public Guid BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int CurrentStock { get; set; }
    public decimal Price { get; set; }
    public decimal InventoryValue { get; set; }
}
