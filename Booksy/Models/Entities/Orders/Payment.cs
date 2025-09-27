using Booksy.Models.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.Entities.Orders
{
    public class Payment : BaseEntity, IAuditableEntity, ISoftDeletable
    {
        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string PaymentMethod { get; set; } = string.Empty; // e.g., Stripe, PayPal

        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;

        public bool IsDeleted { get; set; } = false;
    }
}
