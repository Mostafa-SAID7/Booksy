using System.Linq.Expressions;

namespace Booksy.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>[]? includes = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        Task<T?> GetOneAsync(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, object>>[]? includes = null,
            bool tracked = true);

        Task<T> CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task CommitAsync();
    }
}
