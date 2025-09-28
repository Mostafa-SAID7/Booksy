using Booksy.Areas.Admin.Services.IServices;
using Booksy.Models.DTOs.Request.Category;
using Booksy.Models.DTOs.Response.Category;
using Booksy.Models.Entities.Books;
using Mapster;
using Microsoft.AspNetCore.Mvc;


namespace Booksy.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Get a single category by ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryResponse>> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category is null)
                return NotFound(new { message = $"Category with ID {id} not found." });

            return Ok(category);
        }

        /// <summary>
        /// Create a new category.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CategoryResponse>> Create([FromBody] CategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var category = await _categoryService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        /// <summary>
        /// Update an existing category by ID.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var success = await _categoryService.UpdateAsync(id, request);
            if (!success)
                return NotFound(new { message = $"Category with ID {id} not found." });

            return NoContent();
        }

        /// <summary>
        /// Delete a category by ID.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _categoryService.DeleteAsync(id);
            if (!success)
                return NotFound(new { message = $"Category with ID {id} not found." });

            return NoContent();
        }
    }
}
