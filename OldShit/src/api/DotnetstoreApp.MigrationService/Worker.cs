using DotnetstoreApp.Organization.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotnetstoreApp.MigrationService;

internal sealed class Worker(
    IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {;
        await SetupOrganizationAsync(stoppingToken);
    }

    private async ValueTask SetupOrganizationAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OrganizationDataContext>();
        
        await EnsureDatabaseDeletedAsync(dbContext, stoppingToken);
        await MigrateOrganization(dbContext, stoppingToken);
        await SeedOrganizationAsync(dbContext, stoppingToken);
        
        await dbContext.SaveChangesAsync(stoppingToken);
    }
    
    private static async ValueTask EnsureDatabaseDeletedAsync(OrganizationDataContext dbContext, CancellationToken stoppingToken)
    {
        await dbContext.Database.EnsureDeletedAsync(stoppingToken);
    }

    private static async ValueTask MigrateOrganization(OrganizationDataContext dbContext, CancellationToken stoppingToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () => await dbContext.Database.MigrateAsync(stoppingToken));
    }
    
    private static async ValueTask SeedOrganizationAsync(OrganizationDataContext dbContext, CancellationToken stoppingToken)
    {
        await dbContext.Roles.AddAsync(new IdentityRole<Guid>("Administrator"), stoppingToken);
    }

    // private static async ValueTask EnsureDatabaseAsync(OrganizationDataContext dbContext, CancellationToken stoppingToken)
    // {
    //     var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();
    //     var strategy = dbContext.Database.CreateExecutionStrategy();
    //     
    //     await strategy.ExecuteAsync(async () =>
    //     {
    //         if (!await dbCreator.ExistsAsync(stoppingToken))
    //         {
    //             await dbCreator.CreateAsync(stoppingToken);
    //         }
    //     });
    // }
    //
    // private static async ValueTask RunMigrationAsync(OrganizationDataContext dbContext, CancellationToken stoppingToken)
    // {
    //     try
    //     {
    //         var strategy = dbContext.Database.CreateExecutionStrategy();
    //     
    //         await strategy.ExecuteAsync(async () =>
    //         {
    //             await using var transaction = await dbContext.Database.BeginTransactionAsync(stoppingToken);
    //             await dbContext.Database.MigrateAsync(stoppingToken);
    //             await transaction.CommitAsync(stoppingToken);
    //         });
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //     }
    // }
    //
    // private static async ValueTask SeedDataAsync(OrganizationDataContext dbContext, CancellationToken stoppingToken)
    // {
    //     var strategy = dbContext.Database.CreateExecutionStrategy();
    //     
    //     await strategy.ExecuteAsync(async () =>
    //     {
    //         await using var transaction = await dbContext.Database.BeginTransactionAsync(stoppingToken);
    //
    //         await SeedOrganizationAsync(dbContext, stoppingToken);
    //         await dbContext.SaveChangesAsync(stoppingToken);
    //         await transaction.CommitAsync(stoppingToken);
    //     });
    // }
    //
    // private static async ValueTask SeedOrganizationAsync(OrganizationDataContext dbContext, CancellationToken stoppingToken)
    // {
    //     await dbContext.Roles.AddAsync(
    //         new IdentityRole<Guid>("Administrator"), stoppingToken);
    // }
}