namespace TaskManagerBackend.Helpers;

public class BCryptHelper
{
    /// <summary>
    /// Hash mật khẩu trước khi lưu vào database
    /// </summary>
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <summary>
    /// Kiểm tra mật khẩu nhập vào có khớp với hash đã lưu không
    /// </summary>
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}