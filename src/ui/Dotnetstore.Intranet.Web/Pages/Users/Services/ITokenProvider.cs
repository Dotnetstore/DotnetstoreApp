namespace Dotnetstore.Intranet.Web.Pages.Users.Services;

internal interface ITokenProvider
{
    string? GetJwtToken();
    string? GetRefreshToken();
    void SetTokens(string jwt, string refreshToken);
}