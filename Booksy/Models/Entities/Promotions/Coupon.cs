using Booksy.Models.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.Entities.Promotions
{
    public class Coupon : BaseEntity, IAuditableEntity, ISoftDeletable
    {
        [Required, MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        public bool IsUsed { get; set; } = false;

        [Required]
        public DateTime ExpiryDate { get; set; }

        public bool IsDeleted { get; set; } = false;

        // 🔹 Foreign Key
        public int PromotionId { get; set; }
        public Promotion Promotion { get; set; } = null!;
    }
}
