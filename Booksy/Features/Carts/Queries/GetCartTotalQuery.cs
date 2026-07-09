using Booksy.Core.Interfaces;

namespace Booksy.Features.Carts.Queries;

/// <summary>
/// Query to get cart total price
/// </summary>
public class GetCartTotalQuery : IQuery<decimal>
{
    public string UserId { get; set; } = string.Empty;
}
