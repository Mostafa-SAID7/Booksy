using System.ComponentModel.DataAnnotations;

namespace Booksy.Models
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; } = string.Empty;

        public decimal DiscountPercentage { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive => DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;
    }
}
