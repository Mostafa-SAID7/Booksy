using Booksy.Models.DTOs.Request.User;
using Booksy.Models.DTOs.Response.Auth;
using Booksy.Models.Entities.Users;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Booksy.Areas.Identity.Controllers
{
    [Area(SD.IdentityArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize] // 🔐 Require authentication
    public class ProfilesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfilesController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Get the currently authenticated user's profile
        /// </summary>
        [HttpGet("")]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return Unauthorized(new { msg = "User not found or not logged in." });

            // Map ApplicationUser -> UserProfileResponse
            var profile = user.Adapt<UserProfileResponse>();

            return Ok(profile);
        }

        /// <summary>
        /// Update the current user's personal information
        /// </summary>
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdatePersonalInfoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return Unauthorized(new { msg = "User not found or not logged in." });

            // Map request -> user (only allowed fields)
            request.Adapt(user);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { msg = "Profile updated successfully." });
        }

        /// <summary>
        /// Delete current user account (optional feature)
        /// </summary>
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return Unauthorized(new { msg = "User not found or not logged in." });

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { msg = "Account deleted successfully." });
        }
    }
}
