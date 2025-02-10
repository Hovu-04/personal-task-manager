using AutoMapper;
using TaskManagerBackEnd.DTOs.User;
using TaskManagerBackend.Helpers;
using TaskManagerBackend.Models;
using TaskManagerBackend.Repositories;
using TaskManagerBackend.Services.Interface;

namespace TaskManagerBackend.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public UserService(IRepository<User> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserResponseDto>>(users);
    }

    public async Task<UserResponseDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task CreateUserAsync(UserCreateDto userCreateDto)
    {
        var user = _mapper.Map<User>(userCreateDto);
        user.PasswordHash = BCryptHelper.HashPassword(userCreateDto.Password);
        await _userRepository.AddAsync(user);
    }

    public async Task UpdateUserAsync(int id, UserUpdateDto userUpdateDto)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null)
            throw new KeyNotFoundException("User not found");

        // Ánh xạ dữ liệu từ DTO vào entity
        _mapper.Map(userUpdateDto, existingUser);
        
        existingUser.CreatedAt = DateTime.SpecifyKind(existingUser.CreatedAt, DateTimeKind.Utc);
    
        // Nếu LastLogin có giá trị, chuyển nó sang Utc
        if (existingUser.LastLogin.HasValue)
        {
            existingUser.LastLogin = DateTime.SpecifyKind(existingUser.LastLogin.Value, DateTimeKind.Utc);
        }

        await _userRepository.UpdateAsync(existingUser);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteAsync(id);
    }
}