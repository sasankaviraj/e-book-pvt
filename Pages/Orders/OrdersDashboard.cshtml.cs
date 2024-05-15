using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using e_book_pvt.Models;
using Newtonsoft.Json;

namespace e_book_pvt.Pages.Orders
{
    public class OrdersDashboardModel : PageModel
    {
        private readonly e_book_pvt.Data.ApplicationDbContext _context;

        public OrdersDashboardModel(e_book_pvt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OrderDetailModel> OrderDetail { get;set; } = default!;
        public IList<OrderDetail> OrderDetails { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_context.Order != null)
            {
                OrderDetails = await _context.Order.ToListAsync();
                IList<OrderDetailModel> OrderDetailModels = new List<OrderDetailModel>();
                foreach (var order in OrderDetails)
                {
                    List<OrderItem> items = JsonConvert.DeserializeObject<List<OrderItem>>(order.Items);
                    OrderDetailModel orderDetailModel = new OrderDetailModel
                    {
                        ID = order.ID,
                        FirstName = order.FirstName,
                        LastName = order.LastName,
                        Address = order.Address,
                        ContactNo = order.ContactNo,
                        TotalPrice = order.TotalPrice,
                        OrderDate = order.OrderDate,
                        Items = items
                    };
                    OrderDetailModels.Add(orderDetailModel);
                }
                OrderDetail = OrderDetailModels;
            }
        }
    }
}
