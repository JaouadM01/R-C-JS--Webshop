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
        public async Task<ActionResult<ReceiptDto>> CreateReceipt(Guid UserId, [FromBody] List<ReceiptProductRequest> products)
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
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ReceiptDto>>> GetAllAsync()
        {
            try
            {
                var receipts = await _service.GetAllAsync();
                if (receipts == null) return BadRequest();
                return Ok(receipts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Backend Server Error: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiptDto>>> GetByIdAsync(Guid id)
        {
            try
            {
                var receipts = await _service.GetByIdAsync(id);
                if (receipts == null) return BadRequest();
                return Ok(receipts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Backend Server Error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _service.Delete(id);
                return Ok();
            }
            catch(InvalidOperationException ex){
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Backend Server Error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(Guid id, [FromBody] ReceiptDto receiptDto)
        {
            try {
                var receipt = await _service.Update(id, receiptDto);
                if(receipt == null) return BadRequest();
                return Ok(receipt);
            }
            catch(Exception ex){
                return StatusCode(500, $"Backend Server Error: {ex.Message}");
            }
        }
    }
}