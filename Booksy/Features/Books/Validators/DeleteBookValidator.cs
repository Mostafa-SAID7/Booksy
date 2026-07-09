using FluentValidation;
using Booksy.Features.Books.Commands;

namespace Booksy.Features.Books.Validators;

/// <summary>
/// Validator for DeleteBookCommand
/// </summary>
public class DeleteBookValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Book ID is required");
    }
}
