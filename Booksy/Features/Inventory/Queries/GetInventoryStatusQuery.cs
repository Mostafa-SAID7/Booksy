using Booksy.Core.Interfaces;

namespace Booksy.Features.Inventory.Queries;

/// <summary>
/// Query to get inventory status DTO
/// </summary>
public class GetInventoryStatusQuery : IQuery<InventoryStatusDto> { }

/// <summary>
/// DTO for inventory status
/// </summary>
public class InventoryStatusDto
{
    public int TotalBooks { get; set; }
    public int LowStockCount { get; set; }
    public int OutOfStockCount { get; set; }
    public decimal TotalInventoryValue { get; set; }
}
