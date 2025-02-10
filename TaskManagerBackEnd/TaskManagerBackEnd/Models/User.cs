using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerBackend.Models
{
    /// <summary>
    /// Model cho bảng users
    /// </summary>
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
    
        [Required]
        [Column("full_name")]
        [MaxLength(100)]
        public string FullName { get; set; }
    
        [Required]
        [Column("email")]
        [MaxLength(255)]
        public string Email { get; set; }
    
        [Required]
        [Column("password_hash")]
        public string PasswordHash { get; set; }
    
        [Column("role")]
        [MaxLength(50)]
        public string Role { get; set; } = "user";
    
        [Column("last_login")]
        public DateTime? LastLogin { get; set; } = DateTime.UtcNow;
    
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
        // Navigation properties
        public ICollection<Category> Categories { get; set; }
        public ICollection<TaskItem> Tasks { get; set; }
    }
}
