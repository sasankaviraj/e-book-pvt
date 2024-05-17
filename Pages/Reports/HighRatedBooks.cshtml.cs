using e_book_pvt.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace e_book_pvt.Pages.Reports
{
    public class HighRatedBooksModel : PageModel
    {
        public List<Book> HighRatedBooks { get; set; }

        private readonly ReportService _reportService;

        public HighRatedBooksModel(ReportService reportService)
        {
            _reportService = reportService;
        }

        public void OnGet()
        {
            // Fetch the list of high rated books
            HighRatedBooks = _reportService.GetHighRatedBooks();
        }
    }
}
