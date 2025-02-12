using System.ComponentModel.DataAnnotations;

namespace TaskManagerBackend.DTOs.Tasks;

public class TaskCreateDto
{
    // UserId bắt buộc nếu bạn không sử dụng token
    [Required(ErrorMessage = "UserId is required.")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(255, ErrorMessage = "Title can not exceed 255 characters.")]
    public string Title { get; set; }

    // Mô tả của task (tùy chọn)
    public string Description { get; set; }

    [Required(ErrorMessage = "Priority is required.")]
    [MaxLength(10, ErrorMessage = "Priority must be 'Low', 'Medium', or 'High'.")]
    public string Priority { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    [MaxLength(20, ErrorMessage = "Invalid status.")]
    public string Status { get; set; } = "todo";


    [Required(ErrorMessage = "DueDate is required.")]
    public DateTime DueDate { get; set; }

    // CategoryId là tùy chọn
    public int? CategoryId { get; set; }

}