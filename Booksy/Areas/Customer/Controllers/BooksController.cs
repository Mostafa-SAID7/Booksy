using Booksy.Models.DTOs.Request.Books;
using Booksy.Models.DTOs.Response.Books;
using Booksy.Models.Entities.Books;
using Booksy.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                includes: new Expression<Func<Book, object>>[]
                {
                    b => b.Category
                }
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

            var categories = await _categoryRepository.GetAsync();

            return Ok(new
            {
                books = pagedBooks,
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
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetOneAsync(
                b => b.Id == id,
                includes: new Expression<Func<Book, object>>[]
                {
                    b => b.Category
                }
            );

            if (book == null) return NotFound();

            // Update Traffic
            book.Traffic++;
            await _bookRepository.CommitAsync();

            // Related Books (same category)
            var relatedBooks = (await _bookRepository.GetAsync(
                b => b.CategoryId == book.CategoryId && b.Id != book.Id,
                includes: new Expression<Func<Book, object>>[]
                {
                    b => b.Category
                }
            )).Take(4).ToList();

            // Top Traffic
            var topTraffic = (await _bookRepository.GetAsync(
                b => b.Id != book.Id,
                includes: new Expression<Func<Book, object>>[]
                {
                    b => b.Category
                }
            )).OrderByDescending(b => b.Traffic).Take(4).ToList();

            // Similar Books (title contains keyword)
            var similarBooks = (await _bookRepository.GetAsync(
                b => b.Title.Contains(book.Title) && b.Id != book.Id,
                includes: new Expression<Func<Book, object>>[]
                {
                    b => b.Category
                }
            )).Take(4).ToList();

            var response = new BookWithRelatedResponse
            {
                Book = book,
                RelatedBooks = relatedBooks,
                TopTraffic = topTraffic,
                SimilarBooks = similarBooks
            };

            return Ok(response);
        }
    }
}
