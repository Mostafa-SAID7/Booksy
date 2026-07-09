using Booksy.Models.Entities.Common;
using Booksy.Models.Entities.Orders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booksy.Models.Entities.Books
{
    public class Book : BaseEntity, IAuditableEntity, ISoftDeletable
    {
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required, MaxLength(220)]
        public string Slug { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        [Required]
        public int Stock { get; set; }

        public string? CoverImageUrl { get; set; }
        public decimal Discount { get; set; } = 0;

        // Optional: tracking book views
        public int Traffic { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;

        // Relationships

        // Many-to-One: Category (GUID FK)
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        // Many-to-One: Author (GUID FK)
        public Guid AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        // One-to-Many: Reviews
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        // Many-to-Many: Tags
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        // Optional: Orders containing this book
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public string ISBN { get; set; }
        public int Quantity { get;  set; }
    }
}
