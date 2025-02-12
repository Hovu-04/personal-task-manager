using TaskManagerBackEnd.DTOs.Reminder;
using TaskManagerBackEnd.Helpers;

namespace TaskManagerBackend.Services;

public interface IReminderService
{
    Task<ApiResponse<IEnumerable<ReminderResponseDto>>> GetAllReminders();
    Task<ApiResponse<IEnumerable<ReminderResponseDto>>> GetAllRemindersByTaskId(int taskId);
    Task<ApiResponse<ReminderResponseDto>> GetReminderById(int reminderId);
    Task<ApiResponse<ReminderResponseDto>> CreateReminder(ReminderCreateDto reminderCreateDto);
    Task<ApiResponse<ReminderResponseDto>> UpdateReminder(int id, ReminderUpdateDto reminderUpdateDto);
    Task<ApiResponse<string>> DeleteReminder(int reminderId);
}