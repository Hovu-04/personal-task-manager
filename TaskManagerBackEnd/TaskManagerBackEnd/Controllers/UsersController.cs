using Microsoft.AspNetCore.Mvc;
using TaskManagerBackEnd.DTOs.User;
using TaskManagerBackend.Services.Interface;

namespace TaskManagerBackend.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var response = await _userService.GetAllUsersAsync();
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        // GET: api/users/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserResponseDto>> GetUser(int id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            var response = await _userService.CreateUserAsync(userCreateDto);
            if (response.Success)
            {
                return CreatedAtAction(nameof(GetUser), new { id = response.Data!.Id }, response);
            }

            return BadRequest(response);
        }


        // PUT: api/users/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserResponseDto>> UpdateUser(int id, [FromBody] UserUpdateDto userDto)
        {
            var response = await _userService.UpdateUserAsync(id, userDto);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            var response = await _userService.DeleteUserAsync(id);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }
    }
}