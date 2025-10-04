using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

namespace MrBekoXBlogAppServer.Infrastructure.Security;

public static class TokenHasher
{
    public static string HashToken(string token)
    {
        using var sha256 = SHA256.Create();
        var tokenBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(tokenBytes);
    }
}
