using TaskManagerBackEnd.DTOs.User;
using TaskManagerBackEnd.Helpers;

namespace TaskManagerBackend.Services.Interface
{
    public interface IUserService
    {
        Task<ApiResponse<IEnumerable<UserResponseDto>>> GetAllUsersAsync();
        Task<ApiResponse<UserResponseDto>> GetUserByIdAsync(int id);
        Task<ApiResponse<UserResponseDto>> CreateUserAsync(UserCreateDto userCreateDto);
        Task<ApiResponse<UserResponseDto>> UpdateUserAsync(int id, UserUpdateDto userUpdateDto);
        Task<ApiResponse<string>> DeleteUserAsync(int id);
    }
}