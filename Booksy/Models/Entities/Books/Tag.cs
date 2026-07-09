using System.ComponentModel.DataAnnotations;
using Booksy.Models.Entities.Common;

namespace Booksy.Models.Entities.Books
{
    /// <summary>
    /// Tag entity for categorizing books with flexible many-to-many relationship
    /// </summary>
    public class Tag : BaseEntity, IAuditableEntity, ISoftDeletable
    {
        /// <summary>
        /// Name of the tag (e.g., "Fiction", "Mystery", "Romance")
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// URL-friendly slug for the tag
        /// </summary>
        [Required, MaxLength(120)]
        public string Slug { get; set; } = string.Empty;

        /// <summary>
        /// Description of the tag for better context
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Soft delete flag
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Many-to-Many: Books associated with this tag
        /// </summary>
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
