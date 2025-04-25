using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptController : ControllerBase
    {
        private readonly ReceiptService _service;

        public ReceiptController(ReceiptService service)
        {
            _service = service;
        }

        [HttpPost("CreateReceipt")]
        public async Task<ActionResult<ReceiptDto>> CreateReceipt(Guid UserId ,[FromBody] List<ReceiptProductRequest> products)
        {
            try
            {
                var receipt = await _service.CreateReceipt(UserId, products);
                if (receipt == null)
                {
                    return BadRequest("Error creating receipt");
                }

                return Ok(receipt);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Backend Server Error: {ex.Message}");
            }
        }
    }
}