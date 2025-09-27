using Booksy.DTOs.Response.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;

namespace Booksy.Areas.Customer.Controllers
{
    [Area(SD.CustomerArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<CartItem> _cartItemRepository;

        public CartController(
            UserManager<ApplicationUser> userManager,
            IRepository<Cart> cartRepository,
            IRepository<CartItem> cartItemRepository)
        {
            _userManager = userManager;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }
        // GET: api/customer/cart/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
            var cart = (await _cartRepository.GetAsync(
                c => c.UserId == userId,
                includes: new List<Func<IQueryable<Cart>, IQueryable<Cart>>>
                {
                    q => q.Include(c => c.Items).ThenInclude(i => i.Book)
                }
            )).FirstOrDefault();

            if (cart == null)
                return NotFound();

            return Ok(cart.Adapt<CartResponse>());
        }


        // POST: api/customer/cart/{userId}
        [HttpPost("{userId}")]
        public async Task<IActionResult> AddItem(string userId, [FromBody] CartItemCreateRequest request)
        {
            var cart = (await _cartRepository.GetAsync(
                c => c.UserId == userId,
                includes: new List<Func<IQueryable<Cart>, IQueryable<Cart>>> { q => q.Include(c => c.Items) }
            )).FirstOrDefault();

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _cartRepository.CreateAsync(cart);
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.BookId == request.BookId);
            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
                _cartItemRepository.Update(existingItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    BookId = request.BookId,
                    Quantity = request.Quantity,
                    CartId = cart.Id
                };
                await _cartItemRepository.CreateAsync(cartItem);
            }

            await _cartRepository.CommitAsync();
            await _cartItemRepository.CommitAsync();

            return Ok(new { msg = "Item added to cart successfully" });
        }

        // DELETE: api/customer/cart/{userId}/{bookId}
        [HttpDelete("{userId}/{bookId}")]
        public async Task<IActionResult> RemoveItem(string userId, int bookId)
        {
            var cart = (await _cartRepository.GetAsync(
                c => c.UserId == userId,
                includes: new List<Func<IQueryable<Cart>, IQueryable<Cart>>> { q => q.Include(c => c.Items) }
            )).FirstOrDefault();

            if (cart == null)
                return NotFound();

            var item = cart.Items.FirstOrDefault(i => i.BookId == bookId);
            if (item == null)
                return NotFound();

            _cartItemRepository.Delete(item);
            await _cartItemRepository.CommitAsync();

            return NoContent();
        }
    }
}
