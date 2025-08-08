using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
using Dotnetstore.Intranet.SDK.Services;
using Dotnetstore.Intranet.Web.Pages.Users.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotnetstore.Intranet.Web.Pages.Users;

internal sealed class Login(
    IValidator<LoginModel> validator,
    IApplicationUserClientService applicationUserClientService) : PageModel
{
    [BindProperty]
    public LoginModel LoginModel { get; set; } = new();
    
    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await validator.ValidateAsync(LoginModel);
        
        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return Page();
        }
        
        var request = new ApplicationUserLoginRequest(LoginModel.Username, LoginModel.Password);
        
        var response = await applicationUserClientService.LoginAsync(request).ConfigureAwait(false);
        
        if (!response.IsSuccessStatusCode)
        {
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>().ConfigureAwait(false);
            ModelState.AddModelError(string.Empty, problemDetails?.Detail ?? "An error occurred while processing your request.");
            return Page();
        }
        
        return RedirectToPage("/Index");
    }
}