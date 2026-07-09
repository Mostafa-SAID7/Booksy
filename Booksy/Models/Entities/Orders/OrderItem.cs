using Booksy.Models.Entities.Books;
using Booksy.Models.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.Entities.Orders
{
    public class OrderItem : BaseEntity, IAuditableEntity, ISoftDeletable
    {
        [Required]
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        [Required]
        public Guid BookId { get; set; }
        public Book Book { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public bool IsDeleted { get; set; } = false;
        public int Price { get;  set; }
    }
}
