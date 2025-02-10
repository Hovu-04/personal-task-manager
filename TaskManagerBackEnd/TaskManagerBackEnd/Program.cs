using Microsoft.EntityFrameworkCore;
using TaskManagerBackend.Data;
using TaskManagerBackEnd.DTOs.User;
using TaskManagerBackend.Models;
using TaskManagerBackend.Repositories;
using TaskManagerBackend.Services;
using TaskManagerBackend.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Thêm các dịch vụ vào container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Cấu hình DbContext sử dụng PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Đăng ký open generic repository
builder.Services.AddScoped<IRepository<User>, UserRepository>();

// Đăng ký IUserService
builder.Services.AddScoped<IUserService, UserService>();

// Đăng ký AutoMapper và quét tất cả các assembly hiện có để tìm các Mapping Profile
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Cấu hình pipeline HTTP request.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();