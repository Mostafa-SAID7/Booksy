using FluentValidation;
using Booksy.Features.Books.Commands;

namespace Booksy.Features.Books.Validators;

/// <summary>
/// Validator for CreateBookCommand
/// </summary>
public class CreateBookValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Book title is required")
            .MaximumLength(200)
            .WithMessage("Book title must not exceed 200 characters")
            .MinimumLength(2)
            .WithMessage("Book title must be at least 2 characters");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Book price must be greater than 0");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Book stock must be greater than or equal to 0");

        RuleFor(x => x.Description)
            .MaximumLength(2000)
            .WithMessage("Book description must not exceed 2000 characters")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("Category ID is required");

        RuleFor(x => x.AuthorId)
            .NotEmpty()
            .WithMessage("Author ID is required");

        RuleFor(x => x.ISBN)
            .NotEmpty()
            .WithMessage("ISBN is required")
            .Matches(@"^(?:ISBN(?:-1[03])?:? )?(?=[0-9X]{10}$|(?=(?:[0-9]+[- ]){3})[- 0-9X]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)(?:97[89][- ]?)?[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[X0-9]$")
            .WithMessage("ISBN format is invalid");
    }
}
