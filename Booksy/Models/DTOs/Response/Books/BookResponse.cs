namespace Booksy.Models.DTOs.Response.Books
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? CoverImageUrl { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
    }
}
