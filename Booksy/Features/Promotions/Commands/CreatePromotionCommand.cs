using Booksy.Core.Interfaces;
using Booksy.Features.Reports.DTOs;
using Booksy.Models.Enums;

namespace Booksy.Features.Promotions.Commands;

/// <summary>
/// Command to create a new promotion
/// </summary>
public class CreatePromotionCommand : ICommand<PromotionResponse>
{
    public string Code { get; set; } = string.Empty;
    public PromotionType Type { get; set; }
    public decimal Value { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
