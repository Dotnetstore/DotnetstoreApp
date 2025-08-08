using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
using Dotnetstore.Intranet.SDK.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace Dotnetstore.Intranet.Web.Pages.Users;

public class Confirm(
    IApplicationUserClientService applicationUserClientService,
    IStringLocalizer<Confirm> localizer) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? Code { get; set; }
    
    public string? Message { get; set; }
    
    public async Task OnGetAsync()
    {
        if (string.IsNullOrEmpty(Code))
        {
            Message = localizer["NoCodeProvided"];
            return;
        }

        var request = new ApplicationUserConfirmEmailRequest(Code);
        var result = await applicationUserClientService.ConfirmEmailAddressAsync(request);
        Message = result.IsSuccessStatusCode ? localizer["ConfirmSuccess"] : localizer["ConfirmFail"];
    }
}