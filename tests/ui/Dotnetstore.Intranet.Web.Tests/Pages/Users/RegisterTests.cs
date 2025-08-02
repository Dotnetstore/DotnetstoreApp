using Dotnetstore.Intranet.SDK.Models;
using Dotnetstore.Intranet.TestHelper;
using Microsoft.Playwright;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Web.Tests.Pages.Users;

public class RegisterTests
{
    [Fact]
    public async Task RegisterUser_Should_BeAbleToCreateUser()
    {
        // Arrange
        var settings = Settings.GetSettings().Value;
        var page = await PlaywrightTestHelper.CreateTestPageAsync(slowMo:2000);
        
        // Act
        await Navigate_ToRegisterPageAsync(page, settings);
        await FillInRegisterFormAsync(page);
    }

    [Fact]
    public async Task RegisterUser_InvalidSocialSecurityNumber_Should_DisplayCorrectErrorMessage()
    {
        // Arrange
        var settings = Settings.GetSettings().Value;
        var page = await PlaywrightTestHelper.CreateTestPageAsync(slowMo:2000);
        
        // Act
        await Navigate_ToRegisterPageAsync(page, settings);
        await FillInRegisterFormAsync(page, socialSecurityNumber: "1234567890");
        
        // Assert
        var errorMessage = await page.GetByTestId("validation_summary_user_register").InnerTextAsync();
        errorMessage.ShouldBe("Social Security Number must be a valid format.");
    }
    
    [Fact]
    public async Task RegisterUser_PasswordMismatch_Should_DisplayError()
    {
        // Arrange
        var settings = Settings.GetSettings().Value;
        var page = await PlaywrightTestHelper.CreateTestPageAsync(slowMo:2000);
        
        // Act
        await Navigate_ToRegisterPageAsync(page, settings);
        await FillInRegisterFormAsync(page, password: "Password123!", confirmPassword: "DifferentPassword123!");
        
        // Assert
        var errorMessage = await page.GetByTestId("validation_summary_user_register").InnerTextAsync();
        errorMessage.ShouldBe("Confirm password must match the password.");
    }
    
    [Fact]
    public async Task RegisterUser_TooOldDateOfBirth_Should_DisplayError()
    {
        // Arrange
        var settings = Settings.GetSettings().Value;
        var page = await PlaywrightTestHelper.CreateTestPageAsync(slowMo:2000);
        
        // Act
        await Navigate_ToRegisterPageAsync(page, settings);
        await FillInRegisterFormAsync(page, dateOfBirth: new DateTime(1800, 1, 1));
        
        // Assert
        var errorMessage = await page.GetByTestId("validation_summary_user_register").InnerTextAsync();
        errorMessage.ShouldBe("User must be at most 70 years old.");
    }
    
    [Fact]
    public async Task RegisterUser_TooYoungDateOfBirth_Should_DisplayError()
    {
        // Arrange
        var settings = Settings.GetSettings().Value;
        var page = await PlaywrightTestHelper.CreateTestPageAsync(slowMo:2000);
        
        // Act
        await Navigate_ToRegisterPageAsync(page, settings);
        await FillInRegisterFormAsync(page, dateOfBirth: DateTime.UtcNow.AddYears(1));
        
        // Assert
        var errorMessage = await page.GetByTestId("validation_summary_user_register").InnerTextAsync();
        errorMessage.ShouldBe("User must be at least 15 years old.");
    }
    
    [Fact]
    public async Task RegisterUser_EmailAddress_Should_DisplayCorrectErrorMessage()
    {
        // Arrange
        var settings = Settings.GetSettings().Value;
        var page = await PlaywrightTestHelper.CreateTestPageAsync(slowMo:2000);
        
        // Act
        await Navigate_ToRegisterPageAsync(page, settings);
        await FillInRegisterFormAsync(page, emailAddress: "invalid-email");
        
        // Assert
        var errorMessage = await page.GetByTestId("validation_summary_user_register").InnerTextAsync();
        errorMessage.ShouldBe("Email address must be a valid email format.");
    }
    
    [Fact]
    public async Task RegisterUser_LoginButton_Should_RedirectToLoginPage()
    {
        // Arrange
        var settings = Settings.GetSettings().Value;
        var page = await PlaywrightTestHelper.CreateTestPageAsync(slowMo:2000);
        
        // Act
        await Navigate_ToRegisterPageAsync(page, settings);
        await page.GetByTestId("link_user_register_login").ClickAsync();
        
        // Assert
        var titleText = await page.GetByTestId("title_login").InnerTextAsync();
        titleText.ShouldBe("Login");
    }
    
    [Fact]
    public async Task RegisterUser_BackToStartPage_Should_RedirectToStartPage()
    {
        // Arrange
        var settings = Settings.GetSettings().Value;
        var page = await PlaywrightTestHelper.CreateTestPageAsync(slowMo:2000);
        
        // Act
        await Navigate_ToRegisterPageAsync(page, settings);
        await page.GetByTestId("link_user_register_index").ClickAsync();
        
        // Assert
        var titleText = await page.GetByTestId("title_index").InnerTextAsync();
        titleText.ShouldBe("Welcome");
    }
    
    private static async Task Navigate_ToRegisterPageAsync(IPage page, AppSettings settings)
    {
        await page.GotoAsync(settings.HttpClientBaseAddress);
        await page.GetByTestId("link_layout_register_account").ClickAsync();
    }
    
    private static async Task FillInRegisterFormAsync(
        IPage page,
        DateTime? dateOfBirth = null,
        string? socialSecurityNumber = null,
        string? emailAddress = "test@test.com",
        string? password = "Password123!",
        string? confirmPassword = "Password123!")
    {
        await page.GetByTestId("input_user_register_lastname").FillAsync("Doe");
        await page.GetByTestId("input_user_register_firstname").FillAsync("John");
        await page.GetByTestId("input_user_register_middlename").FillAsync("Middle");
        await page.GetByTestId("input_user_register_dateofbirth").FillAsync(new DateTime(1990, 1, 1).ToShortDateString());
        if (dateOfBirth.HasValue)
            await page.GetByTestId("input_user_register_dateofbirth").FillAsync(dateOfBirth.Value.ToShortDateString());
        await page.GetByTestId("checkbox_user_register_ismale").CheckAsync();

        if (!string.IsNullOrEmpty(socialSecurityNumber))
            await page.GetByTestId("input_user_register_socialsecuritynumber").FillAsync(socialSecurityNumber);
        if (!string.IsNullOrEmpty(emailAddress))
            await page.GetByTestId("input_user_register_emailaddress").FillAsync(emailAddress);
        if (!string.IsNullOrEmpty(password))
            await page.GetByTestId("input_user_register_password").FillAsync(password);
        if (!string.IsNullOrEmpty(confirmPassword))
            await page.GetByTestId("input_user_register_confirmpassword").FillAsync(confirmPassword);
        await page.GetByTestId("submit_user_register_save").ClickAsync();
    }
}