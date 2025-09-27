namespace Booksy.DTOs.Request.Books
{
    public class BookFilterRequest
    {
        public string? BookTitle { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? CategoryId { get; set; }
        public bool IsHot { get; set; } = false;
    }
}
