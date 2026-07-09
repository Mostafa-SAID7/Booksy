using Booksy.Common.Models;
using Booksy.Core.Interfaces;
using Booksy.Features.Reviews.DTOs;

namespace Booksy.Features.Reviews.Queries;

/// <summary>
/// Query to get all reviews with pagination, search, filter, and sort support
/// </summary>
public class GetAllReviewsQuery : IPaginatedQuery<PaginatedResponse<ReviewDetailResponse>>
{
    /// <summary>
    /// Search, filter, and pagination parameters
    /// </summary>
    public SearchFilter Filter { get; set; }

    public GetAllReviewsQuery(SearchFilter? filter = null)
    {
        Filter = filter ?? new SearchFilter();
    }
}
