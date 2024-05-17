using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using e_book_pvt.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

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

        public IList<OrderDetailModel> OrderDetail { get; set; } = default!;
        public IList<OrderDetail> OrderDetails { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Order != null)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null)
                {
                    OrderDetails = await _context.Order.Where(c => c.UserID == currentUser.Id).ToListAsync();
                    IList<OrderDetailModel> OrderDetailModels = new List<OrderDetailModel>();
                    foreach (var order in OrderDetails)
                    {
                        List<Item> items = JsonConvert.DeserializeObject<List<Item>>(order.Items);
                        OrderDetailModel orderDetailModel = new OrderDetailModel
                        {
                            ID = order.ID,
                            FirstName = order.FirstName,
                            LastName = order.LastName,
                            Address = order.Address,
                            ContactNo = order.ContactNo,
                            TotalPrice = order.TotalPrice,
                            OrderDate = order.OrderDate,
                            IsDelivered = order.IsDelivered,
                            Items = items
                        };
                        OrderDetailModels.Add(orderDetailModel);
                    }
                    OrderDetail = OrderDetailModels;
                }
                else 
                {
                    OrderDetail = new List<OrderDetailModel>();
                }
            }
        }
    }
}
