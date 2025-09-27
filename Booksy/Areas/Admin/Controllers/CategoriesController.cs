using Mapster;
using Microsoft.AspNetCore.Mvc;
using Booksy.Models.Entities.Books;
using Booksy.Models.DTOs.Request.Category;
using Booksy.Models.DTOs.Response.Category;


namespace Booksy.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/admin/categories
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var response = categories.Adapt<List<CategoryResponse>>();
            return Ok(response);
        }

        // GET: api/admin/categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
                return NotFound();

            return Ok(category.Adapt<CategoryResponse>());
        }

        // POST: api/admin/categories
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateRequest request)
        {
            var category = request.Adapt<Category>();
            var createdCategory = await _categoryRepository.CreateAsync(category);
            await _categoryRepository.CommitAsync();

            return CreatedAtAction(nameof(Details), new { id = createdCategory.Id }, new { msg = "Category created successfully" });
        }

        // PUT: api/admin/categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CategoryCreateRequest request)
        {
            var categoryInDB = await _categoryRepository.GetByIdAsync(id);
            if (categoryInDB is null)
                return NotFound();

            var updatedCategory = request.Adapt(categoryInDB);
            _categoryRepository.Update(updatedCategory);
            await _categoryRepository.CommitAsync();

            return NoContent();
        }

        // DELETE: api/admin/categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
                return NotFound();

            _categoryRepository.Delete(category);
            await _categoryRepository.CommitAsync();

            return NoContent();
        }
    }
}
