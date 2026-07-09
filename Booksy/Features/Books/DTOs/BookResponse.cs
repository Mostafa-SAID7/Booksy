using Booksy.Features.Authentication.DTOs;
using Booksy.Features.Categories.DTOs;

namespace Booksy.Features.Books.DTOs
{
    public class BookResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? CoverImageUrl { get; set; }
        public AuthorResponse Author { get; set; } = null!;
        public CategoryResponse Category { get; set; } = null!;
    }
}
