using AutoMapper;
using TaskManagerBackend.DTOs.Categories;
using TaskManagerBackEnd.DTOs.User;
using TaskManagerBackend.Models;

namespace TaskManagerBackend.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 🟢 Map từ User -> UserResponseDto
            CreateMap<User, UserResponseDto>();

            // 🟡 Map từ UserCreateDto -> User
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            
            // 🔵 Map từ UserUpdateDto -> User
            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // 🟠 Map từ Category -> CategoryResponseDto
            CreateMap<Category, CategoryResponseDto>();
            
            // 🟣 Map từ CategoryCreateDto -> Category
            CreateMap<CategoryCreateDto, Category>();
            
            // 🔴 Map từ CategoryUpdateDto -> Category
            CreateMap<CategoriesUpdateDto, Category>();
        }
    }
}