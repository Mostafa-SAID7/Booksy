using System.ComponentModel.DataAnnotations;

namespace Booksy.DTOs.Request
{
    public class AuthorCreateRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Bio { get; set; }
    }
}
