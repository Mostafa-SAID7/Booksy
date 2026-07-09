using Booksy.Core.Interfaces;

namespace Booksy.Features.Roles.Queries;

/// <summary>
/// Query to get user roles
/// </summary>
public class GetUserRolesQuery : IQuery<List<string>>
{
    public string UserId { get; set; } = string.Empty;
}
