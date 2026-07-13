using FluentValidation;
using Booksy.Features.Authentication.Commands;
using Microsoft.Extensions.Localization;

namespace Booksy.Features.Authentication.Validators;

/// <summary>
/// Validator for RegisterCommand — validation messages are fully localised
/// via IStringLocalizer&lt;LocalizationController&gt; and respond to the
/// Accept-Language / culture-cookie on the request.
/// </summary>
public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator(IStringLocalizer<LocalizationController> localizer)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(_ => localizer["Email_Required"])
            .EmailAddress()
            .WithMessage(_ => localizer["Email_Invalid"]);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(_ => localizer["Password_Required"])
            .MinimumLength(8)
            .WithMessage(_ => localizer["Password_TooShort"])
            .Matches(@"[A-Z]")
            .WithMessage(_ => localizer["Password_NeedsUppercase"])
            .Matches(@"[a-z]")
            .WithMessage(_ => localizer["Password_NeedsLowercase"])
            .Matches(@"[0-9]")
            .WithMessage(_ => localizer["Password_NeedsDigit"])
            .Matches(@"[^a-zA-Z0-9]")
            .WithMessage(_ => localizer["Password_NeedsSpecial"]);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage(_ => localizer["Passwords_NoMatch"]);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(_ => localizer["Name_Required"])
            .MaximumLength(100)
            .WithMessage(_ => localizer["Name_TooLong"]);

        RuleFor(x => x.FirstName)
            .MaximumLength(100)
            .WithMessage(_ => localizer["FirstName_TooLong"])
            .When(x => !string.IsNullOrEmpty(x.FirstName));

        RuleFor(x => x.LastName)
            .MaximumLength(100)
            .WithMessage(_ => localizer["LastName_TooLong"])
            .When(x => !string.IsNullOrEmpty(x.LastName));
    }
}
