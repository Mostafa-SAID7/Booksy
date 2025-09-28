using Booksy.Models.DTOs.Response.Books;

namespace Booksy.Models.DTOs.Response.Category
{
    public class CategoryResponse
    {
        public int Id { get; set; }          // From BaseEntity
        public string Name { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }

        // Optionally, you can expose related books if needed
        public List<BookResponse> Books { get; set; } = new List<BookResponse>();
    }
}
