using Booksy.Models.DTOs.Request.Books;
using Booksy.Models.DTOs.Response.Auth;
using Booksy.Models.DTOs.Response.Books;
using Booksy.Models.DTOs.Response.Category;
using Booksy.Models.Entities.Books;
using Booksy.Utility;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(
            Summary = "Get a book by its ID",
            Description = "Returns a single book object based on the provided ID"
        )]
        [SwaggerResponse(200, "Book found successfully", typeof(BookResponse))]
        [SwaggerResponse(404, "Book not found")]
        public async Task<IActionResult> Details(int id)
        {
            // Include Author and Category
            var book = await _bookRepository.GetOneAsync(
                b => b.Id == id,
                includes: new List<Func<IQueryable<Book>, IQueryable<Book>>>
                {
            q => q.Include(b => b.Category),
            q => q.Include(b => b.Author)
                });

            if (book is null)
                return NotFound();

            // Map to BookResponse and include nested objects explicitly
            var bookResponse = new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                CoverImageUrl = book.CoverImageUrl,
                Author = book.Author != null
                    ? new AuthorResponse { Id = book.Author.Id, Name = book.Author.Name }
                    : null,
              
            };

            return Ok(bookResponse);
        }


        // POST: api/admin/books
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BookCreateRequest request)
        {
            if (request.CoverImage is not null && request.CoverImage.Length > 0)
            {
                // Save the image
                var fileName = Guid.NewGuid() + Path.GetExtension(request.CoverImage.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                using var stream = System.IO.File.Create(filePath);
                await request.CoverImage.CopyToAsync(stream);

                // Map request to Book entity
                var book = request.Adapt<Book>();
                book.CoverImageUrl = fileName;

                // Save to repository
                var bookReturned = await _bookRepository.CreateAsync(book);
                await _bookRepository.CommitAsync();

                // Fetch the saved book including Author and Category
                var savedBook = await _bookRepository.GetOneAsync(
                    b => b.Id == bookReturned.Id,
                    includes: new List<Func<IQueryable<Book>, IQueryable<Book>>>
                    {
                q => q.Include(b => b.Author),
                q => q.Include(b => b.Category)
                    });

                // Map to BookResponse including nested objects
                var bookResponse = new BookResponse
                {
                    Id = savedBook.Id,
                    Title = savedBook.Title,
                    CoverImageUrl = savedBook.CoverImageUrl,
                    Author = savedBook.Author != null
                        ? new AuthorResponse { Id = savedBook.Author.Id, Name = savedBook.Author.Name }
                        : null,
                   
                };

                return CreatedAtAction(nameof(Details), new { id = bookResponse.Id }, bookResponse);
            }

            return BadRequest("Cover image is required.");
        }


        // PUT: api/admin/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] BookUpdateRequest request)
        {
            var bookInDB = await _bookRepository.GetOneAsync(b => b.Id == id, tracked: false);
            if (bookInDB is null)
                return BadRequest("Book not found.");

            // Map incoming request to Book entity
            var book = request.Adapt<Book>();
            book.Id = id;

            // Handle cover image
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

            // Update in repository
            _bookRepository.Update(book);
            await _bookRepository.CommitAsync();

            // Fetch updated book including Author and Category
            var updatedBook = await _bookRepository.GetOneAsync(
                b => b.Id == id,
                includes: new List<Func<IQueryable<Book>, IQueryable<Book>>>
                {
            q => q.Include(b => b.Author),
            q => q.Include(b => b.Category)
                });

            // Map to BookResponse with nested objects
            var bookResponse = new BookResponse
            {
                Id = updatedBook.Id,
                Title = updatedBook.Title,
                CoverImageUrl = updatedBook.CoverImageUrl,
                Author = updatedBook.Author != null
                    ? new AuthorResponse { Id = updatedBook.Author.Id, Name = updatedBook.Author.Name }
                    : null,
                
            };

            return Ok(bookResponse); // return updated book
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
