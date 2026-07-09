using Booksy.Core.Interfaces;
using Booksy.Features.Reviews.DTOs;

namespace Booksy.Features.Reviews.Queries;

/// <summary>
/// Query to get a review by ID
/// </summary>
public class GetReviewByIdQuery : IQuery<ReviewDetailResponse>
{
    public Guid Id { get; set; }
}
