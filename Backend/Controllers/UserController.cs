using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [Authorize(Roles = "BackendEmployee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _service.GetAllAsync();
            if (users == null || !users.Any())
            {
                return NoContent();
            }
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var createdUser = await _service.Create(userDto);
                if (createdUser == null) return StatusCode(500, "An error occurred while creating the user.");

                return CreatedAtAction(nameof(GetAll), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Backend Server Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UserDto userDto)
        {
            try
            {
                var updatedUser = await _service.Update(id, userDto);
                if (updatedUser == null) return NotFound();

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Backend Server Error: {ex.Message}");

            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _service.Login(loginDto.Email, loginDto.Password);
                if (token == null) return Unauthorized("Invalid Credentials");
                return Ok(new { Token = token });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Backend Server Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _service.Delete(id);
                if (result == false) return BadRequest("Failed to delete account");
                return Ok("Account has been succesfully deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Backend Server Error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            try
            {
                var existingUser = await _service.GetById(id);
                if (existingUser == null) return NotFound();
                return Ok(existingUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Backend Server Error: {ex.Message}");
            }
        }

        [HttpPut("Favourite")]
        public async Task<ActionResult> Favourite(Guid productId, Guid userId)
        {
            try
            {
                var result = await _service.Favourite(productId, userId);
                if (result == false)
                {
                    // If the product is already in the favourites list, remove it
                    var unfavourited = await _service.UnFavourite(productId, userId);
                    if (unfavourited)
                    {
                        return Ok("Product removed from favourites.");
                    }
                    return BadRequest("Failed to remove product from favourites.");
                }
                return Ok("Product added to favourites.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Backend Server Error: {ex.Message}");
            }
        }

        [HttpGet("Profile")]
        [Authorize] // Ensures that the user is authenticated
        public async Task<ActionResult<UserDto>> GetProfile()
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                {
                    return Unauthorized("User not authenticated");
                }

                if (!Guid.TryParse(userId, out var userGuid))
                {
                    return BadRequest("Invalid user ID format");
                }

                var userProfile = await _service.GetUserProfileAsync(userGuid);
                if (userProfile == null)
                {
                    return NotFound("User profile not found");
                }

                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}