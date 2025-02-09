namespace TaskManagerBackEnd.Enums
{
    /// <summary>
    /// Trạng thái của công việc.
    /// Tương ứng với ENUM type 'task_status' trong DB: ('todo', 'in_progress', 'done', 'archived')
    /// </summary>
    public enum TaskPriority
    {
        Low, // 'low'
        Medium, // 'medium'
        High // 'high'
    }
}