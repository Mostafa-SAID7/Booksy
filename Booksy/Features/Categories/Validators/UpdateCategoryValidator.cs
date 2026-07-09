using FluentValidation;
using Booksy.Features.Categories.Commands;

namespace Booksy.Features.Categories.Validators;

/// <summary>
/// Validator for UpdateCategoryCommand
/// </summary>
public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Category ID is required");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Category name is required")
            .MaximumLength(50)
            .WithMessage("Category name must not exceed 50 characters")
            .MinimumLength(2)
            .WithMessage("Category name must be at least 2 characters")
            .Matches(@"^[a-zA-Z0-9\s\-\&]+$")
            .WithMessage("Category name contains invalid characters. Only alphanumeric, spaces, hyphens and ampersands are allowed");
    }
}
