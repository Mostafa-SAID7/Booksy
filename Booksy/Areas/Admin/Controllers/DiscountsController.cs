using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booksy.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IRepository<Promotion> _promotionRepository;

        public DiscountsController(IRepository<Promotion> promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var promotions = await _promotionRepository.GetAsync();
            return Ok(promotions);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Promotion promotion)
        {
            await _promotionRepository.CreateAsync(promotion);
            await _promotionRepository.CommitAsync();
            return CreatedAtAction(nameof(Index), new { id = promotion.Id }, promotion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Promotion promotion)
        {
            var promo = await _promotionRepository.GetOneAsync(p => p.Id == id);
            if (promo == null) return NotFound();

            promo.Code = promotion.Code;
            promo.Discount = promotion.Discount;
            promo.IsActive = promotion.IsActive;
            _promotionRepository.Update(promo);
            await _promotionRepository.CommitAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var promo = await _promotionRepository.GetOneAsync(p => p.Id == id);
            if (promo == null) return NotFound();

            _promotionRepository.Delete(promo);
            await _promotionRepository.CommitAsync();
            return NoContent();
        }
    }

}
