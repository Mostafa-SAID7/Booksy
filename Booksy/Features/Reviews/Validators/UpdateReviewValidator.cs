using FluentValidation;
using Booksy.Features.Reviews.Commands;

namespace Booksy.Features.Reviews.Validators;

/// <summary>
/// Validator for UpdateReviewCommand
/// </summary>
public class UpdateReviewValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Review ID is required");

        RuleFor(x => x.Rating)
            .NotEmpty()
            .WithMessage("Rating is required")
            .InclusiveBetween(1, 5)
            .WithMessage("Rating must be between 1 and 5");

        RuleFor(x => x.Comment)
            .MaximumLength(1000)
            .WithMessage("Comment must not exceed 1000 characters");
    }
}
