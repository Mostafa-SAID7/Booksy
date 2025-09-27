using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.DTOs.Request.Auth
{
    public class AuthorCreateRequest
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Bio { get; set; }
    }
}
