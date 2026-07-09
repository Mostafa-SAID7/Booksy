using FluentValidation;
using Booksy.Features.Orders.Commands;

namespace Booksy.Features.Orders.Validators;

/// <summary>
/// Validator for UpdateOrderStatusCommand
/// </summary>
public class UpdateOrderStatusValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("Order ID is required");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Order status is invalid");
    }
}
