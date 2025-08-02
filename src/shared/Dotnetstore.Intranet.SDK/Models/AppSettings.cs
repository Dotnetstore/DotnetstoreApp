namespace Dotnetstore.Intranet.SDK.Models;

public sealed class AppSettings
{
    public string SecurityKey { get; init; } = string.Empty;
    public string JwtIssuer { get; init; } = string.Empty;
    public string JwtAudience { get; init; } = string.Empty;
    public int JwtExpirationInMinutes { get; init; } = 60;
    public string HttpClientBaseAddress { get; init; } = string.Empty;
    public string JwtTokenCookieName { get; init; } = string.Empty;
    public string RefreshTokenCookieName { get; init; } = string.Empty;
    public string LocalApiClientName { get; init; } = string.Empty;
    public string SmtpServer { get; init; } = string.Empty;
    public int SmtpPort { get; init; }
    public string SmtpUsername { get; init; } = string.Empty;
    public string SmtpPassword { get; init; } = string.Empty;
    public string SmtpNoReplyAddress { get; init; } = string.Empty;
}