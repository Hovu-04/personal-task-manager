using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerBackend.Models
{
    /// <summary>
    /// Model cho bảng reminders
    /// </summary>
    [Table("reminders")]
    public class Reminder
    {
        [Key] 
        [Column("id")] 
        public int Id { get; set; }

        [Required] 
        [Column("task_id")] 
        public int TaskId { get; set; }

        [Required] 
        [Column("reminder_time")] 
         public DateTime ReminderTime { get; set; }

        [Column("is_sent")] 
        public bool IsSent { get; set; } = false;

        [Column("sent_at")] 
        public DateTime? SentAt { get; set; }

        // Navigation property: Reminder thuộc về một task
        [ForeignKey("TaskId")] 
        public TaskItem Task { get; set; }
    }
}