using System.ComponentModel.DataAnnotations;

namespace Booksy.Features.Carts.DTOs
{
    public class CartUpdateRequest
    {
        [Required]
        public Guid BookId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
