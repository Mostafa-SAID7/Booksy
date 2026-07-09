using FluentValidation;
using Booksy.Features.Inventory.Commands;

namespace Booksy.Features.Inventory.Validators;

/// <summary>
/// Validator for UpdateStockCommand
/// </summary>
public class UpdateStockValidator : AbstractValidator<UpdateStockCommand>
{
    public UpdateStockValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty()
            .WithMessage("Book ID is required");

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithMessage("Quantity is required");

        RuleFor(x => x.Reason)
            .NotEmpty()
            .WithMessage("Reason is required")
            .MaximumLength(500)
            .WithMessage("Reason must not exceed 500 characters");
    }
}
