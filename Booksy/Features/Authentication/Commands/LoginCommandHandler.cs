using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;
using Booksy.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Booksy.Features.Authentication.Commands;

/// <summary>
/// Handler for logging in a user
/// </summary>
public class LoginCommandHandler : ICommandHandler<LoginCommand, UserProfileResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMapper _mapper;

    public LoginCommandHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task<UserProfileResponse> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        // Find user by email
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new NotFoundException($"User with email '{request.Email}' not found");
        }

        // Check if user is active
        if (!user.IsActive)
        {
            throw new BusinessException("User account is inactive");
        }

        // Verify password
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
        {
            throw new BusinessException("Invalid email or password");
        }

        // Update last login date
        user.LastLoginDate = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        return _mapper.Map<UserProfileResponse>(user);
    }
}
