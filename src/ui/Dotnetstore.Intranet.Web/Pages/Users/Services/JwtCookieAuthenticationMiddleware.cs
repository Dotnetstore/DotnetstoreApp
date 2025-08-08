using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Dotnetstore.Intranet.SDK.Models;
using Microsoft.IdentityModel.Tokens;

namespace Dotnetstore.Intranet.Web.Pages.Users.Services;

internal sealed class JwtCookieAuthenticationMiddleware(
    ILogger<JwtCookieAuthenticationMiddleware> logger,
    RequestDelegate next,
    TokenValidationParameters tokenValidationParameters,
    AppSettings appSettings)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var jwt = context.Request.Cookies[appSettings.JwtTokenCookieName];
        if (string.IsNullOrEmpty(jwt))
        {
            await next(context);
            return;
        }
        
        var principal = ValidateJwt(jwt, tokenValidationParameters);
        
        if (principal == null)
        {
            logger.LogWarning("JWT token validation failed.");
            context.Response.Cookies.Delete(appSettings.JwtTokenCookieName);
            context.Response.StatusCode = 401;
            return;
        }
        
        context.User = principal;
        await next(context);
    }
    
    private static ClaimsPrincipal? ValidateJwt(string jwt, TokenValidationParameters parameters)
    {
        var handler = new JwtSecurityTokenHandler();
        try
        {
            var principal = handler.ValidateToken(jwt, parameters, out var validatedToken);
            if (validatedToken is not JwtSecurityToken)
                return null;
            return principal;
        }
        catch
        {
            return null;
        }
    }
}