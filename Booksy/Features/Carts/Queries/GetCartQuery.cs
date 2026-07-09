using Booksy.Core.Interfaces;
using Booksy.Features.Carts.DTOs;

namespace Booksy.Features.Carts.Queries;

/// <summary>
/// Query to get user's cart
/// </summary>
public class GetCartQuery : IQuery<CartResponse>
{
    public string UserId { get; set; } = string.Empty;
}
