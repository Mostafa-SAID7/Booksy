using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booksy.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetSettings()
        {
            var settings = _context.AppSettings.ToList(); // Example DbSet<AppSetting>
            return Ok(settings);
        }

        [HttpPut("{key}")]
        public IActionResult UpdateSetting(string key, [FromBody] string value)
        {
            var setting = _context.AppSettings.FirstOrDefault(s => s.Key == key);
            if (setting == null) return NotFound();

            setting.Value = value;
            _context.SaveChanges();
            return NoContent();
        }
    }

}
