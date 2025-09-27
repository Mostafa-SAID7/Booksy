using Booksy.Models.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.Entities.Users
{
    public class Setting : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Key { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string Value { get; set; } = string.Empty;
    }
}
