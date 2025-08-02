using Dotnetstore.Intranet.Organization.Users;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.Intranet.Organization.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterOrganizationServices(this IServiceCollection services)
    {
        services
            .AddScoped<IApplicationUserService, ApplicationUserService>()
            .AddFastEndpoints();

        services
            .Configure<CookieOptions>(options =>
            {
                options.HttpOnly = true;
                options.Secure = true;
                options.SameSite = SameSiteMode.Strict;
                options.Expires = DateTimeOffset.UtcNow.AddHours(4);
            });
        
        return services;
    }
}