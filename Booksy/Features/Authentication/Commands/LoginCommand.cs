using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;

namespace Booksy.Features.Authentication.Commands;

/// <summary>
/// Command to login a user
/// </summary>
public class LoginCommand : ICommand<UserProfileResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
