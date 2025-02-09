using System.ComponentModel.DataAnnotations;

namespace TaskManagerBackEnd.DTOs.User;

public class UserUpdateDto
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }
        
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; }
        
    public string? Password { get; set; }
        
    public string? Role { get; set; }
}