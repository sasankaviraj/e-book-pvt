using e_book_pvt.Data;
using e_book_pvt.Models;

namespace e_book_pvt.Pages.Reports
{
    public class ReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<OrderDetailModel> GetDeliveredOrders()
        {
            var orders = _context.Order
                .Where(o => o.IsDelivered == true)
                .Select(o => new OrderDetailModel
                {
                    ID = o.ID,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    Address = o.Address,
                    ContactNo = o.ContactNo,
                    ItemsJson = o.Items, // Assuming Items is stored as JSON string in the database
                    TotalPrice = o.TotalPrice,
                    OrderDate = o.OrderDate,
                    UserID = o.UserID,
                    IsDelivered = o.IsDelivered
                })
                .ToList();

            foreach (var order in orders)
            {
                order.DeserializeItems();
            }

            return orders;
        }

        public List<OrderDetailModel> GetUndeliveredOrders()
        {
            var orders = _context.Order.Where(o => o.IsDelivered == false || o.IsDelivered == null).Select(o => new OrderDetailModel
            {
                ID = o.ID,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Address = o.Address,
                ContactNo = o.ContactNo,
                ItemsJson = o.Items, // Assuming Items is stored as JSON string in the database
                TotalPrice = o.TotalPrice,
                OrderDate = o.OrderDate,
                UserID = o.UserID,
                IsDelivered = o.IsDelivered
            }).ToList();

            foreach (var order in orders)
            {
                order.DeserializeItems();
            }

            return orders;
        }

        public List<Book> GetLowRatedBooks()
        {
            return _context.Book
                .Where(b => b.Rating.HasValue && b.Rating < 3)
                .ToList();
        }

        public List<Book> GetHighRatedBooks()
        {
            return _context.Book
                .Where(b => _context.Review.Any(r => r.BookID == b.ID && (r.Rating == 4 || r.Rating == 5)))
                .ToList();
        }

    }
}
