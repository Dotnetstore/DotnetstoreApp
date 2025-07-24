using DotnetstoreApp.Organization.Data;
using DotnetstoreApp.Organization.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetstoreApp.Organization.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrganizationServices(this IServiceCollection services, string connectionString)
    {
        // services
        // // services
        // //     .AddDbContext<OrganizationDataContext>(options =>
        // //         options.UseSqlite(connectionString));

        services
            .AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<OrganizationDataContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        return services;
    }
}