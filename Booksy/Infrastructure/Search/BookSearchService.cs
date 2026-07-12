using Booksy.Models.Entities.Books;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.Extensions.Logging;

namespace Booksy.Infrastructure.Search;

/// <summary>
/// Elasticsearch-backed full-text search for books.
/// All public methods catch transport/connectivity exceptions so a missing or
/// unreachable Elasticsearch cluster never crashes the API.
/// </summary>
public class BookSearchService : IBookSearchService
{
    private const string IndexName = "booksy-books";

    private readonly ElasticsearchClient _client;
    private readonly ILogger<BookSearchService> _logger;

    public BookSearchService(ElasticsearchClient client, ILogger<BookSearchService> logger)
    {
        _client = client;
        _logger = logger;
    }

    // ── helpers ─────────────────────────────────────────────────────────────

    private static BookDocument ToDocument(Book book) => new()
    {
        Id            = book.Id.ToString(),
        Title         = book.Title,
        Slug          = book.Slug,
        Description   = book.Description,
        Isbn          = book.ISBN,
        Price         = book.Price,
        Stock         = book.Stock,
        CoverImageUrl = book.CoverImageUrl,
        AuthorId      = book.AuthorId.ToString(),
        AuthorName    = book.Author?.Name   ?? string.Empty,
        CategoryId    = book.CategoryId.ToString(),
        CategoryName  = book.Category?.Name ?? string.Empty,
        TagNames      = book.Tags?.Select(t => t.Name).ToList() ?? new List<string>(),
        CreatedAt     = book.CreatedAt,
        IsDeleted     = book.IsDeleted
    };

    /// <summary>Create the index if it does not exist (uses ES dynamic mapping).</summary>
    private async Task EnsureIndexAsync(CancellationToken ct)
    {
        var exists = await _client.Indices.ExistsAsync(IndexName, ct);
        if (exists.Exists) return;

        var create = await _client.Indices.CreateAsync(IndexName, ct);
        if (!create.IsSuccess())
            _logger.LogWarning("Failed to create index '{Index}': {Error}",
                IndexName, create.DebugInformation);
    }

    // ── public API ──────────────────────────────────────────────────────────

    public async Task IndexBookAsync(Book book, CancellationToken ct = default)
    {
        try
        {
            await EnsureIndexAsync(ct);
            var doc = ToDocument(book);
            var response = await _client.IndexAsync(doc, i => i
                .Index(IndexName)
                .Id(doc.Id), ct);

            if (!response.IsSuccess())
                _logger.LogWarning("ES index failed for book {Id}: {Error}",
                    book.Id, response.DebugInformation);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Elasticsearch unavailable while indexing book {Id}", book.Id);
        }
    }

    public async Task RemoveBookAsync(Guid bookId, CancellationToken ct = default)
    {
        try
        {
            var response = await _client.DeleteAsync(IndexName, bookId.ToString(), ct);
            // 404 is fine — document may not have been indexed yet
            if (!response.IsSuccess() && response.Result != Elastic.Clients.Elasticsearch.Result.NotFound)
                _logger.LogWarning("ES delete failed for book {Id}: {Error}",
                    bookId, response.DebugInformation);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Elasticsearch unavailable while removing book {Id}", bookId);
        }
    }

    public async Task<(List<BookDocument> Items, long TotalCount)> SearchAsync(
        string? searchTerm,
        int page,
        int pageSize,
        CancellationToken ct = default)
    {
        var from = (page - 1) * pageSize;

        try
        {
            await EnsureIndexAsync(ct);

            SearchResponse<BookDocument> response;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                // No term — return all non-deleted books, newest first
                response = await _client.SearchAsync<BookDocument>(s => s
                    .Index(IndexName)
                    .From(from)
                    .Size(pageSize)
                    .Query(q => q.Term(t => t.Field(d => d.IsDeleted).Value(false)))
                    .Sort(sort => sort.Field(f => f.CreatedAt, fs => fs.Order(SortOrder.Desc))), ct);
            }
            else
            {
                // Full-text multi-match with relevance scoring + isDeleted filter
                response = await _client.SearchAsync<BookDocument>(s => s
                    .Index(IndexName)
                    .From(from)
                    .Size(pageSize)
                    .Query(q => q.Bool(b => b
                        .Must(must => must.MultiMatch(mm => mm
                            .Fields(new[]
                            {
                                "title^4",
                                "authorName^3",
                                "isbn^3",
                                "categoryName^2",
                                "tagNames^2",
                                "description"
                            })
                            .Query(searchTerm.Trim())
                            .Fuzziness(new Fuzziness("AUTO"))
                            .Type(TextQueryType.BestFields)
                            .MinimumShouldMatch("1")
                        ))
                        .Filter(f => f.Term(t => t.Field(d => d.IsDeleted).Value(false)))
                    )), ct);
            }

            if (!response.IsSuccess())
            {
                _logger.LogWarning("ES search failed: {Error}", response.DebugInformation);
                return (new List<BookDocument>(), 0);
            }

            return (response.Documents.ToList(), response.Total);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Elasticsearch unavailable during search");
            return (new List<BookDocument>(), 0);
        }
    }

    public async Task ReindexAllAsync(IEnumerable<Book> books, CancellationToken ct = default)
    {
        // Drop and recreate for a clean rebuild
        var exists = await _client.Indices.ExistsAsync(IndexName, ct);
        if (exists.Exists)
            await _client.Indices.DeleteAsync(IndexName, ct);

        await EnsureIndexAsync(ct);

        var bookList = books.ToList();
        if (bookList.Count == 0) return;

        // Bulk-index in batches of 500
        const int batchSize = 500;
        for (int i = 0; i < bookList.Count; i += batchSize)
        {
            var batch = bookList.Skip(i).Take(batchSize).Select(ToDocument).ToList();

            var bulkResponse = await _client.BulkAsync(b => b
                .Index(IndexName)
                .IndexMany(batch, (op, doc) => op.Id(doc.Id)), ct);

            if (bulkResponse.Errors)
                _logger.LogWarning("Bulk index batch had errors: {Error}", bulkResponse.DebugInformation);
        }

        _logger.LogInformation("Reindex complete — {Count} books indexed", bookList.Count);
    }

    public async Task<bool> IsAvailableAsync(CancellationToken ct = default)
    {
        try
        {
            var ping = await _client.PingAsync(ct);
            return ping.IsSuccess();
        }
        catch
        {
            return false;
        }
    }
}
