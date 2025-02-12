using AutoMapper;
using TaskManagerBackend.DTOs.Tasks;
using TaskManagerBackEnd.Helpers;
using TaskManagerBackend.Models;
using TaskManagerBackend.Repositories;
using TaskManagerBackEnd.Services.Interface;

namespace TaskManagerBackend.Services;

public class TaskService : ITaskService
{
    private readonly IMapper _mapper;
    private readonly IRepository<TaskItem> _taskRepository;

    public TaskService(IMapper mapper, IRepository<TaskItem> taskRepository)
    {
        _mapper = mapper;
        _taskRepository = taskRepository;
    }

    public async Task<ApiResponse<IEnumerable<TaskResponseDto>>> GetAllTasksAsync()
    {
        var taskItem = await _taskRepository.GetAllAsync();
        if (!taskItem.Any())
        {
            var emptyList = Enumerable.Empty<TaskResponseDto>();
            return ApiResponse<IEnumerable<TaskResponseDto>>.SuccessResponse(emptyList, "No task found");
        }

        var taskDtos = _mapper.Map<IEnumerable<TaskResponseDto>>(taskItem);
        return ApiResponse<IEnumerable<TaskResponseDto>>.SuccessResponse(taskDtos, "Task retrieved successfully.");
        ;
    }

    public async Task<ApiResponse<TaskResponseDto>> GetTaskByIdAsync(int id)
    {
        var taskItem = await _taskRepository.GetByIdAsync(id);
        if (taskItem == null)
        {
            return ApiResponse<TaskResponseDto>.ErrorResponse("Task not found", 404);
        }

        var taskDto = _mapper.Map<TaskResponseDto>(taskItem);
        return ApiResponse<TaskResponseDto>.SuccessResponse(taskDto, "Task retrieved successfully.");
    }

    public async Task<ApiResponse<TaskResponseDto>> CreateTaskAsync(TaskCreateDto taskCreateDto)
    {
        // Kiểm tra priority có hợp lệ không
        if (!TaskConstants.ValidPriorities.Contains(taskCreateDto.Priority))
        {
            return ApiResponse<TaskResponseDto>.ErrorResponse("Invalid priority value.", 400);
        }

        var taskItem = _mapper.Map<TaskItem>(taskCreateDto);
        if (taskItem == null)
        {
            return ApiResponse<TaskResponseDto>.ErrorResponse("Task mapping failed.", 500);
        }

        await _taskRepository.AddAsync(taskItem);
        var taskDto = _mapper.Map<TaskResponseDto>(taskItem);
        return ApiResponse<TaskResponseDto>.SuccessResponse(taskDto, "Task added successfully.");
    }
    
    public async Task<ApiResponse<TaskResponseDto>> UpdateTaskAsync(int id, TaskUpdateDto taskUpdateDto)
    {
        var existingTask = await _taskRepository.GetByIdAsync(id);
        if (existingTask == null)
        {
            return ApiResponse<TaskResponseDto>.ErrorResponse("Task not found.", 404);
        }

        // Chuyển DueDate sang UTC trước khi cập nhật
        taskUpdateDto.DueDate = DateTime.SpecifyKind(taskUpdateDto.DueDate, DateTimeKind.Utc);

        // Kiểm tra priority hợp lệ
        if (!TaskConstants.ValidPriorities.Contains(taskUpdateDto.Priority))
        {
            return ApiResponse<TaskResponseDto>.ErrorResponse("Invalid priority value.", 400);
        }

        // Kiểm tra status hợp lệ
        if (!TaskConstants.ValidStatuses.Contains(taskUpdateDto.Status))
        {
            return ApiResponse<TaskResponseDto>.ErrorResponse("Invalid status value.", 400);
        }

        // Map DTO vào entity sau khi đã chuẩn hóa thời gian
        _mapper.Map(taskUpdateDto, existingTask);

        await _taskRepository.UpdateAsync(existingTask);

        var taskDto = _mapper.Map<TaskResponseDto>(existingTask);
        return ApiResponse<TaskResponseDto>.SuccessResponse(taskDto, "Task updated successfully.");
    }


    public async Task<ApiResponse<string>> DeleteTaskAsync(int id)
    {
        var existingTask = await _taskRepository.GetByIdAsync(id);
        if (existingTask == null)
        {
            // Sửa "foud" thành "found" và làm rõ thông báo lỗi
            return ApiResponse<string>.ErrorResponse("Task not found.", 404);
        }

        await _taskRepository.DeleteAsync(id);
        return ApiResponse<string>.SuccessResponse("Task deleted successfully.", "Task deleted successfully.");
    }
}