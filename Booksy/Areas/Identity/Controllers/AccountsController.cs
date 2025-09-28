using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.WebUtilities;
using Booksy.Models.DTOs.Request.User;
using Booksy.Models.DTOs.Response;
using Booksy.Models.Entities.Users;

namespace Booksy.Areas.Identity.Controllers
{
    [Area("Identity")]
    [ApiController]
    [Route("api/[area]/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRepository<UserOTP> _userOTP;
        private readonly IStringLocalizer<LocalizationController> _localizer;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            SignInManager<ApplicationUser> signInManager,
            IRepository<UserOTP> userOTP,
            IStringLocalizer<LocalizationController> localizer,
            ILogger<AccountsController> logger)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _userOTP = userOTP;
            _localizer = localizer;
            _logger = logger;
        }

        // Helper: convert IdentityResult errors into a list of descriptions
        private static IEnumerable<string> GetIdentityErrors(IdentityResult result)
            => result.Errors?.Select(e => e.Description) ?? Enumerable.Empty<string>();

        // Helper: unified error response for identity errors
        private BadRequestObjectResult IdentityErrorsResponse(IdentityResult result)
        {
            var errors = GetIdentityErrors(result);
            return BadRequest(new { errors });
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var applicationUser = new ApplicationUser
            {
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                City = registerDTO.City,
                Street = registerDTO.Street,
                State = registerDTO.State,
                ZipCode = registerDTO.ZipCode,
                UserName = registerDTO.UserName,
            };

            var result = await _userManager.CreateAsync(applicationUser, registerDTO.Password);
            if (!result.Succeeded)
            {
                _logger.LogWarning("Register failed for {UserName}: {Errors}", registerDTO.UserName, string.Join("; ", GetIdentityErrors(result)));
                return IdentityErrorsResponse(result);
            }

            // Add user to role (log any role-add failures)
            var roleResult = await _userManager.AddToRoleAsync(applicationUser, SD.CustomerRole);
            if (!roleResult.Succeeded)
            {
                _logger.LogWarning("Failed to add role for user {UserId}: {Errors}", applicationUser.Id, string.Join("; ", GetIdentityErrors(roleResult)));
                // not fatal for registration success, optionally return error instead
            }

            // Send confirmation email
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            // base64url encode token for safe URL transmission
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var callbackUrl = Url.Action(
                action: nameof(ConfirmEmail),
                controller: "Accounts",
                values: new { area = "Identity", token = encodedToken, userId = applicationUser.Id },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(applicationUser.Email!,
                subject: _localizer["Confirm Your Email!"],
                htmlMessage: $"<h1>{_localizer["Confirm Your Email By Clicking"]} <a href='{HtmlEncoder.Default.Encode(callbackUrl ?? string.Empty)}'>Here</a></h1>");

            return Ok(new
            {
                msg = _localizer["Add User"]
            });
        }

        /// <summary>
        /// Login with username/email and password.
        /// </summary>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(loginDTO.EmailOrUserName)
                       ?? await _userManager.FindByNameAsync(loginDTO.EmailOrUserName);

            if (user is null)
            {
                _logger.LogInformation("Login attempt failed - user not found: {EmailOrUserName}", loginDTO.EmailOrUserName);
                return Unauthorized(new NotificationDTO
                {
                    Msg = "Invalid username or password",
                    TraceID = Guid.NewGuid().ToString(),
                    CreatedAT = DateTime.UtcNow
                });
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, loginDTO.RememberMe, lockoutOnFailure: true);
            if (!signInResult.Succeeded)
            {
                if (signInResult.IsLockedOut)
                {
                    var lockoutEnd = user.LockoutEnd?.UtcDateTime;
                    _logger.LogWarning("User {UserId} is locked out until {LockoutEnd}", user.Id, lockoutEnd);
                    return BadRequest(new { msg = "Too many attempts", lockoutEnd });
                }

                return Unauthorized(new { msg = "Invalid username or password" });
            }

            if (!user.EmailConfirmed)
            {
                return BadRequest(new { msg = "Confirm your email first!" });
            }

            // If lockout end is set and in future -> blocked (just in case)
            if (user.LockoutEnd.HasValue && user.LockoutEnd.Value.UtcDateTime > DateTime.UtcNow)
            {
                return BadRequest(new { msg = $"You have a block till {user.LockoutEnd.Value.UtcDateTime}" });
            }

            return Ok(new { msg = "Login successfully" });
        }

