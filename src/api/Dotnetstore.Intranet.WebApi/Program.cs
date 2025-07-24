using Dotnetstore.Intranet.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
var cancellationToken = new CancellationTokenSource().Token;

await builder.StartApplicationAsync(cancellationToken).ConfigureAwait(false);

namespace Dotnetstore.Intranet.WebApi { public class Program; }