namespace Dotnetstore.Intranet.Organization.Services;

// public sealed class RefreshTokenMiddleware(RequestDelegate next)
// {
//     // public async Task InvokeAsync(
//     //     HttpContext context,
//     //     IApplicationUserService applicationUserService,
//     //     IOptions<CookieOptions> cookieOptions)
//     // {
//     //     var refreshToken = context.Request.Query["refresh_token"].FirstOrDefault();
//     //     
//     //     // if (!string.IsNullOrEmpty(refreshToken))
//     //     // {
//     //     //     var result = await applicationUserService.RefreshTokenAsync(refreshToken);
//     //     //
//     //     //     if (result.IsSuccess)
//     //     //     {
//     //     //         context.Response.Cookies.Append("jwt_token", result.Value.Token, cookieOptions.Value);
//     //     //         context.Response.Cookies.Append("refresh_token", result.Value.RefreshToken, cookieOptions.Value);
//     //     //     }
//     //     // }
//     //     
//     //     await next(context);
//     // }
// }