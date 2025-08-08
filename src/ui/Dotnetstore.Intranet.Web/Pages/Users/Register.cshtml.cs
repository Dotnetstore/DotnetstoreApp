using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
using Dotnetstore.Intranet.SDK.Services;
using Dotnetstore.Intranet.Web.Pages.Users.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotnetstore.Intranet.Web.Pages.Users;

internal sealed class Register(
    IValidator<RegisterModel> validator,
    IApplicationUserClientService applicationUserClientService) : PageModel
{
    [BindProperty] 
    public RegisterModel RegisterModel { get; set; } = new();
    
    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await validator.ValidateAsync(RegisterModel);
        
        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return Page();
        }
        
        var request = CreateRequest();
        var response = await applicationUserClientService.RegisterAsync(request).ConfigureAwait(false);
        
        if (!response.IsSuccessStatusCode)
        {
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>().ConfigureAwait(false);
            ModelState.AddModelError(string.Empty, problemDetails?.Detail ?? "An error occurred while processing your request.");
            return Page();
        }
        
        return RedirectToPage("/users/login");
    }

    private ApplicationUserRegisterRequest CreateRequest()
    {
        return new ApplicationUserRegisterRequest(
            RegisterModel.LastName,
            RegisterModel.FirstName,
            RegisterModel.MiddleName,
            RegisterModel.DateOfBirth!.Value,
            RegisterModel.IsMale,
            RegisterModel.SocialSecurityNumber,
            RegisterModel.EmailAddress,
            RegisterModel.Password,
            RegisterModel.ConfirmPassword);
    }
}