using FluentValidation;
using Booksy.Features.Orders.Commands;

namespace Booksy.Features.Orders.Validators;

/// <summary>
/// Validator for CreateOrderCommand
/// </summary>
public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required");
    }
}
