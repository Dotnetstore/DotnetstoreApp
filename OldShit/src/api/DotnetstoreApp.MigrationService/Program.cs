using DotnetstoreApp.MigrationService;
using DotnetstoreApp.Organization.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

builder
    .AddServiceDefaults()
    .AddNpgsqlDbContext<OrganizationDataContext>(connectionName: "DotnetstoreAppDb");

// builder.Services
//     .AddDbContext<OrganizationDataContext>(options =>
//         options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ??
//                             throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")))
//     .AddSqlServerDbContext<OrganizationDataContext>("dotnetstoreintranet");
//
var app = builder.Build();

await app.RunAsync();