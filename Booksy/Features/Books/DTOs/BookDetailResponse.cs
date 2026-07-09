using Booksy.Features.Reviews.DTOs;

namespace Booksy.Features.Books.DTOs
{
    public class BookDetailResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public string? CoverImageUrl { get; set; }
        public int Traffic { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public int Quantity { get; set; }

        // Category
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategorySlug { get; set; } = string.Empty;

        // Author
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorSlug { get; set; } = string.Empty;

        // Reviews
        public List<ReviewResponse> Reviews { get; set; } = new List<ReviewResponse>();

        // Optional: price after discount
        public decimal PriceAfterDiscount => Price - (Price * (Discount / 100));
    }
}
