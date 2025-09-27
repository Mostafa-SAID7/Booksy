using Booksy.Models.Entities.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Booksy.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepository;

        public ReportsController(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("top-books")]
        public async Task<IActionResult> TopBooks(int top = 5)
        {
            var orders = await _orderRepository.GetAsync(
        includes: new Expression<Func<Order, object>>[]
        {
        o => o.OrderItems
        });
            var topBooks = orders
                .SelectMany(o => o.OrderItems)
                .GroupBy(i => i.BookId)
                .Select(g => new { BookId = g.Key, QuantitySold = g.Sum(i => i.Quantity) })
                .OrderByDescending(x => x.QuantitySold)
                .Take(top)
                .ToList();

            return Ok(topBooks);
        }

        [HttpGet("monthly-revenue")]
        public async Task<IActionResult> MonthlyRevenue(int year = 2025)
        {
            var orders = await _orderRepository.GetAsync();

            var revenue = orders
                .Where(o => o.CreatedAt.Year == year)
                .GroupBy(o => o.CreatedAt.Month)
                .Select(g => new { Month = g.Key, TotalRevenue = g.Sum(o => o.OrderItems.Sum(i => i.Price * i.Quantity)) })
                .ToList();

            return Ok(revenue);
        }
    }

}
