using Booksy.Common.Models;
using Booksy.Models.Entities.Books;

namespace Booksy.Infrastructure.Search;

public interface IBookSearchService
{
    /// <summary>Index or replace a single book document.</summary>
    Task IndexBookAsync(Book book, CancellationToken ct = default);

    /// <summary>Remove a book from the index by its ID.</summary>
    Task RemoveBookAsync(Guid bookId, CancellationToken ct = default);

    /// <summary>
    /// Full-text search across title, description, author, category, tags, and ISBN.
    /// Returns a paginated list of matching documents.
    /// </summary>
    Task<(List<BookDocument> Items, long TotalCount)> SearchAsync(
        string? searchTerm,
        int page,
        int pageSize,
        CancellationToken ct = default);

    /// <summary>Delete the index and rebuild it from the supplied books.</summary>
    Task ReindexAllAsync(IEnumerable<Book> books, CancellationToken ct = default);

    /// <summary>Returns true when Elasticsearch is reachable.</summary>
    Task<bool> IsAvailableAsync(CancellationToken ct = default);
}
