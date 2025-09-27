using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.DTOs.Request.Books
{
    public class BookCreateRequest
    {
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        public int Stock { get; set; } = 0;

        public string? Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public IFormFile? CoverImage { get; set; }
    }
}
