using Booksy.Models.DTOs.Request.Category;
using Booksy.Models.DTOs.Response.Category;

namespace Booksy.Areas.Admin.Services.IServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse?> GetByIdAsync(int id);
        Task<CategoryResponse> CreateAsync(CategoryCreateRequest request);
        Task<bool> UpdateAsync(int id, CategoryCreateRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
