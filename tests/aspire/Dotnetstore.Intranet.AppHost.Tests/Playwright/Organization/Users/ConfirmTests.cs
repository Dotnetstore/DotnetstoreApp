using Dotnetstore.Intranet.AppHost.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Dotnetstore.Intranet.AppHost.Tests.Playwright.Organization.Users;

public class ConfirmTests(AspireManager aspireManager) : BasePlaywrightTests(aspireManager)
{
    [Fact]
    public async Task ConfirmPage_NonExistentCode_Should_ReturnError()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();

        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToConfirmAsync("NonExistentCode");

            // Assert
            var errorMessage = await page.GetByTestId("p_confirm_email_address").InnerTextAsync();
            errorMessage.ShouldContain("Något gick fel");
        });
    }

    [Fact]
    public async Task ConfirmPage_ValidCode_Should_ReturnSuccess()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();

        await InteractWithPageAsync("webui", async page =>
        {
            await page.Navigate_ToRegisterPageAsync();
            await page.FillInRegisterFormAsync();

            var user = await OrganizationDataContext
                .ApplicationUsers
                .Where(x => x.EmailAddress == "test@test.com")
                .FirstOrDefaultAsync();
            user.ShouldNotBeNull();

            await page.Navigate_ToIndexPageAsync();
            await page.Navigate_ToConfirmAsync(user.EmailAddressConfirmationCode!);

            // Assert
            var successMessage = await page.GetByTestId("p_confirm_email_address").InnerTextAsync();
            successMessage.ShouldContain("Du lyckades bekräfta din e-postadress. Du kan nu logga in.");
        });
    }
}