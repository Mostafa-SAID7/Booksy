using Booksy.Models.Entities.Books;
using Microsoft.EntityFrameworkCore;

namespace Booksy.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Book> _db;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
            _db = context.Set<Book>();
        }

        public async Task<List<Book>> GetAsync(
            IEnumerable<Func<IQueryable<Book>, IQueryable<Book>>>? includes = null)
        {
            IQueryable<Book> query = _db.AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = include(query);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<Book?> GetOneAsync(
            Func<Book, bool> predicate,
            bool tracked = true,
            IEnumerable<Func<IQueryable<Book>, IQueryable<Book>>>? includes = null)
        {
            IQueryable<Book> query = tracked ? _db.AsQueryable() : _db.AsNoTracking();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = include(query);
                }
            }

            return query.FirstOrDefault(predicate);
        }

        public async Task<Book> CreateAsync(Book book)
        {
            await _db.AddAsync(book);
            return book;
        }

        public void Update(Book book)
        {
            _db.Update(book);
        }

        public void Delete(Book book)
        {
            _db.Remove(book);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
