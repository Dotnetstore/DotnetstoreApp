using DotnetstoreApp.BlazorUi.Models;
using DotnetstoreApp.TestHelper;
using Microsoft.Playwright;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.BlazorUi.Tests.CV;

public sealed class FlowTests
{
    [Fact]
    public async Task Cv_Should_BeAbleToCreateAndView()
    {
        // Arrange
        var settings = Settings.GetSettings().Value;
        var page = await PlaywrightTestHelper.CreateTestPageAsync(slowMo:2000);
        
        // Act
        await Navigate_ToCvPageAsync(page, settings);
        await Create_CvAsync(page);
        await Edit_CvAsync(page);
        await Navigate_ToCvDetailPageAsync(page);
        await Create_Architecture_Information(page);
        await Create_Cloud_Information(page);
        await Create_DesiredRole_Information(page);
        await Create_Devops_Information(page);
        await Create_Language_Information(page);
        await Create_Leadership_Information(page);
        await Create_Programming_Information(page);
        await Create_Experience(page);
        await Update_Experience(page);
        await Create_Education(page);
        await Update_Education(page);
        await Delete_Architecture_Information(page);
        await Delete_Cloud_Information(page);
        await Delete_DesiredRole_Information(page);
        await Delete_Devops_Information(page);
        await Delete_Language_Information(page);
        await Delete_Leadership_Information(page);
        await Delete_Programming_Information(page);
        await Delete_Experience(page);
        await Delete_Education(page);
        
        // Assert
        var textContent = await page.ContentAsync();
        textContent.ShouldContain("Test CV");
    }
    
