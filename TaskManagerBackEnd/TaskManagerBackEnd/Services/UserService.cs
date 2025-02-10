using AutoMapper;
using TaskManagerBackEnd.DTOs.User;
using TaskManagerBackend.Helpers;
using TaskManagerBackEnd.Helpers;
using TaskManagerBackend.Models;
using TaskManagerBackend.Repositories;
using TaskManagerBackend.Services.Interface;

namespace TaskManagerBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<UserResponseDto>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            if (users == null || !users.Any())
            {
                return ApiResponse<IEnumerable<UserResponseDto>>.ErrorResponse("No users found.", 404);
            }

            var result = _mapper.Map<IEnumerable<UserResponseDto>>(users);
            return ApiResponse<IEnumerable<UserResponseDto>>.SuccessResponse(result, "Get list of users successfully.");
        }

        public async Task<ApiResponse<UserResponseDto>> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponse<UserResponseDto>.ErrorResponse($"No user found with ID: {id}.", 404);
            }

            var result = _mapper.Map<UserResponseDto>(user);
            return ApiResponse<UserResponseDto>.SuccessResponse(result, "Get user information successfully.");
        }

        public async Task<ApiResponse<UserResponseDto>> CreateUserAsync(UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<User>(userCreateDto);
            // Mã hóa mật khẩu
            user.PasswordHash = BCryptHelper.HashPassword(userCreateDto.Password);
            await _userRepository.AddAsync(user);
            var userResponse = _mapper.Map<UserResponseDto>(user);
            return ApiResponse<UserResponseDto>.SuccessResponse(userResponse, "Created user successfully.");
        }

        public async Task<ApiResponse<UserResponseDto>> UpdateUserAsync(int id, UserUpdateDto userUpdateDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                return ApiResponse<UserResponseDto>.ErrorResponse("User does not exist.", 404);
            }

            // Ánh xạ dữ liệu từ DTO vào entity hiện có
            _mapper.Map(userUpdateDto, existingUser);

            // Đảm bảo CreatedAt và LastLogin có Kind là Utc (nếu cần)
            existingUser.CreatedAt = DateTime.SpecifyKind(existingUser.CreatedAt, DateTimeKind.Utc);
            if (existingUser.LastLogin.HasValue)
            {
                existingUser.LastLogin = DateTime.SpecifyKind(existingUser.LastLogin.Value, DateTimeKind.Utc);
            }

            await _userRepository.UpdateAsync(existingUser);
            var updatedUser = _mapper.Map<UserResponseDto>(existingUser);
            return ApiResponse<UserResponseDto>.SuccessResponse(updatedUser, "User updated successfully.");
        }

        public async Task<ApiResponse<string>> DeleteUserAsync(int id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                return ApiResponse<string>.ErrorResponse($"No user found with ID: {id}.", 404);
            }

            await _userRepository.DeleteAsync(id);
            return ApiResponse<string>.SuccessResponse("User was successfully deleted.", "User deleted successfully.");
        }
    }
}
