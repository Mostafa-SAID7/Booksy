using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booksy.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IRepository<Book> _bookRepository;

        public InventoryController(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("low-stock")]
        public async Task<IActionResult> LowStock(int threshold = 5)
        {
            var books = (await _bookRepository.GetAsync())
                .Where(b => b.Quantity <= threshold)
                .ToList();

            return Ok(books);
        }

        [HttpPut("{id}/update-quantity")]
        public async Task<IActionResult> UpdateQuantity(int id, [FromBody] int quantity)
        {
            var book = await _bookRepository.GetOneAsync(b => b.Id == id);
            if (book == null) return NotFound();

            book.Quantity = quantity;
            _bookRepository.Update(book);
            await _bookRepository.CommitAsync();
            return NoContent();
        }
    }

}
