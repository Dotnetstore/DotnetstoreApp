using Dotnetstore.Intranet.SDK.Requests.Organization.Users;

namespace Dotnetstore.Intranet.SDK.Services;

public interface IApplicationUserClientService
{
    ValueTask<HttpResponseMessage>  LoginAsync(
        ApplicationUserLoginRequest request, 
        CancellationToken cancellationToken = default);
    
    ValueTask<HttpResponseMessage> RegisterAsync(
        ApplicationUserRegisterRequest request, 
        CancellationToken cancellationToken = default);
    
    ValueTask<HttpResponseMessage> ConfirmEmailAddressAsync(
        ApplicationUserConfirmEmailRequest request, 
        CancellationToken cancellationToken = default);
}