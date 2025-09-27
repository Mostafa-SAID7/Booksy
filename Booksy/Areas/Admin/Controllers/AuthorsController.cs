using Booksy.Models.DTOs.Request.Auth;
using Booksy.Models.DTOs.Response.Auth;
using Booksy.Models.Entities.Books;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booksy.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET: api/admin/authors
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authors = await _authorRepository.GetAllAsync();
            var response = authors.Adapt<List<AuthorResponse>>();
            return Ok(response);
        }

        // GET: api/admin/authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author is null)
                return NotFound();

            return Ok(author.Adapt<AuthorResponse>());
        }

        // POST: api/admin/authors
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorCreateRequest request)
        {
            var author = request.Adapt<Author>();
            var createdAuthor = await _authorRepository.CreateAsync(author);
            await _authorRepository.CommitAsync();

            return CreatedAtAction(nameof(Details), new { id = createdAuthor.Id }, new { msg = "Author created successfully" });
        }

        // PUT: api/admin/authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] AuthorCreateRequest request)
        {
            var authorInDB = await _authorRepository.GetByIdAsync(id);
            if (authorInDB is null)
                return NotFound();

            var updatedAuthor = request.Adapt(authorInDB);
            _authorRepository.Update(updatedAuthor);
            await _authorRepository.CommitAsync();

            return NoContent();
        }

        // DELETE: api/admin/authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author is null)
                return NotFound();

            _authorRepository.Delete(author);
            await _authorRepository.CommitAsync();

            return NoContent();
        }
    }
}
