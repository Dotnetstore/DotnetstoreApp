using Microsoft.Playwright;

namespace Dotnetstore.Intranet.AppHost.Tests.Helpers;

internal static class TestObjectHelper
{
    internal static async Task FillInRegisterFormAsync(
        this IPage page,
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
    

    internal static async Task Navigate_ToRegisterPageAsync(this IPage page)
    {
        await page.GotoAsync("/");
        await page.GetByTestId("link_layout_register_account").ClickAsync();
    }

    internal static async Task Navigate_ToConfirmAsync(this IPage page, string code)
    {
        await page.GotoAsync($"/users/confirm?Code={code}");
    }

    internal static async Task Navigate_ToLoginPageAsync(this IPage page)
    {
        await page.GotoAsync("/");
        await page.GetByTestId("link_layout_login").ClickAsync();
    }

    internal static async Task Navigate_ToIndexPageAsync(this IPage page)
    {
        await page.GotoAsync("/");
    }
     
    internal static async Task FillInLoginFormAsync(
        this IPage page, 
        string email = "test@test.com", 
        string password = "Password123!")
    {
        await page.GetByTestId("input_user_login_username").FillAsync(email);
        await page.GetByTestId("input_user_login_password").FillAsync(password);
        await page.GetByTestId("submit_user_login").ClickAsync();
    }
}