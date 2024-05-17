using e_book_pvt.Data;
using e_book_pvt.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace e_book_pvt.Pages.Reports
{
    public class IndexModel : PageModel
    {
        private readonly ReportService _reportService;

        public IndexModel(ReportService reportService)
        {
            _reportService = reportService;
        }

        public List<OrderDetailModel> DeliveredOrders { get; set; }
        public List<OrderDetailModel> UndeliveredOrders { get; set; }
        public List<Book> LowRatedBooks { get; set; }
        public List<Book> HighRatedBooks { get; set; }

        public void OnGet()
        {
            // Fetch data for reports
            DeliveredOrders = _reportService.GetDeliveredOrders();
            UndeliveredOrders = _reportService.GetUndeliveredOrders();
            LowRatedBooks = _reportService.GetLowRatedBooks();
            HighRatedBooks = _reportService.GetHighRatedBooks();
        }

    }
}
