using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Booksy.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty; // FK to Identity User
        public ApplicationUser User { get; set; } = null!;

        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
