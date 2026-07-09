using Booksy.Core.Interfaces;
using Booksy.Features.Carts.DTOs;

namespace Booksy.Features.Checkouts.Queries;

/// <summary>
/// Query to get checkout cart
/// </summary>
public class GetCheckoutCartQuery : IQuery<CheckoutCartDto>
{
    public string UserId { get; set; } = string.Empty;
}
