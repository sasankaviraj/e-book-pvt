using e_book_pvt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace e_book_pvt.Pages
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        private readonly ILogger<DashboardModel> _logger;
        private readonly e_book_pvt.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public IList<Book> Book { get; set; } = default!;
        public DashboardModel(ILogger<DashboardModel> logger, e_book_pvt.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager; // Inject UserManager
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.Email != "admin@admin.com")
            {
                return RedirectToPage("/Index");
            }
            if (_context.Book != null)
            {
                Book = await _context.Book.ToListAsync();
            }
            return Page();
        }
    }
}