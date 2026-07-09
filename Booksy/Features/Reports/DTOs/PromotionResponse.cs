namespace Booksy.Features.Reports.DTOs;

/// <summary>
/// DTO for promotion response
/// </summary>
public class PromotionResponse
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal DiscountPercentage { get; set; }
    public int MaxUses { get; set; }
    public int CurrentUses { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
}
