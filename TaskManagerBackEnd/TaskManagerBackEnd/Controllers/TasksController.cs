using Microsoft.AspNetCore.Mvc;
using TaskManagerBackend.DTOs.Tasks;
using TaskManagerBackEnd.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManagerBackend.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
    
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetTasks()
        {
            var response = await _taskService.GetAllTasksAsync();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskResponseDto>> GetTaskById(int id)
        {
            var response = await _taskService.GetTaskByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        
        [HttpPost]
        public async Task<ActionResult<TaskResponseDto>> CreateTask([FromBody] TaskCreateDto taskCreateDto)
        {
            var response = await _taskService.CreateTaskAsync(taskCreateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return CreatedAtAction(nameof(GetTaskById), new { id = response.Data!.Id }, response);
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult<TaskResponseDto>> UpdateTask(int id, [FromBody] TaskUpdateDto taskUpdateDto)
        {
            var response = await _taskService.UpdateTaskAsync(id, taskUpdateDto);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> DeleteTask(int id)
        {
            var response = await _taskService.DeleteTaskAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
