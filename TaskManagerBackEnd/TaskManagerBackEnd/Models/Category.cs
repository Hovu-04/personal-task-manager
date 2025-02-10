using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerBackend.Models
{
    /// <summary>
    /// Model cho bảng categories (mỗi user có danh mục riêng)
    /// </summary>
    [Table("categories")]
    public class Category
    {
        [Key] 
        [Column("id")] 
        public int Id { get; set; }

        [Required]
        [Column("user_id")] 
        public int UserId { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(100)]
        public string Name { get; set; }

        // Navigation property: Danh mục thuộc về một user
        [ForeignKey("UserId")] public User User { get; set; }

        // Navigation property: Danh mục có thể chứa nhiều task
        public ICollection<TaskItem> Tasks { get; set; }
    }
}