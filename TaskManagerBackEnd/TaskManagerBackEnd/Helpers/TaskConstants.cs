namespace TaskManagerBackEnd.Helpers;

public static class TaskConstants
{
    public static readonly HashSet<string> ValidPriorities = new() { "Low", "Medium", "High" };
    public static readonly HashSet<string> ValidStatuses = new() { "todo", "in_progress", "done", "archived" };
}