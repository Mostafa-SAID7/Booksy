using Booksy.Core.Interfaces;
using MediatR;

namespace Booksy.Features.Roles.Commands;

/// <summary>
/// Command to assign role to user
/// </summary>
public class AssignRoleToUserCommand : ICommand<Unit>
{
    public string UserId { get; set; } = string.Empty;
    public string RoleId { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
}
