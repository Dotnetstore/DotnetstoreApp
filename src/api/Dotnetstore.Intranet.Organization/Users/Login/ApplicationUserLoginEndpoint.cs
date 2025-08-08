namespace Dotnetstore.Intranet.Organization.Users.Login;

internal sealed class ApplicationUserLoginEndpoint(
    IApplicationUserService applicationUserService,
    ILogger<ApplicationUserLoginEndpoint> logger,
    IOptions<CookieOptions> cookieOptions) : Ep.Req<ApplicationUserLoginRequest>.NoRes
{
    public override void Configure()
    {        
        Post(ApiEndpoints.Organization.ApplicationUser.Login);
        Description(x => x
            .ProducesProblemDetails(statusCode: StatusCodes.Status400BadRequest)
            .ProducesProblemDetails(statusCode: StatusCodes.Status401Unauthorized)
            .AutoTagOverride("Application Users"));
        Summary(s =>
        {
            s.Summary = "Login an application user";
            s.Description = "This endpoint allows an application user to log in and receive a JWT token. The user must provide a valid username and password. If the credentials are correct, a token will be returned for further authenticated requests.";
        });
        AllowAnonymous();
    }

    public override async Task HandleAsync(ApplicationUserLoginRequest req, CancellationToken ct)
    {
        logger.LogInformation("Handling login request for user: {Username}", req.Username);
        var result = await applicationUserService.LoginAsync(req, ct).ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            var message = string.Join(", ", result.Errors);
            logger.LogWarning("Login failed for user {Username}: {Message}", req.Username, message);
            AddError(message);
            
            if (result.Status == ResultStatus.NotFound)
            {
                await Send.ErrorsAsync(statusCode: StatusCodes.Status401Unauthorized, ct).ConfigureAwait(false);
                return;
            }
            
            await Send.ErrorsAsync(statusCode: StatusCodes.Status400BadRequest, ct).ConfigureAwait(false);
            return;
        }
        
        HttpContext.Response.Cookies.Append("jwt_token", result.Value.Token, cookieOptions.Value);
        HttpContext.Response.Cookies.Append("refresh_token", result.Value.RefreshToken, cookieOptions.Value);
        
        logger.LogInformation("Login successful for user: {Username}", req.Username);
        await Send.OkAsync(cancellation: ct).ConfigureAwait(false);
    }
}