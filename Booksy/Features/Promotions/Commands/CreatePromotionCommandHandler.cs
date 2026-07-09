using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Reports.DTOs;
using Booksy.Models.Entities.Promotions;
using Booksy.Models.Enums;

namespace Booksy.Features.Promotions.Commands;

/// <summary>
/// Handler for creating a promotion
/// </summary>
public class CreatePromotionCommandHandler : ICommandHandler<CreatePromotionCommand, PromotionResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePromotionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PromotionResponse> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
    {
        // Validate dates
        if (request.StartDate >= request.EndDate)
        {
            throw new BusinessException("Start date must be before end date");
        }

        // Create promotion
        var promotion = new Promotion
        {
            Code = request.Code,
            Value = request.Value,
            Type = request.Type,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsDeleted = false
        };

        // Save to database
        await _unitOfWork.Promotions.AddAsync(promotion);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PromotionResponse>(promotion);
    }
}
