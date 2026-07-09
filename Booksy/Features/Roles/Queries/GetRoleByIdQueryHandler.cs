using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Booksy.Features.Roles.Queries;

/// <summary>
/// Handler for getting a role by ID
/// </summary>
public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleDetailsDto>
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetRoleByIdQueryHandler(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<RoleDetailsDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId);
        if (role == null)
        {
            throw new NotFoundException($"Role with ID {request.RoleId} not found");
        }

        var users = await _userManager.GetUsersInRoleAsync(role.Name ?? string.Empty);
        var userCount = users.Count;

        return new RoleDetailsDto
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty,
            UserCount = userCount
        };
    }
}
