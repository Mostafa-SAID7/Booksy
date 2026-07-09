using FluentValidation;
using Booksy.Features.Carts.Commands;

namespace Booksy.Features.Carts.Validators;

/// <summary>
/// Validator for AddToCartCommand
/// </summary>
public class AddToCartValidator : AbstractValidator<AddToCartCommand>
{
    public AddToCartValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required");

        RuleFor(x => x.BookId)
            .NotEmpty()
            .WithMessage("Book ID is required");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0")
            .LessThanOrEqualTo(1000)
            .WithMessage("Quantity must not exceed 1000");
    }
}
