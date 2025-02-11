using AutoMapper;
using TaskManagerBackend.DTOs.Categories;
using TaskManagerBackEnd.Helpers;
using TaskManagerBackend.Models;
using TaskManagerBackend.Repositories;
using TaskManagerBackend.Services.Interface;

namespace TaskManagerBackend.Services;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Category> _categoryRepository;

    public CategoryService(IMapper mapper, IRepository<Category> categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }


    public async Task<ApiResponse<IEnumerable<CategoryResponseDto>>> GetAllCategoryAsync()
    {
        var category = await _categoryRepository.GetAllAsync();
        if (category == null || !category.Any())
        {
            return ApiResponse<IEnumerable<CategoryResponseDto>>.ErrorResponse("No categories found", 404);
        }

        var result = _mapper.Map<IEnumerable<CategoryResponseDto>>(category);
        return ApiResponse<IEnumerable<CategoryResponseDto>>.SuccessResponse(result,
            "Successfully retrieved categories");
    }

    public async Task<ApiResponse<CategoryResponseDto>> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return ApiResponse<CategoryResponseDto>.ErrorResponse($"No category found with ID: {id}", 404);
        }

        var result = _mapper.Map<CategoryResponseDto>(category);
        return ApiResponse<CategoryResponseDto>.SuccessResponse(result, "Get category information successfully.");
    }

    public async Task<ApiResponse<CategoryResponseDto>> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
    {
        var category = _mapper.Map<Category>(categoryCreateDto);
        await _categoryRepository.AddAsync(category);
        var result = _mapper.Map<CategoryResponseDto>(category);
        return ApiResponse<CategoryResponseDto>.SuccessResponse(result, "Category created successfully.");
    }

    public async Task<ApiResponse<CategoryResponseDto>> UpdateCategoryAsync(int id, CategoriesUpdateDto categoriesUpdateDto)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(id);
        if (existingCategory == null)
        {
            return ApiResponse<CategoryResponseDto>.ErrorResponse("Category does not exist.", 404);
        }

        _mapper.Map(categoriesUpdateDto, existingCategory);
        await _categoryRepository.UpdateAsync(existingCategory);
        var result = _mapper.Map<CategoryResponseDto>(existingCategory);
        return ApiResponse<CategoryResponseDto>.SuccessResponse(result, "Category updated successfully.");
    }

    public async Task<ApiResponse<string>> DeleteCategoryByIdAsync(int id)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(id);
        if (existingCategory == null)
        {
            return ApiResponse<string>.ErrorResponse("Category does not exist.", 404);
        }

        await _categoryRepository.DeleteAsync(id);
        return ApiResponse<string>.SuccessResponse("Category was successfully deleted.","Category deleted successfully.");
    }
}