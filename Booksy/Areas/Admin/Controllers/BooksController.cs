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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Booksy.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Author> _authorRepository;

        public BooksController(
            IBookRepository bookRepository,
            IRepository<Category> categoryRepository,
            IRepository<Author> authorRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _authorRepository = authorRepository;
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
            var book = await _bookRepository.GetOneAsync(
                b => b.Id == id,
                includes: new List<Func<IQueryable<Book>, IQueryable<Book>>>
                {
                    q => q.Include(b => b.Category),
                    q => q.Include(b => b.Author)
                });

            if (book is null)
                return NotFound(new { msg = "Book not found." });

            var bookResponse = new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                Stock = book.Stock,
                CoverImageUrl = book.CoverImageUrl,
                Author = book.Author != null ? new AuthorResponse { Id = book.Author.Id, Name = book.Author.Name } : null,
                Category = book.Category != null ? new CategoryResponse { Id = book.Category.Id, Name = book.Category.Name } : null
            };

            return Ok(bookResponse);
        }

        // POST: api/admin/books
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BookCreateRequest request)
        {
            if (request == null)
                return BadRequest(new { msg = "Invalid request." });

            // Validate FK: Author
            var author = await _authorRepository.GetOneAsync(a => a.Id == request.AuthorId);
            if (author == null)
                return BadRequest(new { msg = "Invalid AuthorId." });

            // Validate FK: Category
            var category = await _categoryRepository.GetOneAsync(c => c.Id == request.CategoryId);
            if (category == null)
                return BadRequest(new { msg = "Invalid CategoryId." });

            if (request.CoverImage is null || request.CoverImage.Length == 0)
                return BadRequest(new { msg = "Cover image is required." });

            // Save image
            var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            Directory.CreateDirectory(imagesFolder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.CoverImage.FileName)}";
            var filePath = Path.Combine(imagesFolder, fileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                await request.CoverImage.CopyToAsync(stream);
            }

            // Map and set server-side fields
            var book = request.Adapt<Book>();
            book.CoverImageUrl = fileName;

            // Persist
            var created = await _bookRepository.CreateAsync(book);
            await _bookRepository.CommitAsync();

            // Reload with includes for DTO
            var saved = await _bookRepository.GetOneAsync(
                b => b.Id == created.Id,
                includes: new List<Func<IQueryable<Book>, IQueryable<Book>>>
                {
                    q => q.Include(b => b.Author),
                    q => q.Include(b => b.Category)
                });

            var response = new BookResponse
            {
                Id = saved.Id,
                Title = saved.Title,
                Price = saved.Price,
                Stock = saved.Stock,
                CoverImageUrl = saved.CoverImageUrl,
                Author = saved.Author != null ? new AuthorResponse { Id = saved.Author.Id, Name = saved.Author.Name } : null,
                Category = saved.Category != null ? new CategoryResponse { Id = saved.Category.Id, Name = saved.Category.Name } : null
            };

            return CreatedAtAction(nameof(Details), new { id = response.Id }, response);
        }

        // PUT: api/admin/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] BookUpdateRequest request)
        {
            if (request == null)
                return BadRequest(new { msg = "Invalid request." });

            // Load existing entity tracked by context (tracked: true). This avoids duplicate tracking.
            var existing = await _bookRepository.GetOneAsync(
                b => b.Id == id,
                includes: new List<Func<IQueryable<Book>, IQueryable<Book>>>
                {
                    q => q.Include(b => b.Author),
                    q => q.Include(b => b.Category)
                },
                tracked: true);

            if (existing is null)
                return NotFound(new { msg = "Book not found." });

            // Validate FK: Author
            var author = await _authorRepository.GetOneAsync(a => a.Id == request.AuthorId);
            if (author == null)
                return BadRequest(new { msg = "Invalid AuthorId." });

            // Validate FK: Category
            var category = await _categoryRepository.GetOneAsync(c => c.Id == request.CategoryId);
            if (category == null)
                return BadRequest(new { msg = "Invalid CategoryId." });

            // Update only allowed fields on the tracked entity (do not attach a new Book instance)
            existing.Title = request.Title ?? existing.Title;
            existing.Price = request.Price;
            existing.Description = request.Description ?? existing.Description;
            existing.Stock = request.Stock;
            existing.AuthorId = request.AuthorId;
            existing.CategoryId = request.CategoryId;
            existing.ISBN = request.ISBN ?? existing.ISBN;
            // Do not overwrite Traffic or IsDeleted here unless explicitly intended

            // Handle cover image update
            var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            Directory.CreateDirectory(imagesFolder);

            if (request.CoverImage is not null && request.CoverImage.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.CoverImage.FileName)}";
                var filePath = Path.Combine(imagesFolder, fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await request.CoverImage.CopyToAsync(stream);
                }

                // delete old image if exists
                if (!string.IsNullOrEmpty(existing.CoverImageUrl))
                {
                    var oldPath = Path.Combine(imagesFolder, existing.CoverImageUrl);
                    if (System.IO.File.Exists(oldPath))
                    {
                        try { System.IO.File.Delete(oldPath); } catch { /* ignore */ }
                    }
                }

                existing.CoverImageUrl = fileName;
            }
            // else keep existing.CoverImageUrl

            // Commit changes (EF is tracking 'existing' so just save)
            await _bookRepository.CommitAsync();

            // Reload updated entity to include nested relations for response
            var updated = await _bookRepository.GetOneAsync(
                b => b.Id == id,
                includes: new List<Func<IQueryable<Book>, IQueryable<Book>>>
                {
                    q => q.Include(b => b.Author),
                    q => q.Include(b => b.Category)
                });

            var response = new BookResponse
            {
                Id = updated.Id,
                Title = updated.Title,
                Price = updated.Price,
                Stock = updated.Stock,
                CoverImageUrl = updated.CoverImageUrl,
                Author = updated.Author != null ? new AuthorResponse { Id = updated.Author.Id, Name = updated.Author.Name } : null,
                Category = updated.Category != null ? new CategoryResponse { Id = updated.Category.Id, Name = updated.Category.Name } : null
            };

            return Ok(response);
        }

        // DELETE: api/admin/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetOneAsync(b => b.Id == id);
            if (book is null)
                return NotFound(new { msg = "Book not found." });

            // Delete cover image file if present
            if (!string.IsNullOrEmpty(book.CoverImageUrl))
            {
                var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                var filePath = Path.Combine(imagesFolder, book.CoverImageUrl);
                if (System.IO.File.Exists(filePath))
                {
                    try { System.IO.File.Delete(filePath); } catch { /* ignore */ }
                }
            }

            _bookRepository.Delete(book);
            await _bookRepository.CommitAsync();

            return NoContent();
        }
    }
}
