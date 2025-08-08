using System.Diagnostics;
using Dotnetstore.Intranet.Organization.Data;
using Dotnetstore.Intranet.Organization.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dotnetstore.Intranet.MigrationService;

internal sealed class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    private const string MigratingDatabaseMessage = "Migrating database...";
    private const string ActivitySourceName = "Dotnetstore.Intranet.Services.MigrationService";
    private static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = ActivitySource.StartActivity(MigratingDatabaseMessage, ActivityKind.Client);

        try
        {
            await SetupOrganizationDatabase(activity, stoppingToken);
        }
        catch (Exception ex)
        {
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private async Task SetupOrganizationDatabase(Activity? activity, CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OrganizationDataContext>();

        await EnsureDatabaseAsync(dbContext, stoppingToken);
        await RunMigrationAsync(dbContext, stoppingToken);
        await SeedDataAsync(dbContext, stoppingToken);

        activity?.SetStatus(ActivityStatusCode.Ok);
    }

    private static async ValueTask EnsureDatabaseAsync(
        OrganizationDataContext dbContext,
        CancellationToken stoppingToken)
    {
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();
        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            if (!await dbCreator.ExistsAsync(stoppingToken))
            {
                await dbCreator.CreateAsync(stoppingToken);
            }
        });
    }

    private static async ValueTask RunMigrationAsync(
        OrganizationDataContext dbContext,
        CancellationToken stoppingToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        Console.WriteLine($"Connectionstring: {dbContext.Database.GetConnectionString()}");

        await strategy.ExecuteAsync(async () =>
        {
            // await using var transaction = await dbContext.Database.BeginTransactionAsync(stoppingToken);
            await dbContext.Database.MigrateAsync(stoppingToken);
            // await transaction.CommitAsync(stoppingToken);
        });
    }

    private static async ValueTask SeedDataAsync(
        OrganizationDataContext dbContext,
        CancellationToken stoppingToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            // await using var transaction = await dbContext.Database.BeginTransactionAsync(stoppingToken);

            await SeedOrganizationAsync(dbContext, stoppingToken);

            await dbContext.SaveChangesAsync(stoppingToken);
            // await transaction.CommitAsync(stoppingToken);
        });
    }

    private static async ValueTask SeedOrganizationAsync(OrganizationDataContext dbContext,
        CancellationToken stoppingToken)
    {
        var administratorRole = await dbContext.ApplicationUserRoles
            .FirstOrDefaultAsync(r => r.Name == "Administrator", stoppingToken);
        var userRole = await dbContext.ApplicationUserRoles
            .FirstOrDefaultAsync(r => r.Name == "User", stoppingToken);

        if (administratorRole is null)
        {
            dbContext.ApplicationUserRoles.Add(ApplicationUserRole.Create(
                "Administrator",
                "Administrators have full access to the system.",
                DateTime.UtcNow,
                isSystem: true));
        }

        if (userRole is null)
        {
            dbContext.ApplicationUserRoles.Add(ApplicationUserRole.Create(
                "User",
                "Users have limited access to the system.",
                DateTime.UtcNow,
                isSystem: true));
        }
    }
}