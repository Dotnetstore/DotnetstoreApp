using Dotnetstore.Intranet.SDK.Models;
using Dotnetstore.Intranet.SDK.Requests.Organization.Users;

namespace Dotnetstore.Intranet.SDK.Services;

internal sealed class ApplicationUserClientService(
    IHttpClientFactory factory,
    AppSettings appSettings) : HttpClientHelper(factory), IApplicationUserClientService
{
    async ValueTask<HttpResponseMessage> IApplicationUserClientService.LoginAsync(
        ApplicationUserLoginRequest request, 
        CancellationToken cancellationToken)
    {
        return await PostAsync(
            appSettings.LocalApiClientName,
            ApiEndpoints.Organization.ApplicationUser.Login,
            request,
            cancellationToken);
    }

    async ValueTask<HttpResponseMessage> IApplicationUserClientService.RegisterAsync(
        ApplicationUserRegisterRequest request, 
        CancellationToken cancellationToken)
    {
        return await PostAsync(
            appSettings.LocalApiClientName,
            ApiEndpoints.Organization.ApplicationUser.Create,
            request,
            cancellationToken);
    }

    async ValueTask<HttpResponseMessage> IApplicationUserClientService.ConfirmEmailAddressAsync(
        ApplicationUserConfirmEmailRequest request,
        CancellationToken cancellationToken)
    {
        return await PostAsync(
            appSettings.LocalApiClientName,
            ApiEndpoints.Organization.ApplicationUser.ConfirmEmailAddress,
            request,
            cancellationToken);
    }
}