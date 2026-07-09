using AutoMapper;
using Booksy.Features.Carts.DTOs;
using Booksy.Models.Entities.Users;

namespace Booksy.Features.Carts.Mappings;

/// <summary>
/// AutoMapper profiles for Cart feature
/// </summary>
public class CartMappingProfile : Profile
{
    public CartMappingProfile()
    {
        // Entities to DTOs
        CreateMap<Cart, CartResponse>();
        CreateMap<CartItem, CartItemResponse>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Book.Price));
    }
}
