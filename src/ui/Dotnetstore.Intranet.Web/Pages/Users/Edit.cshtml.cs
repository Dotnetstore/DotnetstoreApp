// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using Dotnetstore.Intranet.Web.Data;
// using Dotnetstore.Intranet.Web.Data.Models;
//
// namespace Dotnetstore.Intranet.Web.Pages.Users
// {
//     public class EditModel : PageModel
//     {
//         private readonly Dotnetstore.Intranet.Web.Data.WebDataContext _context;
//
//         public EditModel(Dotnetstore.Intranet.Web.Data.WebDataContext context)
//         {
//             _context = context;
//         }
//
//         [BindProperty]
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
//             // var applicationuser =  await _context.ApplicationUsers.FirstOrDefaultAsync(m => m.Id == id);
//             // if (applicationuser == null)
//             // {
//             //     return NotFound();
//             // }
//             // ApplicationUser = applicationuser;
//             return Page();
//         }
//
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more information, see https://aka.ms/RazorPagesCRUD.
//         public async Task<IActionResult> OnPostAsync()
//         {
//             if (!ModelState.IsValid)
//             {
//                 return Page();
//             }
//
//             _context.Attach(ApplicationUser).State = EntityState.Modified;
//
//             try
//             {
//                 await _context.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!ApplicationUserExists(ApplicationUser.Id))
//                 {
//                     return NotFound();
//                 }
//                 else
//                 {
//                     throw;
//                 }
//             }
//
//             return RedirectToPage("./Index");
//         }
//
//         private bool ApplicationUserExists(ApplicationUserId id)
//         {
//             return _context.ApplicationUsers.Any(e => e.Id == id);
//         }
//     }
// }
