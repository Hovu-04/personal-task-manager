using System.ComponentModel.DataAnnotations;

namespace TaskManagerBackend.DTOs.Categories;

public class CategoriesUpdateDto
{
    [Required] [MaxLength(100)] public string Name { get; set; }
}