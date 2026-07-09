using AutoMapper;
using Booksy.Features.Reports.DTOs;
using Booksy.Models.Entities.Promotions;

namespace Booksy.Features.Promotions.Mappings;

/// <summary>
/// AutoMapper profiles for Promotions feature
/// </summary>
public class PromotionMappingProfile : Profile
{
    public PromotionMappingProfile()
    {
        // Entities to DTOs
        CreateMap<Promotion, PromotionResponse>();
    }
}
