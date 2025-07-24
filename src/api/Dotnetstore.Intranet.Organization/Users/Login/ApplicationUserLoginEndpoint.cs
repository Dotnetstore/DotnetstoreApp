using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
using Dotnetstore.Intranet.SDK.Responses.Organization.Users;
using Dotnetstore.Intranet.SDK.Services;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Dotnetstore.Intranet.Organization.Users.Login;

internal sealed class ApplicationUserLoginEndpoint(
    IApplicationUserService applicationUserService,
    ILogger<ApplicationUserLoginEndpoint> logger) : Ep.Req<ApplicationUserLoginRequest>.Res<ApplicationUserTokenResponse>
{
    public override void Configure()
    {        
        Post(ApiEndpoints.Organization.ApplicationUser.Login);
        Description(x => x
            .Produces<ApplicationUserTokenResponse>()
            .ProducesProblemDetails()
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
            await Send.ErrorsAsync(statusCode: StatusCodes.Status400BadRequest, ct).ConfigureAwait(false);
            return;
        }
        
        logger.LogInformation("Login successful for user: {Username}", req.Username);
        await Send.OkAsync(result.Value, ct).ConfigureAwait(false);
    }
}