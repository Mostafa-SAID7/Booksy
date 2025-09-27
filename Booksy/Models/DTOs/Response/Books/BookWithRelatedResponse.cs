using Booksy.Models.Entities.Books;

namespace Booksy.Models.DTOs.Response.Books
{
    public class BookWithRelatedResponse
    {
        public Book Book { get; set; } = null!;
        public IEnumerable<Book> RelatedBooks { get; set; } = new List<Book>();
        public IEnumerable<Book> TopTraffic { get; set; } = new List<Book>();
        public IEnumerable<Book> SimilarBooks { get; set; } = new List<Book>();
    }
}
