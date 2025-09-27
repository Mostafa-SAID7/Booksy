using Booksy.Models.Entities.Promotions;
using Booksy.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Booksy.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly IRepository<Promotion> _promotionRepository;

        public PromotionsController(IRepository<Promotion> promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        // GET: api/admin/promotions
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var promotions = await _promotionRepository.GetAsync(
               includes: new Expression<Func<Promotion, object>>[]
{
    p => p.Coupons,
    p => p.Discounts,
    p => p.Books
});

            return Ok(promotions);
        }

        // GET: api/admin/promotions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var promo = await _promotionRepository.GetOneAsync(
                p => p.Id == id,
              includes: new Expression<Func<Promotion, object>>[]
{
    p => p.Coupons,
    p => p.Discounts,
    p => p.Books
});

            if (promo == null) return NotFound();

            return Ok(promo);
        }

        // POST: api/admin/promotions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Promotion promotion)
        {
            await _promotionRepository.CreateAsync(promotion);
            await _promotionRepository.CommitAsync();
            return CreatedAtAction(nameof(Details), new { id = promotion.Id }, promotion);
        }

        // PUT: api/admin/promotions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Promotion promotion)
        {
            var promo = await _promotionRepository.GetOneAsync(p => p.Id == id);
            if (promo == null) return NotFound();

            promo.Code = promotion.Code;
            promo.Type = promotion.Type;
            promo.Value = promotion.Value;
            promo.StartDate = promotion.StartDate;
            promo.EndDate = promotion.EndDate;

            _promotionRepository.Update(promo);
            await _promotionRepository.CommitAsync();

            return NoContent();
        }

        // DELETE: api/admin/promotions/5
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
