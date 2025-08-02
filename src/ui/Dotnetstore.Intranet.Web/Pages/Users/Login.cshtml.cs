using Dotnetstore.Intranet.Web.Pages.Organization.ApplicationUsers.Models;
using Dotnetstore.Intranet.Web.Pages.Organization.ApplicationUsers.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotnetstore.Intranet.Web.Pages.Users;

internal sealed class Login(
    IValidator<LoginModel> validator,
    IApplicationUserClientService applicationUserClientService,
    ITokenProvider tokenProvider) : PageModel
{
    [BindProperty]
    public LoginModel LoginModel { get; set; } = new();
    
    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var test1 = validator.ToString();
        test1 = applicationUserClientService.ToString();
        test1 = tokenProvider.ToString();
        await Task.CompletedTask;
        // var result = await validator.ValidateAsync(LoginModel);
        //
        // if (!result.IsValid)
        // {
        //     foreach (var error in result.Errors)
        //     {
        //         ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        //     }
        //     return Page();
        // }
        //
        // var request = new ApplicationUserLoginRequest(LoginModel.Username, LoginModel.Password);
        //
        // var response = await applicationUserClientService.LoginAsync(request).ConfigureAwait(false);
        //
        // if (!response.IsSuccessStatusCode)
        // {
        //     var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>().ConfigureAwait(false);
        //     ModelState.AddModelError(string.Empty, problemDetails?.Detail ?? "An error occurred while processing your request.");
        //     return Page();
        // }
        //
        // var content = await response.Content.ReadFromJsonAsync<ApplicationUserTokenResponse>().ConfigureAwait(false);
        // tokenProvider.SetTokens(content.Token, content.RefreshToken);
        
        return RedirectToPage("/Index");
    }
}