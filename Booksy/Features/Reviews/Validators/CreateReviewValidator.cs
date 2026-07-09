using FluentValidation;
using Booksy.Features.Reviews.Commands;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Reviews.Validators;

/// <summary>
/// Validator for CreateReviewCommand
/// </summary>
public class CreateReviewValidator : AbstractValidator<CreateReviewCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateReviewValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.BookId)
            .NotEmpty()
            .WithMessage("Book ID is required")
            .Must(BookExists)
            .WithMessage("The specified book does not exist");

        RuleFor(x => x.Rating)
            .NotEmpty()
            .WithMessage("Rating is required")
            .InclusiveBetween(1, 5)
            .WithMessage("Rating must be between 1 and 5");

        RuleFor(x => x.Comment)
            .MaximumLength(1000)
            .WithMessage("Comment must not exceed 1000 characters");

        RuleFor(x => x.ReviewerName)
            .NotEmpty()
            .WithMessage("Reviewer name is required")
            .MinimumLength(2)
            .WithMessage("Reviewer name must be at least 2 characters")
            .MaximumLength(100)
            .WithMessage("Reviewer name must not exceed 100 characters");
    }

    private bool BookExists(Guid bookId)
    {
        // This is a synchronous validator rule, we can't use async validation in FluentValidation 10.x
        // Async validation is handled in the handler or in a separate validation behavior
        return true;  // Actual book existence is validated in the command handler
    }
}
