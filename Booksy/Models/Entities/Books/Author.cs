using System.ComponentModel.DataAnnotations;
using Booksy.Models.Entities.Common;

namespace Booksy.Models.Entities.Books
{
    public class Author : BaseEntity, IAuditableEntity, ISoftDeletable
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Bio { get; set; }

        public bool IsDeleted { get; set; } = false;

        // Many-to-Many: Books
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
