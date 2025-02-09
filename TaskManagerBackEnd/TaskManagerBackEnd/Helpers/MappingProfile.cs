using AutoMapper;
using TaskManagerBackEnd.DTOs.User;
using TaskManagerBackend.Models;

namespace TaskManagerBackend.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map từ User -> UserResponseDto (trả về client)
            CreateMap<User, UserResponseDto>();

            // Map từ UserCreateDto -> User (dùng khi tạo user)
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            
            // Map từ UserUpdateDto -> User (dùng khi cập nhật)
            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }
    }
}