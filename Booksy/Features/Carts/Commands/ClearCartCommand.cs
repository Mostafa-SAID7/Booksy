using Booksy.Core.Interfaces;
using MediatR;

namespace Booksy.Features.Carts.Commands;

/// <summary>
/// Command to clear the entire cart
/// </summary>
public class ClearCartCommand : ICommand<Unit>
{
    public string UserId { get; set; } = string.Empty;
}


