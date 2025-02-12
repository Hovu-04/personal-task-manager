using System.ComponentModel.DataAnnotations;

namespace TaskManagerBackend.DTOs.Tasks;

public class TaskUpdateDto
{
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(255, ErrorMessage = "Title can not exceed 255 characters.")]
    public string Title { get; set; }

    public string Description { get; set; }

    [Required(ErrorMessage = "Priority is required.")]
    public string Priority { get; set; }

    [Required(ErrorMessage = "DueDate is required.")]
    public DateTime DueDate { get; set; }

    public string Status { get; set; }

    public int? CategoryId { get; set; }

    // Ép kiểu thành UTC khi nhận request
    public void Normalize()
    {
        DueDate = DateTime.SpecifyKind(DueDate, DateTimeKind.Utc);
    }
}
