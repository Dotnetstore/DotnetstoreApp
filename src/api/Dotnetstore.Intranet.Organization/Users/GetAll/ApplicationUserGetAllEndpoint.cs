// using Dotnetstore.Intranet.SDK.Responses.Organization.Users;
// using Dotnetstore.Intranet.SDK.Services;
// using FastEndpoints;
// using FastEndpoints.Swagger;
// using Microsoft.AspNetCore.Http;
// using Microsoft.Extensions.Logging;
//
// namespace Dotnetstore.Intranet.Organization.Users.GetAll;
//
// internal sealed class ApplicationUserGetAllEndpoint(
//     IApplicationUserService applicationUserService,
//     ILogger<ApplicationUserGetAllEndpoint> logger) : Ep.NoReq.Res<IEnumerable<ApplicationUserResponse>>
// {
//     public override void Configure()
//     {
//         Get(ApiEndpoints.Organization.ApplicationUser.GetAll);
//         Description(x => x
//             .Produces<IEnumerable<ApplicationUserResponse>>()
//             .AutoTagOverride("Application Users"));
//         Summary(s =>
//         {
//             s.Summary = "Get all application users";
//             s.Description = "This endpoint retrieves all application users. It returns a list of user details. If no users are found, an empty list will be returned.";
//         });
//         Roles("Administrator");
//     }
//
//     public override async Task HandleAsync(CancellationToken ct)
//     {
//         logger.LogInformation("Handling request to get all application users");
//
//         var list = await applicationUserService.GetAllAsync(ct);
//         
//         await Send.OkAsync(list, ct).ConfigureAwait(false);
//     }
// }