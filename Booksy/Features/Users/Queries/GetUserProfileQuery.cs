using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;

namespace Booksy.Features.Users.Queries;

/// <summary>
/// Query to get user profile
/// </summary>
public class GetUserProfileQuery : IQuery<UserProfileResponse>
{
    public string UserId { get; set; } = string.Empty;
}
