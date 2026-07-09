using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;
using Booksy.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Booksy.Features.Authentication.Commands;

/// <summary>
/// Handler for registering a new user
/// </summary>
public class RegisterCommandHandler : ICommandHandler<RegisterCommand, UserProfileResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(
        UserManager<ApplicationUser> userManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UserProfileResponse> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        // Check if user already exists
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new ConflictException($"A user with email '{request.Email}' already exists");
        }

        // Create new user
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            Name = request.Name,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailConfirmed = false,
            RegisteredDate = DateTime.UtcNow
        };

        // Create user with password
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BusinessException($"User registration failed: {errors}");
        }

        return _mapper.Map<UserProfileResponse>(user);
    }
}
