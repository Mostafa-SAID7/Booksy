using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booksy.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        [Required]
        public int Stock { get; set; }

        public string? CoverImageUrl { get; set; }

        // Relationships
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
    }
}
