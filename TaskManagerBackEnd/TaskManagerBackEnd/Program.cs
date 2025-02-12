using System.Text.Json.Serialization;
using TaskManagerBackend.Data;
using Microsoft.EntityFrameworkCore;
using TaskManagerBackend.Models;
using TaskManagerBackend.Repositories;
using TaskManagerBackend.Services;
using TaskManagerBackend.Services.Interface;
using TaskManagerBackEnd.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Thêm các dịch vụ vào container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => { options.CustomSchemaIds(x => x.FullName); });
builder.Services.AddControllers();

// Cấu hình DbContext sử dụng PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Đăng ký các repository
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IRepository<Reminder>, ReminderRepository>();
builder.Services.AddScoped<IRepository<TaskItem>, TaskRepository>();

// Đăng ký các service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IReminderService, ReminderService>();
builder.Services.AddScoped<ITaskService, TaskService>();

// Đăng ký AutoMapper và quét các assembly hiện có để tìm các Mapping Profile
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Thêm CORS vào dịch vụ
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();