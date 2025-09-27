using Booksy.Models.Entities.Users;
using Booksy.Repositories.IRepositories;
using Booksy.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Booksy.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IRepository<Setting> _settingRepository;

        public SettingsController(IRepository<Setting> settingRepository)
        {
            _settingRepository = settingRepository;
        }

        // GET: api/admin/settings
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var settings = await _settingRepository.GetAsync();
            return Ok(settings);
        }

        // GET: api/admin/settings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var setting = await _settingRepository.GetOneAsync(s => s.Id == id);
            if (setting == null) return NotFound();
            return Ok(setting);
        }

        // POST: api/admin/settings
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Setting setting)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _settingRepository.CreateAsync(setting);
            await _settingRepository.CommitAsync();

            return CreatedAtAction(nameof(Get), new { id = setting.Id }, setting);
        }

        // PUT: api/admin/settings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Setting setting)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var settingInDb = await _settingRepository.GetOneAsync(s => s.Id == id);
            if (settingInDb == null) return NotFound();

            settingInDb.Key = setting.Key;
            settingInDb.Value = setting.Value;

            _settingRepository.Update(settingInDb);
            await _settingRepository.CommitAsync();

            return NoContent();
        }

        // DELETE: api/admin/settings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var setting = await _settingRepository.GetOneAsync(s => s.Id == id);
            if (setting == null) return NotFound();

            _settingRepository.Delete(setting);
            await _settingRepository.CommitAsync();

            return NoContent();
        }
    }
}
