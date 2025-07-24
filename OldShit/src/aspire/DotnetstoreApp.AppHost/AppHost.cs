var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin(pgadmin => pgadmin.WithHostPort(5050))
    .WithPgWeb()
    .AddDatabase("DotnetstoreAppDb");

var webui = builder.AddProject<Projects.DotnetstoreApp_WebApp>("webui")
    .WithReference(postgres)
    .WaitFor(postgres);

var migrationService = builder.AddProject<Projects.DotnetstoreApp_MigrationService>("migrationService")
    .WithReference(postgres)
    .WaitFor(postgres)
    .WithReference(webui)
    .WaitFor(webui);

builder.Build().Run();