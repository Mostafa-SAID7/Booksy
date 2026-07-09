using Booksy.Common.Specifications;
using Booksy.DataAccess;
using Booksy.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Booksy.Repositories
{
    /// <summary>
    /// Generic repository implementation for data access operations.
    /// All save operations must go through IUnitOfWork for centralized transaction management.
    /// </summary>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        /// <summary>
        /// Query entities with optional filtering, includes, and ordering
        /// </summary>
        public async Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            Expression<Func<T, object>>[]? includes = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        /// <summary>
        /// Get a single entity matching the filter
        /// </summary>
        public async Task<T?> GetOneAsync(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, object>>[]? includes = null,
            bool tracked = true)
        {
            IQueryable<T> query = tracked ? _dbSet : _dbSet.AsNoTracking();

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        /// <summary>
        /// Add entity to the context (does not save - use IUnitOfWork.SaveChangesAsync())
        /// </summary>
        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        /// <summary>
        /// Mark entity as updated (does not save - use IUnitOfWork.SaveChangesAsync())
        /// </summary>
        public void Update(T entity) => _dbSet.Update(entity);

        /// <summary>
        /// Mark entity for deletion (does not save - use IUnitOfWork.SaveChangesAsync())
        /// </summary>
        public void Delete(T entity) => _dbSet.Remove(entity);

        // CQRS-friendly wrapper methods
        /// <summary>
        /// Get all entities of this type
        /// </summary>
        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, object>>[]? includes = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null) =>
            await GetAsync(filter: null, includes, orderBy);

        /// <summary>
        /// Get entity by primary key
        /// </summary>
        public async Task<T?> GetByIdAsync(Guid id, Expression<Func<T, object>>[]? includes = null)
        {
            var keyProperty = _context.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.FirstOrDefault();
            if (keyProperty == null)
                throw new InvalidOperationException($"Entity {typeof(T).Name} does not have a primary key");

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, keyProperty.Name);
            var constant = Expression.Constant(id);
            var equality = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);

            return await GetOneAsync(lambda, includes);
        }

        /// <summary>
        /// Add entity to the context (does not save - use IUnitOfWork.SaveChangesAsync())
        /// </summary>
        public async Task<T> AddAsync(T entity) => await CreateAsync(entity);

        /// <summary>
        /// Get entities matching a specification
        /// </summary>
        public async Task<IEnumerable<T>> GetBySpecificationAsync(Specification<T> specification)
        {
            var query = SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), specification);
            return await query.ToListAsync();
        }

        /// <summary>
        /// Get paginated results matching a specification
        /// Returns both items and total count for pagination
        /// </summary>
        public async Task<(List<T> Items, int TotalCount)> GetPaginatedAsync(Specification<T> specification)
        {
            // Get total count WITHOUT pagination
            var countQuery = SpecificationEvaluator<T>.GetQueryWithoutPaging(_dbSet.AsQueryable(), specification);
            var totalCount = await countQuery.CountAsync();

            // Get paginated items
            var query = SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), specification);
            var items = await query.ToListAsync();

            return (items, totalCount);
        }

        /// <summary>
        /// Get total count of entities matching a specification
        /// </summary>
        public async Task<int> CountAsync(Specification<T> specification)
        {
            var query = SpecificationEvaluator<T>.GetQueryWithoutPaging(_dbSet.AsQueryable(), specification);
            return await query.CountAsync();
        }
    }
}
