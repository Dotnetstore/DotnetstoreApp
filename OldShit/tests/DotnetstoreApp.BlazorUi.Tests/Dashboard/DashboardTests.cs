using Microsoft.Playwright;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.BlazorUi.Tests.Dashboard;

public sealed class DashboardTests
{
    [Fact]
    public async Task Menu_Should_Contain4Links()
    {
        using var playwright = await Playwright.CreateAsync();
        var chromium = playwright.Chromium;
        var browser = await chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = false, 
                SlowMo = 3000
            });
        var page = await browser.NewPageAsync();
        await page.GotoAsync("https://localhost:7078");
        var linkCount = await page.GetByTestId("nav-link").CountAsync();
        
        // Assert
        linkCount.ShouldBe(4, "The navigation menu should contain 4 links.");
    }
}