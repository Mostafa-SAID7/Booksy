using System.ComponentModel.DataAnnotations;

namespace Booksy.DTOs.Request
{
    public class CartItemCreateRequest
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
