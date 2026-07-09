using Booksy.Core.Interfaces;

using MediatR;


namespace Booksy.Features.Orders.Commands;



/// <summary>

/// Command to cancel an order

/// </summary>

public class CancelOrderCommand : ICommand<Unit>

{

    public Guid OrderId { get; set; }

    /// <summary>
    /// The ID of the user requesting the cancellation
    /// Used for ownership validation
    /// </summary>
    public string UserId { get; set; }

}




