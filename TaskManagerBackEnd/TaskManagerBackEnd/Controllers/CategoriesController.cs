using Microsoft.AspNetCore.Mvc;
using TaskManagerBackend.DTOs.Categories;
using TaskManagerBackend.Services.Interface;

namespace TaskManagerBackend.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET: api/categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetCategory()
    {
        var response = await _categoryService.GetAllCategoryAsync();
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }

    // GET: api/categories/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryResponseDto>> GetCategoryById(int id)
    {
        var response = await _categoryService.GetCategoryByIdAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }

    // POST: api/categories
    [HttpPost]
    public async Task<ActionResult<CategoryResponseDto>> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto)
    {
        var response = await _categoryService.CreateCategoryAsync(categoryCreateDto);
        if (response.Success)
        {
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryCreateDto.UserId }, categoryCreateDto);
        }

        return BadRequest(response);
    }

    // PUT: api/categories/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoryResponseDto>> UpdateCategory(int id, [FromBody] CategoriesUpdateDto categoriesUpdateDto)
    {
        var response = await _categoryService.UpdateCategoryAsync(id, categoriesUpdateDto);
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }

    // Delete: api/categories/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<string>> DeleteCategory(int id)
    {
        var response = await _categoryService.DeleteCategoryByIdAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
}