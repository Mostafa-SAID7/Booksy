namespace Booksy.Models.DTOs.Response.Auth
{
    
        public class UserProfileResponse
        {
            // 🔹 Identity
            public string Id { get; set; } = string.Empty;
            public string UserName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public bool EmailConfirmed { get; set; }

            // 🔹 Basic Info
            public string? Name { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? ProfilePictureUrl { get; set; }

            // 🔹 Contact Info
            public string? PhoneNumber { get; set; }
            public bool PhoneNumberConfirmed { get; set; }

            // 🔹 Address Info
            public string? Street { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            public string? ZipCode { get; set; }
            public string? Country { get; set; }

            // 🔹 Personal Info
            public string? Gender { get; set; }
            public DateTime? DateOfBirth { get; set; }

            // 🔹 Preferences
            public string PreferredLanguage { get; set; } = "en";
            public string? ThemePreference { get; set; }
            public bool ReceiveNewsletter { get; set; }

            // 🔹 Activity
            public DateTime RegisteredDate { get; set; }
            public DateTime? LastLoginDate { get; set; }
            public bool IsActive { get; set; }
        }
    }


