namespace Booksy.Features.Carts.DTOs;

/// <summary>
/// DTO for checkout cart response
/// </summary>
public class CheckoutCartDto
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public List<CartItemDto> CartItems { get; set; } = new();
    public decimal Total { get; set; }
}

/// <summary>
/// DTO for cart item
/// </summary>
public class CartItemDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
