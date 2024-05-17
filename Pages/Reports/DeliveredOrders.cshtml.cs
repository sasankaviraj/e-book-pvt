using e_book_pvt.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace e_book_pvt.Pages.Reports
{
    public class DeliveredOrdersModel : PageModel
    {
        public List<OrderDetailModel> DeliveredOrders { get; set; }

        private readonly ReportService _reportService;

        public DeliveredOrdersModel(ReportService reportService)
        {
            _reportService = reportService;
        }
        public void OnGet()
        {
            // Fetch the list of delivered orders
            DeliveredOrders = DeliveredOrders = _reportService.GetDeliveredOrders();
        }
    }
}
