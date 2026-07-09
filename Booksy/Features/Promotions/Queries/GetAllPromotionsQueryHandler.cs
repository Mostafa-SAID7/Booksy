using AutoMapper;
using Booksy.Core.Interfaces;
using Booksy.Features.Reports.DTOs;
using Booksy.Models.Entities.Promotions;

namespace Booksy.Features.Promotions.Queries;

/// <summary>
/// Handler for getting all promotions
/// </summary>
public class GetAllPromotionsQueryHandler : IQueryHandler<GetAllPromotionsQuery, List<PromotionResponse>>
{
    private readonly IRepository<Promotion> _promotionRepository;
    private readonly IMapper _mapper;

    public GetAllPromotionsQueryHandler(IRepository<Promotion> promotionRepository, IMapper mapper)
    {
        _promotionRepository = promotionRepository;
        _mapper = mapper;
    }

    public async Task<List<PromotionResponse>> Handle(GetAllPromotionsQuery request, CancellationToken cancellationToken)
    {
        var promotions = await _promotionRepository.GetAllAsync();
        return _mapper.Map<List<PromotionResponse>>(promotions);
    }
}
