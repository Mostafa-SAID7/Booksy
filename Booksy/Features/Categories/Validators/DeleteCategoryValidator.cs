using FluentValidation;
using Booksy.Features.Categories.Commands;

namespace Booksy.Features.Categories.Validators;

/// <summary>
/// Validator for DeleteCategoryCommand
/// </summary>
public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Category ID is required");
    }
}
