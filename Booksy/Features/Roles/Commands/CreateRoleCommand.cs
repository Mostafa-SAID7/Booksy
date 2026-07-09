using Booksy.Core.Interfaces;

namespace Booksy.Features.Roles.Commands;

/// <summary>
/// Command to create a new role
/// </summary>
public class CreateRoleCommand : ICommand<RoleDto>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Permissions { get; set; } = new();
}

/// <summary>
/// DTO for role
/// </summary>
public class RoleDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
