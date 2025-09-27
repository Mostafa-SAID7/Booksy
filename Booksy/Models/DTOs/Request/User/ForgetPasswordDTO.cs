using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.DTOs.Request.User
{
    public class ForgetPasswordDTO
    {
        [Required]
        public string EmailOrUserName { get; set; } = string.Empty;
    }
}
