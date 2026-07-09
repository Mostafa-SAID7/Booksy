using FluentValidation;
using Booksy.Features.Reviews.Commands;

namespace Booksy.Features.Reviews.Validators;

/// <summary>
/// Validator for DeleteReviewCommand
/// </summary>
public class DeleteReviewValidator : AbstractValidator<DeleteReviewCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReviewValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Review ID is required");
    }
}
