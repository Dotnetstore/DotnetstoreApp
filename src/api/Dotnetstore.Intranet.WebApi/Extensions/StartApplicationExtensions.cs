using Dotnetstore.Intranet.Organization.Data;
using Dotnetstore.Intranet.Organization.Extensions;
using Dotnetstore.Intranet.ServiceDefaults;
using Dotnetstore.Intranet.SharedKernel.Extensions;
using Dotnetstore.Intranet.SharedKernel.Models;
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
        RegisterServices(builder);
        var app = BuildApplication(builder);
        RegisterMiddlewares(app);
        await RunAsync(app, cancellationToken);
    }
    
    private static void RegisterServices(WebApplicationBuilder builder)
    {
        builder
            .AddServiceDefaults()
            .AddNpgsqlDbContext<OrganizationDataContext>(connectionName: "DotnetstoreIntranet");

        builder.Services
            .Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"))
            .RegisterSharedKernelServices()
            .RegisterOrganizationServices()
            .AddSingleton(TimeProvider.System)
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
    }
    
    private static WebApplication BuildApplication(WebApplicationBuilder builder)
    {
        return builder.Build();
    }
    
    private static void RegisterMiddlewares(WebApplication app)
    {
        app
            .UseAuthentication()
            .UseAuthorization()
            .UseHttpsRedirection()
            .UseFastEndpoints(c =>
            {
                c.Errors.UseProblemDetails();
            })
            .UseSwaggerGen();
    }
    
    private static async ValueTask RunAsync(
        WebApplication app, 
        CancellationToken cancellationToken)
    {
        await app.RunAsync(cancellationToken).ConfigureAwait(false);
    }
}