using System.ComponentModel.DataAnnotations;

namespace TaskManagerBackEnd.DTOs.Reminder;

public class ReminderCreateDto
{
    [Required] public int TaskId { get; set; }

    [Required] public DateTime ReminderTime { get; set; }
}