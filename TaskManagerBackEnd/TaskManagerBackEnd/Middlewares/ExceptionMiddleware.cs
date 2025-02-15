using System.Text.Json;
using TaskManagerBackEnd.Helpers;

namespace TaskManagerBackend.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (ex is not NotFoundException)
                {
                    _logger.LogError(ex, "An error occurred.");
                }

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            int statusCode;
            string error;
            string message = exception.Message;

            switch (exception)
            {
                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    error = "Not Found";
                    break;

                case ValidationException validationEx:
                    statusCode = StatusCodes.Status400BadRequest;
                    error = "Bad Request";
                    message = validationEx.Message;
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    error = "Internal Server Error";
                    message = "An unexpected error occurred.";
                    break;
            }

            response.StatusCode = statusCode;

            var errorResponse = new
            {
                status = statusCode,
                error,
                message,
                path = context.Request.Path,
                timestamp = DateTime.UtcNow
            };

            var result = JsonSerializer.Serialize(errorResponse);
            await response.WriteAsync(result);
        }
    }
}