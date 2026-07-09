using System.ComponentModel.DataAnnotations;

namespace Booksy.Features.Books.DTOs
{
    public class BookUpdateRequest 
    {
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        public int Stock { get; set; } = 0;

        public string? Description { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        public IFormFile? CoverImage { get; set; }
        public string ISBN { get; set; } // <- Add this

    }
}
