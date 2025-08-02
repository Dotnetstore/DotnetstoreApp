// using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
// using Dotnetstore.Intranet.SDK.Responses.Organization.Users;
// using Dotnetstore.Intranet.SDK.Services;
// using FastEndpoints;
// using FastEndpoints.Swagger;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Logging;
//
// namespace Dotnetstore.Intranet.Organization.Users.RenewRefreshToken;
//
// internal sealed class RenewRefreshTokenEndpoint(
//     IApplicationUserService applicationUserService,
//     ILogger<RenewRefreshTokenEndpoint> logger) : Ep.Req<ApplicationUserRefreshTokenRequest>.Res<ApplicationUserTokenResponse>
// {
//     public override void Configure()
//     {
//         Post(ApiEndpoints.Organization.ApplicationUser.RenewRefreshToken);
//         Description(x => x
//             .Produces<ApplicationUserTokenResponse>()
//             .ProducesProblemDetails()
//             .AutoTagOverride("Application Users"));
//         Summary(s =>
//         {
//             s.Summary = "Renew Refresh Token";
//             s.Description = "Renews the refresh token for an application user. This endpoint is used to obtain a new refresh token when the current one is about to expire.";
//         });
//         AllowAnonymous();
//     }
//
//     public override async Task HandleAsync(ApplicationUserRefreshTokenRequest req, CancellationToken ct)
//     {
//         logger.LogInformation("Handling refresh token request for user ID: {UserId}", req.UserId);
//
//         var result = await applicationUserService.RefreshTokenAsync(req.RefreshToken, ct).ConfigureAwait(false);
//
//         if (!result.IsSuccess)
//         {
//             var message = string.Join(", ", result.Errors);
//             logger.LogError("Failed to renew refresh token: {Error}", message);
//             AddError(message);
//             await Send.ErrorsAsync(statusCode: StatusCodes.Status400BadRequest, ct).ConfigureAwait(false);
//             return;
//         }
//
//         logger.LogInformation("Refresh token renewed successfully for user ID: {UserId}", req.UserId);
//         await Send.OkAsync(result.Value, ct).ConfigureAwait(false);
//     }
// }