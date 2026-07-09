using MediatR;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;
using Booksy.Security;
using Microsoft.Extensions.Logging;

namespace Booksy.Features.Reviews.Commands;

/// <summary>
/// Handler for deleting a review
/// </summary>
public class DeleteReviewCommandHandler : ICommandHandler<DeleteReviewCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizationService _authorizationService;
    private readonly ILogger<DeleteReviewCommandHandler> _logger;

    public DeleteReviewCommandHandler(
        IUnitOfWork unitOfWork,
        IAuthorizationService authorizationService,
        ILogger<DeleteReviewCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
        _logger = logger;
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

        // OWNERSHIP VALIDATION: Verify user owns this review
        if (!_authorizationService.CanUserAccessReview(request.UserId, review.UserId))
        {
            _logger.LogWarning(
                "Unauthorized review delete attempt: User {UserId} tried to delete Review {ReviewId}",
                request.UserId,
                request.Id);
            throw new Booksy.Core.Exceptions.AuthorizationException($"You are not authorized to delete this review");
        }

        // Delete from repository
        _unitOfWork.Reviews.Delete(review);

        // Save through UnitOfWork
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Review {ReviewId} deleted by user {UserId}",
            request.Id,
            request.UserId);

        return Unit.Value;
    }
}
