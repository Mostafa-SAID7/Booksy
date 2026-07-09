using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Promotions;
using MediatR;

namespace Booksy.Features.Promotions.Commands;

/// <summary>
/// Handler for activating/deactivating a promotion
/// </summary>
public class ActivatePromotionCommandHandler : ICommandHandler<ActivatePromotionCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public ActivatePromotionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ActivatePromotionCommand request, CancellationToken cancellationToken)
    {
        // Get promotion
        var promotion = await _unitOfWork.Promotions.GetByIdAsync(request.PromotionId);
        if (promotion == null)
        {
            throw new NotFoundException($"Promotion with ID {request.PromotionId} not found");
        }

        // Update status by toggling IsDeleted (IsActive is computed as !IsDeleted && within date range)
        promotion.IsDeleted = !request.IsActive;

        // Save changes
        _unitOfWork.Promotions.Update(promotion);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
