// using Dotnetstore.Intranet.MigrationService;
// using Dotnetstore.Intranet.Organization.Data;
// using Dotnetstore.Intranet.ServiceDefaults;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
//
// var builder = Host.CreateApplicationBuilder(args);
//
// builder.Services.AddHostedService<Worker>();
//
// builder
//     .AddServiceDefaults();
//
// builder
//     .AddNpgsqlDbContext<OrganizationDataContext>("DotnetstoreIntranet");
//
// var app = builder.Build();
//
// await app.RunAsync();

Console.WriteLine("Test migration service is running...");