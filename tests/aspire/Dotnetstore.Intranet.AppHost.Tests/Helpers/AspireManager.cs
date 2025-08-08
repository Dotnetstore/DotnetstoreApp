using Aspire.Hosting;
using Dotnetstore.Intranet.Organization.Data;
using Microsoft.EntityFrameworkCore;

namespace Dotnetstore.Intranet.AppHost.Tests.Helpers;

public class AspireManager : IAsyncLifetime
{
    internal readonly PlaywrightManager PlaywrightManager = new();
    internal DistributedApplication? App { get; private set; }
    public OrganizationDataContext OrganizationDataContext { get; private set; } = null!;
    
    public async Task<DistributedApplication> ConfigureAsync<TEntryPoint>(
        string[]? args = null,
        Action<IDistributedApplicationBuilder>? configureBuilder = null) where TEntryPoint : class
    {
        if (App is not null) return App;

        var builder = await DistributedApplicationTestingBuilder.CreateAsync<TEntryPoint>(
            args: args ?? [],
            configureBuilder: static (options, _) =>
            {
                options.DisableDashboard = false;
            });

        builder.Configuration["ASPIRE_ALLOW_UNSECURED_TRANSPORT"] = "true";

        configureBuilder?.Invoke(builder);

        App = await builder.BuildAsync();

        await App.StartAsync();
        
        OrganizationDataContext = await GetDbContextAsync(builder);

        return App;
    }
    
    private static async Task<OrganizationDataContext> GetDbContextAsync(IDistributedApplicationTestingBuilder appHost)
    {
        var db = appHost.Resources.OfType<PostgresDatabaseResource>()
            .Single(r => r.Name == "DotnetstoreIntranet");

        var connectionString = await db.ConnectionStringExpression.GetValueAsync(CancellationToken.None);
        var options = new DbContextOptionsBuilder<OrganizationDataContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new OrganizationDataContext(options);
    }
    
    public async Task InitializeAsync()
    {
        await PlaywrightManager.InitializeAsync(); 
    }

    public async Task DisposeAsync()
    {
        await PlaywrightManager.DisposeAsync();

        await (App?.DisposeAsync() ?? ValueTask.CompletedTask);
    }
}