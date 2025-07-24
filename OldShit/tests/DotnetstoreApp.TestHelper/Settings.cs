using DotnetstoreApp.BlazorUi.Models;
using Microsoft.Extensions.Options;

namespace DotnetstoreApp.TestHelper;

public sealed class Settings
{
    public static IOptions<Parameters> GetSettings()
    {
        var parameters = new Parameters
        {
            BaseUrl = "https://localhost:7078"
        };
        
        var options = Options.Create(parameters);
        return options;
    }
}