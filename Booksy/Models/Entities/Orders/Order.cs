using Booksy.Models.Entities.Common;
using Booksy.Models.Entities.Users;
using Booksy.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.Entities.Orders
{
    public class Order : BaseEntity, IAuditableEntity, ISoftDeletable
    {
        [Required]
        public string UserId { get; set; } = string.Empty; // FK to ApplicationUser
        public ApplicationUser User { get; set; } = null!;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public TransactionStatus TransactionStatus { get; set; } = TransactionStatus.Pending;

        public string? TransactionId { get; set; }
        public string? SessionId { get; set; }

        public bool IsDeleted { get; set; } = false;

        // Navigation: Order Items
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Shipment? Shipment { get; set; }
        public OrderStatus OrderStatus { get; internal set; }
    }
}
