using AutoMapper;
using Booksy.Common.Models;
using Booksy.Common.Results;
using Booksy.Features.Books.DTOs;
using Booksy.Infrastructure.Search;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booksy.Features.Search;

/// <summary>
/// Elasticsearch-backed search endpoints.
/// Falls back to an empty result set (not an error) when Elasticsearch is unreachable,
/// so consumers can degrade gracefully.
/// </summary>
[Route("api/search")]
[ApiController]
[Tags("Search")]
public class SearchController : ControllerBase
{
    private readonly IBookSearchService _search;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<SearchController> _logger;

    public SearchController(
        IBookSearchService search,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<SearchController> logger)
    {
        _search = search;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Full-text search across books using Elasticsearch.
    /// Matches against title (×4), author name (×3), ISBN (×3), category (×2), tags (×2), and description.
    /// Supports fuzzy matching — typos in the query are tolerated automatically.
    /// Returns an empty page (not an error) when Elasticsearch is unavailable.
    /// </summary>
    /// <param name="q">Search query. Omit to list all books ordered by newest.</param>
    /// <param name="pageNumber">Page number (default: 1)</param>
    /// <param name="pageSize">Items per page (default: 10, max: 100)</param>
    [HttpGet("books")]
    [ProducesResponseType(typeof(Result<PaginatedResponse<BookSearchResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<PaginatedResponse<BookSearchResponse>>>> SearchBooks(
        [FromQuery] string? q = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        pageNumber = Math.Max(1, pageNumber);
        pageSize   = Math.Clamp(pageSize, 1, 100);

        var (docs, total) = await _search.SearchAsync(q, pageNumber, pageSize, ct);

        var items = docs.Select(d => new BookSearchResponse
        {
            Id            = Guid.Parse(d.Id),
            Title         = d.Title,
            Slug          = d.Slug,
            Price         = d.Price,
            Stock         = d.Stock,
            CoverImageUrl = d.CoverImageUrl,
            AuthorName    = d.AuthorName,
            CategoryName  = d.CategoryName,
            Description   = d.Description,
            Isbn          = d.Isbn,
            TagNames      = d.TagNames
        }).ToList();

        var paginated = new PaginatedResponse<BookSearchResponse>(items, pageNumber, pageSize, (int)total);
        return Ok(Result<PaginatedResponse<BookSearchResponse>>.Ok(paginated));
    }

    /// <summary>
    /// Check whether Elasticsearch is reachable and the books index exists.
    /// </summary>
    [HttpGet("health")]
    [ProducesResponseType(typeof(Result<object>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<object>>> Health(CancellationToken ct)
    {
        var available = await _search.IsAvailableAsync(ct);
        var payload = new { elasticsearch = available ? "up" : "down" };
        return Ok(Result<object>.Ok(payload));
    }

    /// <summary>
    /// Rebuild the Elasticsearch books index from the PostgreSQL database.
    /// Deletes all existing index data then re-indexes every non-deleted book.
    /// Admin only.
    /// </summary>
    [HttpPost("reindex")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status502BadGateway)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Reindex(CancellationToken ct)
    {
        try
        {
            var books = await _unitOfWork.Books.GetAsync(
                filter: b => !b.IsDeleted,
                includes: new System.Linq.Expressions.Expression<Func<Book, object>>[]
                {
                    b => b.Author,
                    b => b.Category,
                    b => b.Tags
                });

            await _search.ReindexAllAsync(books, ct);
            return Ok(Result.Ok($"Reindex complete — {books.Count()} books indexed."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Reindex failed");
            return StatusCode(StatusCodes.Status502BadGateway,
                Result.Fail("Elasticsearch is unavailable. Reindex aborted."));
        }
    }
}
