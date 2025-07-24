using Microsoft.Playwright;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.BlazorUi.Tests.CV;

public sealed class ListTests
{
    [Fact]
    public async Task CV_H3_ShouldWriteCV()
    {
        // Arrange
        using var playwright = await Playwright.CreateAsync();
        var chromium = playwright.Chromium;
        var browser = await chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
        {
            Headless = false, 
            SlowMo = 5000
        });
        var page = await browser.NewPageAsync();
        
        // Act
        await page.GotoAsync("https://localhost:7078");
        await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "CV" }).ClickAsync();
        var textContent = await page.GetByTestId("cv_header").InnerTextAsync();
        
        // Assert
        textContent.ShouldBe("CV");
    }
    
    [Fact]
    public async Task CV_Description_ShouldWriteCorrectContent()
    {
        // Arrange
        using var playwright = await Playwright.CreateAsync();
        var chromium = playwright.Chromium;
        var browser = await chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = false, 
                SlowMo = 5000
            });
        var page = await browser.NewPageAsync();
        
        // Act
        await page.GotoAsync("https://localhost:7078");
        await page.GetByRole(AriaRole.Link, new() { Name = "CV" }).ClickAsync();
        var textContent = await page.GetByTestId("cv_description").InnerTextAsync();
        
        // Assert
        textContent.ShouldBe("Här är en lista med alla dina CV.");
    }
}