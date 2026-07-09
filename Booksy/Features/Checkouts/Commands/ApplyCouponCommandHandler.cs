using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Orders;
using Booksy.Models.Entities.Promotions;
using Booksy.Models.Entities.Users;
using Booksy.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Booksy.Features.Checkouts.Commands;

/// <summary>
/// Handler for applying coupon to checkout
/// </summary>
public class ApplyCouponCommandHandler : ICommandHandler<ApplyCouponCommand, ApplyCouponResultDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ApplyCouponCommandHandler> _logger;

    public ApplyCouponCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<ApplyCouponCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ApplyCouponResultDto> Handle(ApplyCouponCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Applying coupon '{CouponCode}' for user: {UserId}", request.CouponCode, request.UserId);

        // Validate input
        if (string.IsNullOrWhiteSpace(request.UserId))
            throw new BusinessException("User ID is required");
        if (string.IsNullOrWhiteSpace(request.CouponCode))
            throw new BusinessException("Coupon code is required");

        // Get user's cart - use GetOneAsync instead of GetAllAsync
        var userCart = await _unitOfWork.Carts.GetOneAsync(c => c.UserId == request.UserId);
        if (userCart == null)
        {
            _logger.LogWarning("Cart not found for user: {UserId}", request.UserId);
            throw new NotFoundException("Cart not found");
        }

        // Get promotion by code - use GetOneAsync instead of GetAllAsync
        var promotion = await _unitOfWork.Promotions.GetOneAsync(p => p.Code == request.CouponCode);
        if (promotion == null)
        {
            _logger.LogWarning("Coupon code '{CouponCode}' not found", request.CouponCode);
            return new ApplyCouponResultDto
            {
                IsValid = false,
                DiscountAmount = 0,
                Message = "Coupon code not found"
            };
        }

        // Validate promotion
        if (!promotion.IsActive)
        {
            _logger.LogInformation("Coupon '{CouponCode}' is inactive", request.CouponCode);
            return new ApplyCouponResultDto
            {
                IsValid = false,
                DiscountAmount = 0,
                Message = "Coupon code is inactive"
            };
        }

        if (promotion.StartDate > DateTime.UtcNow || promotion.EndDate < DateTime.UtcNow)
        {
            _logger.LogInformation("Coupon '{CouponCode}' is outside valid date range", request.CouponCode);
            return new ApplyCouponResultDto
            {
                IsValid = false,
                DiscountAmount = 0,
                Message = "Coupon code is not valid at this time"
            };
        }

        // Calculate discount based on promotion value (percentage or fixed amount)
        var cartTotal = userCart.Items.Sum(ci => (decimal)ci.Book.Price * ci.Quantity);
        var discountAmount = promotion.Type == Models.Enums.PromotionType.Percentage 
            ? (cartTotal * promotion.Value) / 100m 
            : promotion.Value;

        _logger.LogInformation(
            "Coupon '{CouponCode}' applied successfully. Discount: {DiscountAmount}",
            request.CouponCode,
            discountAmount);

        return new ApplyCouponResultDto
        {
            IsValid = true,
            DiscountAmount = discountAmount,
            Message = $"Coupon applied successfully. Discount: {promotion.Value}{(promotion.Type == Models.Enums.PromotionType.Percentage ? "%" : " USD")}"
        };
    }
}
