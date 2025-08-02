var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .AddDatabase("DotnetstoreIntranet");

var webApi = builder.AddProject<Projects.Dotnetstore_Intranet_WebApi>("webapi")
    .WithReference(postgres)
    .WaitFor(postgres);

var webUi = builder.AddProject<Projects.Dotnetstore_Intranet_Web>("webui")
    .WithReference(webApi)
    .WaitFor(webApi);

builder.Build().Run();