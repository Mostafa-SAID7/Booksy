using Booksy.Core.Interfaces;
using MediatR;

namespace Booksy.Features.Inventory.Commands;

/// <summary>
/// Command to update stock for a book
/// </summary>
public class UpdateStockCommand : ICommand<Unit>
{
    public Guid BookId { get; set; }
    public int Quantity { get; set; }
    public string Reason { get; set; } = string.Empty;
}
