using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;
using MediatR;

namespace Booksy.Features.Roles.Commands;

/// <summary>
/// Handler for assigning role to user
/// </summary>
public class AssignRoleToUserCommandHandler : ICommandHandler<AssignRoleToUserCommand, Unit>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AssignRoleToUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Unit> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        // Get user
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new NotFoundException($"User with ID {request.UserId} not found");
        }

        // Get role
        var role = await _roleManager.FindByIdAsync(request.RoleId);
        if (role == null)
        {
            throw new NotFoundException($"Role with ID {request.RoleId} not found");
        }

        // Check if user already has role
        var hasRole = await _userManager.IsInRoleAsync(user, role.Name ?? request.RoleName);
        if (hasRole)
        {
            throw new BusinessException($"User already has role '{role.Name}'");
        }

        // Assign role to user
        var result = await _userManager.AddToRoleAsync(user, role.Name ?? request.RoleName);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BusinessException($"Failed to assign role: {errors}");
        }

        return Unit.Value;
    }
}
