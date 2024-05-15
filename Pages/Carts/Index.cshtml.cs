using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using e_book_pvt.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace e_book_pvt.Pages.Carts
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly e_book_pvt.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        [BindProperty]
        public OrderDetail Order { get; set; }
        public IndexModel(e_book_pvt.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager; // Inject UserManager
        }

        public List<Cart> CartItems { get; set; } = new List<Cart>();
        public double TotalPrice { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Fetch cart items based on the current user
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                CartItems = await _context.Cart.Where(c => c.UserID == currentUser.Id).ToListAsync();
                TotalPrice = CartItems.Sum(c => c.Price);
                return Page();
            }
            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var currentUser = await _userManager.GetUserAsync(User);
            CartItems = await _context.Cart.Where(c => c.UserID == currentUser.Id).ToListAsync();
            TotalPrice = CartItems.Sum(c => c.Price);
            Order.OrderDate = DateTime.Now;
            Order.TotalPrice = TotalPrice;
            Order.Items = SerializeCartItems(CartItems);
            Order.UserID = currentUser.Id;
            _context.Order.Add(Order);
            await _context.SaveChangesAsync();

            // Clear the cart after placing the order
            _context.Cart.RemoveRange(CartItems);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

        private string SerializeCartItems(List<Cart> cartItems)
        {
            // Implement serialization logic to convert cart items to JSON
            return JsonSerializer.Serialize(cartItems);
        }
    }
}
