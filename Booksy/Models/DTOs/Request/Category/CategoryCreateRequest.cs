using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.DTOs.Request.Category
{
    public class CategoryCreateRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
