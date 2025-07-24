namespace Dotnetstore.Intranet.SDK.Requests.Organization.Users;

public record struct ApplicationUserRefreshTokenRequest(
    Guid UserId,
    string RefreshToken);