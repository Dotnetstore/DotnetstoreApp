using Dotnetstore.Intranet.Organization.Users;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.Intranet.Organization.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterOrganizationServices(this IServiceCollection services)
    {
        services
            .AddScoped<IApplicationUserService, ApplicationUserService>()
            .AddFastEndpoints();
        
        return services;
    }
}