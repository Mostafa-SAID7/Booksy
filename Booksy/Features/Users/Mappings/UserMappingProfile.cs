using AutoMapper;
using Booksy.Features.Users.Commands;
using Booksy.Features.Authentication.DTOs;
using Booksy.Models.Entities.Users;

namespace Booksy.Features.Users.Mappings;

/// <summary>
/// AutoMapper profiles for User feature
/// </summary>
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        // Commands to Entities
        CreateMap<UpdateProfileCommand, ApplicationUser>();

        // Entities to DTOs
        CreateMap<ApplicationUser, UserProfileResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
    }
}
