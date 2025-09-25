using System.ComponentModel.DataAnnotations;

namespace Booksy.DTOs.Request
{
    public class ResendEmailConfirmationDTO
    {
        [Required]
        public string EmailOrUserName { get; set; } = string.Empty;
    }
}
