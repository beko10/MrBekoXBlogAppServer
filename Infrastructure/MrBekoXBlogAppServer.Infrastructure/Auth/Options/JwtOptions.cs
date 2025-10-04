namespace MrBekoXBlogAppServer.Infrastructure.Auth.Options;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    public string Issuer { get; init; } = default!;
    public List<string> Audience { get; init; } = default!;
    public int AccessTokenExpirationMinutes { get; init; }
    public int RefreshTokenExpirationDays { get; init; }
    public string SecretKey { get; set; } = null!;
}
