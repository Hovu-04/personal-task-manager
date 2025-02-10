using System.ComponentModel.DataAnnotations;

namespace TaskManagerBackEnd.DTOs.User;

public class UserCreateDto
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }
        
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; }
        
    [Required]
    [MaxLength(100)]
    public string Password { get; set; }
}