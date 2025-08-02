namespace Dotnetstore.Intranet.Web.Pages.Organization.ApplicationUsers.Services;

internal interface ITokenProvider
{
    string? GetJwtToken();
    string? GetRefreshToken();
    void SetTokens(string jwt, string refreshToken);
}