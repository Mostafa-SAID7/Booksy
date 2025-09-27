using Booksy.Models.Entities.Books;
using Booksy.Models.Entities.Common;
using Booksy.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.Entities.Promotions
{
    public class Promotion : BaseEntity, IAuditableEntity, ISoftDeletable
    {
        [Required, MaxLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        public PromotionType Type { get; set; } = PromotionType.Percentage;

        [Required]
        public decimal Value { get; set; } // % or fixed amount

        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsDeleted { get; set; } = false;

        // 🔹 Computed property, not mapped
        public bool IsActive => !IsDeleted && DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;

        // 🔹 Relationships
        public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    }
}
