using FluentValidation;
using Booksy.Features.Inventory.Commands;

namespace Booksy.Features.Inventory.Validators;

/// <summary>
/// Validator for RestockItemCommand
/// </summary>
public class RestockItemValidator : AbstractValidator<RestockItemCommand>
{
    public RestockItemValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty()
            .WithMessage("Book ID is required");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");

        RuleFor(x => x.Supplier)
            .NotEmpty()
            .WithMessage("Supplier is required")
            .MaximumLength(100)
            .WithMessage("Supplier name must not exceed 100 characters");
    }
}
