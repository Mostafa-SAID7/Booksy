using Booksy.Models.Entities.Books;
using Booksy.Models.Entities.Orders;
using Booksy.Models.Entities.Users;
using Booksy.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Booksy.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<ApplicationUser> _userRepository;

        public StatisticsController(
            IRepository<Book> bookRepository,
            IRepository<Author> authorRepository,
            IRepository<Category> categoryRepository,
            IRepository<Order> orderRepository,
            IRepository<ApplicationUser> userRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        // GET: api/admin/statistics
        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            var books = await _bookRepository.GetAsync();
            var authors = await _authorRepository.GetAsync();
            var categories = await _categoryRepository.GetAsync();
            var users = await _userRepository.GetAsync();

            // Include OrderItems for revenue calculation
            var orders = await _orderRepository.GetAsync(
                includes: new Expression<Func<Order, object>>[]
                {
                    o => o.OrderItems
                });

            decimal totalRevenue = orders.Sum(o => o.OrderItems.Sum(i => i.Price * i.Quantity));

            var stats = new
            {
                TotalBooks = books.Count(),
                TotalAuthors = authors.Count(),
                TotalCategories = categories.Count(),
                TotalOrders = orders.Count(),
                TotalUsers = users.Count(),
                TotalRevenue = totalRevenue
            };

            return Ok(stats);
        }
    }
}
