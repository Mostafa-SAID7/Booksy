using Booksy.Core.Interfaces;
using Booksy.Features.Orders.DTOs;

namespace Booksy.Features.Checkouts.Commands;

/// <summary>
/// Command to process a checkout
/// </summary>
public class ProcessCheckoutCommand : ICommand<CheckoutResultDto>
{
    public string UserId { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public string BillingAddress { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
}

/// <summary>
/// DTO for checkout result
/// </summary>
public class CheckoutResultDto
{
    public Guid OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public DateTime OrderDate { get; set; }
}
