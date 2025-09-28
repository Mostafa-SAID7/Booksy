using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.DTOs.Request.User
{
    public class UpdatePersonalInfoRequest
    {
        // 🔹 Basic Info
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        [Url]
        public string? ProfilePictureUrl { get; set; }

        // 🔹 Contact Info
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        // 🔹 Address Info
        [StringLength(200)]
        public string? Street { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? State { get; set; }

        [StringLength(20)]
        public string? ZipCode { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        // 🔹 Personal Info
        [StringLength(20)]
        public string? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        // 🔹 Preferences
        [StringLength(10)]
        public string PreferredLanguage { get; set; } = "en";

        [StringLength(20)]
        public string? ThemePreference { get; set; }

        public bool ReceiveNewsletter { get; set; }
    }
}
