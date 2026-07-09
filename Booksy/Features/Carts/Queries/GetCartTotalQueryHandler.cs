using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Users;

namespace Booksy.Features.Carts.Queries;

/// <summary>
/// Handler for getting cart total
/// </summary>
public class GetCartTotalQueryHandler : IQueryHandler<GetCartTotalQuery, decimal>
{
    private readonly IRepository<Cart> _cartRepository;

    public GetCartTotalQueryHandler(IRepository<Cart> cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<decimal> Handle(GetCartTotalQuery request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetOneAsync(c => c.UserId == request.UserId);

        if (cart == null)
        {
            throw new NotFoundException($"Cart not found for user {request.UserId}");
        }

        return cart.Items.Sum(item => item.Book.Price * item.Quantity);
    }
}
