using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotnetstore.Intranet.Web.Pages;

public class Lang : PageModel
{
    public void OnGet()
    {
        var culture = Request.Query["culture"].ToString();
        if (!string.IsNullOrEmpty(culture))
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    IsEssential = true // Ensure the cookie is set as essential
                });
        }
        
        var returnUrl = Request.Headers.Referer.ToString() ?? "/";
        Response.Redirect(returnUrl);
    }
}