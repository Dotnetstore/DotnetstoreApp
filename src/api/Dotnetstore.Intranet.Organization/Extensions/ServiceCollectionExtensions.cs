using Dotnetstore.Intranet.Organization.Data;
using Dotnetstore.Intranet.Organization.Handlers;
using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.Organization.Services;
using Dotnetstore.Intranet.Organization.UserInRoles;
using Dotnetstore.Intranet.Organization.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dotnetstore.Intranet.Organization.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder RegisterOrganizationServices(this WebApplicationBuilder builder)
    {
        builder
            .AddNpgsqlDbContext<OrganizationDataContext>(connectionName: "DotnetstoreIntranet");
            
        builder.Services
            .AddScoped<ApplicationUserCreatedCheckApproveStatusHandler>()
            .AddScoped<ApplicationUserCreatedSetRoleHandler>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IEventService, EventService>()
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IApplicationUserRepository, ApplicationUserRepository>()
            .AddScoped<IApplicationUserService, ApplicationUserService>()
            .AddScoped<IApplicationUserRoleRepository, ApplicationUserRoleRepository>()
            .AddScoped<IApplicationUserRoleService, ApplicationUserRoleService>()
            .AddScoped<IApplicationUserInRoleRepository, ApplicationUserInRoleRepository>()
            .AddScoped<IApplicationUserInRoleService, ApplicationUserInRoleService>()
            .AddFastEndpoints();

        builder.Services
            .Configure<CookieOptions>(options =>
            {
                options.HttpOnly = true;
                options.Secure = true;
                options.SameSite = SameSiteMode.Strict;
                options.Expires = DateTimeOffset.UtcNow.AddHours(4);
            });
        
        return builder;
    }
    
    public static WebApplication UseOrganizationServices(this WebApplication app)
    {
        app
            .UseMiddleware<RefreshTokenMiddleware>()
            .UseAuthentication()
            .UseAuthorization();
        
        return app;
    }
}