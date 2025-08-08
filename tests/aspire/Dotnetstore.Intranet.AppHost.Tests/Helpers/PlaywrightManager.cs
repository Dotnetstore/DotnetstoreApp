using System.Diagnostics;
using Microsoft.Playwright;

namespace Dotnetstore.Intranet.AppHost.Tests.Helpers;

public class PlaywrightManager : IAsyncLifetime
{
    private static bool _isDebugging = Debugger.IsAttached;
    private const bool IsHeadless = false;
    private const int SlowMo = 2000;

    private IPlaywright? _playwright;
    
    internal IBrowser Browser { get; private set; } = null!;
    
    public async Task InitializeAsync()
    {
        Assertions.SetDefaultExpectTimeout(10_000);
        _playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        var options = new BrowserTypeLaunchOptions
        {
            Headless = IsHeadless,
            SlowMo = SlowMo
        };
        
        Browser = await _playwright.Chromium.LaunchAsync(options).ConfigureAwait(false);
    }

    public async Task DisposeAsync()
    {
        await Browser.CloseAsync();

        _playwright?.Dispose();
    }
}