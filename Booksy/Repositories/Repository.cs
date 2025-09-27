using Booksy.DataAccess;
using Booksy.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Booksy.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

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

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public async Task CommitAsync() => await _context.SaveChangesAsync();
    }
}
