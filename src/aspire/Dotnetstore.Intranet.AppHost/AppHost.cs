var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .AddDatabase("DotnetstoreIntranet");

var webApi = builder.AddProject<Projects.Dotnetstore_Intranet_WebApi>("webapi")
    .WithReference(postgres)
    .WaitFor(postgres);

builder.Build().Run();