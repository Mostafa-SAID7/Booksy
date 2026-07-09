using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using MediatR;

namespace Booksy.Features.Roles.Commands;

/// <summary>
/// Handler for deleting a role
/// </summary>
public class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand, Unit>
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public DeleteRoleCommandHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        // Get role
        var role = await _roleManager.FindByIdAsync(request.RoleId);
        if (role == null)
        {
            throw new NotFoundException($"Role with ID {request.RoleId} not found");
        }

        // Delete role
        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BusinessException($"Failed to delete role: {errors}");
        }

        return Unit.Value;
    }
}
