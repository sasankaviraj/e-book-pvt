using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using e_book_pvt.Models;
using Microsoft.AspNetCore.Identity;

namespace e_book_pvt.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly e_book_pvt.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public IndexModel(e_book_pvt.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager; // Inject UserManager
        }

        public IList<OrderDetail> OrderDetail { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Order != null)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                OrderDetail = await _context.Order.Where(c => c.UserID == currentUser.Id).ToListAsync();
            }
        }
    }
}
