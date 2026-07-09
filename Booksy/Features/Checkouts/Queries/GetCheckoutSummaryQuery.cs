using Booksy.Core.Interfaces;

namespace Booksy.Features.Checkouts.Queries;

/// <summary>
/// Query to get checkout summary
/// </summary>
public class GetCheckoutSummaryQuery : IQuery<CheckoutSummaryDto>
{
    public string UserId { get; set; } = string.Empty;
}

/// <summary>
/// DTO for checkout summary
/// </summary>
public class CheckoutSummaryDto
{
    public int ItemCount { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Shipping { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
}
