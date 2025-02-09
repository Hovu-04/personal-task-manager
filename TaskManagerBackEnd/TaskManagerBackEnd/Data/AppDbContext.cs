using Microsoft.EntityFrameworkCore;
using TaskManagerBackend.Models;

namespace TaskManagerBackend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Reminder> Reminders { get; set; }
}