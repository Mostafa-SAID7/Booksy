using Booksy.Core.Interfaces;
using Booksy.Features.Reports.DTOs;
using Booksy.Models.Enums;

namespace Booksy.Features.Promotions.Commands;

/// <summary>
/// Command to update an existing promotion
/// </summary>
public class UpdatePromotionCommand : ICommand<PromotionResponse>
{
    public Guid PromotionId { get; set; }
    public string Code { get; set; } = string.Empty;
    public PromotionType Type { get; set; }
    public decimal Value { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
