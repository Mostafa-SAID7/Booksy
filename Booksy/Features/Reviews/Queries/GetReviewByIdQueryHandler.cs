using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Reviews.DTOs;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Reviews.Queries;

/// <summary>
/// Handler for getting a review by ID
/// </summary>
public class GetReviewByIdQueryHandler : IQueryHandler<GetReviewByIdQuery, ReviewDetailResponse>
{
    private readonly IRepository<Review> _reviewRepository;
    private readonly IRepository<Book> _bookRepository;
    private readonly IMapper _mapper;

    public GetReviewByIdQueryHandler(
        IRepository<Review> reviewRepository,
        IRepository<Book> bookRepository,
        IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<ReviewDetailResponse> Handle(
        GetReviewByIdQuery request,
        CancellationToken cancellationToken)
    {
        var review = await _reviewRepository.GetByIdAsync(request.Id);
        if (review is null)
        {
            throw new NotFoundException("Review", request.Id);
        }

        var response = _mapper.Map<ReviewDetailResponse>(review);

        // Get book title
        var book = await _bookRepository.GetByIdAsync(review.BookId);
        if (book != null)
        {
            response.BookTitle = book.Title;
        }

        return response;
    }
}
