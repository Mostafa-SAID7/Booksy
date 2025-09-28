using Booksy.Areas.Admin.Services.IServices;
using Booksy.Models.DTOs.Request.Category;
using Booksy.Models.DTOs.Response.Category;
using Booksy.Models.Entities.Books;
using Mapster;

namespace Booksy.Areas.Admin.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // Get all categories with mapped DTOs
        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Adapt<IEnumerable<CategoryResponse>>();
        }

        // Get single category by ID
        public async Task<CategoryResponse?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category?.Adapt<CategoryResponse>();
        }

        // Create new category
        public async Task<CategoryResponse> CreateAsync(CategoryCreateRequest request)
        {
            var category = request.Adapt<Category>(); // Map request → entity
            var created = await _categoryRepository.CreateAsync(category);
            await _categoryRepository.CommitAsync();

            return created.Adapt<CategoryResponse>(); // Map entity → response
        }

        // Update existing category
        public async Task<bool> UpdateAsync(int id, CategoryCreateRequest request)
        {
            var categoryInDb = await _categoryRepository.GetByIdAsync(id);
            if (categoryInDb is null)
                return false;

            request.Adapt(categoryInDb); // Map request onto existing entity
            _categoryRepository.Update(categoryInDb);
            await _categoryRepository.CommitAsync();

            return true;
        }

        // Delete category by ID
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
                return false;

            _categoryRepository.Delete(category);
            await _categoryRepository.CommitAsync();

            return true;
        }
    }
}
