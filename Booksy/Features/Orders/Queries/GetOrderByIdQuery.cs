using Booksy.Core.Interfaces;
using Booksy.Features.Orders.DTOs;

namespace Booksy.Features.Orders.Queries;

/// <summary>
/// Query to get a single order by ID
/// </summary>
public class GetOrderByIdQuery : IQuery<OrderResponse>
{
    public Guid Id { get; set; }
}
