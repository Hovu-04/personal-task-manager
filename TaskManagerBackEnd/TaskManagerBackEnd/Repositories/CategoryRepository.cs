using Microsoft.EntityFrameworkCore;
using TaskManagerBackend.Data;
using TaskManagerBackend.Models;

namespace TaskManagerBackend.Repositories;

public class CategoryRepository : IRepository<Category>
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task AddAsync(Category entity)
    {
        await _context.Categories.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category entity)
    {
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = _context.Categories.Find(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}