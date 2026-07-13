using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;
using Booksy.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Booksy.Features.Authentication.Commands;

/// <summary>
/// Handler for logging in a user — error messages are localised
/// </summary>
public class LoginCommandHandler : ICommandHandler<LoginCommand, UserProfileResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<LocalizationController> _localizer;

    public LoginCommandHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IMapper mapper,
        IStringLocalizer<LocalizationController> localizer)
    {
        _userManager   = userManager;
        _signInManager = signInManager;
        _mapper        = mapper;
        _localizer     = localizer;
    }

    public async Task<UserProfileResponse> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            throw new NotFoundException(_localizer["UserNotFound"]);

        if (!user.IsActive)
            throw new BusinessException(_localizer["UserInactive"]);

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            throw new BusinessException(_localizer["InvalidCredentials"]);

        user.LastLoginDate = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        return _mapper.Map<UserProfileResponse>(user);
    }
}
