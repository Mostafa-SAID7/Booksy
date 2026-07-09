using FluentValidation;
using Booksy.Features.Authors.Commands;

namespace Booksy.Features.Authors.Validators;

/// <summary>
/// Validator for DeleteAuthorCommand
/// </summary>
public class DeleteAuthorValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Author ID is required");
    }
}
