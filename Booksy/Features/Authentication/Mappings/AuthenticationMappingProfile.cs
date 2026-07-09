using AutoMapper;
using Booksy.Features.Authentication.DTOs;
using Booksy.Models.Entities.Users;

namespace Booksy.Features.Authentication.Mappings;

/// <summary>
/// AutoMapper profiles for Authentication feature
/// </summary>
public class AuthenticationMappingProfile : Profile
{
    public AuthenticationMappingProfile()
    {
        // Entities to DTOs
        CreateMap<ApplicationUser, UserProfileResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
    }
}
