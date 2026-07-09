using AutoMapper;
using Booksy.Common.Models;
using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;
using Booksy.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Booksy.Features.Users.Queries;

/// <summary>
/// Handler for getting all users with pagination, search, filter, and sort support
/// </summary>
public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, PaginatedResponse<UserProfileResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(
        UserManager<ApplicationUser> userManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<UserProfileResponse>> Handle(
        GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        // Validate filter
        if (!request.Filter.IsValid(out var errors))
        {
            throw new ArgumentException($"Invalid search filter: {string.Join(", ", errors)}");
        }

        // Get all users with pagination support
        var query = _userManager.Users.AsQueryable();

        // Apply search filter if provided
        if (!string.IsNullOrWhiteSpace(request.Filter.SearchTerm))
        {
            var searchTerm = request.Filter.SearchTerm.ToLower();
            query = query.Where(u =>
                u.UserName.ToLower().Contains(searchTerm) ||
                u.Email.ToLower().Contains(searchTerm) ||
                u.FirstName.ToLower().Contains(searchTerm) ||
                u.LastName.ToLower().Contains(searchTerm));
        }

        // Get total count
        var totalCount = query.Count();

        // Apply pagination
        var users = query
            .Skip((request.Filter.PageNumber - 1) * request.Filter.PageSize)
            .Take(request.Filter.PageSize)
            .ToList();

        // Map to response DTOs
        var userResponses = _mapper.Map<List<UserProfileResponse>>(users);

        // Return paginated response
        return new PaginatedResponse<UserProfileResponse>(
            userResponses,
            request.Filter.PageNumber,
            request.Filter.PageSize,
            totalCount);
    }
}
