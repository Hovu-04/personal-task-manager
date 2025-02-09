using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagerBackEnd.Enums;
using TaskStatus = TaskManagerBackEnd.Enums.TaskStatus;

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
        public TaskPriority Priority { get; set; }

        [Required] 
        [Column("due_date")] 
        public DateTime DueDate { get; set; }

        [Column("status")] 
        public TaskStatus Status { get; set; } = TaskStatus.Todo;

        [Column("created_at")] 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property: Task thuộc về một user
        [ForeignKey("UserId")] public User User { get; set; }

        // Navigation property: Task có thể thuộc một danh mục
        [ForeignKey("CategoryId")] public Category Category { get; set; }

        // Navigation property: Một task có thể có nhiều reminder
        public ICollection<Reminder> Reminders { get; set; }
    }
}