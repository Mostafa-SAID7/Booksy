using AutoMapper;
using Booksy.Features.Carts.DTOs;
using Booksy.Models.Entities.Orders;
using Booksy.Models.Entities.Users;

namespace Booksy.Features.Checkouts.Mappings;

/// <summary>
/// AutoMapper profiles for Checkouts feature
/// </summary>
public class CheckoutsMappingProfile : Profile
{
    public CheckoutsMappingProfile()
    {
        // Entities to DTOs
        CreateMap<Cart, CheckoutCartDto>();
    }
}
