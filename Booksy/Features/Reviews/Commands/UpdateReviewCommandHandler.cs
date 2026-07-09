using MediatR;
using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;
using Booksy.Common.Services;
using Microsoft.Extensions.Logging;

namespace Booksy.Features.Reviews.Commands;

/// <summary>
/// Handler for updating an existing review
/// </summary>
public class UpdateReviewCommandHandler : ICommandHandler<UpdateReviewCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateReviewCommandHandler> _logger;

    public UpdateReviewCommandHandler(
        IUnitOfWork unitOfWork,
        IAuthorizationService authorizationService,
        IMapper mapper,
        ILogger<UpdateReviewCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
        _mapper = mapper;
        _logger = logger;
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

        // OWNERSHIP VALIDATION: Verify user owns this review
        if (!_authorizationService.CanUserAccessReview(request.UserId, review.UserId))
        {
            _logger.LogWarning(
                "Unauthorized review update attempt: User {UserId} tried to update Review {ReviewId}",
                request.UserId,
                request.Id);
            throw new AuthorizationException($"You are not authorized to update this review");
        }

        // Update review fields
        review.Rating = request.Rating;
        review.Comment = request.Comment;
        review.UpdatedAt = DateTime.UtcNow;

        // Update in repository
        _unitOfWork.Reviews.Update(review);

        // Save through UnitOfWork
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Review {ReviewId} updated by user {UserId}",
            request.Id,
            request.UserId);

        return Unit.Value;
    }
}
