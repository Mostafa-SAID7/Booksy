using Booksy.Models.DTOs.Response.Reviews;

namespace Booksy.Models.DTOs.Response.Books
{
    public class BookDetailResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public string? CoverImageUrl { get; set; }
        public int Traffic { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public int Quantity { get; set; }

        // Category
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        // Author
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = string.Empty;

        // Reviews
        public List<ReviewResponse> Reviews { get; set; } = new List<ReviewResponse>();

        // Optional: price after discount
        public decimal PriceAfterDiscount => Price - (Price * (Discount / 100));
    }
}
