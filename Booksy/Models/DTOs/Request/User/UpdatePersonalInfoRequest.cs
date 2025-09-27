using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.DTOs.Request.User
{
    public class UpdatePersonalInfoRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
    }
}
