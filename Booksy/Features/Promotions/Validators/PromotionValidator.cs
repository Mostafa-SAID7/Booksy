using FluentValidation;
using Booksy.Features.Promotions.Commands;

namespace Booksy.Features.Promotions.Validators;

/// <summary>
/// Validator for CreatePromotionCommand
/// </summary>
public class CreatePromotionValidator : AbstractValidator<CreatePromotionCommand>
{
    public CreatePromotionValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Promotion code is required")
            .MaximumLength(50)
            .WithMessage("Promotion code must not exceed 50 characters");

        RuleFor(x => x.Value)
            .GreaterThan(0)
            .WithMessage("Promotion value must be greater than 0");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required")
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage("Start date must be in the future");

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .WithMessage("End date is required")
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be after start date");
    }
}

/// <summary>
/// Validator for UpdatePromotionCommand
/// </summary>
public class UpdatePromotionValidator : AbstractValidator<UpdatePromotionCommand>
{
    public UpdatePromotionValidator()
    {
        RuleFor(x => x.PromotionId)
            .NotEmpty()
            .WithMessage("Promotion ID is required");

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Promotion code is required")
            .MaximumLength(50)
            .WithMessage("Promotion code must not exceed 50 characters");

        RuleFor(x => x.Value)
            .GreaterThan(0)
            .WithMessage("Promotion value must be greater than 0");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be after start date");
    }
}
