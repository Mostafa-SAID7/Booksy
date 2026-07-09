using Booksy.Core.Interfaces;

namespace Booksy.Features.Roles.Queries;

/// <summary>
/// Query to get all roles
/// </summary>
public class GetAllRolesQuery : IQuery<List<RoleDetailsDto>> { }

/// <summary>
/// DTO for role details
/// </summary>
public class RoleDetailsDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int UserCount { get; set; }
}
