using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Booksy.Features.Roles.Queries;

/// <summary>
/// Handler for getting all roles
/// </summary>
public class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, List<RoleDetailsDto>>
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetAllRolesQueryHandler(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<List<RoleDetailsDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = _roleManager.Roles.ToList();

        var result = new List<RoleDetailsDto>();
        foreach (var role in roles)
        {
            var users = await _userManager.GetUsersInRoleAsync(role.Name ?? string.Empty);
            var userCount = users.Count;
            result.Add(new RoleDetailsDto
            {
                Id = role.Id,
                Name = role.Name ?? string.Empty,
                UserCount = userCount
            });
        }

        return result;
    }
}
