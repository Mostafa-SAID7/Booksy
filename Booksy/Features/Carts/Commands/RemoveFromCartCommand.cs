using Booksy.Core.Interfaces;

using MediatR;


namespace Booksy.Features.Carts.Commands;



/// <summary>

/// Command to remove an item from cart

/// </summary>

public class RemoveFromCartCommand : ICommand<Unit>

{

    public string UserId { get; set; } = string.Empty;

    public Guid BookId { get; set; }

}




