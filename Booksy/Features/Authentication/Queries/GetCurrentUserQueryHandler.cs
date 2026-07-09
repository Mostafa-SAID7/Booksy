using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;
using Booksy.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Booksy.Features.Authentication.Queries;

/// <summary>
/// Handler for getting current user
/// </summary>
public class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, UserProfileResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public GetCurrentUserQueryHandler(
        UserManager<ApplicationUser> userManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UserProfileResponse> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        // Find user by ID
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new NotFoundException($"User with ID '{request.UserId}' not found");
        }

        return _mapper.Map<UserProfileResponse>(user);
    }
}
