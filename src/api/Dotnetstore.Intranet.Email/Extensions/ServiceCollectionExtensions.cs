using Dotnetstore.Intranet.Email.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.Intranet.Email.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterEmailServices(this IServiceCollection services)
    {
        services
            .AddScoped<IApplicationUserEmail, ApplicationUserEmail>();
        
        return services;
    }
}