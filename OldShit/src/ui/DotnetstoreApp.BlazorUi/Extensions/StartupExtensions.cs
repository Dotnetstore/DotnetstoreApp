using DotnetstoreApp.BlazorUi.Components;
using DotnetstoreApp.CV.Extensions;
using MudBlazor.Services;

namespace DotnetstoreApp.BlazorUi.Extensions;

internal static class StartupExtensions
{
    internal static async ValueTask BuildApplicationStartup(this WebApplicationBuilder builder, CancellationToken cancellationToken = default)
    {
        AddServices(builder);

        var app = builder.Build();

        RegisterMiddlewares(app);

        await RunApplicationAsync(app, cancellationToken).ConfigureAwait(false);
    }
    
    private static void AddServices(WebApplicationBuilder builder)
    {
        builder.Services
            .AddCv()
            .AddAuthenticationServices()
            .AddMudServices()
            .AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services
            .AddControllers();
    }
    
    private static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        return services;
    }
    
    private static void RegisterMiddlewares(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app
                .UseExceptionHandler("/Error", createScopeForErrors: true)
                .UseHsts();
        }

        app
            .UseHttpsRedirection()
            .UseAntiforgery();

        app.MapStaticAssets();
        
        app
            .MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
    }
    
    private static async ValueTask RunApplicationAsync(this WebApplication app, CancellationToken cancellationToken)
    {
        await app.RunAsync(cancellationToken);
    }
}