using Dotnetstore.Intranet.Email.Extensions;
using Dotnetstore.Intranet.Organization.Data;
using Dotnetstore.Intranet.Organization.Extensions;
using Dotnetstore.Intranet.Organization.Services;
using Dotnetstore.Intranet.SDK.Models;
using Dotnetstore.Intranet.ServiceDefaults;
using Dotnetstore.Intranet.SharedKernel.Extensions;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;

namespace Dotnetstore.Intranet.WebApi.Extensions;

internal static class StartApplicationExtensions
{
    internal static async ValueTask StartApplicationAsync(
        this WebApplicationBuilder builder,
        CancellationToken cancellationToken = default)
    {
        await builder
            .RegisterServices()
            .BuildApplication()
            .RegisterMiddlewares()
            .RunApplicationAsync(cancellationToken);
    }
    
    private static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        builder
            .AddServiceDefaults()
            .AddNpgsqlDbContext<OrganizationDataContext>(connectionName: "DotnetstoreIntranet");
        
        var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
        ArgumentNullException.ThrowIfNull(appSettings, nameof(appSettings));

        builder.Services
            .RegisterSharedKernelServices()
            .RegisterEmailServices()
            .RegisterOrganizationServices()
            .AddSingleton(TimeProvider.System)
            .AddSingleton(appSettings)
            .AddAuthenticationJwtBearer(s =>
            {
                s.SigningKey = "SecretKeyForJwtAuthentication";
            })
            .AddAuthorization()
            .AddFastEndpoints()
            .SwaggerDocument(o =>
            {
                o.DocumentSettings = s =>
                {
                    s.Title = "Dotnetstore Intranet API";
                    s.Version = "v1";
                    s.Description = "API for the Dotnetstore Intranet application.";
                };
            });
        
        return builder;
    }
    
    private static WebApplication BuildApplication(this WebApplicationBuilder builder)
    {
        return builder.Build();
    }
    
    private static WebApplication RegisterMiddlewares(this WebApplication app)
    {
        app
            .UseMiddleware<RefreshTokenMiddleware>()
            .UseAuthentication()
            .UseAuthorization()
            .UseHttpsRedirection()
            .UseFastEndpoints(c =>
            {
                c.Errors.UseProblemDetails();
            })
            .UseSwaggerGen();

        return app;
    }
    
    private static async ValueTask RunApplicationAsync(
        this WebApplication app, 
        CancellationToken cancellationToken)
    {
        await app.RunAsync(cancellationToken).ConfigureAwait(false);
    }
}