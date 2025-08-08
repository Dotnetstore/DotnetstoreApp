using Dotnetstore.Intranet.AppHost.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Dotnetstore.Intranet.AppHost.Tests.Playwright.Organization.Users;

public class LoginTests(AspireManager aspireManager) : BasePlaywrightTests(aspireManager)
{
    [Fact]
    public async Task LoginPage_Should_LoadSuccessfully()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();

        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToLoginPageAsync();

            var titleText = await page.GetByTestId("title_login").InnerTextAsync();
            titleText.ShouldBe("Logga in");
        });
    }
    
    [Fact]
    public async Task LoginPage_EmptyUsername_Should_DisplayCorrectErrorMessage()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();

        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToLoginPageAsync();
            await page.FillInLoginFormAsync(email: "");

            var errorMessage = await page.GetByTestId("input_user_login_username_validation").InnerTextAsync();
            errorMessage.ShouldNotBeEmpty();
        });
    }

    [Fact]
    public async Task LoginPage_BadUsername_Should_DisplayCorrectErrorMessage()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();

        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToLoginPageAsync();
            await page.FillInLoginFormAsync(email: "test");

            var errorMessage = await page.GetByTestId("input_user_login_username_validation").InnerTextAsync();
            errorMessage.ShouldNotBeEmpty();
        });
    }

    [Fact]
    public async Task LoginPage_EmptyPassword_Should_DisplayCorrectErrorMessage()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();

        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToLoginPageAsync();
            await page.FillInLoginFormAsync(password: "");

            var errorMessage = await page.GetByTestId("input_user_login_password_validation").InnerTextAsync();
            errorMessage.ShouldNotBeEmpty();
        });
    }

    [Fact]
    public async Task LoginPage_WrongCredentials_Should_DisplayCorrectErrorMessage()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();

        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToLoginPageAsync();
            await page.FillInLoginFormAsync();

            var errorMessage = await page.GetByTestId("validation_summary_user_login").InnerTextAsync();
            errorMessage.ShouldNotBeEmpty();
        });
    }

    [Fact]
    public async Task LoginPage_BackToStartPage_Should_RedirectToStartPage()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();

        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToLoginPageAsync();
            await page.GetByTestId("link_user_back_to_index").ClickAsync();

            var titleText = await page.GetByTestId("title_index").InnerTextAsync();
            titleText.ShouldNotBeEmpty();
        });
    }

    [Fact]
    public async Task LoginPage_BackToStart_Should_RedirectToStartPage()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();

        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToLoginPageAsync();
            await page.GetByTestId("link_user_back_to_index").ClickAsync();

            var titleText = await page.GetByTestId("title_index").InnerTextAsync();
            titleText.ShouldNotBeEmpty();
        });
    }

    [Fact]
    public async Task LoginPage_LoginButton_Should_RedirectToDashboard()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();

        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToRegisterPageAsync();
            await page.FillInRegisterFormAsync();

            // Simulate user confirmation
            var user = await OrganizationDataContext
                .ApplicationUsers
                .Where(x => x.EmailAddress == "test@test.com")
                .FirstOrDefaultAsync();
            user.ShouldNotBeNull();

            await page.Navigate_ToIndexPageAsync();
            await page.Navigate_ToConfirmAsync(user.EmailAddressConfirmationCode!);

            await page.Navigate_ToLoginPageAsync();
            await page.FillInLoginFormAsync();

            // Assert
            var titleText = await page.GetByTestId("title_index").InnerTextAsync();
            titleText.ShouldNotBeEmpty();
        });
    }
}