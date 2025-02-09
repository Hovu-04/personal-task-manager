using Microsoft.AspNetCore.Mvc;
using TaskManagerBackEnd.DTOs.User;
using TaskManagerBackend.Services.Interface;

namespace TaskManagerBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserCreateDto userCreateDto)
    {
        await _userService.CreateUserAsync(userCreateDto);
        return CreatedAtAction(nameof(GetUser), new { id = userCreateDto.Email }, userCreateDto);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userDto)
    {
        await _userService.UpdateUserAsync(id, userDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}