using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Booksy.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}
