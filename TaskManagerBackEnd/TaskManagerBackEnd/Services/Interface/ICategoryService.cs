using TaskManagerBackend.DTOs.Categories;
using TaskManagerBackEnd.Helpers;

namespace TaskManagerBackend.Services.Interface;

public interface ICategoryService
{
    Task<ApiResponse<IEnumerable<CategoryResponseDto>>> GetAllCategoryAsync();
    Task<ApiResponse<CategoryResponseDto>> GetCategoryByIdAsync(int id);
    Task<ApiResponse<CategoryResponseDto>> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
    Task<ApiResponse<CategoryResponseDto>> UpdateCategoryAsync(int id, CategoriesUpdateDto categoriesUpdateDto);
    Task<ApiResponse<string>> DeleteCategoryByIdAsync(int id);
}