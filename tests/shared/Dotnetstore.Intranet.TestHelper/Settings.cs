using Dotnetstore.Intranet.SDK.Models;
using Microsoft.Extensions.Options;

namespace Dotnetstore.Intranet.TestHelper;

public sealed class Settings
{
    public static IOptions<AppSettings> GetSettings()
    {
        var parameters = new AppSettings
        {
            HttpClientBaseAddress = "https://localhost:7012"
        };
        
        var options = Options.Create(parameters);
        return options;
    }
}