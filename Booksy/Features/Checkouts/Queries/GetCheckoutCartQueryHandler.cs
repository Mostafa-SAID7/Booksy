using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Carts.DTOs;
using Booksy.Models.Entities.Orders;
using Booksy.Models.Entities.Users;

namespace Booksy.Features.Checkouts.Queries;

/// <summary>
/// Handler for getting checkout cart
/// </summary>
public class GetCheckoutCartQueryHandler : IQueryHandler<GetCheckoutCartQuery, CheckoutCartDto>
{
    private readonly IRepository<Cart> _cartRepository;
    private readonly IMapper _mapper;

    public GetCheckoutCartQueryHandler(IRepository<Cart> cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public async Task<CheckoutCartDto> Handle(GetCheckoutCartQuery request, CancellationToken cancellationToken)
    {
        var carts = await _cartRepository.GetAllAsync();
        var userCart = carts.FirstOrDefault(c => c.UserId == request.UserId);
        if (userCart == null)
        {
            throw new NotFoundException("Cart not found");
        }

        return _mapper.Map<CheckoutCartDto>(userCart);
    }
}
