using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.DTOs.Request.Category
{
    public class CategoryRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Status { get; set; }
    }
}
