namespace Booksy.DTOs.Response.Books
{
    public class BookWithRelatedResponse
    {
        public Book Book { get; set; } = null!;
        public List<Book> RelatedBooks { get; set; } = new();
        public List<Book> TopTraffic { get; set; } = new();
        public List<Book> SimilarBooks { get; set; } = new();
    }
}
