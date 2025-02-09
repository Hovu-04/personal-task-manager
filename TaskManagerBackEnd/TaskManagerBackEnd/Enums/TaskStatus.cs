namespace TaskManagerBackEnd.Enums
{
    /// <summary>
    /// Mức độ ưu tiên của công việc.
    /// Tương ứng với ENUM type 'task_priority' trong DB: ('low', 'medium', 'high')
    /// </summary>
    public enum TaskStatus
    {
        Todo, // 'todo'
        InProgress, // 'in_progress'
        Done, // 'done'
        Archived // 'archived'
    }
}