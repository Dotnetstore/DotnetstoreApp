// using Aspire.Hosting;
// using Dotnetstore.Intranet.AppHost.Tests.Extensions;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Hosting;
//

using Aspire.Hosting;

namespace Dotnetstore.Intranet.AppHost.Tests.Fixtures;

public sealed class ApiFixture : IDisposable
{
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(60);
    private static DistributedApplication _application = null!;

    internal static async ValueTask<HttpClient> CreateHttpClientAsync()
    {
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.Dotnetstore_Intranet_AppHost>();
        
        _application = await appHost.BuildAsync().WaitAsync(DefaultTimeout);
        await _application.StartAsync().WaitAsync(DefaultTimeout);

        await _application.ResourceNotifications.WaitForResourceHealthyAsync("webapi").WaitAsync(DefaultTimeout);

        return _application.CreateHttpClient("webapi");
    }

    public void Dispose()
    {
        _application.Dispose();
    }
}