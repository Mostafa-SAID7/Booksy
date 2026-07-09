using Booksy.Core.Interfaces;
using Booksy.Features.Reports.DTOs;

namespace Booksy.Features.Promotions.Queries;

/// <summary>
/// Query to get all promotions
/// </summary>
public class GetAllPromotionsQuery : IQuery<List<PromotionResponse>> { }
