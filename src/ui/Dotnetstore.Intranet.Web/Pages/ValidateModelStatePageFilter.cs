using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotnetstore.Intranet.Web.Pages;

public class ValidateModelStatePageFilter : IPageFilter
{
    public void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
    // No action needed here
    }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        if (context.HandlerInstance is PageModel { ModelState.IsValid: false } page)
        {
            context.Result = page.Page();
        }
    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
    // No action needed here
    }
}