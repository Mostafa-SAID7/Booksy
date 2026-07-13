using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;
using Booksy.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Booksy.Features.Authentication.Commands;

/// <summary>
/// Handler for registering a new user — error messages are localised
/// </summary>
public class RegisterCommandHandler : ICommandHandler<RegisterCommand, UserProfileResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<LocalizationController> _localizer;

    public RegisterCommandHandler(
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        IStringLocalizer<LocalizationController> localizer)
    {
        _userManager = userManager;
        _mapper      = mapper;
        _localizer   = localizer;
    }

    public async Task<UserProfileResponse> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
            throw new ConflictException(_localizer["UserAlreadyExists"]);

        var user = new ApplicationUser
        {
            UserName       = request.Email,
            Email          = request.Email,
            Name           = request.Name,
            FirstName      = request.FirstName,
            LastName       = request.LastName,
            EmailConfirmed = false,
            RegisteredDate = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BusinessException($"{_localizer["RegistrationFailed"]}: {errors}");
        }

        return _mapper.Map<UserProfileResponse>(user);
    }
}
