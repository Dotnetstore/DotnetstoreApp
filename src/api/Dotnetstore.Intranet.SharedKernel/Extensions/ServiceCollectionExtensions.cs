using Dotnetstore.Intranet.SharedKernel.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.Intranet.SharedKernel.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder RegisterSharedKernelServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddScoped<IAuthService, AuthService>();

        return builder;
    }
}