using Booksy.Models.DTOs.Request.Books;
using Booksy.Models.DTOs.Response.Books;
using Booksy.Models.Entities.Books;
using Booksy.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace Booksy.Areas.Customer.Controllers
{
    [Area(SD.CustomerArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Category> _categoryRepository;

        public BooksController(IRepository<Book> bookRepository, IRepository<Category> categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index([FromQuery] BookFilterRequest filterRequest, int page = 1)
        {
            const double hotDiscountThreshold = 50;

            var booksQuery = (await _bookRepository.GetAsync(
                includes: new Expression<Func<Book, object>>[] { b => b.Category, b => b.Author }
            )).AsQueryable();

            // Filters
            if (!string.IsNullOrEmpty(filterRequest.BookTitle))
                booksQuery = booksQuery.Where(b => b.Title.Contains(filterRequest.BookTitle));

            if (filterRequest.MinPrice.HasValue)
                booksQuery = booksQuery.Where(b => b.Price - b.Price * (b.Discount / 100) >= filterRequest.MinPrice);

            if (filterRequest.MaxPrice.HasValue)
                booksQuery = booksQuery.Where(b => b.Price - b.Price * (b.Discount / 100) <= filterRequest.MaxPrice);

            if (filterRequest.CategoryId.HasValue)
                booksQuery = booksQuery.Where(b => b.CategoryId == filterRequest.CategoryId);

            if (filterRequest.IsHot)
                booksQuery = booksQuery.Where(b => (double)b.Discount > hotDiscountThreshold);

            // Pagination
            int pageSize = 8;
            double totalPages = Math.Ceiling(booksQuery.Count() / (double)pageSize);
            var pagedBooks = booksQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Map to DTO
            var booksDto = pagedBooks.Select(b => new BookResponse
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                Stock = b.Stock,
                CoverImageUrl = b.CoverImageUrl,
                Author = new Models.DTOs.Response.Auth.AuthorResponse
                {
                    Id = b.Author.Id,
                    Name = b.Author.Name
                },
                Category = new Models.DTOs.Response.Category.CategoryResponse
                {
                    Id = b.Category.Id,
                    Name = b.Category.Name
                }
            }).ToList();

            var categories = await _categoryRepository.GetAsync();

            return Ok(new
            {
                books = booksDto,
                filterRequest.BookTitle,
                filterRequest.MinPrice,
                filterRequest.MaxPrice,
                filterRequest.CategoryId,
                filterRequest.IsHot,
                totalPages,
                currentPage = page,
                categories
            });
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get a book by its ID",
            Description = "Returns a single book object based on the provided ID"
        )]
        [SwaggerResponse(200, "Book found successfully", typeof(BookDetailResponse))]
        [SwaggerResponse(404, "Book not found")]
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetOneAsync(
                b => b.Id == id,
                includes: new Expression<Func<Book, object>>[] { b => b.Category, b => b.Author, b => b.Reviews }
            );

            if (book == null) return NotFound();

            // Update traffic
            book.Traffic++;
            await _bookRepository.CommitAsync();

            // Map to BookDetailResponse
            var response = new BookDetailResponse
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                Discount = book.Discount,
                Description = book.Description,
                Stock = book.Stock,
                CoverImageUrl = book.CoverImageUrl,
                Traffic = book.Traffic,
                ISBN = book.ISBN,
                Quantity = book.Quantity,
                CategoryId = book.CategoryId,
                CategoryName = book.Category.Name,
                AuthorId = book.AuthorId,
                AuthorName = book.Author.Name,
                Reviews = book.Reviews.Select(r => new Models.DTOs.Response.Reviews.ReviewResponse
                {
                    Id = r.Id,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    ReviewerName = r.ReviewerName
                }).ToList()
            };

            return Ok(response);
        }
    }
}
