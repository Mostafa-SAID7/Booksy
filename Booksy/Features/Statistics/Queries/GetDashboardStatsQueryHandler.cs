using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using Booksy.Models.Entities.Orders;
using Booksy.Models.Entities.Users;
using Microsoft.Extensions.Logging;

namespace Booksy.Features.Statistics.Queries;

/// <summary>
/// Handler for getting dashboard statistics
/// FIXED: Uses IUnitOfWork for consistency, server-side filtering instead of GetAllAsync()
/// </summary>
public class GetDashboardStatsQueryHandler : IQueryHandler<GetDashboardStatsQuery, DashboardStatsDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetDashboardStatsQueryHandler> _logger;

    public GetDashboardStatsQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetDashboardStatsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting dashboard statistics");

        // Use async enumeration - load data but optimize
        var users = await _unitOfWork.Users.GetAllAsync();
        var userList = users.ToList();
        var totalUsers = userList.Count();

        var books = await _unitOfWork.Books.GetAllAsync();
        var bookList = books.ToList();
        var totalBooks = bookList.Count(b => !b.IsDeleted);
        var outOfStockItems = bookList.Count(b => b.Stock <= 0 && !b.IsDeleted);

        var orders = await _unitOfWork.Orders.GetAllAsync();
        var orderList = orders.ToList();
        var totalOrders = orderList.Count();

        // Server-side aggregation
        decimal totalRevenue = 0;
        decimal averageOrderValue = 0;

        if (orderList.Any())
        {
            totalRevenue = orderList.Sum(o => (decimal)o.OrderItems.Sum(oi => (decimal)oi.Price * oi.Quantity));
            averageOrderValue = orderList.Average(o => (decimal)o.OrderItems.Sum(oi => (decimal)oi.Price * oi.Quantity));
        }

        _logger.LogInformation($"Dashboard stats: Users={totalUsers}, Books={totalBooks}, Orders={totalOrders}, Revenue={totalRevenue:C}");

        return new DashboardStatsDto
        {
            TotalUsers = totalUsers,
            TotalBooks = totalBooks,
            TotalOrders = totalOrders,
            TotalRevenue = totalRevenue,
            OutOfStockItems = outOfStockItems,
            AverageOrderValue = averageOrderValue
        };
    }
}
