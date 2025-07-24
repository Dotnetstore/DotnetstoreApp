using DotnetstoreApp.BlazorUi.Extensions;

var builder = WebApplication.CreateBuilder(args);
var cancellationToken = new CancellationTokenSource().Token;

await builder.BuildApplicationStartup(cancellationToken);