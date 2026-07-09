using Booksy.Common.Models;
using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;

namespace Booksy.Features.Authors.Queries;

/// <summary>
/// Query to get all authors with pagination, search, filter, and sort support
/// </summary>
public class GetAllAuthorsQuery : IPaginatedQuery<PaginatedResponse<AuthorResponse>>
{
    /// <summary>
    /// Search, filter, and pagination parameters
    /// </summary>
    public SearchFilter Filter { get; set; }

    public GetAllAuthorsQuery(SearchFilter? filter = null)
    {
        Filter = filter ?? new SearchFilter();
    }
}
