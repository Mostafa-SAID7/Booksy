using AutoMapper;
using Booksy.Common.Models;
using Booksy.Core.Interfaces;
using Booksy.Features.Reviews.DTOs;
using Booksy.Features.Reviews.Queries.Specifications;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Reviews.Queries;

/// <summary>
/// Handler for getting all reviews with pagination, search, filter, and sort support
/// Uses database-level filtering to avoid N+1 queries
/// </summary>
public class GetAllReviewsQueryHandler : IQueryHandler<GetAllReviewsQuery, PaginatedResponse<ReviewDetailResponse>>
{
    private readonly IRepository<Review> _reviewRepository;
    private readonly IMapper _mapper;

    public GetAllReviewsQueryHandler(
        IRepository<Review> reviewRepository,
        IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<ReviewDetailResponse>> Handle(
        GetAllReviewsQuery request,
        CancellationToken cancellationToken)
    {
        // Validate filter
        if (!request.Filter.IsValid(out var errors))
        {
            throw new ArgumentException($"Invalid search filter: {string.Join(", ", errors)}");
        }

        // Create specification with search/filter/sort/pagination
        // Includes Book and User to avoid N+1 query problem
        var specification = new GetReviewsSpecification(request.Filter);

        // Get paginated results from database (with includes applied)
        var (items, totalCount) = await _reviewRepository.GetPaginatedAsync(specification);

        // Map to response DTOs
        var reviewResponses = _mapper.Map<List<ReviewDetailResponse>>(items);

        // Return paginated response
        return new PaginatedResponse<ReviewDetailResponse>(
            reviewResponses,
            request.Filter.PageNumber,
            request.Filter.PageSize,
            totalCount);
    }
}
