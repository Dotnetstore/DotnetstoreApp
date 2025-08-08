using Dotnetstore.Intranet.SDK.Models;
using Dotnetstore.Intranet.SDK.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.Intranet.SDK.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder RegisterSdk(
        this WebApplicationBuilder builder, 
        AppSettings appSettings)
    {
        builder.Services
            .AddScoped<IApplicationUserClientService, ApplicationUserClientService>()
            .AddHttpClient(appSettings.LocalApiClientName, client =>
        {
            client.BaseAddress = new Uri(appSettings.HttpClientBaseAddress);
        });

        return builder;
    }
}