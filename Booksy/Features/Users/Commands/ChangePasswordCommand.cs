using Booksy.Core.Interfaces;
using MediatR;

namespace Booksy.Features.Users.Commands;

/// <summary>
/// Command to change user password
/// </summary>
public class ChangePasswordCommand : ICommand<Unit>
{
    public string UserId { get; set; } = string.Empty;
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}


