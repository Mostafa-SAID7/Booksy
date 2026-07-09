using MediatR;
using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Reviews.Commands;

/// <summary>
/// Handler for updating an existing review
/// </summary>
public class UpdateReviewCommandHandler : ICommandHandler<UpdateReviewCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateReviewCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(
        UpdateReviewCommand request,
        CancellationToken cancellationToken)
    {
        // Get existing review
        var review = await _unitOfWork.Reviews.GetByIdAsync(request.Id);
        if (review is null)
        {
            throw new NotFoundException("Review", request.Id);
        }

        // Update review fields
        review.Rating = request.Rating;
        review.Comment = request.Comment;
        review.UpdatedAt = DateTime.UtcNow;

        // Update in repository
        _unitOfWork.Reviews.Update(review);

        // Save through UnitOfWork
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
