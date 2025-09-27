namespace Booksy.Repositories.IRepositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAsync(
               IEnumerable<Func<IQueryable<Book>, IQueryable<Book>>>? includes = null);

        Task<Book?> GetOneAsync(
             Func<Book, bool> predicate,
             bool tracked = true,
             IEnumerable<Func<IQueryable<Book>, IQueryable<Book>>>? includes = null);

        Task<Book> CreateAsync(Book book);
        void Update(Book book);
        void Delete(Book book);
        Task CommitAsync();
    }
}
