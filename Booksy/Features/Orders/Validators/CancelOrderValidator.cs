using FluentValidation;
using Booksy.Features.Orders.Commands;

namespace Booksy.Features.Orders.Validators;

/// <summary>
/// Validator for CancelOrderCommand
/// </summary>
public class CancelOrderValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("Order ID is required");
    }
}
