using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Booksy.Features.Roles.Queries;

/// <summary>
/// Handler for getting user roles
/// </summary>
public class GetUserRolesQueryHandler : IQueryHandler<GetUserRolesQuery, List<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUserRolesQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<List<string>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new NotFoundException($"User with ID {request.UserId} not found");
        }

        var roles = await _userManager.GetRolesAsync(user);
        return roles.ToList();
    }
}
