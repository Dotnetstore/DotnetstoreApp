namespace Dotnetstore.Intranet.SDK.Requests.Organization.Users;

public record struct ApplicationUserLoginRequest(
    string Username,
    string Password);