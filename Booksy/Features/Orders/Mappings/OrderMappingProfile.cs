using AutoMapper;
using Booksy.Features.Orders.DTOs;
using Booksy.Models.Entities.Orders;

namespace Booksy.Features.Orders.Mappings;

/// <summary>
/// AutoMapper profiles for Order feature
/// </summary>
public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        // Entities to DTOs
        CreateMap<Order, OrderResponse>();
        CreateMap<OrderItem, OrderItemResponse>()
            .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
    }
}
