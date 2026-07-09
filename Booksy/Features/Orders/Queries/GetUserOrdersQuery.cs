using Booksy.Core.Interfaces;
using Booksy.Features.Orders.DTOs;

namespace Booksy.Features.Orders.Queries;

/// <summary>
/// Query to get all orders for a user
/// </summary>
public class GetUserOrdersQuery : IQuery<IEnumerable<OrderResponse>>
{
    public string UserId { get; set; } = string.Empty;
}
