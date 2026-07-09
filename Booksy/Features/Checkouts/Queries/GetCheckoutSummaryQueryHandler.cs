using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Orders;
using Booksy.Models.Entities.Users;

namespace Booksy.Features.Checkouts.Queries;

/// <summary>
/// Handler for getting checkout summary
/// </summary>
public class GetCheckoutSummaryQueryHandler : IQueryHandler<GetCheckoutSummaryQuery, CheckoutSummaryDto>
{
    private readonly IRepository<Cart> _cartRepository;

    public GetCheckoutSummaryQueryHandler(IRepository<Cart> cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<CheckoutSummaryDto> Handle(GetCheckoutSummaryQuery request, CancellationToken cancellationToken)
    {
        var carts = await _cartRepository.GetAllAsync();
        var userCart = carts.FirstOrDefault(c => c.UserId == request.UserId);
        if (userCart == null)
        {
            throw new NotFoundException("Cart not found");
        }

        var subtotal = userCart.Items.Sum(ci => (decimal)ci.Book.Price * ci.Quantity);
        var tax = subtotal * 0.1m; // 10% tax
        var shipping = 5m; // Fixed shipping

        return new CheckoutSummaryDto
        {
            ItemCount = userCart.Items.Sum(ci => ci.Quantity),
            Subtotal = subtotal,
            Tax = tax,
            Shipping = shipping,
            Discount = 0,
            Total = subtotal + tax + shipping
        };
    }
}
