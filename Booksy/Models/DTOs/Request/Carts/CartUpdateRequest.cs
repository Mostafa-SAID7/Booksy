using System.ComponentModel.DataAnnotations;

namespace Booksy.Models.DTOs.Request.Carts
{
    public class CartUpdateRequest
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
