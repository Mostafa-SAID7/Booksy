using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;
using Booksy.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Booksy.Features.Users.Commands;

/// <summary>
/// Handler for updating user profile
/// </summary>
public class UpdateProfileCommandHandler : ICommandHandler<UpdateProfileCommand, UserProfileResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public UpdateProfileCommandHandler(
        UserManager<ApplicationUser> userManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UserProfileResponse> Handle(
        UpdateProfileCommand request,
        CancellationToken cancellationToken)
    {
        // Find user by ID
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new NotFoundException($"User with ID '{request.UserId}' not found");
        }

        // Update user properties
        if (!string.IsNullOrEmpty(request.FirstName))
            user.FirstName = request.FirstName;

        if (!string.IsNullOrEmpty(request.LastName))
            user.LastName = request.LastName;

        if (!string.IsNullOrEmpty(request.PhoneNumber))
            user.PhoneNumber = request.PhoneNumber;

        if (!string.IsNullOrEmpty(request.Street))
            user.Street = request.Street;

        if (!string.IsNullOrEmpty(request.City))
            user.City = request.City;

        if (!string.IsNullOrEmpty(request.State))
            user.State = request.State;

        if (!string.IsNullOrEmpty(request.ZipCode))
            user.ZipCode = request.ZipCode;

        if (!string.IsNullOrEmpty(request.Country))
            user.Country = request.Country;

        // Save changes
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BusinessException($"Profile update failed: {errors}");
        }

        return _mapper.Map<UserProfileResponse>(user);
    }
}
