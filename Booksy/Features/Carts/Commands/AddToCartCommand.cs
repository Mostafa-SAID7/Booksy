using Booksy.Core.Interfaces;

using MediatR;


namespace Booksy.Features.Carts.Commands;



/// <summary>

/// Command to add an item to cart

/// </summary>

public class AddToCartCommand : ICommand<Unit>

{

    public string UserId { get; set; } = string.Empty;

    public Guid BookId { get; set; }

    public int Quantity { get; set; }

}




