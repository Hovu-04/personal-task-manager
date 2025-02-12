using TaskManagerBackend.DTOs.Tasks;
using TaskManagerBackEnd.Helpers;

namespace TaskManagerBackEnd.Services.Interface;

public interface ITaskService
{
    Task<ApiResponse<IEnumerable<TaskResponseDto>>> GetAllTasksAsync();
    Task<ApiResponse<TaskResponseDto>> GetTaskByIdAsync(int id);
    Task<ApiResponse<TaskResponseDto>> CreateTaskAsync(TaskCreateDto taskCreateDto);
    Task<ApiResponse<TaskResponseDto>> UpdateTaskAsync(int id, TaskUpdateDto taskUpdateDto);
    Task<ApiResponse<string>> DeleteTaskAsync(int id);
}