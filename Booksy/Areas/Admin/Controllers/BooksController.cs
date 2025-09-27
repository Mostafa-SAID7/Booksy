using Booksy.Models.DTOs.Request.Books;
using Booksy.Models.DTOs.Response.Books;
using Booksy.Models.Entities.Books;
using Booksy.Utility;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Booksy.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: api/admin/books
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAsync(includes: new List<Func<IQueryable<Book>, IQueryable<Book>>>
            {
                q => q.Include(b => b.Category),
                q => q.Include(b => b.Author)
            });

            var response = books.Adapt<List<BookResponse>>();
            return Ok(response);
        }

        // GET: api/admin/books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetOneAsync(
                b => b.Id == id,
                includes: new List<Func<IQueryable<Book>, IQueryable<Book>>>
                {
                    q => q.Include(b => b.Category),
                    q => q.Include(b => b.Author)
                });

            if (book is null)
                return NotFound();

            return Ok(book.Adapt<BookResponse>());
        }

        // POST: api/admin/books
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BookCreateRequest request)
        {
            if (request.CoverImage is not null && request.CoverImage.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(request.CoverImage.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                using var stream = System.IO.File.Create(filePath);
                await request.CoverImage.CopyToAsync(stream);

                var book = request.Adapt<Book>();
                book.CoverImageUrl = fileName;

                var bookReturned = await _bookRepository.CreateAsync(book);
                await _bookRepository.CommitAsync();

                return CreatedAtAction(nameof(Details), new { id = bookReturned.Id }, new { msg = "Created Book Successfully" });
            }

            return BadRequest();
        }

        // PUT: api/admin/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] BookUpdateRequest request)
        {
            var bookInDB = await _bookRepository.GetOneAsync(b => b.Id == id, tracked: false);
            if (bookInDB is null)
                return BadRequest();

            var book = request.Adapt<Book>();
            book.Id = id;

            if (request.CoverImage is not null && request.CoverImage.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(request.CoverImage.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                using var stream = System.IO.File.Create(filePath);
                await request.CoverImage.CopyToAsync(stream);

                // Delete old image
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", bookInDB.CoverImageUrl ?? string.Empty);
                if (System.IO.File.Exists(oldFilePath))
                    System.IO.File.Delete(oldFilePath);

                book.CoverImageUrl = fileName;
            }
            else
            {
                book.CoverImageUrl = bookInDB.CoverImageUrl;
            }

            _bookRepository.Update(book);
            await _bookRepository.CommitAsync();

            return NoContent();
        }

        // DELETE: api/admin/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetOneAsync(b => b.Id == id);
            if (book is null)
                return NotFound();

            // Delete cover image
            if (!string.IsNullOrEmpty(book.CoverImageUrl))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", book.CoverImageUrl);
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }

            _bookRepository.Delete(book);
            await _bookRepository.CommitAsync();

            return NoContent();
        }
    }
}
