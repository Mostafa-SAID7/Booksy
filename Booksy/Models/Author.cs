using System.ComponentModel.DataAnnotations;

namespace Booksy.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Bio { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
