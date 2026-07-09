using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Orders;
using Microsoft.Extensions.Logging;

namespace Booksy.Features.Reports.Queries;

/// <summary>
/// Handler for getting monthly revenue
/// </summary>
public class GetMonthlyRevenueQueryHandler : IQueryHandler<GetMonthlyRevenueQuery, List<MonthlyRevenueDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetMonthlyRevenueQueryHandler> _logger;

    public GetMonthlyRevenueQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetMonthlyRevenueQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<MonthlyRevenueDto>> Handle(GetMonthlyRevenueQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching monthly revenue for {Months} months", request.Months);

        var startDate = DateTime.UtcNow.AddMonths(-request.Months);

        // Use async enumeration - load orders and items
        var orders = await _unitOfWork.Orders.GetAsync(o => o.OrderDate >= startDate);
        var orderItems = await _unitOfWork.OrderItems.GetAsync();

        var monthlyRevenue = orders
            .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
            .Select(g => new MonthlyRevenueDto
            {
                Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                Revenue = orderItems.Where(oi => g.Any(o => o.Id == oi.OrderId)).Sum(oi => (decimal)oi.Quantity * oi.Price),
                OrderCount = g.Count()
            })
            .OrderBy(x => x.Month)
            .ToList();

        _logger.LogInformation("Monthly revenue calculated for {Count} months", monthlyRevenue.Count);

        return monthlyRevenue;
    }
}
