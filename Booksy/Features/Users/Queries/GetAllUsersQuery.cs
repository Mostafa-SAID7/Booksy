using Booksy.Common.Models;
using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;

namespace Booksy.Features.Users.Queries;

/// <summary>
/// Query to get all users with pagination, search, filter, and sort support
/// </summary>
public class GetAllUsersQuery : IPaginatedQuery<PaginatedResponse<UserProfileResponse>>
{
    /// <summary>
    /// Search, filter, and pagination parameters
    /// </summary>
    public SearchFilter Filter { get; set; }

    public GetAllUsersQuery(SearchFilter? filter = null)
    {
        Filter = filter ?? new SearchFilter();
    }
}
