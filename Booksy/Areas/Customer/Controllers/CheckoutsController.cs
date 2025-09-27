using Booksy.Models.Entities.Books;
using Booksy.Models.Entities.Orders;
using Booksy.Models.Entities.Users;
using Booksy.Models.Enums;
using Booksy.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using System.Linq.Expressions;

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
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CheckoutsController> _logger;

        public CheckoutsController(
            IRepository<Order> orderRepository,
            UserManager<ApplicationUser> userManager,
            IRepository<Cart> cartRepository,
            IRepository<CartItem> cartItemRepository,
            IRepository<OrderItem> orderItemRepository,
            IRepository<Book> bookRepository,
            ApplicationDbContext context,
            ILogger<CheckoutsController> logger)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
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
                // 1️⃣ Load order
                var order = await _orderRepository.GetOneAsync(o => o.Id == orderId, tracked: true);
                if (order == null)
                    return NotFound(new { msg = "Order not found." });

                // 2️⃣ Update Stripe transaction info
                var sessionService = new SessionService();
                var session = sessionService.Get(order.SessionId);

                order.TransactionId = session.PaymentIntentId;
                order.TransactionStatus = TransactionStatus.Success;
                order.OrderStatus = OrderStatus.UnShipped;
                _orderRepository.Update(order);
                await _orderRepository.CommitAsync();

                // 3️⃣ Load user's cart with items and books
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return NotFound(new { msg = "User not found." });

                var cart = (await _cartRepository.GetAsync(
                    c => c.UserId == user.Id,
                    includes: new Expression<Func<Cart, object>>[]
                    {
                        c => c.Items
                    }
                )).FirstOrDefault();

                if (cart == null || cart.Items.Count == 0)
                    return BadRequest(new { msg = "Cart is empty." });

                // 4️⃣ Convert CartItems to OrderItems and update stock
                foreach (var item in cart.Items)
                {
                    var book = await _bookRepository.GetOneAsync(b => b.Id == item.BookId);
                    if (book == null) continue;

                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        BookId = book.Id,
                        Quantity = item.Quantity,
                        TotalPrice = book.Price * item.Quantity
                    };
                    await _orderItemRepository.CreateAsync(orderItem);

                    // Update book stock
                    book.Quantity -= item.Quantity;
                }

                await _orderItemRepository.CommitAsync();
                await _bookRepository.CommitAsync();

                // 5️⃣ Clear user's cart items
                foreach (var item in cart.Items.ToList())
                {
                    _cartItemRepository.Delete(item);
                }
                await _cartItemRepository.CommitAsync();

                // Optionally delete empty cart
                _cartRepository.Delete(cart);
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
