namespace Booksy.Features.Carts.DTOs
{
    public class CartResponse
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public List<CartItemResponse> Items { get; set; } = new List<CartItemResponse>();
        public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);
    }
}
