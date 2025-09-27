using System.ComponentModel.DataAnnotations;
using Booksy.Models.Entities.Common;

namespace Booksy.Models.Entities.Books
{
    public class Category : BaseEntity, IAuditableEntity, ISoftDeletable
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        // One-to-Many: Books in this category
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
