namespace Booksy.Models.DTOs.Response.Carts
{
    public class CartResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public List<CartItemResponse> Items { get; set; } = new List<CartItemResponse>();
        public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);
    }
}
