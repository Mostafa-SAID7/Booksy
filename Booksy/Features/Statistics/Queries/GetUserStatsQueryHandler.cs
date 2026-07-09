using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Orders;
using Booksy.Models.Entities.Users;
using Microsoft.Extensions.Logging;

namespace Booksy.Features.Statistics.Queries;

/// <summary>
/// Handler for getting user statistics
/// </summary>
public class GetUserStatsQueryHandler : IQueryHandler<GetUserStatsQuery, UserStatsDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetUserStatsQueryHandler> _logger;

    public GetUserStatsQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetUserStatsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<UserStatsDto> Handle(GetUserStatsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching user statistics");

        var thisMonth = DateTime.UtcNow.AddMonths(-1);

        // Use async enumeration - load data but once
        var users = await _unitOfWork.Users.GetAsync();
        var orders = await _unitOfWork.Orders.GetAsync();

        var userList = users.ToList();
        var orderList = orders.ToList();

        var usersWithOrders = orderList.Select(o => o.UserId).Distinct().Count();

        _logger.LogInformation(
            "User statistics calculated: ActiveUsers={Active}, NewThisMonth={New}, UsersWithOrders={WithOrders}",
            userList.Count(u => u.IsActive),
            userList.Count(u => u.RegisteredDate >= thisMonth),
            usersWithOrders);

        return new UserStatsDto
        {
            TotalActiveUsers = userList.Count(u => u.IsActive),
            TotalInactiveUsers = userList.Count(u => !u.IsActive),
            NewUsersThisMonth = userList.Count(u => u.RegisteredDate >= thisMonth),
            UsersWithOrders = usersWithOrders,
            AverageUserOrderValue = orderList.Any() ? (decimal)orderList.Average(o => (double)o.OrderItems.Sum(oi => (decimal)oi.Price * oi.Quantity)) : 0
        };
    }
}
