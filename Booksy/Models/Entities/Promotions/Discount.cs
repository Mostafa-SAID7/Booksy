using Booksy.Models.Entities.Books;
using Booksy.Models.Entities.Common;
using Booksy.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.Entities.Promotions
{
    public class Discount : BaseEntity, IAuditableEntity, ISoftDeletable
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DiscountType Type { get; set; } = DiscountType.Percentage;

        [Required]
        public decimal Value { get; set; }  // % or fixed amount

        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsDeleted { get; set; } = false;

        // 🔹 Computed property
        public bool IsActive => !IsDeleted && DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;

        // 🔹 Optional: apply to specific books
        public int? BookId { get; set; }
        public Book? Book { get; set; }

        // 🔹 Foreign Key
        public int PromotionId { get; set; }
        public Promotion Promotion { get; set; } = null!;
    }
}
