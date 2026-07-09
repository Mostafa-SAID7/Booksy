using Booksy.Common.Models;
using Booksy.Core.Interfaces;
using Booksy.Features.Categories.DTOs;

namespace Booksy.Features.Categories.Queries;

/// <summary>
/// Query to get all categories with pagination, search, filter, and sort support
/// </summary>
public class GetAllCategoriesQuery : IPaginatedQuery<PaginatedResponse<CategoryResponse>>
{
    /// <summary>
    /// Search, filter, and pagination parameters
    /// </summary>
    public SearchFilter Filter { get; set; }

    public GetAllCategoriesQuery(SearchFilter? filter = null)
    {
        Filter = filter ?? new SearchFilter();
    }
}
