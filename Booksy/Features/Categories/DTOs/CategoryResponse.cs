using Booksy.Features.Books.DTOs;

namespace Booksy.Features.Categories.DTOs
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }          // From BaseEntity
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
    }
}
