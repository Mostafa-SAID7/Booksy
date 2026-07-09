using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Reports.DTOs;
using Booksy.Models.Entities.Promotions;

namespace Booksy.Features.Promotions.Queries;

/// <summary>
/// Handler for getting a promotion by ID
/// </summary>
public class GetPromotionByIdQueryHandler : IQueryHandler<GetPromotionByIdQuery, PromotionResponse>
{
    private readonly IRepository<Promotion> _promotionRepository;
    private readonly IMapper _mapper;

    public GetPromotionByIdQueryHandler(IRepository<Promotion> promotionRepository, IMapper mapper)
    {
        _promotionRepository = promotionRepository;
        _mapper = mapper;
    }

    public async Task<PromotionResponse> Handle(GetPromotionByIdQuery request, CancellationToken cancellationToken)
    {
        var promotion = await _promotionRepository.GetByIdAsync(request.PromotionId);
        if (promotion == null)
        {
            throw new NotFoundException($"Promotion with ID {request.PromotionId} not found");
        }

        return _mapper.Map<PromotionResponse>(promotion);
    }
}
