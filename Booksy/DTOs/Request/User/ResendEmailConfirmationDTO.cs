using System.ComponentModel.DataAnnotations;

namespace Booksy.DTOs.Request.User
{
    public class ResendEmailConfirmationDTO
    {
        [Required]
        public string EmailOrUserName { get; set; } = string.Empty;
    }
}
