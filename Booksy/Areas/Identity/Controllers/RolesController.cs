using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Booksy.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index() => Ok(_roleManager.Roles.ToList());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            return Ok(new { msg = "Role created" });
        }

        [HttpDelete("{roleName}")]
        public async Task<IActionResult> Delete(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return NotFound();

            await _roleManager.DeleteAsync(role);
            return NoContent();
        }
    }

}
