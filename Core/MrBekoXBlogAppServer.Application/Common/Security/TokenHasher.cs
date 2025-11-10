using System.Security.Cryptography;

namespace MrBekoXBlogAppServer.Application.Common.Security;

/// <summary>
/// Token'ları hash'lemek için SHA256 kullanan utility class
/// </summary>
public static class TokenHasher
{
    /// <summary>
    /// Verilen token'ı SHA256 ile hash'ler ve Base64 string olarak döner
    /// </summary>
    /// <param name="token">Hash'lenecek token</param>
    /// <returns>Base64 encoded hash</returns>
    public static string HashToken(string token)
    {
        using var sha256 = SHA256.Create();
        var tokenBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(tokenBytes);
    }
}
