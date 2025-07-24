using Microsoft.Playwright;

namespace DotnetstoreApp.TestHelper;

public static class PlaywrightTestHelper
{
    public static async Task<IPage> CreateTestPageAsync(bool headless = false, int slowMo = 5000)
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