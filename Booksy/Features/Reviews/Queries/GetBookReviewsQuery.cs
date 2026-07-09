using Booksy.Common.Models;
using Booksy.Core.Interfaces;
using Booksy.Features.Reviews.DTOs;

namespace Booksy.Features.Reviews.Queries;

/// <summary>
/// Query to get all reviews for a specific book with pagination support
/// </summary>
public class GetBookReviewsQuery : IPaginatedQuery<PaginatedResponse<ReviewDetailResponse>>
{
    public Guid BookId { get; set; }

    /// <summary>
    /// Search, filter, and pagination parameters
    /// </summary>
    public SearchFilter Filter { get; set; }

    public GetBookReviewsQuery(Guid bookId, SearchFilter? filter = null)
    {
        BookId = bookId;
        Filter = filter ?? new SearchFilter();
    }
}
