using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;
using MediatR;

namespace Booksy.Features.Authentication.Commands;

/// <summary>
/// Handler for confirming email
/// </summary>
public class ConfirmEmailCommandHandler : ICommandHandler<ConfirmEmailCommand, Unit>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ConfirmEmailCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(
        ConfirmEmailCommand request,
        CancellationToken cancellationToken)
    {
        // Find user by ID
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new NotFoundException($"User with ID '{request.UserId}' not found");
        }

        // Confirm email
        var result = await _userManager.ConfirmEmailAsync(user, request.Token);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BusinessException($"Email confirmation failed: {errors}");
        }

        return Unit.Value;
    }
}


