namespace Dotnetstore.Intranet.SharedKernel.Models;

public sealed class AppSettings
{
    public string SecurityKey { get; init; } = string.Empty;
    public string JwtIssuer { get; init; } = string.Empty;
    public string JwtAudience { get; init; } = string.Empty;
    public int JwtExpirationInMinutes { get; init; } = 60;
}