using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booksy.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoriesController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // CRUD
        // GET / categories
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAsync();

            return Ok(categories);
        }

        // GET / categories/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetOneAsync(e => e.Id == id);

            if (category is null)
                return NotFound();

            return Ok(category);
        }

        // POST / categories
        [HttpPost]
        public async Task<IActionResult> Create(CategoryRequest categoryRequest)
        {
            await _categoryRepository.CreateAsync(categoryRequest.Adapt<Category>());
            await _categoryRepository.CommitAsync();

            return Ok(new
            {
                msg = "Add Category Successfully"
            });
        }

        // PUT / categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryRequest categoryRequest)
        {
            var categoryInDB = await _categoryRepository.GetOneAsync(e => e.Id == id);

            if (categoryInDB is null)
                return NotFound();

            categoryInDB.Name = categoryRequest.Name;
            categoryInDB.Description = categoryRequest.Description;
            categoryInDB.Status = categoryRequest.Status;

            await _categoryRepository.CommitAsync();

            return Ok(new
            {
                msg = "Update Category Successfully"
            });
        }

        // DELETE / categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoryInDB = await _categoryRepository.GetOneAsync(e => e.Id == id);

            if (categoryInDB is null)
                return NotFound();
            
            _categoryRepository.Delete(categoryInDB);
            await _categoryRepository.CommitAsync();

            return Ok(new
            {
                msg = "Delete Category Successfully"
            });
        }
    }
}
