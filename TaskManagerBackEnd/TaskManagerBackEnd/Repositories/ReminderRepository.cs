using Microsoft.EntityFrameworkCore;
using TaskManagerBackend.Data;
using TaskManagerBackend.Models;

namespace TaskManagerBackend.Repositories;

public class ReminderRepository : IRepository<Reminder>
{
    private readonly AppDbContext _context;

    public ReminderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Reminder>> GetAllAsync()
    {
        return await _context.Reminders.ToListAsync();
    }

    public async Task<Reminder?> GetByIdAsync(int id)
    {
        var reminder = await  _context.Reminders.FindAsync(id);
        return reminder;
    }

    public async Task AddAsync(Reminder entity)
    {
        await _context.Reminders.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Reminder entity)
    {
        _context.Reminders.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Reminders.FindAsync(id);
        if (entity != null)
        {
            _context.Reminders.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}