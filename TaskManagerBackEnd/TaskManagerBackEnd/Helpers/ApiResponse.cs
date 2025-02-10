namespace TaskManagerBackEnd.Helpers;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    public int? ErrorCode { get; set; }
    public List<string>? Errors { get; set; }

    public ApiResponse(bool success, string message, T? data, int? errorCode = null, List<string>? errors = null)
    {
        Success = success;
        Message = message;
        Data = data;
        ErrorCode = errorCode;
        Errors = errors;
    }

    // ✅ Tạo response thành công
    public static ApiResponse<T> SuccessResponse(T data, string message = "Thành công")
    {
        return new ApiResponse<T>(true, message, data);
    }

    // ✅ Tạo response thất bại (với mã lỗi)
    public static ApiResponse<T> ErrorResponse(string message, int errorCode = 400, List<string>? errors = null)
    {
        return new ApiResponse<T>(false, message, default, errorCode, errors);
    }

    // ✅ Tạo response thành công nhưng không có data
    public static ApiResponse<T> EmptySuccess(string message = "Thành công")
    {
        return new ApiResponse<T>(true, message, default);
    }
}