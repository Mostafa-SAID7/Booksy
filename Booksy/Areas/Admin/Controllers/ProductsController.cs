using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Booksy.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Product>? products = await _productRepository.GetAsync(includes: [e => e.Category]);

            //Id, Name, Des, Price, Img, CategoryName, CategoryStatus
            var productResponse = products.Adapt<List<ProductResponse>>();

            return Ok(productResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productRepository.GetOneAsync(e => e.Id == id);

            if (product is null)
                return NotFound();

            return Ok(product.Adapt<ProductResponse>());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest productCreateRequest)
        {
            if (productCreateRequest.MainImg is not null && productCreateRequest.MainImg.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(productCreateRequest.MainImg.FileName);
                // 0924fdsfs-d429-fskdf-jsd230-423.png

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                // Save Img in wwwroot
                using (var stream = System.IO.File.Create(filePath))
                {
                    await productCreateRequest.MainImg.CopyToAsync(stream);
                }

                // Save img name in DB
                var product = productCreateRequest.Adapt<Product>();
                product.MainImg = fileName;

                // Save in DB
                var productReturned = await _productRepository.CreateAsync(product);
                await _productRepository.CommitAsync();

                //return Created();

                //return Created($"{Request.Scheme}://{Request.Host}/api/admin/products/{productReturned.Id}", new
                //{
                //    msg = "Created Product Successfully"
                //});

                return CreatedAtAction(nameof(Details), new { id = productReturned.Id }, new
                {
                    msg = "Created Product Successfully"
                });
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ProductUpdateRequest productUpdateRequest)
        {

            var productInDB = await _productRepository.GetOneAsync(e => e.Id == id, tracked: false);

            if (productInDB is null)
                return BadRequest();

            var product = productUpdateRequest.Adapt<Product>();
            product.Id = id;

            if (productUpdateRequest.MainImg is not null && productUpdateRequest.MainImg.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(productUpdateRequest.MainImg.FileName);
                // 0924fdsfs-d429-fskdf-jsd230-423.png

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                // Save Img in wwwroot
                using (var stream = System.IO.File.Create(filePath))
                {
                    await productUpdateRequest.MainImg.CopyToAsync(stream);
                }

                // Delete old img from wwwroot
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", productInDB.MainImg);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                // Update img name in DB
                product.MainImg = fileName;
            }
            else
            {
                product.MainImg = productInDB.MainImg;
            }

            // Update in DB
            _productRepository.Update(product);
            await _productRepository.CommitAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetOneAsync(e => e.Id == id);

            if (product is null)
                return NotFound();

            // Delete old img from wwwroot
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", product.MainImg);
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }

            // Remove in DB
            _productRepository.Delete(product);
            await _productRepository.CommitAsync();

            return NoContent();
        }
    }
}
