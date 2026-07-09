using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Promotions;
using MediatR;

namespace Booksy.Features.Promotions.Commands;

/// <summary>
/// Handler for deleting a promotion
/// </summary>
public class DeletePromotionCommandHandler : ICommandHandler<DeletePromotionCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePromotionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeletePromotionCommand request, CancellationToken cancellationToken)
    {
        // Get promotion
        var promotion = await _unitOfWork.Promotions.GetByIdAsync(request.PromotionId);
        if (promotion == null)
        {
            throw new NotFoundException($"Promotion with ID {request.PromotionId} not found");
        }

        // Delete promotion
        _unitOfWork.Promotions.Delete(promotion);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
