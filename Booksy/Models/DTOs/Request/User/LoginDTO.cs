using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.DTOs.Request.User
{
    public class LoginDTO
    {
        [Required]
        public string EmailOrUserName { get; set; } = string.Empty;
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
