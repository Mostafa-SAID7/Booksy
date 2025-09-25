using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Booksy.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public StatisticsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = (await _productRepository.GetAsync(includes: [e => e.Category])).GroupBy(e => e.Category.Name).Select(e => new
            {
                e.Key,
                avg = e.Average(e => e.Price).ToString("c"),
                count = e.Count(),
                sum = e.Sum(e => e.Price).ToString("c"),
            });

            return Ok(products);
        }
    }
}
