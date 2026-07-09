using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Carts.DTOs;
using Booksy.Models.Entities.Users;

namespace Booksy.Features.Carts.Queries;

/// <summary>
/// Handler for getting user's cart
/// </summary>
public class GetCartQueryHandler : IQueryHandler<GetCartQuery, CartResponse>
{
    private readonly IRepository<Cart> _cartRepository;
    private readonly IMapper _mapper;

    public GetCartQueryHandler(
        IRepository<Cart> cartRepository,
        IMapper mapper)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
    }

    public async Task<CartResponse> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetOneAsync(c => c.UserId == request.UserId);

        if (cart == null)
        {
            // Create empty cart if it doesn't exist
            cart = new Cart { UserId = request.UserId };
        }

        return _mapper.Map<CartResponse>(cart);
    }
}
