using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagerBackend.Data;
using TaskManagerBackEnd.DTOs.Reminder;
using TaskManagerBackEnd.Helpers;
using TaskManagerBackend.Models;
using TaskManagerBackend.Repositories;

namespace TaskManagerBackend.Services;

public class ReminderService : IReminderService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Reminder> _reminderRepository;
    private readonly AppDbContext _context;

    public ReminderService(IMapper mapper, IRepository<Reminder> reminderRepository, AppDbContext context)
    {
        _mapper = mapper;
        _reminderRepository = reminderRepository;
        _context = context;
    }

    public async Task<ApiResponse<IEnumerable<ReminderResponseDto>>> GetAllReminders()
    {
        var reminders = await _reminderRepository.GetAllAsync();
        if (reminders == null || !reminders.Any())
        {
            var emptyReminder = Enumerable.Empty<ReminderResponseDto>();
            return ApiResponse<IEnumerable<ReminderResponseDto>>.SuccessResponse(emptyReminder, "No reminders found");
        }

        var reminderDtos = _mapper.Map<IEnumerable<ReminderResponseDto>>(reminders);
        return ApiResponse<IEnumerable<ReminderResponseDto>>.SuccessResponse(reminderDtos,
            "Reminders retrieved successfully.");
    }

    public async Task<ApiResponse<IEnumerable<ReminderResponseDto>>> GetAllRemindersByTaskId(int taskId)
    {
        // Truy vấn danh sách reminder theo TaskId từ DbContext
        var reminders = await _context.Reminders
            .Where(r => r.TaskId == taskId)
            .ToListAsync();

        // Nếu không tìm thấy reminder nào, trả về lỗi với thông báo
        if (reminders == null || !reminders.Any())
        {
            return ApiResponse<IEnumerable<ReminderResponseDto>>.ErrorResponse("No reminders found for the given task.",
                404);
        }

        // Ánh xạ danh sách Reminder sang ReminderResponseDto
        var reminderDtos = _mapper.Map<IEnumerable<ReminderResponseDto>>(reminders);

        // Trả về response thành công với dữ liệu và thông báo
        return ApiResponse<IEnumerable<ReminderResponseDto>>.SuccessResponse(reminderDtos,
            "Reminders retrieved successfully.");
    }

    public async Task<ApiResponse<ReminderResponseDto>> GetReminderById(int reminderId)
    {
        var reminder = await _reminderRepository.GetByIdAsync(reminderId);
        if (reminder == null)
        {
            return ApiResponse<ReminderResponseDto>.ErrorResponse("Reminder not found.", 404);
        }

        return ApiResponse<ReminderResponseDto>.SuccessResponse(_mapper.Map<ReminderResponseDto>(reminder));
    }

    public async Task<ApiResponse<ReminderResponseDto>> CreateReminder(ReminderCreateDto reminderCreateDto)
    {
        var reminder = _mapper.Map<Reminder>(reminderCreateDto);
        if (reminder == null)
        {
            return ApiResponse<ReminderResponseDto>.ErrorResponse("Reminder not found.", 404);
        }

        await _reminderRepository.AddAsync(reminder);
        return ApiResponse<ReminderResponseDto>.SuccessResponse(_mapper.Map<ReminderResponseDto>(reminder));
    }

    public async Task<ApiResponse<ReminderResponseDto>> UpdateReminder(int id, ReminderUpdateDto reminderUpdateDto)
    {
        var reminder = await _reminderRepository.GetByIdAsync(id);
        if (reminder == null)
        {
            return ApiResponse<ReminderResponseDto>.ErrorResponse("Reminder not found.", 404);
        }

        _mapper.Map(reminderUpdateDto, reminder);
        _reminderRepository.UpdateAsync(reminder);
        return ApiResponse<ReminderResponseDto>.SuccessResponse(_mapper.Map<ReminderResponseDto>(reminder));
    }

    public async Task<ApiResponse<string>> DeleteReminder(int reminderId)
    {
        await _reminderRepository.DeleteAsync(reminderId);
        return ApiResponse<string>.SuccessResponse("Reminder deleted successfully.", "Reminder deleted successfully.");
    }
}