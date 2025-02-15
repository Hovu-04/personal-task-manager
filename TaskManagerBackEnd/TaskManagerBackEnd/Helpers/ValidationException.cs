namespace TaskManagerBackEnd.Helpers;

public class ValidationException : CustomException
{
    public List<string> Errors { get; }

    public ValidationException(string message, List<string> errors) : base(message, 400)
    {
        Errors = errors;
    }
}