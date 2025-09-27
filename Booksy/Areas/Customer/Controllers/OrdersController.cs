using Booksy.Models.Entities.Orders;
using Booksy.Models.Entities.Promotions;
using Booksy.Models.Entities.Users;
using Booksy.Models.Enums;
using Booksy.Utility;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
                o => o.UserId == userId,
                includes: new Expression<Func<Order, object>>[]
                {
                    o => o.OrderItems
                }
            );

            var response = orders.Select(o => new
            {
                o.Id,
                o.OrderDate,
                TotalPrice = o.OrderItems.Sum(i => i.Price * i.Quantity),
                Items = o.OrderItems.Select(i => new
                {
                    i.BookId,
                    i.Book.Title,
                    i.Quantity,
                    i.Price
                })
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
                includes: new Expression<Func<Cart, object>>[]
                {
                    c => c.Items
                }
            )).FirstOrDefault();

            if (cart == null || !cart.Items.Any())
                return BadRequest(new { msg = "Cart is empty." });

            // Calculate total
            decimal total = 0;
            foreach (var item in cart.Items)
            {
                total += item.Quantity * item.Book.Price;
            }

            // Apply promotion
            Promotion? promo = null;
            if (!string.IsNullOrEmpty(promotionCode))
            {
                promo = (await _promotionRepository.GetAsync(
                    p => p.Code == promotionCode && p.IsActive
                )).FirstOrDefault();

                if (promo != null)
                {
                    decimal discount = promo.Type == PromotionType.Percentage
                        ? total * (promo.Value / 100)
                        : promo.Value;

                    total -= discount;
                    if (total < 0) total = 0;
                }
            }

            // Create order
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow
            };

            await _orderRepository.CreateAsync(order);
            await _orderRepository.CommitAsync(); // Save to get order.Id

            // Create order items
            foreach (var item in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    Price = (int)item.Book.Price,
                    TotalPrice = item.Book.Price * item.Quantity
                };
                await _orderItemRepository.CreateAsync(orderItem);
            }

            await _orderItemRepository.CommitAsync();

            // Clear cart items
            foreach (var item in cart.Items.ToList())
            {
                _cartRepository.Delete(cart); // Remove cart itself after processing
            }
            await _cartRepository.CommitAsync();

            return Ok(new { msg = "Order created successfully", orderId = order.Id, totalPrice = total });
        }

        // GET: api/customer/orders/details/{orderId}
        [HttpGet("details/{orderId}")]
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var order = (await _orderRepository.GetAsync(
                o => o.Id == orderId,
                includes: new Expression<Func<Order, object>>[]
                {
                    o => o.OrderItems
                }
            )).FirstOrDefault();

            if (order == null)
                return NotFound();

            var response = new
            {
                order.Id,
                order.OrderDate,
                TotalPrice = order.OrderItems.Sum(i => i.Price * i.Quantity),
                Items = order.OrderItems.Select(i => new
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
