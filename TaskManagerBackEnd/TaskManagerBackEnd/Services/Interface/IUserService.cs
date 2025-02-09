using TaskManagerBackEnd.DTOs.User;
using TaskManagerBackend.Models;

namespace TaskManagerBackend.Services.Interface;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
    Task<UserResponseDto?> GetUserByIdAsync(int id);
    Task CreateUserAsync(UserCreateDto userCreateDto);
    Task UpdateUserAsync(int id, UserUpdateDto userUpdateDto);
    Task DeleteUserAsync(int id);
}