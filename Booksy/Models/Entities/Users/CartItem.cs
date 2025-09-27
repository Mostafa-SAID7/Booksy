using Booksy.Models.Entities.Books;
using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.Entities.Users
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public int Quantity { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;
    }
}
