// using Dotnetstore.Intranet.Organization.Data;
// using Microsoft.Extensions.Hosting;
//
// namespace Dotnetstore.Intranet.AppHost.Tests.Extensions;
//
// internal static class Extensions
// {
//     public static async Task EnsureDbCreated(this IHost app)
//     {
//         using var serviceScope = app.Services.CreateScope();
//         var db = serviceScope.ServiceProvider.GetRequiredService<OrganizationDataContext>();
//         await db.Database.EnsureCreatedAsync();
//     }
// }