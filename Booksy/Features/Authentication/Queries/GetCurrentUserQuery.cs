using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;

namespace Booksy.Features.Authentication.Queries;

/// <summary>
/// Query to get current authenticated user
/// </summary>
public class GetCurrentUserQuery : IQuery<UserProfileResponse>
{
    public string UserId { get; set; } = string.Empty;
}
