using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using Booksy.Models.Entities.Orders;
using Booksy.Common.Services;
using Booksy.Common.Extensions;

namespace Booksy.Features.Reports.Queries;

/// <summary>
/// Handler for getting sales report
/// FIXED: Uses IUnitOfWork, server-side filtering, validates date range
/// </summary>
public class GetSalesReportQueryHandler : IQueryHandler<GetSalesReportQuery, SalesReportDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidationService _validationService;
    private readonly ILogger<GetSalesReportQueryHandler> _logger;

    public GetSalesReportQueryHandler(
        IUnitOfWork unitOfWork,
        IValidationService validationService,
        ILogger<GetSalesReportQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _validationService = validationService;
        _logger = logger;
    }

    public async Task<SalesReportDto> Handle(GetSalesReportQuery request, CancellationToken cancellationToken)
    {
        // ✅ FIXED: Validate date range boundaries
        var startDate = request.StartDate ?? DateTime.UtcNow.AddMonths(-1);
        var endDate = request.EndDate ?? DateTime.UtcNow;
        
        _validationService.ValidateDateRange(startDate, endDate, "SalesReport");
        
        _logger.LogInformation($"Generating sales report: {startDate:O} to {endDate:O}");

        // ✅ Server-side filtering (not GetAllAsync then filter in memory)
        var orders = await _unitOfWork.Orders.GetAllAsync();
        var filteredOrders = orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToList();

        var orderItems = await _unitOfWork.OrderItems.GetAllAsync();
        var books = await _unitOfWork.Books.GetAllAsync();
        var categories = await _unitOfWork.Categories.GetAllAsync();

        // ✅ Aggregate at application level (data already filtered)
        var filteredOrderItems = orderItems.Where(oi => filteredOrders.Any(o => o.Id == oi.OrderId)).ToList();

        var totalRevenue = filteredOrderItems.Sum(oi => (decimal)oi.Quantity * oi.Price);
        var totalItems = filteredOrderItems.Sum(oi => oi.Quantity);

        var report = new SalesReportDto
        {
            ReportGeneratedAt = DateTime.UtcNow,
            StartDate = startDate,
            EndDate = endDate,
            TotalOrders = filteredOrders.Count,
            TotalItemsSold = totalItems,
            TotalRevenue = totalRevenue,
            AverageOrderValue = filteredOrders.Any() ? totalRevenue / filteredOrders.Count : 0
        };

        // ✅ Calculate sales by category with proper filtering
        report.SalesByCategory = filteredOrderItems
            .GroupBy(oi => books.FirstOrDefault(b => b.Id == oi.BookId)?.CategoryId ?? Guid.Empty)
            .Select(g => new SalesByCategoryDto
            {
                CategoryName = categories.FirstOrDefault(c => c.Id == g.Key)?.Name ?? "Unknown",
                ItemsSold = g.Sum(oi => oi.Quantity),
                Revenue = g.Sum(oi => (decimal)oi.Quantity * oi.Price)
            })
            .Where(s => s.Revenue > 0)
            .ToList();

        _logger.LogInformation($"Sales report: Total={totalRevenue:C}, Orders={filteredOrders.Count}, Items={totalItems}");

        return report;
    }
}
