using Booksy.Models.Entities.Books;

namespace Booksy.Repositories.IRepositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author> CreateAsync(Author author);
        void Update(Author author);
        void Delete(Author author);
        Task CommitAsync();
    }
}
