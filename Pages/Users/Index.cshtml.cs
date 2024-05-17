using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace e_book_pvt.Pages.Users
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IList<IdentityUser> UserDetail { get; set; } = default!;
        // Inject UserManager into your controller or service
        private readonly UserManager<IdentityUser> _userManager;
        private readonly e_book_pvt.Data.ApplicationDbContext _context;

        public IndexModel(e_book_pvt.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager; // Inject UserManager
        }
        public IdentityUser lUser { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.Email != "admin@admin.com")
            {
                return RedirectToPage("/Index");
            }
            // Get all users
            UserDetail = await _userManager.Users.ToListAsync();
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
                lUser = user;
                await _userManager.DeleteAsync(user);
            }

            return RedirectToPage("./Index");
        }
    }
}
