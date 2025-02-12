using Microsoft.AspNetCore.Mvc;
using TaskManagerBackEnd.DTOs.Reminder;
using TaskManagerBackend.Models;
using TaskManagerBackend.Services;

namespace TaskManagerBackend.Controllers;

[ApiController]
[Route("api/reminders")]
public class ReminderController : ControllerBase
{
    private readonly IReminderService _reminderService;

    public ReminderController(IReminderService reminderService)
    {
        _reminderService = reminderService;
    }

    // Get: api/reminders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reminder>>> GetReminders()
    {
        var reminder = await _reminderService.GetAllReminders();
        if (!reminder.Success)
            return NotFound(reminder);
        return Ok(reminder);
    }

    // Get: api/reminders/tasks/{id}
    [HttpGet("tasks/{id:int}")]
    public async Task<ActionResult<IEnumerable<Reminder>>> GetAllTaskById(int id)
    {
        var reminder = await _reminderService.GetAllRemindersByTaskId(id);
        if (!reminder.Success)
            return NotFound(reminder);
        return Ok(reminder);
    }

    // Get: api/reminders/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Reminder>> GetReminderById(int id)
    {
        var reminder = await _reminderService.GetReminderById(id);
        if (!reminder.Success)
        {
            return BadRequest(reminder);
        }

        return Ok(reminder);
    }

    // Post: api/reminders
    [HttpPost]
    public async Task<ActionResult<Reminder>> CreateReminder([FromBody] ReminderCreateDto reminderCreateDto)
    {
        var reminder = await _reminderService.CreateReminder(reminderCreateDto);
        if (!reminder.Success)
        {
            return BadRequest(reminder);
        }

        return CreatedAtAction(nameof(GetReminderById), new { id = reminder.Data!.Id }, reminder);
    }

    // Put: api/reminders/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Reminder>> UpdateReminder(int id, [FromBody] ReminderUpdateDto reminderUpdateDto)
    {
        var reminder = await _reminderService.UpdateReminder(id, reminderUpdateDto);
        if (reminder.Success)
        {
            return Ok(reminder);
        }

        return BadRequest(reminder);
    }

    // Delete: api/reminders/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<string>> DeleteReminder(int id)
    {
        var reminder = await _reminderService.DeleteReminder(id);
        if (reminder.Success)
        {
            return Ok(reminder);
        }

        return Ok(reminder);
    }
}