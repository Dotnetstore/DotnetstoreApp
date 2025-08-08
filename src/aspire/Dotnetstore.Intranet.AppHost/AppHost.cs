var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("postgres")
    .WithPgAdmin()
    .PublishAsConnectionString()
    .AddDatabase("DotnetstoreIntranet");

var webApi = builder.AddProject<Projects.Dotnetstore_Intranet_WebApi>("webapi")
    .WithReference(postgres)
    .WaitFor(postgres);

var webUi = builder.AddProject<Projects.Dotnetstore_Intranet_Web>("webui")
    .WithReference(webApi)
    .WaitFor(webApi);

var emailService = builder.AddProject<Projects.Dotnetstore_Intranet_EmailService>("emailservice")
    .WithReference(webApi)
    .WaitFor(webApi);

var migrations = builder.AddProject<Projects.Dotnetstore_Intranet_MigrationService>("migrations")
    .WithReference(postgres)
    .WithReference(webApi)
    .WaitFor(emailService);

builder.Build().Run();