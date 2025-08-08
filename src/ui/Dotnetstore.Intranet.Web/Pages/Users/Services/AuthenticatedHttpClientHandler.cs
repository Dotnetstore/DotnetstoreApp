using System.Net.Http.Headers;

namespace Dotnetstore.Intranet.Web.Pages.Users.Services;

internal sealed class AuthenticatedHttpClientHandler(ITokenProvider tokenProvider) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var jwt = tokenProvider.GetJwtToken();
        var refreshToken = tokenProvider.GetRefreshToken();

        if (!string.IsNullOrEmpty(jwt))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        if (!string.IsNullOrEmpty(refreshToken))
            request.Headers.Add("X-Refresh-Token", refreshToken);

        return await base.SendAsync(request, cancellationToken);
    }
}