using Booksy.Core.Interfaces;

namespace Booksy.Features.Roles.Commands;

/// <summary>
/// Command to update an existing role
/// </summary>
public class UpdateRoleCommand : ICommand<RoleDto>
{
    public string RoleId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
