using Microsoft.Playwright;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.BlazorUi.Tests.CV;

public sealed class CreateTests
{
    [Fact]
    public async Task CV_Create_ShouldWriteCV()
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
        await page.GetByTestId("add_cv_button").ClickAsync();
        
        await page.GetByTestId("cv_name_textfield").FillAsync("Test CV");
        await page.GetByTestId("cv_language_textfield").FillAsync("Svenska");
        await page.GetByTestId("cv_lastname_textfield").FillAsync("Sjödin");
        await page.GetByTestId("cv_firstname_textfield").FillAsync("Hans");
        await page.GetByTestId("cv_dateofbirth_picker").FillAsync("1971-05-20");
        await page.GetByTestId("cv_introduction_textfield").FillAsync("Detta är ett test CV.");
        
        await page.GetByTestId("cv_save_button").ClickAsync();
        var textContent = await page.ContentAsync();
        
        // Assert
        textContent.ShouldContain("Test CV");
    }
}