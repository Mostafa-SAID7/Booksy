namespace Booksy.Models.DTOs.Response.Carts
{
    public class CartItemResponse
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
