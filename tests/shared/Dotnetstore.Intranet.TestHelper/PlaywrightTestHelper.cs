using Dotnetstore.Intranet.SDK.Models;
using Microsoft.Playwright;

namespace Dotnetstore.Intranet.TestHelper;

public static class PlaywrightTestHelper
{
    public static async Task<(IPage Page, AppSettings Settings)> CreateTestPageWithSettingsAsync(bool headless = false, int slowMo = 3000)
    {
        var settings = Settings.GetSettings().Value;
        var page = await CreateTestPageAsync(headless, slowMo);
        await page.GotoAsync(settings.HttpClientBaseAddress);
        return (page, settings);
    }
    
    public static async Task<IPage> CreateTestPageAsync(bool headless = false, int slowMo = 3000)
    {
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = headless,
            SlowMo = slowMo
        });
        return await browser.NewPageAsync();
    }
}