using AutoMapper;
using Booksy.Common.Models;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Reviews.DTOs;
using Booksy.Features.Reviews.Queries.Specifications;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Reviews.Queries;

/// <summary>
/// Handler for getting all reviews for a specific book with pagination support
/// </summary>
public class GetBookReviewsQueryHandler : IQueryHandler<GetBookReviewsQuery, PaginatedResponse<ReviewDetailResponse>>
{
    private readonly IRepository<Review> _reviewRepository;
    private readonly IRepository<Book> _bookRepository;
    private readonly IMapper _mapper;

    public GetBookReviewsQueryHandler(
        IRepository<Review> reviewRepository,
        IRepository<Book> bookRepository,
        IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<ReviewDetailResponse>> Handle(
        GetBookReviewsQuery request,
        CancellationToken cancellationToken)
    {
        // Verify book exists
        var book = await _bookRepository.GetByIdAsync(request.BookId);
        if (book is null)
        {
            throw new NotFoundException("Book", request.BookId);
        }

        // Validate filter
        if (!request.Filter.IsValid(out var errors))
        {
            throw new ArgumentException($"Invalid search filter: {string.Join(", ", errors)}");
        }

        // Create specification for book reviews with pagination
        var specification = new GetReviewsSpecification(request.BookId, request.Filter);

        // Get paginated results from database
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
