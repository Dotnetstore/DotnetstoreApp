using Dotnetstore.Intranet.SharedKernel.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.Intranet.SharedKernel.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterSharedKernelServices(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthService, AuthService>();

        return services;
    }
}