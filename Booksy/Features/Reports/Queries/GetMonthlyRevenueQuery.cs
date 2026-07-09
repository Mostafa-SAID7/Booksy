using Booksy.Core.Interfaces;

namespace Booksy.Features.Reports.Queries;

/// <summary>
/// Query to get monthly revenue
/// </summary>
public class GetMonthlyRevenueQuery : IQuery<List<MonthlyRevenueDto>>
{
    public int Months { get; set; } = 12;
}

/// <summary>
/// DTO for monthly revenue data
/// </summary>
public class MonthlyRevenueDto
{
    public DateTime Month { get; set; }
    public decimal Revenue { get; set; }
    public int OrderCount { get; set; }
}
