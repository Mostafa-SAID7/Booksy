using AutoMapper;
using Booksy.Core.Interfaces;
using Booksy.Features.Reports.DTOs;
using Booksy.Models.Entities.Promotions;
using Microsoft.Extensions.Logging;

namespace Booksy.Features.Promotions.Queries;

/// <summary>
/// Handler for getting active promotions
/// </summary>
public class GetActivePromotionsQueryHandler : IQueryHandler<GetActivePromotionsQuery, List<PromotionResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetActivePromotionsQueryHandler> _logger;

    public GetActivePromotionsQueryHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<GetActivePromotionsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<PromotionResponse>> Handle(GetActivePromotionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching active promotions");

        var now = DateTime.UtcNow;
        var activePromotions = await _unitOfWork.Promotions.GetAsync(
            p => p.IsActive && p.StartDate <= now && p.EndDate >= now);

        _logger.LogInformation("Found {PromotionCount} active promotions", activePromotions.Count());

        return _mapper.Map<List<PromotionResponse>>(activePromotions);
    }
}
