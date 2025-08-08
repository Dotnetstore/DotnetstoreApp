using Dotnetstore.Intranet.SDK.Models;

namespace Dotnetstore.Intranet.Web.Pages.Users.Services;

internal sealed class TokenProvider(
    IHttpContextAccessor httpContextAccessor,
    AppSettings appSettings) : ITokenProvider
{
    string? ITokenProvider.GetJwtToken()
    {
        return httpContextAccessor.HttpContext?.Request.Cookies[appSettings.JwtTokenCookieName];
    }

    string? ITokenProvider.GetRefreshToken()
    {
        return httpContextAccessor.HttpContext?.Request.Cookies[appSettings.RefreshTokenCookieName];
    }

    void ITokenProvider.SetTokens(string jwt, string refreshToken)
    {
        if (httpContextAccessor.HttpContext != null)
            StoreTokens(httpContextAccessor.HttpContext, jwt, refreshToken);
    }

    private void StoreTokens(HttpContext httpContext, string jwt, string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7),
            IsEssential = true
        };
        
        httpContext.Response.Cookies.Append(appSettings.JwtTokenCookieName, jwt, cookieOptions);
        httpContext.Response.Cookies.Append(appSettings.RefreshTokenCookieName, refreshToken, cookieOptions);
    }
}