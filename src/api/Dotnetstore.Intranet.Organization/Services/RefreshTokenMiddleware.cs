namespace Dotnetstore.Intranet.Organization.Services;

internal sealed class RefreshTokenMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        ITokenService tokenService,
        IOptions<CookieOptions> cookieOptions)
    {
        var refreshToken = context.Request.Query["refresh_token"].FirstOrDefault();
        
        if (!string.IsNullOrEmpty(refreshToken))
        {
            var result = await tokenService.UpdateRefreshTokenAsync(refreshToken, context.RequestAborted);
        
            context.Response.Cookies.Append("jwt_token", result.Token, cookieOptions.Value);
            context.Response.Cookies.Append("refresh_token", result.RefreshToken, cookieOptions.Value);
        }
        
        await next(context);
    }
}