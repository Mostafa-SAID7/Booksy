using System.ComponentModel.DataAnnotations;

namespace Booksy.DTOs.Request.User
{
    public class ResetPasswordDTO
    {
        [Required]
        public string OTPNumber { get; set; } = string.Empty;
        public string ApplicationUserId { get; set; } = string.Empty;
    }
}
