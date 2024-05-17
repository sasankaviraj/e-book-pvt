using e_book_pvt.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace e_book_pvt.Pages.Reports
{
    public class LowRatedBooksModel : PageModel
    {
        public List<Book> LowRatedBooks { get; set; }

        private readonly ReportService _reportService;

        public LowRatedBooksModel(ReportService reportService)
        {
            _reportService = reportService;
        }

        public void OnGet()
        {
            // Fetch the list of low rated books
            LowRatedBooks = _reportService.GetLowRatedBooks();
        }
    }
}
