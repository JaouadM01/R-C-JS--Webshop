using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Dtos;

namespace Backend.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase {
        private readonly UserService _service;

        public UserController(UserService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(){
            var users = await _service.GetAllAsync();
            if(users == null || !users.Any()){
                return NoContent();
            }
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserDto userDto){
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try {
                var createdUser = await _service.Create(userDto);
                if (createdUser == null)  return StatusCode(500, "An error occurred while creating the user."); 

                return CreatedAtAction(nameof(GetAll), new { id = createdUser.Id }, createdUser);
            }
            catch(Exception ex){
                return StatusCode(500, $"Backend Server Error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UserDto userDto)
        {
            try{
                var updatedUser = await _service.Update(id, userDto);
                if(updatedUser == null) return NotFound();

                return Ok(updatedUser);
            }
            catch(Exception ex){
                return StatusCode(500, $"Backend Server Error: {ex.Message}");

            }
        }
    }
}