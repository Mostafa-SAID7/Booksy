using FluentValidation;
using Booksy.Features.Authentication.Commands;

namespace Booksy.Features.Authentication.Validators;

/// <summary>
/// Validator for ConfirmEmailCommand
/// </summary>
public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required");

        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage("Email confirmation token is required");
    }
}
