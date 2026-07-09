using Booksy.Core.Interfaces;
using MediatR;

namespace Booksy.Features.Roles.Commands;

/// <summary>
/// Command to delete a role
/// </summary>
public class DeleteRoleCommand : ICommand<Unit>
{
    public string RoleId { get; set; } = string.Empty;
}
