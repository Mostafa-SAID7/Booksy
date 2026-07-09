using Booksy.Core.Interfaces;

namespace Booksy.Features.Statistics.Queries;

/// <summary>
/// Query to get user statistics
/// </summary>
public class GetUserStatsQuery : IQuery<UserStatsDto> { }

/// <summary>
/// DTO for user statistics
/// </summary>
public class UserStatsDto
{
    public int TotalActiveUsers { get; set; }
    public int TotalInactiveUsers { get; set; }
    public int NewUsersThisMonth { get; set; }
    public int UsersWithOrders { get; set; }
    public decimal AverageUserOrderValue { get; set; }
}
