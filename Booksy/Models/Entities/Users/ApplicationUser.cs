using Booksy.Models.Entities.Orders;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.Entities.Users
{
    public class ApplicationUser : IdentityUser
    {
        // 🔹 Basic Info
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        [MaxLength(200)]
        public string? ProfilePictureUrl { get; set; }

        // 🔹 Address Info
        [MaxLength(200)]
        public string? Street { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? State { get; set; }

        [MaxLength(20)]
        public string? ZipCode { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        // 🔹 Personal Info
        [MaxLength(10)]
        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(50)]
        public string? PreferredLanguage { get; set; } = "en";

        // 🔹 Security & Activity
        public bool TwoFactorEnabled { get; set; } // Already in IdentityUser, but you can track usage
        public DateTime? LastLoginDate { get; set; }
        public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;

        [MaxLength(50)]
        public string? TimeZone { get; set; }

        public bool IsActive { get; set; } = true; // soft delete / active flag

        // 🔹 Relationships
        public ICollection<Cart>? Carts { get; set; }
        public ICollection<Order>? Orders { get; set; }

        // 🔹 Preferences
        public bool ReceiveNewsletter { get; set; } = true;
        public string? ThemePreference { get; set; } // "light" / "dark" / "system"
    }
}
