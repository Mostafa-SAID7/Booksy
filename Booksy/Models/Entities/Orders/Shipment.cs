using Booksy.Models.Entities.Common;
using Booksy.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.Entities.Orders
{
    public class Shipment : BaseEntity, IAuditableEntity, ISoftDeletable
    {
        // Linked Order
        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        // Carrier Information
        [MaxLength(100)]
        public string CarrierName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string CarrierTrackingId { get; set; } = string.Empty;

        // Shipment Dates
        public DateTime ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }

        // Shipment Status
        public ShipmentStatus Status { get; set; } = ShipmentStatus.Pending;

        public bool IsDeleted { get; set; } = false;
    }
}
