using FluentValidation;
using Booksy.Features.Authors.Commands;

namespace Booksy.Features.Authors.Validators;

/// <summary>
/// Validator for CreateAuthorCommand
/// </summary>
public class CreateAuthorValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Author name is required")
            .MaximumLength(100)
            .WithMessage("Author name must not exceed 100 characters")
            .MinimumLength(2)
            .WithMessage("Author name must be at least 2 characters");

        RuleFor(x => x.Bio)
            .MaximumLength(1000)
            .WithMessage("Author bio must not exceed 1000 characters")
            .When(x => !string.IsNullOrEmpty(x.Bio));
    }
}