        /// <summary>
        /// Confirm email endpoint. Token is expected to be Base64Url encoded.
        /// </summary>
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string userId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(userId))
                return BadRequest(new { msg = "Missing token or userId" });

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return NotFound();

            try
            {
                var decodedBytes = WebEncoders.Base64UrlDecode(token);
                var decodedToken = Encoding.UTF8.GetString(decodedBytes);

                var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Email confirmation failed for user {UserId}: {Errors}", userId, string.Join("; ", GetIdentityErrors(result)));
                    return BadRequest(new { msg = "Link expired or invalid, resend email confirmation" });
                }

                return Ok(new { msg = "Confirm Email successfully" });
            }
            catch (FormatException fe)
            {
                _logger.LogError(fe, "Invalid email confirmation token format for user {UserId}", userId);
                return BadRequest(new { msg = "Invalid token format" });
            }
        }

        /// <summary>
        /// Resend confirmation email.
        /// </summary>
        [HttpPost("ResendEmailConfirmation")]
        public async Task<IActionResult> ResendEmailConfirmation([FromBody] ResendEmailConfirmationDTO dto, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(dto.EmailOrUserName)
                       ?? await _userManager.FindByNameAsync(dto.EmailOrUserName);

            if (user is null)
            {
                return NotFound(new { msg = "Invalid username or email" });
            }

            if (user.EmailConfirmed)
            {
                return BadRequest(new { msg = "Already confirmed" });
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var callbackUrl = Url.Action(nameof(ConfirmEmail), "Accounts", new { area = "Identity", token = encodedToken, userId = user.Id }, Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email!, _localizer["Confirm Your Email!"], $"<h1>{_localizer["Confirm Your Email By Clicking"]} <a href='{HtmlEncoder.Default.Encode(callbackUrl ?? string.Empty)}'>Here</a></h1>");

            return Ok(new { msg = _localizer["Add User"] });
        }

        /// <summary>
        /// Start forget password flow -> send OTP to email.
        /// </summary>
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDTO dto, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(dto.EmailOrUserName)
                       ?? await _userManager.FindByNameAsync(dto.EmailOrUserName);

            if (user is null)
                return NotFound(new { msg = "Invalid username or email" });

            var OTPNumber = new Random().Next(1000, 9999);
            await _emailSender.SendEmailAsync(user.Email!, "Reset Password!", $"<h1>Reset Password Using {OTPNumber}. Don't share it!</h1>");

            await _userOTP.CreateAsync(new UserOTP
            {
                ApplicationUserId = user.Id,
                OTPNumber = OTPNumber.ToString(),
                ValidTo = DateTime.UtcNow.AddDays(1)
            });

            await _userOTP.CommitAsync();

            return Ok(new { msg = "Send OTP Number to Your Email successfully", userId = user.Id });
        }

        /// <summary>
        /// Validate OTP for reset password.
        /// </summary>
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(dto.ApplicationUserId);
            if (user is null)
                return NotFound(new { msg = "Invalid user" });

            var userOTPs = (await _userOTP.GetAsync(e => e.ApplicationUserId == dto.ApplicationUserId)).OrderBy(e => e.Id);
            var userOTP = userOTPs.LastOrDefault();
            if (userOTP is null)
                return NotFound(new { msg = "No OTP found" });

            if (userOTP.OTPNumber != dto.OTPNumber)
                return BadRequest(new { msg = "Invalid OTP" });

            if (DateTime.UtcNow > userOTP.ValidTo)
                return BadRequest(new { msg = "Expired OTP" });

            return Ok(new { msg = "Success OTP", userId = user.Id });
        }

        /// <summary>
        /// Set new password after OTP verification.
        /// </summary>
        [HttpPost("NewPassword")]
        public async Task<IActionResult> NewPassword([FromBody] NewPasswordDTO dto, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(dto.ApplicationUserId);
            if (user is null)
                return NotFound(new { msg = "Invalid user" });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetResult = await _userManager.ResetPasswordAsync(user, token, dto.Password);
            if (!resetResult.Succeeded)
            {
                _logger.LogWarning("Reset password failed for {UserId}: {Errors}", user.Id, string.Join("; ", GetIdentityErrors(resetResult)));
                return IdentityErrorsResponse(resetResult);
            }

            return Ok(new { msg = "Change Password Successfully!" });
        }

        /// <summary>
        /// Logout (clears sign-in).
        /// </summary>
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken = default)
        {
            await _signInManager.SignOutAsync();
            return Ok(new { msg = "Logged out" });
        }
    }
}
