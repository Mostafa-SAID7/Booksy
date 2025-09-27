namespace Booksy.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        void Update(Category category);
        void Delete(Category category);
        Task CommitAsync();
    }
}
