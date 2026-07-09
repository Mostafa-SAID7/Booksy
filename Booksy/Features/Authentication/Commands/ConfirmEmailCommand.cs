using Booksy.Core.Interfaces;
using MediatR;

namespace Booksy.Features.Authentication.Commands;

/// <summary>
/// Command to confirm user email
/// </summary>
public class ConfirmEmailCommand : ICommand<Unit>
{
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}


