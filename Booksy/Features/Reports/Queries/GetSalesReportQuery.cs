using Booksy.Core.Interfaces;

namespace Booksy.Features.Reports.Queries;

/// <summary>
/// Query to get sales report
/// </summary>
public class GetSalesReportQuery : IQuery<SalesReportDto>
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

/// <summary>
/// DTO for sales report
/// </summary>
public class SalesReportDto
{
    public DateTime ReportGeneratedAt { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalOrders { get; set; }
    public int TotalItemsSold { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal AverageOrderValue { get; set; }
    public List<SalesByCategoryDto> SalesByCategory { get; set; } = new();
}

/// <summary>
/// DTO for sales by category
/// </summary>
public class SalesByCategoryDto
{
    public string CategoryName { get; set; } = string.Empty;
    public int ItemsSold { get; set; }
    public decimal Revenue { get; set; }
}
