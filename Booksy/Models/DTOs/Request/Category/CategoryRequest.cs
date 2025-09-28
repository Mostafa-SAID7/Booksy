using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.DTOs.Request.Category
{
    public class CategoryRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
