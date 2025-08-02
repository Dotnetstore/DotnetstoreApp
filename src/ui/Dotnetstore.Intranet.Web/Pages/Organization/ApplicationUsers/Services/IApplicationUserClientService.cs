using Dotnetstore.Intranet.SDK.Requests.Organization.Users;

namespace Dotnetstore.Intranet.Web.Pages.Organization.ApplicationUsers.Services;

internal interface IApplicationUserClientService
{
    // ValueTask<HttpResponseMessage>  LoginAsync(ApplicationUserLoginRequest request, CancellationToken cancellationToken = default);
    
    ValueTask<HttpResponseMessage> RegisterAsync(ApplicationUserRegisterRequest request, CancellationToken cancellationToken = default);
}