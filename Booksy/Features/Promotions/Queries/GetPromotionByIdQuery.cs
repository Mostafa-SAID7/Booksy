using Booksy.Core.Interfaces;
using Booksy.Features.Reports.DTOs;

namespace Booksy.Features.Promotions.Queries;

/// <summary>
/// Query to get a promotion by ID
/// </summary>
public class GetPromotionByIdQuery : IQuery<PromotionResponse>
{
    public Guid PromotionId { get; set; }
}
