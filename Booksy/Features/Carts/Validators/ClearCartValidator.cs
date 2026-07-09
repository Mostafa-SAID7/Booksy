using FluentValidation;
using Booksy.Features.Carts.Commands;

namespace Booksy.Features.Carts.Validators;

/// <summary>
/// Validator for ClearCartCommand
/// </summary>
public class ClearCartValidator : AbstractValidator<ClearCartCommand>
{
    public ClearCartValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required");
    }
}
