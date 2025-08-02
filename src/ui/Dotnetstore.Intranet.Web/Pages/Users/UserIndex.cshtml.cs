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
//     public class IndexModel : PageModel
//     {
//         private readonly Dotnetstore.Intranet.Web.Data.WebDataContext _context;
//
//         public IndexModel(Dotnetstore.Intranet.Web.Data.WebDataContext context)
//         {
//             _context = context;
//         }
//
//         public IList<ApplicationUser> ApplicationUser { get;set; } = default!;
//
//         public async Task OnGetAsync()
//         {
//             ApplicationUser = await _context.ApplicationUsers.ToListAsync();
//         }
//     }
// }
