using FluentValidation;
using Booksy.Features.Tags.Commands;

namespace Booksy.Features.Tags.Validators
{
    /// <summary>
    /// Validator for DeleteTagCommand
    /// </summary>
    public class DeleteTagValidator : AbstractValidator<DeleteTagCommand>
    {
        public DeleteTagValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Tag ID is required");
        }
    }
}
