using FluentValidation;
using Booksy.Features.Carts.Commands;

namespace Booksy.Features.Carts.Validators;

/// <summary>
/// Validator for RemoveFromCartCommand
/// </summary>
public class RemoveFromCartValidator : AbstractValidator<RemoveFromCartCommand>
{
    public RemoveFromCartValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required");

        RuleFor(x => x.BookId)
            .NotEmpty()
            .WithMessage("Book ID is required");
    }
}
