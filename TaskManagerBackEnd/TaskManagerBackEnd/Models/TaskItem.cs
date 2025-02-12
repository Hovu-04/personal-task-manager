using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerBackend.Models
{
    /// <summary>
    /// Model cho bảng tasks.
    /// Đặt tên là TaskItem để tránh nhầm lẫn với System.Threading.Tasks.Task
    /// </summary>
    [Table("tasks")]
    public class TaskItem
    {
        [Key] [Column("id")] public int Id { get; set; }

        [Required] [Column("user_id")] public int UserId { get; set; }

        [Column("category_id")] public int? CategoryId { get; set; }

        [Required]
        [Column("title")]
        [MaxLength(255)]
        public string Title { get; set; }

        [Column("description")] 
        public string Description { get; set; }

        [Required]
        [Column("priority")]
        [MaxLength(10)]
        public string Priority { get; set; } 
        
        [Column("due_date",TypeName = "timestamp with time zone")]
        public DateTime DueDate { get; set; }

        [Column("created_at", TypeName = "timestamp with time zone")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [Required]
        [Column("status")]
        [MaxLength(20)]
        public string Status { get; set; } = "todo"; 
        
        // Navigation property: Task thuộc về một user
        [ForeignKey("UserId")] public User User { get; set; }

        // Navigation property: Task có thể thuộc một danh mục
        [ForeignKey("CategoryId")] public Category Category { get; set; }

        // Navigation property: Một task có thể có nhiều reminder
        public ICollection<Reminder> Reminders { get; set; }
    }
}