using Booksy.Common.Specifications;
using System.Linq.Expressions;

namespace Booksy.Repositories.IRepositories
{
    /// <summary>
    /// Generic repository interface for data access operations.
    /// All save operations must go through IUnitOfWork for centralized transaction management.
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Query entities with optional filtering, includes, and ordering
        /// </summary>
        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>[]? includes = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        /// <summary>
        /// Get a single entity matching the filter
        /// </summary>
        Task<T?> GetOneAsync(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, object>>[]? includes = null,
            bool tracked = true);

        /// <summary>
        /// Add entity to the context (does not save)
        /// </summary>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Mark entity as updated (does not save)
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// Mark entity for deletion (does not save)
        /// </summary>
        void Delete(T entity);

        // CQRS-friendly wrapper methods
        /// <summary>
        /// Get all entities of this type
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>>[]? includes = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        /// <summary>
        /// Get entity by primary key
        /// </summary>
        Task<T?> GetByIdAsync(Guid id, Expression<Func<T, object>>[]? includes = null);

        /// <summary>
        /// Add entity to the context (does not save - use UnitOfWork.SaveChangesAsync())
        /// </summary>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Get entities matching a specification with pagination support
        /// </summary>
        Task<IEnumerable<T>> GetBySpecificationAsync(Specification<T> specification);

        /// <summary>
        /// Get paginated results matching a specification
        /// Returns both items and total count for pagination
        /// </summary>
        Task<(List<T> Items, int TotalCount)> GetPaginatedAsync(Specification<T> specification);

        /// <summary>
        /// Get total count of entities matching a specification
        /// </summary>
        Task<int> CountAsync(Specification<T> specification);
    }
}
