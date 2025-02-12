using AutoMapper;
using TaskManagerBackend.DTOs.Categories;
using TaskManagerBackEnd.DTOs.Reminder;
using TaskManagerBackend.DTOs.Tasks;
using TaskManagerBackEnd.DTOs.User;
using TaskManagerBackend.Models;

namespace TaskManagerBackend.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map từ User -> UserResponseDto
            CreateMap<User, UserResponseDto>();

            // Map từ UserCreateDto -> User
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Map từ UserUpdateDto -> User
            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Map từ Category -> CategoryResponseDto
            CreateMap<Category, CategoryResponseDto>();

            // Map từ CategoryCreateDto -> Category
            CreateMap<CategoryCreateDto, Category>();

            // Map từ CategoryUpdateDto -> Category
            CreateMap<CategoriesUpdateDto, Category>();

            // Map từ Reminder -> ReminderResponseDto
            CreateMap<Reminder, ReminderResponseDto>();

            // Map từ ReminderCreateDto -> Reminder
            CreateMap<ReminderCreateDto, Reminder>();

            // Map từ ReminderUpdateDto -> Reminder
            CreateMap<ReminderUpdateDto, Reminder>();

            // Map từ TaskItem -> TaskResponseDto
            CreateMap<TaskItem, TaskResponseDto>();
            
            // Map từ TaskCreateDto -> TaskItem
            CreateMap<TaskCreateDto, TaskItem>();

            // Map từ TaskUpdateDto -> TaskItem
            CreateMap<TaskUpdateDto, TaskItem>();
        }
    }
}