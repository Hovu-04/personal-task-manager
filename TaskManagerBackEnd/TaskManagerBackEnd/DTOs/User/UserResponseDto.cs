namespace TaskManagerBackEnd.DTOs.User;

public class UserResponseDto
{
    public int Id { get; set; }
        
    public string FullName { get; set; }
        
    public string Email { get; set; }
        
    public string Role { get; set; }
        
    public DateTime? LastLogin { get; set; }
        
    public DateTime CreatedAt { get; set; }
}