    private static async Task Navigate_ToCvPageAsync(IPage page, Parameters settings)
    {
        await page.GotoAsync(settings.BaseUrl);
        await page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "CV" }).ClickAsync();
    }
    
    private static async Task Create_CvAsync(IPage page)
    {
        await page.GetByTestId("add_cv_button").ClickAsync();
        
        await page.GetByTestId("cv_name_textfield").FillAsync("Test CV");
        await page.GetByTestId("cv_language_textfield").FillAsync("Svenska");
        await page.GetByTestId("cv_lastname_textfield").FillAsync("Sjödin");
        await page.GetByTestId("cv_firstname_textfield").FillAsync("Hans");
        await page.GetByTestId("cv_dateofbirth_picker").FillAsync("1971-05-20");
        await page.GetByTestId("cv_introduction_textfield").FillAsync("Detta är ett test CV.");
        
        await page.GetByTestId("cv_save_button").ClickAsync();
    }

    private static async Task Edit_CvAsync(IPage page)
    {
        var button = page.Locator("table button[data-id=\"Test CV\"][data-testid=\"cv_edit_button\"]");
        await button.ClickAsync();
        
        await page.GetByTestId("cv_name_textfield").FillAsync("Test CV");
        await page.GetByTestId("cv_language_textfield").FillAsync("Svenska");
        await page.GetByTestId("cv_lastname_textfield").FillAsync("Sjödin");
        await page.GetByTestId("cv_firstname_textfield").FillAsync("Freja");
        await page.GetByTestId("cv_dateofbirth_picker").FillAsync("1971-05-20");
        await page.GetByTestId("cv_introduction_textfield").FillAsync("Detta är ett test CV.");
        
        await page.GetByTestId("cv_save_button").ClickAsync();
    }
    
    private static async Task Navigate_ToCvDetailPageAsync(IPage page)
    {
        var button = page.Locator("table button[data-id=\"Test CV\"][data-testid=\"view_cv_button\"]");
        await button.ClickAsync();
        await page.Mouse.WheelAsync(0, 400);
    }

    private static async Task Create_Architecture_Information(IPage page)
    {
        var button = page.Locator("button[data-testid=\"add_information_button\"][data-id=\"Architecture\"]");
        await button.ClickAsync();
        
        await page.GetByTestId("information_name_textfield").FillAsync("Clean Architecture");
        
        await page.GetByTestId("save_information_button").ClickAsync();
        await page.Mouse.WheelAsync(0, 500);
    }

    private static async Task Create_Cloud_Information(IPage page)
    {
        var button = page.Locator("button[data-testid=\"add_information_button\"][data-id=\"Cloud\"]");
        await button.ClickAsync();
        
        await page.GetByTestId("information_name_textfield").FillAsync("AWS");
        
        await page.GetByTestId("save_information_button").ClickAsync();
        await page.Mouse.WheelAsync(0, 750);
    }

    private static async Task Create_DesiredRole_Information(IPage page)
    {
        var button = page.Locator("button[data-testid=\"add_information_button\"][data-id=\"DesiredRole\"]");
        await button.ClickAsync();
        
        await page.GetByTestId("information_name_textfield").FillAsync("Architect");
        
        await page.GetByTestId("save_information_button").ClickAsync();
        await page.Mouse.WheelAsync(0, 1000);
    }

    private static async Task Create_Devops_Information(IPage page)
    {
        var button = page.Locator("button[data-testid=\"add_information_button\"][data-id=\"Devops\"]");
        await button.ClickAsync();
        
        await page.GetByTestId("information_name_textfield").FillAsync("Azure DevOps");
        
        await page.GetByTestId("save_information_button").ClickAsync();
        await page.Mouse.WheelAsync(0, 1250);
    }

    private static async Task Create_Language_Information(IPage page)
    {
        var button = page.Locator("button[data-testid=\"add_information_button\"][data-id=\"Language\"]");
        await button.ClickAsync();
        
        await page.GetByTestId("information_name_textfield").FillAsync("English");
        
        await page.GetByTestId("save_information_button").ClickAsync();
        await page.Mouse.WheelAsync(0, 1500);
    }

    private static async Task Create_Leadership_Information(IPage page)
    {
        var button = page.Locator("button[data-testid=\"add_information_button\"][data-id=\"Leadership\"]");
        await button.ClickAsync();
        
        await page.GetByTestId("information_name_textfield").FillAsync("Tech lead");
        
        await page.GetByTestId("save_information_button").ClickAsync();
        await page.Mouse.WheelAsync(0, 1750);
    }

    private static async Task Create_Programming_Information(IPage page)
    {
        var button = page.Locator("button[data-testid=\"add_information_button\"][data-id=\"Programming\"]");
        await button.ClickAsync();
        
        await page.GetByTestId("information_name_textfield").FillAsync("C#");
        
        await page.GetByTestId("save_information_button").ClickAsync();
        await page.Mouse.WheelAsync(0, 2000);
    }

    private static async Task Create_Experience(IPage page)
    {
        var button = page.Locator("button[data-testid=\"add_experience_button\"]");
        await button.ClickAsync();
        
        await page.GetByTestId("startdate_textfield").FillAsync("2020-01-01");
        await page.GetByTestId("enddate_textfield").FillAsync("2023-01-01");
        await page.GetByTestId("company_textfield").FillAsync("Test Company");
        await page.GetByTestId("isconsultant_checkbox").SetCheckedAsync(true);
        await page.GetByTestId("role_textfield").FillAsync("Software Engineer");
        await page.GetByTestId("extent_textfield").FillAsync("100");
        await page.GetByTestId("tools_textfield").FillAsync("C#, Azure DevOps");
        await page.GetByTestId("companyneeds_textfield").FillAsync("Need to improve CI/CD pipeline");
        await page.Mouse.WheelAsync(0, 400);
        await page.GetByTestId("mission_textfield").FillAsync("Worked on improving the CI/CD pipeline and developing new features.");
        
        await page.GetByTestId("save_experience_button").ClickAsync();
        await page.Mouse.WheelAsync(0, 2250);
    }

    private static async Task Update_Experience(IPage page)
    {
        var button = page.Locator("button[data-testid=\"update_experience_button\"][data-id=\"Test Company\"]");
        await button.ClickAsync();
        
        await page.GetByTestId("startdate_textfield").FillAsync("2020-01-01");
        await page.GetByTestId("enddate_textfield").FillAsync("2023-01-01");
        await page.GetByTestId("company_textfield").FillAsync("Test Company");
        await page.GetByTestId("isconsultant_checkbox").SetCheckedAsync(true);
        await page.GetByTestId("role_textfield").FillAsync("Architect");
        await page.GetByTestId("extent_textfield").FillAsync("100");
        await page.GetByTestId("tools_textfield").FillAsync("C#, Azure DevOps");
        await page.GetByTestId("companyneeds_textfield").FillAsync("Need to improve CI/CD pipeline");
        await page.Mouse.WheelAsync(0, 400);
        await page.GetByTestId("mission_textfield").FillAsync("Worked on improving the CI/CD pipeline and developing new features.");
        
        await page.GetByTestId("save_experience_button").ClickAsync();
        await page.Mouse.WheelAsync(0, 2800);
    }

    private static async Task Create_Education(IPage page)
    {
        var button = page.Locator("button[data-testid=\"add_education_button\"]");
        await button.ClickAsync();
        
        await page.GetByTestId("startdate_textfield").FillAsync("2020-01-01");
        await page.GetByTestId("enddate_textfield").FillAsync("2023-01-01");
        await page.GetByTestId("school_textfield").FillAsync("Lund");
        await page.GetByTestId("level_textfield").FillAsync("High School");
        
        await page.GetByTestId("save_education_button").ClickAsync();
        await page.Mouse.WheelAsync(0, 2800);
    }

    private static async Task Update_Education(IPage page)
    {
        var button = page.Locator("button[data-testid=\"update_education_button\"][data-id=\"Lund\"]");
        await button.ClickAsync();
        
        await page.GetByTestId("startdate_textfield").FillAsync("2020-01-01");
        await page.GetByTestId("enddate_textfield").FillAsync("2023-01-01");
        await page.GetByTestId("school_textfield").FillAsync("Lund");
        await page.GetByTestId("level_textfield").FillAsync("University");
        
        await page.GetByTestId("save_education_button").ClickAsync();
        await page.Mouse.WheelAsync(0, 2800);
    }

    private static async Task Delete_Architecture_Information(IPage page)
    {
        await page.Mouse.WheelAsync(0, -3500);
        await page.Mouse.WheelAsync(0, 500);
        var button = page.Locator("button[data-testid=\"delete_information_button\"][data-id=\"Architecture\"]");
        await button.ClickAsync();
        await page.Mouse.WheelAsync(0, 700);
    }

    private static async Task Delete_Cloud_Information(IPage page)
    {
        var button = page.Locator("button[data-testid=\"delete_information_button\"][data-id=\"Cloud\"]");
        await button.ClickAsync();
        await page.Mouse.WheelAsync(0, 900);
    }

    private static async Task Delete_DesiredRole_Information(IPage page)
    {
        var button = page.Locator("button[data-testid=\"delete_information_button\"][data-id=\"DesiredRole\"]");
        await button.ClickAsync();
        await page.Mouse.WheelAsync(0, 1100);
    }

    private static async Task Delete_Devops_Information(IPage page)
    {
        var button = page.Locator("button[data-testid=\"delete_information_button\"][data-id=\"Devops\"]");
        await button.ClickAsync();
        await page.Mouse.WheelAsync(0, 1300);
    }

    private static async Task Delete_Language_Information(IPage page)
    {
        var button = page.Locator("button[data-testid=\"delete_information_button\"][data-id=\"Language\"]");
        await button.ClickAsync();
        await page.Mouse.WheelAsync(0, 1500);
    }

    private static async Task Delete_Leadership_Information(IPage page)
    {
        var button = page.Locator("button[data-testid=\"delete_information_button\"][data-id=\"Leadership\"]");
        await button.ClickAsync();
        await page.Mouse.WheelAsync(0, 1700);
    }

    private static async Task Delete_Programming_Information(IPage page)
    {
        var button = page.Locator("button[data-testid=\"delete_information_button\"][data-id=\"Programming\"]");
        await button.ClickAsync();
        await page.Mouse.WheelAsync(0, 1900);
    }

    private static async Task Delete_Experience(IPage page)
    {
        var button = page.Locator("button[data-testid=\"delete_experience_button\"][data-id=\"Test Company\"]");
        await button.ClickAsync();
        await page.Mouse.WheelAsync(0, 2100);
    }

    private static async Task Delete_Education(IPage page)
    {
        var button = page.Locator("button[data-testid=\"delete_education_button\"][data-id=\"Lund\"]");
        await button.ClickAsync();
        await page.Mouse.WheelAsync(0, 2300);
    }
}