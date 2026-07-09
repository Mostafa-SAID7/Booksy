using FluentValidation;
using Booksy.Features.Tags.DTOs;

namespace Booksy.Features.Tags.Validators
{
    /// <summary>
    /// Validator for CreateTagRequest
    /// </summary>
    public class CreateTagValidator : AbstractValidator<TagCreateRequest>
    {
        public CreateTagValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tag name is required")
                .MaximumLength(100).WithMessage("Tag name cannot exceed 100 characters")
                .MinimumLength(1).WithMessage("Tag name cannot be empty")
                .Matches(@"^[a-zA-Z0-9\s\-]+$").WithMessage("Tag name can only contain letters, numbers, spaces, and hyphens");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Tag description cannot exceed 500 characters")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}
