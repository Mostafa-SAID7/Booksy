using FluentValidation;
using Booksy.Features.Authentication.Commands;

namespace Booksy.Features.Authentication.Validators;

/// <summary>
/// Validator for LoginCommand
/// </summary>
public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email format is invalid");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}
