using System.ComponentModel.DataAnnotations;

namespace Booksy.Features.Categories.DTOs
{
    public class CategoryCreateRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(120)]
        public string? Slug { get; set; }
    }
}
