using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Booksy.Features.Roles.Mappings;

/// <summary>
/// AutoMapper profiles for Roles feature
/// </summary>
public class RolesMappingProfile : Profile
{
    public RolesMappingProfile()
    {
        // No mappings needed for roles as they use custom DTOs
    }
}
