using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using e_book_pvt.Models;
using Microsoft.AspNetCore.Identity;

namespace e_book_pvt.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly e_book_pvt.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DeleteModel(e_book_pvt.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager; // Inject UserManager
        }

        [BindProperty]
      public new IdentityUser User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            else 
            {
                User = user;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                User = user;
                var result = await _userManager.DeleteAsync(user);
            }

            return RedirectToPage("./Index");
        }
    }
}
