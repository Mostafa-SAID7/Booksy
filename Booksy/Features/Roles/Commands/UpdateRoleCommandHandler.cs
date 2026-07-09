using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Booksy.Features.Roles.Commands;

/// <summary>
/// Handler for updating a role
/// </summary>
public class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand, RoleDto>
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public UpdateRoleCommandHandler(RoleManager<IdentityRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<RoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        // Get role
        var role = await _roleManager.FindByIdAsync(request.RoleId);
        if (role == null)
        {
            throw new NotFoundException($"Role with ID {request.RoleId} not found");
        }

        // Update role
        role.Name = request.Name;
        role.NormalizedName = request.Name.ToUpper();

        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BusinessException($"Failed to update role: {errors}");
        }

        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty,
            Description = request.Description
        };
    }
}
