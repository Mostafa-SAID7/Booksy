using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Booksy.Features.Roles.Commands;

/// <summary>
/// Handler for creating a role
/// </summary>
public class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, RoleDto>
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public CreateRoleCommandHandler(RoleManager<IdentityRole> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        // Check if role exists
        var roleExists = await _roleManager.RoleExistsAsync(request.Name);
        if (roleExists)
        {
            throw new BusinessException($"Role '{request.Name}' already exists");
        }

        // Create role
        var role = new IdentityRole
        {
            Name = request.Name,
            NormalizedName = request.Name.ToUpper()
        };

        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BusinessException($"Failed to create role: {errors}");
        }

        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty,
            Description = request.Description
        };
    }
}
