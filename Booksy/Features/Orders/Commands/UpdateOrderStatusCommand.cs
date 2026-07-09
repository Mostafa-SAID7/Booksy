using Booksy.Core.Interfaces;

using Booksy.Models.Enums;

using MediatR;


namespace Booksy.Features.Orders.Commands;



/// <summary>

/// Command to update order status

/// </summary>

public class UpdateOrderStatusCommand : ICommand<Unit>

{

    public Guid OrderId { get; set; }

    public OrderStatus Status { get; set; }

}




