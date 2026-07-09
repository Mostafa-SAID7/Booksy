using Booksy.Core.Interfaces;

namespace Booksy.Features.Statistics.Queries;

/// <summary>
/// Query to get dashboard statistics
/// </summary>
public class GetDashboardStatsQuery : IQuery<DashboardStatsDto> { }

/// <summary>
/// DTO for dashboard statistics
/// </summary>
public class DashboardStatsDto
{
    public int TotalUsers { get; set; }
    public int TotalBooks { get; set; }
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public int OutOfStockItems { get; set; }
    public decimal AverageOrderValue { get; set; }
}
