using Booksy.Models.Entities.Books;
using Microsoft.EntityFrameworkCore;

namespace Booksy.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Author> _db;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
            _db = context.Set<Author>();
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _db.Include(a => a.Books).ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _db.Include(a => a.Books)
                            .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Author> CreateAsync(Author author)
        {
            await _db.AddAsync(author);
            return author;
        }

        public void Update(Author author)
        {
            _db.Update(author);
        }

        public void Delete(Author author)
        {
            _db.Remove(author);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
