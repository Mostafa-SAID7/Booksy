using Booksy.Core.Interfaces;
using MediatR;

namespace Booksy.Features.Authentication.Commands;

/// <summary>
/// Command to reset user password
/// </summary>
public class ResetPasswordCommand : ICommand<Unit>
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}


