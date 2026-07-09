using Booksy.Core.Interfaces;

using MediatR;


namespace Booksy.Features.Orders.Commands;



/// <summary>

/// Command to cancel an order

/// </summary>

public class CancelOrderCommand : ICommand<Unit>

{

    public Guid OrderId { get; set; }

}




