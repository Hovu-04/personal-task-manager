using System.ComponentModel.DataAnnotations;

namespace TaskManagerBackEnd.DTOs.Reminder;

public class ReminderUpdateDto
{
    [Required] public DateTime ReminderTime { get; set; }

    // Cho phép cập nhật trạng thái gửi của reminder.
    public bool IsSent { get; set; } = false;

    // Thời gian gửi, có thể để null nếu chưa gửi.
    public DateTime? SentAt { get; set; }
}