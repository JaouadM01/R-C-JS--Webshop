using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Dtos;
using Microsoft.AspNetCore.Authorization;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseHistoryController : ControllerBase
    {
        private readonly PurchaseHistoryService _service;

        public PurchaseHistoryController(PurchaseHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try {
                var history = await _service.GetAllAsync();
                if (history == null) return NotFound();
                else {
                    return Ok(history);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Backend Server Error: {ex.Message}");
            }
        }

        [HttpGet("getbasedonuserid")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try {
                var history = await _service.GetByIdAsync(id);
                if (history == null) return null;
                else {
                    return Ok(history);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Backend Server Error: {ex.Message}");
            }
        }
    }
}