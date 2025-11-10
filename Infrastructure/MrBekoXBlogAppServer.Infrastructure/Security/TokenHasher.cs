// Bu class Application.Common.Security.TokenHasher'a taşındı
// Geriye dönük uyumluluk için bu wrapper bırakıldı
namespace MrBekoXBlogAppServer.Infrastructure.Security;

public static class TokenHasher
{
    public static string HashToken(string token)
    {
        return Application.Common.Security.TokenHasher.HashToken(token);
    }
}
