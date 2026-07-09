using Booksy.Common.Models;
using Booksy.Core.Interfaces;
using Booksy.Features.Books.DTOs;

namespace Booksy.Features.Books.Queries;

/// <summary>
/// Query to get all books with pagination, search, filter, and sort support
/// </summary>
public class GetAllBooksQuery : IPaginatedQuery<PaginatedResponse<BookResponse>>
{
    /// <summary>
    /// Search, filter, and pagination parameters
    /// </summary>
    public SearchFilter Filter { get; set; }

    public GetAllBooksQuery(SearchFilter? filter = null)
    {
        Filter = filter ?? new SearchFilter();
    }
}
