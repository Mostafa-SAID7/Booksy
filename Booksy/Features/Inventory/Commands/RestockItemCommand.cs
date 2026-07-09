using Booksy.Core.Interfaces;
using MediatR;

namespace Booksy.Features.Inventory.Commands;

/// <summary>
/// Command to restock an item
/// </summary>
public class RestockItemCommand : ICommand<Unit>
{
    public Guid BookId { get; set; }
    public int Quantity { get; set; }
    public string Supplier { get; set; } = string.Empty;
}
