using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Dtos;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductsController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _service.GetAllAsync();
            if (products == null || !products.Any())
            {
                return NoContent();
            }
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto, Guid userId)
        {
            try
            {
                var product = await _service.Create(productDto, userId);
                return CreatedAtAction(nameof(Create), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create product for user {userId}: {ex}");
                return StatusCode(500, "An error occurred while creating the product.");
            }
        }
    }
}