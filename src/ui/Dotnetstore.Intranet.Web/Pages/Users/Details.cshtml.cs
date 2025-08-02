// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.EntityFrameworkCore;
// using Dotnetstore.Intranet.Web.Data;
// using Dotnetstore.Intranet.Web.Data.Models;
//
// namespace Dotnetstore.Intranet.Web.Pages.Users
// {
//     public class DetailsModel : PageModel
//     {
//         private readonly Dotnetstore.Intranet.Web.Data.WebDataContext _context;
//
//         public DetailsModel(Dotnetstore.Intranet.Web.Data.WebDataContext context)
//         {
//             _context = context;
//         }
//
//         public ApplicationUser ApplicationUser { get; set; } = default!;
//
//         public async Task<IActionResult> OnGetAsync(ApplicationUserId id)
//         {
//             await Task.CompletedTask;
//             // if (id == null)
//             // {
//             //     return NotFound();
//             // }
//             //
//             // var applicationuser = await _context.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == id);
//             //
//             // if (applicationuser is not null)
//             // {
//             //     ApplicationUser = applicationuser;
//             //
//             //     return Page();
//             // }
//
//             return NotFound();
//         }
//     }
// }
