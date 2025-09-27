using Booksy.Models;
using Booksy.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var orders = await _orderRepository.GetAsync(includes: new List<Func<IQueryable<Order>, IQueryable<Order>>>
            {
                q => q.Include(o => o.Items)
            });
            var users = await _userRepository.GetAsync();

            decimal totalRevenue = orders.Sum(o => o.Items.Sum(i => i.Price * i.Quantity));

            var stats = new
            {
                TotalBooks = books.Count,
                TotalAuthors = authors.Count,
                TotalCategories = categories.Count,
                TotalOrders = orders.Count,
                TotalUsers = users.Count,
                TotalRevenue = totalRevenue
            };

            return Ok(stats);
        }
    }
}
