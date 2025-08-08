using Dotnetstore.Intranet.AppHost.Tests.Helpers;
using Shouldly;

namespace Dotnetstore.Intranet.AppHost.Tests.Playwright.Organization.Users;

public class RegisterTests(AspireManager aspireManager) : BasePlaywrightTests(aspireManager)
{
    [Fact]
    public async Task RegisterPage_Should_LoadSuccessfully()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();

        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToRegisterPageAsync();
            
            var titleText = await page.GetByTestId("title_user_register").InnerTextAsync();
            titleText.ShouldBe("Registrera");
        });
    }

    [Fact]
    public async Task RegisterUser_Should_BeAbleToCreateUser()
    {
        // Arrange
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();

        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToRegisterPageAsync();
            await page.FillInRegisterFormAsync();
        });
    }
    
    [Fact]
    public async Task RegisterUser_InvalidSocialSecurityNumber_Should_DisplayCorrectErrorMessage()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();
        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToRegisterPageAsync();
            await page.FillInRegisterFormAsync(socialSecurityNumber: "1234567890");

            // Assert
            var errorMessage = await page.GetByTestId("validation_summary_user_register").InnerTextAsync();
            errorMessage.ShouldBe("Social Security Number must be a valid format.");
        });
    }
    
    [Fact]
    public async Task RegisterUser_PasswordMismatch_Should_DisplayError()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();
        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToRegisterPageAsync();
            await page.FillInRegisterFormAsync(password: "Password123!", confirmPassword: "DifferentPassword123!");
            
            // Assert
            var errorMessage = await page.GetByTestId("validation_summary_user_register").InnerTextAsync();
            errorMessage.ShouldBe("Confirm password must match the password.");
        });
    }
    
    [Fact]
    public async Task RegisterUser_TooOldDateOfBirth_Should_DisplayError()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();
        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToRegisterPageAsync();
            await page.FillInRegisterFormAsync(dateOfBirth: DateTime.UtcNow.AddYears(-100));
            
            // Assert
            var errorMessage = await page.GetByTestId("validation_summary_user_register").InnerTextAsync();
            errorMessage.ShouldBe("User must be at most 70 years old.");
        });
    }
    
    [Fact]
    public async Task RegisterUser_TooYoungDateOfBirth_Should_DisplayError()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();
        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToRegisterPageAsync();
            await page.FillInRegisterFormAsync(dateOfBirth: DateTime.UtcNow.AddYears(1));
            
            // Assert
            var errorMessage = await page.GetByTestId("validation_summary_user_register").InnerTextAsync();
            errorMessage.ShouldBe("User must be at least 15 years old.");
        });
    }
    
    [Fact]
    public async Task RegisterUser_EmailAddress_Should_DisplayCorrectErrorMessage()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();
        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToRegisterPageAsync();
            await page.FillInRegisterFormAsync(emailAddress: "invalid-email");
            
            // Assert
            var errorMessage = await page.GetByTestId("validation_summary_user_register").InnerTextAsync();
            errorMessage.ShouldBe("Email address must be a valid email format.");
        });
    }
    
    [Fact]
    public async Task RegisterUser_LoginButton_Should_RedirectToLoginPage()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();
        
        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToRegisterPageAsync();
            await page.GetByTestId("link_user_register_login").ClickAsync();
            
            // Assert
            var titleText = await page.GetByTestId("title_login").InnerTextAsync();
            titleText.ShouldBe("Logga in");
        });
    }
    
    [Fact]
    public async Task RegisterUser_BackToStartPage_Should_RedirectToStartPage()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();
        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToRegisterPageAsync();
            await page.GetByTestId("link_user_register_index").ClickAsync();
            
            // Assert
            var titleText = await page.GetByTestId("title_index").InnerTextAsync();
            titleText.ShouldNotBeEmpty();
        });
    }
}