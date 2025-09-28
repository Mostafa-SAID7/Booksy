using Booksy.Models.DTOs.Response.Books;

namespace Booksy.Models.DTOs.Response.Category
{
    public class CategoryResponse
    {
        public int Id { get; set; }          // From BaseEntity
        public string Name { get; set; } = string.Empty;

    }
}
