using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Dotnetstore.Intranet.SDK.Models;
using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
using Dotnetstore.Intranet.SDK.Services;

namespace Dotnetstore.Intranet.Web.Pages.Organization.ApplicationUsers.Services;

internal sealed class ApplicationUserClientService(
    IHttpClientFactory factory,
    AppSettings appSettings) : IApplicationUserClientService
{
    // async ValueTask<HttpResponseMessage> IApplicationUserClientService.LoginAsync(
    //     ApplicationUserLoginRequest request, 
    //     CancellationToken cancellationToken)
    // {
    //     var client = factory.CreateClient(appSettings.LocalApiClientName);
    //     var content = new StringContent(
    //         JsonSerializer.Serialize(request, new JsonSerializerOptions(JsonSerializerDefaults.Web)), 
    //         Encoding.UTF8, 
    //         MediaTypeNames.Application.Json);
    //     
    //     return await client.PostAsync(ApiEndpoints.Organization.ApplicationUser.Login, content, cancellationToken);
    // }

    async ValueTask<HttpResponseMessage> IApplicationUserClientService.RegisterAsync(
        ApplicationUserRegisterRequest request, 
        CancellationToken cancellationToken)
    {
        var client = factory.CreateClient(appSettings.LocalApiClientName);
        var content = new StringContent(
            JsonSerializer.Serialize(request, new JsonSerializerOptions(JsonSerializerDefaults.Web)), 
            Encoding.UTF8, 
            MediaTypeNames.Application.Json);
        
        return await client.PostAsync(ApiEndpoints.Organization.ApplicationUser.Create, content, cancellationToken);
    }
}