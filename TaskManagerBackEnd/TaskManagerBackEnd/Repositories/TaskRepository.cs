using Microsoft.EntityFrameworkCore;
using TaskManagerBackend.Data;
using TaskManagerBackend.Models;

namespace TaskManagerBackend.Repositories;

public class TaskRepository : IRepository<TaskItem>
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _context.TaskItems.ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        var taskItem = await _context.TaskItems.FindAsync(id);
        return taskItem;
    }

    public async Task AddAsync(TaskItem entity)
    {
        await _context.TaskItems.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskItem entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var taskItem = await _context.TaskItems.FindAsync(id);
        if (taskItem != null)
        {
            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();
        }
    }
}