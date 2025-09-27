using Booksy.Models.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.Entities.Users
{
    public class Cart: BaseEntity
    {
      

        public string UserId { get; set; } = string.Empty; // FK to Identity User
        public ApplicationUser User { get; set; } = null!;

        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
