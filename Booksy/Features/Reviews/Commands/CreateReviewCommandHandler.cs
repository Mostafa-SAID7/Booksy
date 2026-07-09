using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Reviews.DTOs;
using Booksy.Models.Entities.Books;
using Booksy.Models.Enums;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Reviews.Commands;

/// <summary>
/// Handler for creating a new review
/// </summary>
public class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand, ReviewDetailResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateReviewCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ReviewDetailResponse> Handle(
        CreateReviewCommand request,
        CancellationToken cancellationToken)
    {
        // Verify book exists
        var book = await _unitOfWork.Books.GetByIdAsync(request.BookId);
        if (book is null)
        {
            throw new NotFoundException("Book", request.BookId);
        }

        // Create new review entity
        var review = new Review
        {
            BookId = request.BookId,
            Rating = request.Rating,
            Comment = request.Comment,
            ReviewerName = request.ReviewerName,
            Status = ReviewStatus.Pending,
            UserId = string.Empty,  // Will be set from authentication in controller if needed
            CreatedAt = DateTime.UtcNow
        };

        // Add to repository
        await _unitOfWork.Reviews.AddAsync(review);

        // Save through UnitOfWork for centralized transaction management
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Map and return response
        var response = _mapper.Map<ReviewDetailResponse>(review);
        response.BookTitle = book.Title;
        response.UserEmail = string.Empty;

        return response;
    }
}
