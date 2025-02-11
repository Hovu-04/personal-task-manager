namespace TaskManagerBackEnd.DTOs.Reminder;

public class ReminderResponseDto
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public DateTime ReminderTime { get; set; }
    public bool IsSent { get; set; }
    public DateTime? SentAt { get; set; }
}