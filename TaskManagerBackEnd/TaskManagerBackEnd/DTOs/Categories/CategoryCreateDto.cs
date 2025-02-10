using System.ComponentModel.DataAnnotations;

namespace TaskManagerBackend.DTOs.Categories;

public class CategoryCreateDto
{
    [Required] [MaxLength(100)] public string Name { get; set; }
    [Required] public int UserId { get; set; }
}