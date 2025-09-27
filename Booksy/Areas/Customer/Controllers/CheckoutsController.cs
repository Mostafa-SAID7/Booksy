using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using Booksy.Models;
using Booksy.Utility;

namespace Booksy.Areas.Customer.Controllers
{
    [Area(SD.CustomerArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class CheckoutsController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CheckoutsController> _logger;

        public CheckoutsController(
            IRepository<Order> orderRepository,
            UserManager<ApplicationUser> userManager,
            IRepository<Cart> cartRepository,
            IRepository<OrderItem> orderItemRepository,
            IRepository<Book> bookRepository,
            ApplicationDbContext context,
            ILogger<CheckoutsController> logger)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
            _cartRepository = cartRepository;
            _orderItemRepository = orderItemRepository;
            _bookRepository = bookRepository;
            _context = context;
            _logger = logger;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Success(int orderId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1️⃣ Update transaction info from Stripe
                var order = await _orderRepository.GetOneAsync(o => o.Id == orderId);
                if (order == null)
                    return NotFound();

                var sessionService = new SessionService();
                var session = sessionService.Get(order.SessionId);

                order.TransactionId = session.PaymentIntentId;
                order.TransactionStatus = TransactionStatus.Confirmed;
                order.OrderStatus = OrderStatus.UnShipped;
                await _orderRepository.CommitAsync();

                // 2️⃣ Get user's cart items
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return NotFound();

                var carts = (await _cartRepository.GetAsync(
                    c => c.UserId == user.Id,
                    includes: new List<Func<IQueryable<Cart>, IQueryable<Cart>>> { q => q.Include(c => c.Items).ThenInclude(i => i.Book) }
                )).FirstOrDefault();

                if (carts == null || !carts.Items.Any())
                    return BadRequest(new { msg = "Cart is empty." });

                // 3️⃣ Convert CartItems to OrderItems
                var orderItems = carts.Items.Select(i => new OrderItem
                {
                    OrderId = order.Id,
                    BookId = i.BookId,
                    Quantity = i.Quantity,
                    Price = i.Book.Price
                }).ToList();

                foreach (var item in orderItems)
                    await _orderItemRepository.CreateAsync(item);

                await _orderItemRepository.CommitAsync();

                // 4️⃣ Update Book stock
                foreach (var item in carts.Items)
                {
                    item.Book.Quantity -= item.Quantity;
                }
                await _bookRepository.CommitAsync();

                // 5️⃣ Clear cart
                foreach (var item in carts.Items.ToList())
                {
                    _cartRepository.Delete(carts);
                }
                await _cartRepository.CommitAsync();

                await transaction.CommitAsync();

                return Ok(new { msg = "Order placed successfully!" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Checkout failed for order {OrderId}", orderId);
                return BadRequest(new { msg = "Failed to place order." });
            }
        }
    }
}
