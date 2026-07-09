using Booksy.Core.Interfaces;
using MediatR;

namespace Booksy.Features.Checkouts.Commands;

/// <summary>
/// Command to apply a coupon to checkout
/// </summary>
public class ApplyCouponCommand : ICommand<ApplyCouponResultDto>
{
    public string UserId { get; set; } = string.Empty;
    public string CouponCode { get; set; } = string.Empty;
}

/// <summary>
/// DTO for apply coupon result
/// </summary>
public class ApplyCouponResultDto
{
    public bool IsValid { get; set; }
    public decimal DiscountAmount { get; set; }
    public string Message { get; set; } = string.Empty;
}
