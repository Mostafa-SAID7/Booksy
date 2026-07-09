using MediatR;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Reviews.Commands;

/// <summary>
/// Handler for deleting a review
/// </summary>
public class DeleteReviewCommandHandler : ICommandHandler<DeleteReviewCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReviewCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        DeleteReviewCommand request,
        CancellationToken cancellationToken)
    {
        // Get existing review
        var review = await _unitOfWork.Reviews.GetByIdAsync(request.Id);
        if (review is null)
        {
            throw new NotFoundException("Review", request.Id);
        }

        // Delete from repository
        _unitOfWork.Reviews.Delete(review);

        // Save through UnitOfWork
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
