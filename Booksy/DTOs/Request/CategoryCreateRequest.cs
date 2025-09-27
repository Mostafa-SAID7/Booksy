using System.ComponentModel.DataAnnotations;

namespace Booksy.DTOs.Request
{
    public class CategoryCreateRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
