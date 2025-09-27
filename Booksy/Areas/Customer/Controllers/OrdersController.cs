using Booksy.Models;
using Booksy.Utility;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booksy.Areas.Customer.Controllers
{
    [Area(SD.CustomerArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Promotion> _promotionRepository;

        public OrdersController(
            UserManager<ApplicationUser> userManager,
            IRepository<Cart> cartRepository,
            IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository,
            IRepository<Promotion> promotionRepository)
        {
            _userManager = userManager;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _promotionRepository = promotionRepository;
        }

        // GET: api/customer/orders/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrders(string userId)
        {
            var orders = await _orderRepository.GetAsync(
                o => o.ApplicationUserId == userId,
                includes: new List<Func<IQueryable<Order>, IQueryable<Order>>>
                {
                    q => q.Include(o => o.Items).ThenInclude(i => i.Book)
                }
            );

            var response = orders.Select(o => new
            {
                o.Id,
                o.OrderDate,
                TotalPrice = o.Items.Sum(i => i.Price * i.Quantity),
                Items = o.Items.Adapt<List<OrderItemResponse>>()
            });

            return Ok(response);
        }

        // POST: api/customer/orders/{userId}?promotionCode=XYZ
        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateOrder(string userId, [FromQuery] string? promotionCode)
        {
            // Get cart
            var cart = (await _cartRepository.GetAsync(
                c => c.UserId == userId,
                includes: new List<Func<IQueryable<Cart>, IQueryable<Cart>>>
                {
                    q => q.Include(c => c.Items).ThenInclude(i => i.Book)
                }
            )).FirstOrDefault();

            if (cart == null || !cart.Items.Any())
                return BadRequest(new { msg = "Cart is empty." });

            // Calculate total
            decimal total = cart.Items.Sum(i => i.Quantity * i.Book.Price);

            // Apply promotion
            Promotion? promo = null;
            if (!string.IsNullOrEmpty(promotionCode))
            {
                promo = (await _promotionRepository.GetAsync(
                    p => p.Code == promotionCode && p.IsActive)).FirstOrDefault();

                if (promo != null)
                {
                    total -= promo.DiscountAmount;
                    if (total < 0) total = 0;
                }
            }

            // Create order
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                PromotionId = promo?.Id
            };

            await _orderRepository.CreateAsync(order);
            await _orderRepository.CommitAsync(); // save to generate order.Id

            // Create order items
            foreach (var item in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    Price = item.Book.Price
                };
                await _orderItemRepository.CreateAsync(orderItem);
            }

            await _orderItemRepository.CommitAsync();

            // Clear cart
            foreach (var item in cart.Items.ToList())
            {
                _cartRepository.Delete(item.Cart); // or _cartItemRepository.Delete(item)
            }
            await _cartRepository.CommitAsync();

            return Ok(new { msg = "Order created successfully", orderId = order.Id });
        }

        // GET: api/customer/orders/details/{orderId}
        [HttpGet("details/{orderId}")]
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var order = (await _orderRepository.GetAsync(
                o => o.Id == orderId,
                includes: new List<Func<IQueryable<Order>, IQueryable<Order>>>
                {
                    q => q.Include(o => o.Items).ThenInclude(i => i.Book)
                }
            )).FirstOrDefault();

            if (order == null)
                return NotFound();

            var response = new
            {
                order.Id,
                order.OrderDate,
                TotalPrice = order.Items.Sum(i => i.Price * i.Quantity),
                Items = order.Items.Select(i => new
                {
                    i.BookId,
                    i.Book.Title,
                    i.Quantity,
                    i.Price
                })
            };

            return Ok(response);
        }
    }
}
