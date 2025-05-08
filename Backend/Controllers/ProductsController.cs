using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Dtos;
using Microsoft.AspNetCore.Authorization;

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
        //[Authorize(Roles = "BackendEmployee")]
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
        [HttpGet("listed")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllListed()
        {
            var products = await _service.GetAllListedAsync();
            if (products == null || !products.Any())
            {
                return NoContent();
            }
            return Ok(products);
        }
        //[Authorize]
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
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Update(Guid id, [FromBody] ProductDto product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var updatedProduct = await _service.Update(id, product);
                if (updatedProduct == null)
                {
                    return NotFound();
                }
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update product: {ex}");
                return StatusCode(500, "An error occured while updating the product");
            }
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid productId)
        {
            try
            {
                await _service.Delete(productId);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete product: {ex}");
                return StatusCode(500, "An error occurred while deleting the product.");
            }
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            try {
                var product = await _service.GetById(id);
                if(product == null){
                    return NotFound($"Product with ID: {id} not found");
                }
                else return Ok(product);
            }
            catch(Exception ex){
                Console.WriteLine($"Failed to retrieve the product: {ex}");
                return StatusCode(500, "An error occurred while retrieving the product");
            }
        }
        //[Authorize]
        [HttpGet("GetProductList")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductListById(Guid id)
        {
            try {
                var products = await _service.GetProductListById(id);
                if(products == null) return NotFound();
                return Ok(products);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occured while retrieving the product list: {ex.Message}");
            }
        }

        [HttpPut("updateOwner")]
        public async Task<ActionResult> UpdateOwner(Guid id, Guid productId)
        {
            try {
                var updatedProduct = await _service.UpdateOwner(id, productId);
                if(updatedProduct != null)return Ok();
                return BadRequest();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occured while retrieving the product list: {ex.Message}");
            }
        }
    }
}