using Booksy.Core.Interfaces;
using Booksy.Features.Orders.DTOs;

namespace Booksy.Features.Orders.Commands;

/// <summary>
/// Command to create a new order from cart
/// </summary>
public class CreateOrderCommand : ICommand<OrderResponse>
{
    public string UserId { get; set; } = string.Empty;
}
