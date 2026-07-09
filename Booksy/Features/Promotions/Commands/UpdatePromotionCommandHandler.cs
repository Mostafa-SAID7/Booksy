using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Reports.DTOs;
using Booksy.Models.Entities.Promotions;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Promotions.Commands;

/// <summary>
/// Handler for updating a promotion
/// </summary>
public class UpdatePromotionCommandHandler : ICommandHandler<UpdatePromotionCommand, PromotionResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdatePromotionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PromotionResponse> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
    {
        // Get promotion
        var promotion = await _unitOfWork.Promotions.GetByIdAsync(request.PromotionId);
        if (promotion == null)
        {
            throw new NotFoundException($"Promotion with ID {request.PromotionId} not found");
        }

        // Validate dates
        if (request.StartDate >= request.EndDate)
        {
            throw new BusinessException("Start date must be before end date");
        }

        // Update promotion
        promotion.Code = request.Code;
        promotion.Type = request.Type;
        promotion.Value = request.Value;
        promotion.StartDate = request.StartDate;
        promotion.EndDate = request.EndDate;

        // Save changes through UnitOfWork
        _unitOfWork.Promotions.Update(promotion);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PromotionResponse>(promotion);
    }
}
