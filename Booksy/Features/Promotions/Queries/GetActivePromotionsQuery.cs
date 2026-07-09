using Booksy.Core.Interfaces;
using Booksy.Features.Reports.DTOs;

namespace Booksy.Features.Promotions.Queries;

/// <summary>
/// Query to get active promotions
/// </summary>
public class GetActivePromotionsQuery : IQuery<List<PromotionResponse>> { }
