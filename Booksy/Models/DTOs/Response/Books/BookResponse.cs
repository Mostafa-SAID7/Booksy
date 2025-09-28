using Booksy.Models.DTOs.Response.Auth;
using Booksy.Models.DTOs.Response.Category;

namespace Booksy.Models.DTOs.Response.Books
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? CoverImageUrl { get; set; }
        public AuthorResponse Author { get; set; }
        public CategoryResponse Category { get; set; }
    }
}
