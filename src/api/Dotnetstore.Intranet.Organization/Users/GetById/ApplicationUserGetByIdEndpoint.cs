// using Dotnetstore.Intranet.SDK.Responses.Organization.Users;
// using Dotnetstore.Intranet.SDK.Services;
// using FastEndpoints;
// using FastEndpoints.Swagger;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Logging;
//
// namespace Dotnetstore.Intranet.Organization.Users.GetById;
//
// internal sealed class ApplicationUserGetByIdEndpoint(
//     IApplicationUserService applicationUserService,
//     ILogger<ApplicationUserGetByIdEndpoint> logger) : Ep.NoReq.Res<ApplicationUserResponse>
// {
//     public override void Configure()
//     {
//         Get(ApiEndpoints.Organization.ApplicationUser.GetById);
//         Description(x => x
//             .Produces<ApplicationUserResponse>()
//             .ProducesProblemDetails(StatusCodes.Status404NotFound)
//             .AutoTagOverride("Application Users"));
//         Summary(s =>
//         {
//             s.Summary = "Get an application user by ID";
//             s.Description = "This endpoint retrieves an application user by their unique identifier (ID). The ID must be provided in the request path. If the user is found, their details will be returned. If not, a 404 Not Found response will be returned.";
//         });
//         AllowAnonymous();
//     }
//
//     public override async Task HandleAsync(CancellationToken ct)
//     {
//         var id = Route<Guid>("id");
//         logger.LogInformation("Handling request to get user by ID: {Id}", id);
//         
//         var result = await applicationUserService.GetByIdAsync(id, ct).ConfigureAwait(false);
//         
//         if (!result.IsSuccess)
//         {
//             var message = string.Join(", ", result.Errors);
//             logger.LogWarning("Failed to retrieve user by ID {Id}: {Message}", id, message);
//             AddError(message);
//             await Send.ErrorsAsync(StatusCodes.Status404NotFound, ct).ConfigureAwait(false);
//             return;
//         }
//
//         await Send.OkAsync(result.Value, ct).ConfigureAwait(false);
//     }
// }