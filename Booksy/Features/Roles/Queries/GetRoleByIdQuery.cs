using Booksy.Core.Interfaces;

namespace Booksy.Features.Roles.Queries;

/// <summary>
/// Query to get a role by ID
/// </summary>
public class GetRoleByIdQuery : IQuery<RoleDetailsDto>
{
    public string RoleId { get; set; } = string.Empty;
}
