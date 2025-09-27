using Microsoft.EntityFrameworkCore;

namespace Booksy.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Category> _db;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
            _db = context.Set<Category>();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _db.Include(c => c.Books).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _db.Include(c => c.Books)
                            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _db.AddAsync(category);
            return category;
        }

        public void Update(Category category)
        {
            _db.Update(category);
        }

        public void Delete(Category category)
        {
            _db.Remove(category);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
