using FluentValidation;
using Booksy.Features.Checkouts.Commands;

namespace Booksy.Features.Checkouts.Validators;

/// <summary>
/// Validator for ProcessCheckoutCommand
/// </summary>
public class CheckoutValidator : AbstractValidator<ProcessCheckoutCommand>
{
    public CheckoutValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required");

        RuleFor(x => x.ShippingAddress)
            .NotEmpty()
            .WithMessage("Shipping address is required")
            .MaximumLength(500)
            .WithMessage("Shipping address must not exceed 500 characters");

        RuleFor(x => x.BillingAddress)
            .NotEmpty()
            .WithMessage("Billing address is required")
            .MaximumLength(500)
            .WithMessage("Billing address must not exceed 500 characters");

        RuleFor(x => x.PaymentMethod)
            .NotEmpty()
            .WithMessage("Payment method is required")
            .Must(x => new[] { "CreditCard", "DebitCard", "PayPal", "BankTransfer" }.Contains(x))
            .WithMessage("Invalid payment method");
    }
}

/// <summary>
/// Validator for ApplyCouponCommand
/// </summary>
public class CouponValidator : AbstractValidator<ApplyCouponCommand>
{
    public CouponValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required");

        RuleFor(x => x.CouponCode)
            .NotEmpty()
            .WithMessage("Coupon code is required")
            .MaximumLength(50)
            .WithMessage("Coupon code must not exceed 50 characters");
    }
}
